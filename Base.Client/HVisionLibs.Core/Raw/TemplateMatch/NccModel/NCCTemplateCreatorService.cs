using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core.TemplateMatch.NccModel
{
    public class NCCTemplateCreatorService : BindableBase
    {
        // 创建 NCC 模型
        public HTuple nccModel;
        public NCCTemplateCreatorService()
        {
            info = new MethodInfo()
            {
                Name = "create_ncc_model",
                Description = "The operator create_ncc_model prepares a template, which is passed in the image Template, as an NCC model used for matching using the normalized cross correlation (NCC).\r\n" ,
  
            Parameters = new List<MethodParmeter>()
                {
                    new MethodParmeter(){Name="NumLevels",Description="Maximum number of pyramid levels."},
                    new MethodParmeter(){Name="AngleStart",Description="Smallest rotation of the pattern."},
                    new MethodParmeter(){Name="AngleExtent",Description="Extent of the rotation angles."},
                    new MethodParmeter(){Name="AngleStep",Description="Step length of the angles (resolution)."},
                    new MethodParmeter(){Name="Metric",Description="Match metric."},
                },
                Predecessors = new List<string>()
                {
                   // "set_metrology_model_image_size"
                }
            };
            RunParameter = new NCCTemplateCreatorRunParameter();
            RunParameter.ApplyDefaultParameter();
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value; RaisePropertyChanged(); }
        }


        public MethodInfo info { get; set; }

        private NCCTemplateCreatorRunParameter runParameter;
        public NCCTemplateCreatorRunParameter RunParameter { get { return runParameter; } set { runParameter = value; RaisePropertyChanged(); } }






        public HOperateResult Run(HObject image, HObject ROIObj)
        {
            const string ImageError = "图片异常!";
            const string ROIError = "ROI未设置!";
            const string ModeError = "参数异常!";

            // 清理旧的 NCC 模型（如果存在）
            if (nccModel != null)
            {
                nccModel.ClearHandle();
                nccModel = null;
            }

            // 检查图像和 ROI 是否为 null
            if (image == null)
                return HOperateResult.CreateFailResult(ImageError);
            if (ROIObj == null)
                return HOperateResult.CreateFailResult(ROIError);

            // 检查运行参数是否有效
            if (RunParameter.NumLevels == null || RunParameter.AngleStart == 0 || RunParameter.AngleExtent == 0)
                return HOperateResult.CreateFailResult(ModeError);

            try
            {
                // 限制图像区域
                HOperatorSet.ReduceDomain(image, ROIObj, out HObject imageReduced);

                // 创建 NCC 模型
                HOperatorSet.CreateNccModel(imageReduced, RunParameter.NumLevels, RunParameter.AngleStart, RunParameter.AngleExtent, RunParameter.AngleStep, RunParameter.Metric, out nccModel);

                // 返回成功结果
                return new HOperateResult() { IsSuccess = true, Message = "生成NCC模板成功" };
            }
            catch (Exception ex)
            {
                // 捕获异常并返回失败结果
                return HOperateResult.CreateFailResult($"执行失败: {ex.Message}\n{ex.StackTrace}");
            }
        }


        /// <summary>
        /// <summary>
        /// 保存 NCC 模板
        /// </summary>
        public HOperateResult SaveNccTemplate(HTuple nccTemplate, string filePath)
        {
            const string SaveError = "保存NCC模板失败!";
            const string InvalidTemplateError = "NCC模板无效!";

            if (nccTemplate == null)
                return HOperateResult.CreateFailResult(InvalidTemplateError);

            try
            {
                // 根据文件扩展名决定模板保存格式
                string extension = System.IO.Path.GetExtension(filePath).ToLower();
                string templateFormat = extension switch
                {
                    ".ncc" => "ncc", // 假设支持保存为 .ncc 格式
                    _ => throw new NotSupportedException("不支持的模板格式")
                };

                // 保存 NCC 模板
                HOperatorSet.WriteNccModel(nccTemplate, filePath);
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"{SaveError} {ex.ToString()}");
            }

            return new HOperateResult { IsSuccess = true, Message = $"NCC模板成功保存到: {filePath}" };
        }

    }

    public class NCCTemplateCreatorRunParameter : BaseParameter
    {

        private string numLevels;
        public string NumLevels
        {
            get { return numLevels; }
            set { numLevels = value;RaisePropertyChanged(); }
        }


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
            set { angleExtent = value;RaisePropertyChanged(); }
        }

        private string angleStep;
        public string AngleStep
        {
            get { return angleStep; }
            set { angleStep = value; RaisePropertyChanged(); }
        }
        private string metric;
        public string Metric
        {
            get { return metric; }
            set { metric = value; }
        }


        public override void ApplyDefaultParameter()
        {
            NumLevels = "auto";
            AngleStart = -0.39;
            AngleExtent = 0.79;
            AngleStep = "auto";
            Metric= "use_polarity";
        }
    }
}

