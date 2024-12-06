using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core.Preprocessing
{
    public class MirrorService:BindableBase
    {
        public MirrorService()
        {
            info = new MethodInfo()
            {
                Name = "mirror_image",
                Description = "mirror_image reflects an image Image about one of three possible axes. If Mode is set to 'row', it is reflected about the horizontal axis, if Mode is set to 'column', about the vertical axis, and if Mode is set to 'diagonal', about the main diagonal x=y.\r\n\r\n",
                Parameters = new List<MethodParmeter>()
                {
                    new MethodParmeter(){Name="Mode",Description="镜像模式"},
                },
                Predecessors = new List<string>()
                {
                    "set_metrology_model_image_size"
                }
            };
            RunParameter = new MirrorRunParameter();
            RunParameter.ApplyDefaultParameter();
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value; RaisePropertyChanged(); }
        }


        public MethodInfo info { get; set; }

        private MirrorRunParameter runParameter;
        public MirrorRunParameter RunParameter { get { return runParameter; } set { runParameter = value; RaisePropertyChanged(); } }
        public HOperateResult Run(ref HObject image)
        {
            const string ImageError = "图片异常!";
            const string ModeError = "参数异常，请先设置镜像模式!";

            if (image == null)
                return HOperateResult.CreateFailResult(ImageError);

            if (string.IsNullOrEmpty(RunParameter.Mode))
                return HOperateResult.CreateFailResult(ModeError);

            // 使用 using 语句以确保资源正确释放
            HObject imageMirror;
            try
            {
                HOperatorSet.MirrorImage(image, out imageMirror, RunParameter.Mode);
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"镜像处理失败: {ex.ToString()}");
            }

            // 更新图像
            image = imageMirror;
            return new HOperateResult { IsSuccess = true, Message= $"镜像设置完成，Mode: {RunParameter.Mode}" };
        }

        /// <summary>
        /// 保存图像
        /// </summary>
        public HOperateResult SaveImage(HObject image, string filePath)
        {
            const string SaveError = "保存图片失败!";
            const string InvalidImageError = "图像无效!";

            if (image == null)
                return HOperateResult.CreateFailResult(InvalidImageError);

            try
            {
                // 根据文件扩展名决定图像格式
                string extension = System.IO.Path.GetExtension(filePath).ToLower();
                string imageFormat = extension switch
                {
                    ".png" => "png",
                    ".jpg" => "jpeg",
                    ".bmp" => "bmp",
                    _ => throw new NotSupportedException("不支持的图像格式")
                };

                // 保存图像
                HOperatorSet.WriteImage(image, imageFormat, 0, filePath);
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"{SaveError} {ex.ToString()}");
            }

            return new HOperateResult { IsSuccess = true, Message = $"图像成功保存到: {filePath}" };
        }
    }

    public class MirrorRunParameter : BaseParameter
    {
        private string mode;

        public string Mode
        {
            get { return mode; }
            set { mode = value; RaisePropertyChanged(); }
        }

        public override void ApplyDefaultParameter()
        {
            Mode = "row";
        }
    }
}

