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

namespace HVisionLibs.Core.TemplateMatch.NccModel
{
    public class NCCTemplateSearcherService : BindableBase
    {
        public NCCTemplateSearcherService()
        {
            info = new MethodInfo()
            {
                Name = "find_ncc_model",
                Description = "find_ncc_model — Find the best matches of an NCC model in an image.",
  
          
                Parameters = new List<MethodParmeter>()
                {
                    new MethodParmeter(){Name="AngleStart :",Description="Smallest rotation of the model."},
                    new MethodParmeter(){Name="AngleExtent ",Description="Extent of the rotation angles."},
                    new MethodParmeter(){Name="MinScore ",Description="Minimum score of the instances of the model to be found."},
                    new MethodParmeter(){Name="NumMatches ",Description="Number of instances of the model to be found (or 0 for all matches)."},
                    new MethodParmeter(){Name="MaxOverlap ",Description="Maximum overlap of the instances of the model to be found. "},
                    new MethodParmeter(){Name="SubPixel  ",Description="Subpixel accuracy."},
                    new MethodParmeter(){Name="NumLevels  ",Description="Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2)."},
                },
                Predecessors = new List<string>()
                {
                   // "set_metrology_model_image_size"
                }
            };
            Setting = new MatchResultSetting();
            RunParameter = new NCCTemplateSearcherRunParameter();
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

        // 创建 NCC 模型
        private HTuple _nccModel;
        public HTuple nccModel
        {
            get { return _nccModel; }
            set { _nccModel = value; }
        }

        private string modeName;
        public string ModeName
        {
            get { return modeName; }
            set { modeName = value;RaisePropertyChanged(); }
        }


        private NCCTemplateSearcherRunParameter runParameter;
        public NCCTemplateSearcherRunParameter RunParameter { get { return runParameter; } set { runParameter = value; RaisePropertyChanged(); } }



        public MatchResult Run(HObject image, HObject ROIObj = null)
        {
            MatchResult matchResult = new MatchResult();
            matchResult.Results = new ObservableCollection<TemplateMatchResult>();


            // 检查运行参数的有效性
            if (RunParameter.NumLevels == null || RunParameter.AngleStart == 0 || RunParameter.AngleExtent == 0) 
            {
                matchResult.Message = "参数异常!";
                return matchResult;
            }
            // 检查图像是否为 null
            if (image == null)
            {
                matchResult.Message = "输入图像无效!";
                return matchResult;
            }
            // 校验 NCC 模型是否已经创建
            if (nccModel == null)
            {
                matchResult.Message = "输入模板无效!";
                return matchResult;
            }
            try
            {
                // 如果没有提供 ROIObj，直接使用整张图像进行 NCC 模型查找
                if (ROIObj != null)
                {
                    // 如果有提供 ROIObj，则根据 ROI 限制图像区域
                    HOperatorSet.ReduceDomain(image, ROIObj, out HObject imageReduced);
                    image = imageReduced;
                }

                // 创建一个 Stopwatch 实例
                Stopwatch stopwatch = new Stopwatch();

                // 启动计时器
                stopwatch.Start();

                // 使用 FindNccModel 算子查找模型
                HOperatorSet.FindNccModel(image, nccModel, RunParameter.AngleStart, RunParameter.AngleExtent,
                                          RunParameter.MinScore, RunParameter.NumMatches, RunParameter.MaxOverlap,
                                          RunParameter.SubPixel, RunParameter.NumLevels
                                          , out HTuple reasultRow, out HTuple reasultColumn
                                          ,out HTuple resaultAngle,out HTuple resaultScore);

                
                for (int i = 0; i < resaultScore.Length; i++)
                {
                    matchResult.Results.Add(new TemplateMatchResult()
                    {
                        Index = i + 1,
                        Row = reasultRow[i],
                        Column = reasultColumn[i],
                        Angle = resaultAngle.DArr[i],
                        Score = resaultScore.DArr[i],
                        Contours = GetNccModelContours(nccModel, reasultRow[i], reasultColumn[i], resaultAngle.DArr[i], 0)
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
        /// <summary>
        /// 获取相关性匹配的结果轮廓
        /// </summary>
        /// <param name="hv_ModelID"></param>
        /// <param name="hv_Row"></param>
        /// <param name="hv_Column"></param>
        /// <param name="hv_Angle"></param>
        /// <param name="hv_Model"></param>
        /// <returns></returns>
        public HObject GetNccModelContours(HTuple hv_ModelID, HTuple hv_Row, HTuple hv_Column, HTuple hv_Angle, HTuple hv_Model)
        {
            HObject ho_ModelRegion = null, ho_ModelContours = null;
            HObject ho_ContoursAffinTrans = null, ho_Cross = null;

            // Local control variables 

            HTuple hv_NumMatches = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Match = new HTuple(), hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DRotate = new HTuple(), hv_HomMat2DTranslate = new HTuple();
            HTuple hv_RowTrans = new HTuple(), hv_ColTrans = new HTuple();
            HTuple hv_Model_COPY_INP_TMP = new HTuple(hv_Model);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ModelRegion);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffinTrans);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            //This procedure displays the results of Correlation-Based Matching.
            //
            hv_NumMatches.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_NumMatches = new HTuple(hv_Row.TupleLength()
                    );
            }
            if ((int)(new HTuple(hv_NumMatches.TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength())).TupleEqual(
                    0))) != 0)
                {
                    hv_Model_COPY_INP_TMP.Dispose();
                    HOperatorSet.TupleGenConst(hv_NumMatches, 0, out hv_Model_COPY_INP_TMP);
                }
                else if ((int)(new HTuple((new HTuple(hv_Model_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(1))) != 0)
                {
                    {
                        HTuple ExpTmpOutVar_0;
                        HOperatorSet.TupleGenConst(hv_NumMatches, hv_Model_COPY_INP_TMP, out ExpTmpOutVar_0);
                        hv_Model_COPY_INP_TMP.Dispose();
                        hv_Model_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ModelID.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_ModelRegion.Dispose();
                        HOperatorSet.GetNccModelRegion(out ho_ModelRegion, hv_ModelID.TupleSelect(
                            hv_Index));
                    }
                    ho_ModelContours.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_ModelRegion, out ho_ModelContours, "border_holes");
                    HTuple end_val13 = hv_NumMatches - 1;
                    HTuple step_val13 = 1;
                    for (hv_Match = 0; hv_Match.Continue(end_val13, step_val13); hv_Match = hv_Match.TupleAdd(step_val13))
                    {
                        if ((int)(new HTuple(hv_Index.TupleEqual(hv_Model_COPY_INP_TMP.TupleSelect(
                            hv_Match)))) != 0)
                        {
                            hv_HomMat2DIdentity.Dispose();
                            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_HomMat2DRotate.Dispose();
                                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, hv_Angle.TupleSelect(
                                    hv_Match), 0, 0, out hv_HomMat2DRotate);
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_HomMat2DTranslate.Dispose();
                                HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row.TupleSelect(
                                    hv_Match), hv_Column.TupleSelect(hv_Match), out hv_HomMat2DTranslate);
                            }
                            ho_ContoursAffinTrans.Dispose();
                            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffinTrans, hv_HomMat2DTranslate);
                        }
                    }
                }
            }
            ho_ModelRegion.Dispose();
            ho_ModelContours.Dispose();

            hv_Model_COPY_INP_TMP.Dispose();
            hv_NumMatches.Dispose();
            hv_Index.Dispose();
            hv_Match.Dispose();
            hv_HomMat2DIdentity.Dispose();
            hv_HomMat2DRotate.Dispose();
            hv_HomMat2DTranslate.Dispose();
            hv_RowTrans.Dispose();
            hv_ColTrans.Dispose();

            return ho_ContoursAffinTrans;
        }

        // 根据路径加载 NCC 模板
        public  HOperateResult LoadNCCModelFromPath(string templatePath)
        {
            if (string.IsNullOrEmpty(templatePath))
            {
                return HOperateResult.CreateFailResult("Template path cannot be null or empty.");
            }

            // 假设有一个方法可以加载 NCC 模板
            if(nccModel != null)
            {
                nccModel.ClearHandle();
                nccModel = null;
                ModeName = "NULL";
            }

            try
            {

                HOperatorSet.ReadNccModel(templatePath, out _nccModel);
                if (templatePath == null)
                {
                    return HOperateResult.CreateFailResult("Failed to load NCC model from the specified path.");

                }

                // 你可以在这里添加进一步的处理， 如验证模板是否合法等
            }
            catch (Exception ex)
            {
                // 处理加载模板过程中的错误
                return HOperateResult.CreateFailResult("Error loading NCC model: " + ex.ToString());

            }
            // 获取文件名和扩展名
            ModeName = System.IO.Path.GetFileName(templatePath);
            return new HOperateResult() { IsSuccess = true, Message = $"NCC模板【{ModeName}】 加载成功" };
        }
    }

    
    public class NCCTemplateSearcherRunParameter : BaseParameter
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

        public override void ApplyDefaultParameter()
        {
            AngleStart = -0.39;
            AngleExtent = 0.79;
            MinScore = 0.8;
            NumMatches = 1;
            MaxOverlap = 0.5;
            SubPixel = "true";
            NumLevels = 0; // default pyramid level
        }
    }
}

