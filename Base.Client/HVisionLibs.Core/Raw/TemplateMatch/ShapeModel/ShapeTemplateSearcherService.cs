using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HVisionLibs.Core.TemplateMatch
{
    public class ShapeTemplateSearcherService : BindableBase
    {
        public ShapeTemplateSearcherService()
        {
            info = new MethodInfo()
            {
                Name = "find_shape_model",
                Description = "find_shape_model — Find the best matches of a shape model in an image.",
                Parameters = new List<MethodParmeter>()        
                {
                new MethodParmeter(){ Name = "AngleStart :", Description = "Smallest rotation of the model." },
                new MethodParmeter(){ Name = "AngleExtent", Description = "Extent of the rotation angles." },
                new MethodParmeter(){ Name = "MinScore", Description = "Minimum score of the instances of the model to be found." },
                new MethodParmeter(){ Name = "NumMatches", Description = "Number of instances of the model to be found (or 0 for all matches)." },
                new MethodParmeter(){ Name = "MaxOverlap", Description = "Maximum overlap of the instances of the model to be found." },
                new MethodParmeter(){ Name = "SubPixel", Description = "Subpixel accuracy." },
                new MethodParmeter(){ Name = "NumLevels", Description = "Number of pyramid levels used in the matching." },
            },
                Predecessors = new List<string>()
                {
                    // "set_shape_model"
                }
            };
            Setting = new MatchResultSetting();
            RunParameter = new ShapeModeSearcherRunParameter();
            RunParameter.ApplyDefaultParameter();
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value; RaisePropertyChanged(); }
        }

        public MatchResultSetting Setting { get; set; }
        public MethodInfo info { get; set; }

        private HTuple _shapeModel;
        public HTuple ModelId
        {
            get { return _shapeModel; }
            set { _shapeModel = value; }
        }

        private string modeName;
        public string ModeName
        {
            get { return modeName; }
            set { modeName = value; RaisePropertyChanged(); }
        }

        private ShapeModeSearcherRunParameter runParameter;
        public ShapeModeSearcherRunParameter RunParameter
        {
            get { return runParameter; }
            set { runParameter = value; RaisePropertyChanged(); }
        }

        // 创建 Shape 模型
        public HOperateResult LoadShapeModelFromPath(string templatePath)
        {
            if (string.IsNullOrEmpty(templatePath))
            {
                return HOperateResult.CreateFailResult("Template path cannot be null or empty.");
            }

            if (ModelId != null)
            {
                ModelId.ClearHandle();
                ModelId = null;
                ModeName = "NULL";
            }

            try
            {
                HOperatorSet.ReadShapeModel(templatePath, out _shapeModel);
                ModeName = System.IO.Path.GetFileName(templatePath);
                return new HOperateResult() { IsSuccess = true, Message = $"Shape model 【{ModeName}】 loaded successfully." };
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult("Error loading shape model: " + ex.ToString());
            }
        }

        // 执行 Shape 模型搜索
        public MatchResult Run(HObject image, HObject ROIObj = null)
        {
            MatchResult matchResult = new MatchResult();
            matchResult.Results = new ObservableCollection<TemplateMatchResult>();

            // 校验运行参数
            if (RunParameter.NumLevels == null || RunParameter.AngleStart == 0 || RunParameter.AngleExtent == 0)
            {
                matchResult.Message = "参数异常!";
                return matchResult;
            }

            if (image == null)
            {
                matchResult.Message = "输入图像无效!";
                return matchResult;
            }

            if (ModelId == null)
            {
                matchResult.Message = "输入模板无效!";
                return matchResult;
            }

            try
            {
                if (ROIObj != null)
                {
                    // 如果有提供 ROIObj，缩小图像区域
                    HOperatorSet.ReduceDomain(image, ROIObj, out HObject imageReduced);
                    image = imageReduced;
                }

                // 启动计时器
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // 使用 FindShapeModel 查找模板
                HOperatorSet.FindShapeModel(
                    image, 
                    ModelId, 
                    RunParameter.AngleStart,
                    RunParameter.AngleExtent,               
                    RunParameter.MinScore, 
                    RunParameter.NumMatches,
                    RunParameter.MaxOverlap,        
                    RunParameter.SubPixel, 
                    RunParameter.NumLevels,
                    RunParameter.Greediness,
                    out HTuple resultRow,
                    out HTuple resultColumn,                     
                    out HTuple resultAngle,                  
                    out HTuple resultScore);

                //获取形状模板轮廓
                HOperatorSet.GetShapeModelContours(out HObject modelContours, ModelId, 1);

                // 遍历所有的匹配结果
                for (int i = 0; i < resultScore.Length; i++)
                {
                    // 计算仿射变换矩阵：匹配的位姿
                    HTuple hv_AffineTransMatrix;
                    HOperatorSet.HomMat2dIdentity(out hv_AffineTransMatrix);  // 创建一个单位仿射矩阵
                    HOperatorSet.HomMat2dRotate(hv_AffineTransMatrix, resultAngle.DArr[i], 0, 0, out hv_AffineTransMatrix);  // 旋转
                    HOperatorSet.HomMat2dTranslate(hv_AffineTransMatrix, resultRow.DArr[i], resultColumn.DArr[i], out hv_AffineTransMatrix);  // 平移

                    // 仿射变换模板轮廓
                    HObject transformedContours;
                    HOperatorSet.AffineTransContourXld(modelContours, out transformedContours, hv_AffineTransMatrix);

                    // 添加到结果集
                    matchResult.Results.Add(new TemplateMatchResult()
                    {
                        Index = i + 1,
                        Row = Math.Round(resultRow.DArr[i], 4),      // 保留四位小数
                        Column = Math.Round(resultColumn.DArr[i], 4), // 保留四位小数
                        Angle = Math.Round(resultAngle.DArr[i], 4),  // 保留四位小数
                        Score = Math.Round(resultScore.DArr[i], 4),  // 保留四位小数
                        Contours = transformedContours,  // 仿射变换后的轮廓
                    });
                }


                matchResult.TimeSpan = stopwatch.ElapsedMilliseconds;
                matchResult.Setting = Setting;

                // 停止计时器
                stopwatch.Stop();
                matchResult.Message = $"匹配耗时:{matchResult.TimeSpan} ms , 匹配个数:{matchResult.Results.Count}";
                return matchResult;
            }
            catch (Exception ex)
            {
                matchResult.Message = $"执行失败: {ex.ToString()}";
                return matchResult;
            }
        }

    }



    public class ShapeModeSearcherRunParameter : BaseParameter
    {
        private double angleStart;
        public double AngleStart
        {
            get { return angleStart; }
            set { angleStart = value; RaisePropertyChanged(); }
        }

        private double angleExtent;
        public double AngleExtent
        {
            get { return angleExtent; }
            set { angleExtent = value; RaisePropertyChanged(); }
        }

        private double minScore;
        public double MinScore
        {
            get { return minScore; }
            set { minScore = value; RaisePropertyChanged(); }
        }

        private int numMatches;
        public int NumMatches
        {
            get { return numMatches; }
            set { numMatches = value; RaisePropertyChanged(); }
        }

        private double maxOverlap;
        public double MaxOverlap
        {
            get { return maxOverlap; }
            set { maxOverlap = value; RaisePropertyChanged(); }
        }

        private string subPixel;
        public string SubPixel
        {
            get { return subPixel; }
            set { subPixel = value; RaisePropertyChanged(); }
        }

        private int numLevels;
        public int NumLevels
        {
            get { return numLevels; }
            set { numLevels = value; RaisePropertyChanged(); }
        }

        private double greediness;
        public double Greediness
        {
            get { return greediness; }
            set { greediness = value; }
        }

        public override void ApplyDefaultParameter()
        {
            AngleStart = -0.39;
            AngleExtent = 0.79;
            MinScore = 0.8;
            NumMatches = 1;
            MaxOverlap = 0.5;
            SubPixel = "true";
            NumLevels = 0; // default pyramid level
            Greediness = 0.9;
        }
    }

}

