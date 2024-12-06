using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVisionLibs.Core.Preprocessing
{
    public class ZoomFactorService : BindableBase
    {
        public ZoomFactorService()
        {
            info = new MethodInfo()
            {
                Name = "zoom_image_factor",
                Description = "zoom_image_factor scales the image Image by a factor of " +
                "ScaleWidth in width and a factor ScaleHeight in height. " +
                " The parameter Interpolation determines the type of interpolation used (see affine_trans_image). The domain of the input image is ignored, i.e., assumed to be the full rectangle of the image.\r\n\r\n",
                Parameters = new List<MethodParmeter>()
                {
                    new MethodParmeter(){Name="ScaleWidth ",Description="Scale factor for the width of the image."},
                    new MethodParmeter(){Name="ScaleHeight  ",Description="Scale factor for the height of the image."},
                    new MethodParmeter()
                    {
                        Name="Interpolation",
                        Description=
                        "Type of interpolation.\r\n"
                    +"List of values: 'bicubic', 'bilinear', 'constant', 'nearest_neighbor', 'weighted'.\r\n"
                    +"bicubic（双三次插值）：\r\n描述：通过考虑周围16个像素的值来计算新像素的值，提供平滑的图像效果。\r\n应用：适合用于需要高质量和细节的图像缩放，尤其是在图像放大时。\r\n"
                    +"bicubic（双线性插值）：\r\n描述：根据周围四个像素的值进行线性插值，计算新像素的值。\r\n应用：相比于最近邻插值，双线性插值生成的图像质量更高，但计算速度比双三次插值快，适合快速处理。\r\n"
                    +"constant（常量插值）：\r\n描述：新像素的值被设定为周围像素的固定值，通常是最接近的一个像素的值。\r\n应用：这种方法简单且快速，但在处理时可能会产生明显的边界和失真。\r\n"
                    +"nearest_neighbor（最近邻插值）：\r\n描述：将新像素的值直接设为最近的原始像素的值，简单且计算快速。\r\n应用：适合于不需要平滑处理的情况，例如处理像素艺术或某些特定的图像效果。可能导致锯齿状的边缘。\r\n"
                    +"weighted（加权插值）：\r\n描述：基于周围像素的值进行加权计算，权重通常根据距离来设置，离新像素越近的像素权重越大。\r\n应用：提供比双线性和最近邻插值更好的质量，适合在需要平滑效果的情况下使用。\r\n"},
                },
                Predecessors = new List<string>()
                {
                   // "set_metrology_model_image_size"
                }
            };
            RunParameter = new ZoomRunParameter();
            RunParameter.ApplyDefaultParameter();
        }

        private HWindow hWindow;

        public HWindow HWindow
        {
            get { return hWindow; }
            set { hWindow = value; RaisePropertyChanged(); }
        }


        public MethodInfo info { get; set; }

        private ZoomRunParameter runParameter;
        public ZoomRunParameter RunParameter { get { return runParameter; } set { runParameter = value; RaisePropertyChanged(); } }
        public HOperateResult Run(ref HObject image)
        {
            const string ImageError = "图片异常!";
            const string ModeError = "参数异常，请先设置镜像模式!";

            if (image == null)
                return HOperateResult.CreateFailResult(ImageError);

            if (string.IsNullOrEmpty(RunParameter.Interpolation)|| RunParameter.ScaleWidth==0|| RunParameter.ScaleHeight==0)
                return HOperateResult.CreateFailResult(ModeError);

            // 使用 using 语句以确保资源正确释放
            try
            {
                HOperatorSet.ZoomImageFactor(image, out HObject OutImage, RunParameter.ScaleWidth, RunParameter.ScaleHeight, RunParameter.Interpolation);
                // 更新图像
                image = OutImage;
            }
            catch (Exception ex)
            {
                return HOperateResult.CreateFailResult($"Setting the zoom factor failed: {ex.ToString()}: {ex.ToString()}");
            }

        
            return new HOperateResult { IsSuccess = true, Message= $"Setting the zoom factor successed" };
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

    public class ZoomRunParameter : BaseParameter
    {

        private double scaleWidth;
        public double ScaleWidth
        {
            get { return scaleWidth; }
            set { scaleWidth = value;RaisePropertyChanged(); }
        }


        private double scaleHeight;
        public double ScaleHeight
        {
            get { return scaleHeight; }
            set { scaleHeight = value; RaisePropertyChanged(); }
        }

        private string interpolation;
        public string Interpolation
        {
            get { return interpolation; }
            set { interpolation = value;RaisePropertyChanged(); }
        }

        public override void ApplyDefaultParameter()
        {
            ScaleWidth = 0.5;
            ScaleHeight = 0.5;
            Interpolation = "constant";
        }
    }
}

