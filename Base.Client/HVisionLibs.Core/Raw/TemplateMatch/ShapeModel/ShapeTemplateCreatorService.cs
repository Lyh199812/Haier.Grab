using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace HVisionLibs.Core.TemplateMatch
{
    public class ShapeTemplateCreatorService : BindableBase
    {
        public HTuple ModelID;
        public ShapeTemplateCreatorService()
        {
            info = new MethodInfo()
            {
                Name = "create_shape_model ",
                Description = "create_shape_model — Prepare a shape model for matching..\r\n",
  
            Parameters = new List<MethodParmeter>()
                {
                    new MethodParmeter(){Name="NumLevels",Description="Maximum number of pyramid levels."},
                    new MethodParmeter(){Name="AngleStart",Description="Smallest rotation of the pattern."},
                    new MethodParmeter(){Name="AngleExtent",Description="Extent of the rotation angles."},
                    new MethodParmeter(){Name="AngleStep",Description="Step length of the angles (resolution)."},
                    new MethodParmeter(){Name="Optimization ",Description="Kind of optimization and optionally method used for generating the model."},
                    new MethodParmeter(){Name="Metric",Description="Match metric."},
                    new MethodParmeter(){Name="Contrast",Description="Threshold or hysteresis thresholds for the contrast of the object in the template image and optionally minimum size of the object parts."},
                    new MethodParmeter(){Name="MinContrast ",Description="Minimum contrast of the objects in the search images."},

                },
                Predecessors = new List<string>()
                {
                   // "set_metrology_model_image_size"
                }
            };
            RunParameter = new ShapeTemplateCreatorRunParameter();
            RunParameter.ApplyDefaultParameter();
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value; RaisePropertyChanged(); }
        }


        public MethodInfo info { get; set; }

        private ShapeTemplateCreatorRunParameter runParameter;
        public ShapeTemplateCreatorRunParameter RunParameter { get { return runParameter; } set { runParameter = value; RaisePropertyChanged(); } }



       


        public HOperateResult Run(HObject image,HObject ROIObj)
        {
            const string ImageError = "图片异常!";
            const string ROIError = "ROI未设置!";
            const string ModeError = "参数异常!";
            if(ModelID!=null)
            {
                ModelID.ClearHandle();
                ModelID = null;
            }


            if (image == null)
                return HOperateResult.CreateFailResult(ImageError);
            if(ROIObj == null)
                return HOperateResult.CreateFailResult(ROIError);
            // 检查运行参数的有效性
            if (RunParameter.NumLevels == null || RunParameter.AngleStart == 0 || RunParameter.AngleExtent == 0)
                return HOperateResult.CreateFailResult(ModeError);

            // 使用 using 语句以确保资源正确释放
            try
            {
                HOperatorSet.ReduceDomain(image, ROIObj,out HObject imageReduced);

                // 处理 NCC 模型的结果（如果需要）
                // 例如，可以保存模型或将其返回
                // 假设 imageReduced 是经过处理的输入图像，RunParameter 是包含参数的对象
                HOperatorSet.CreateShapeModel(
                    imageReduced,                    // 输入图像
                    RunParameter.NumLevels,          // 金字塔层数
                    RunParameter.AngleStart,         // 最小旋转角度
                    RunParameter.AngleExtent,        // 角度范围
                    RunParameter.AngleStep,          // 角度步长
                    RunParameter.Optimization,       // 优化方法
                    RunParameter.Metric,             // 匹配度量
                    RunParameter.Contrast,           // 对比度
                    RunParameter.MinContrast,        // 最小对比度
                    out ModelID                      // 输出模型ID
                );

                // 返回成功结果
                return new HOperateResult() { IsSuccess = true ,Message="生成Shape模板成功" };
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"执行失败: {ex.ToString()}");
            }

        }

        /// <summary>
        /// <summary>
        /// 保存模板
        /// </summary>
        public HOperateResult SaveShapeTemplate(HTuple shapeModel, string filePath)
        {
            const string SaveError = "保存Shape模板失败!";
            const string InvalidTemplateError = "Shape模板无效!";

            if (shapeModel == null)
                return HOperateResult.CreateFailResult(InvalidTemplateError);

            try
            {
                // 根据文件扩展名决定模板保存格式
                string extension = System.IO.Path.GetExtension(filePath).ToLower();
                string templateFormat = extension switch
                {
                    ".shm" => "shm", // 假设支持保存为 .shm 格式
                    _ => throw new NotSupportedException("不支持的模板格式")
                };

                // 保存 Shape 模板
                HOperatorSet.WriteShapeModel(shapeModel, filePath);
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"{SaveError} {ex.ToString()}");
            }

            return new HOperateResult { IsSuccess = true, Message = $"Shape模板成功保存到: {filePath}" };
        }

    }

    public class ShapeTemplateCreatorRunParameter : BaseParameter
    {
        // 参数：NumLevels
        private string numLevels;
        public string NumLevels
        {
            get { return numLevels; }
            set { numLevels = value; RaisePropertyChanged(); }
        }

        // 参数：AngleStart
        private double angleStart;
        public double AngleStart
        {
            get { return angleStart; }
            set { angleStart = value; RaisePropertyChanged(); }
        }

        // 参数：AngleExtent
        private double angleExtent;
        public double AngleExtent
        {
            get { return angleExtent; }
            set { angleExtent = value; RaisePropertyChanged(); }
        }

        // 参数：AngleStep
        private string angleStep;
        public string AngleStep
        {
            get { return angleStep; }
            set { angleStep = value; RaisePropertyChanged(); }
        }

        // 参数：Optimization
        private string optimization;
        public string Optimization
        {
            get { return optimization; }
            set { optimization = value; RaisePropertyChanged(); }
        }

        // 参数：Metric
        private string metric;
        public string Metric
        {
            get { return metric; }
            set { metric = value; RaisePropertyChanged(); }
        }

        // 参数：Contrast
        private string contrast;
        public string Contrast
        {
            get { return contrast; }
            set { contrast = value; RaisePropertyChanged(); }
        }

        // 参数：MinContrast
        private string minContrast;
        public string MinContrast
        {
            get { return minContrast; }
            set { minContrast = value; RaisePropertyChanged(); }
        }

        // 默认参数设置
        public override void ApplyDefaultParameter()
        {
            NumLevels = "auto";
            AngleStart = -0.39;
            AngleExtent = 0.79;
            AngleStep = "auto";
            Optimization = "auto"; // 默认优化方法
            Metric = "use_polarity"; // 默认匹配度量
            Contrast = "auto"; // 默认对比度设置
            MinContrast = "auto"; // 默认最小对比度
        }
    }

}

