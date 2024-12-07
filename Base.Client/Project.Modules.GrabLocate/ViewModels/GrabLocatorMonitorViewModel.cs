using Base.Client.Common;
using Base.Client.Entity;
using Base.Client.IBLL;
using HalconDotNet;
using HVisionLibs.Core.TemplateMatch;
using HVisionLibs.Core;
using HVisionLibs.Shared.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Base.Client.BLL;
using System.Windows;
using HVisionLibs.Core.Extensions;
using Project.Modules.GrabLocate.Models;
using Project.Modules.GrabLocate;
using Prism.Dialogs;
using Project.Modules.GrabLocate.Models.Resault;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Reflection.Metadata;
using Device.Models.Config;
using Device.DataConvertLib;
using Device.Communication;
using DataFormat = Device.DataConvertLib.DataFormat;
using System.IO;
using Project.Modules.GrabLocate.DAL;
using System.Windows.Navigation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project.Modules.GlueLocator.ViewModels
{
    public class GrabLocatorMonitorViewModel : PageViewModelBase
    {
        public IRunLogBLL runLogBLL { get; set; }
        public CommonConfigService commonConfig { get; set; }
        public ProductConfigService productConfigService { get; set; }
        public GrabLocatorMonitorViewModel(ProductConfigService _productConfigService, CommonConfigService _commonConfig, IRunLogBLL _runLogBLL, CameraController _cameraController, IUnityContainer unityContainer, IRegionManager regionManager, ShapeTemplateSearcherService mirrorService, IRoleBLL roleBLL, IUserBLL userBLL, IMenuBLL menuBLL)
    : base(unityContainer, regionManager)
        {
            this.PageTitle = "抓取引导";
            this.IsCanClose = false;
            runLogBLL = _runLogBLL;
            commonConfig = _commonConfig;
            this.Service = mirrorService;
            DrawObjectList = new ObservableCollection<DrawingObjectInfo>();
            LoadImageCommand = new DelegateCommand(LoadImage);
            RunCommand = new DelegateCommand(RunByHand);
            SaveImageCommand = new DelegateCommand(Save);
            LoadModeCommand = new DelegateCommand(LoadMode);
            //产品配置
            productConfigService = _productConfigService;
            var rstproductConfig = productConfigService.GetCurrentConfig();
            if (rstproductConfig.IsSuccess)
            {
                ProductConfig = rstproductConfig.Content;
                runLogBLL.AddSuccessLog("加载产品配置成功");
            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-加载产品配置失败" + rstproductConfig.Message);
                return;

            }
            //通用配置
            var rstLoadTestConfig = commonConfig.Load(1);
            if (rstLoadTestConfig.IsSuccess)
            {
                CommonConfig = rstLoadTestConfig.Content;
                runLogBLL.AddSuccessLog("加载通用Config成功");
            }
            else
            {
                runLogBLL.AddErrorLog("软件启动条件不满足-通用Config失败");
                return;

            }
            //相机
            try
            {
                cameraController = _cameraController;
                var camRst = cameraController.InitializeCamera();
                if (camRst.IsSuccess)
                {
                    runLogBLL.AddSuccessLog("相机初始化完成");
                }
                else
                {
                    runLogBLL.AddErrorLog("启动条件不足-相机初始化失败" + camRst.Message);
                    //return;
                }
                CaptureImageCommand = new DelegateCommand(CaptureImage);

            }
            catch (Exception ex)
            {

                runLogBLL.AddErrorLog("启动条件不足-相机初始化发生异常" + ex.ToString());

            }

            //开始
            LoadInfo();

        }

        #region 产品配方
        //CommonConfig 
        private string _LogGridStatus2 = "UnLock";
        public string LogGridStatus2
        {
            get { return _LogGridStatus2; }
            set
            {
                _LogGridStatus2 = value;
                RaisePropertyChanged();
            }
        }

        private bool _LogGridIsReadOnly2 = true;
        public bool LogGridIsReadOnly2
        {
            get { return _LogGridIsReadOnly2; }
            set { _LogGridIsReadOnly2 = value; RaisePropertyChanged(); }
        }

        private T_ProductConfig _ProductConfig;
        public T_ProductConfig ProductConfig
        {
            get { return _ProductConfig; }
            set { _ProductConfig = value; RaisePropertyChanged(); }
        }
        public DelegateCommand OnSeT_ProductConfigStatusCommand => new DelegateCommand(OnSeT_ProductConfigStatus);
        public void OnSeT_ProductConfigStatus()
        {
            if (LogGridIsReadOnly2)
            {
                //如果有解锁动作
                LogGridStatus2 = "Lock";
            }
            else
            {
                LogGridStatus2 = "UnLock";

            }
            LogGridIsReadOnly2 = !LogGridIsReadOnly2;
        }

        public DelegateCommand OnSaveProductConfigStatusCommand => new DelegateCommand(OnSaveProductConfigStatus);
        public void OnSaveProductConfigStatus()
        {
            var rst = productConfigService.Save(ProductConfig);
            if (rst.IsSuccess)
            {

                MessageBox.Show("保存成功");

            }
            else
            {
                MessageBox.Show("保存失败\r\n" + rst.Message);
            }
        }
        #endregion

        #region 通用配置
        //CommonConfig 
        private string _LogGridStatus = "UnLock";
        public string LogGridStatus
        {
            get { return _LogGridStatus; }
            set
            {
                _LogGridStatus = value;
                RaisePropertyChanged();
            }
        }

        private bool _LogGridIsReadOnly = true;
        public bool LogGridIsReadOnly
        {
            get { return _LogGridIsReadOnly; }
            set { _LogGridIsReadOnly = value; RaisePropertyChanged(); }
        }

        private T_CommonConfig _CommonConfig;
        public T_CommonConfig CommonConfig
        {
            get { return _CommonConfig; }
            set { _CommonConfig = value; RaisePropertyChanged(); }
        }
        public DelegateCommand OnSeT_CommonConfigStatusCommand => new DelegateCommand(OnSeT_CommonConfigStatus);
        public void OnSeT_CommonConfigStatus()
        {
            if (LogGridIsReadOnly)
            {
                //如果有解锁动作
                LogGridStatus = "Lock";
            }
            else
            {
                LogGridStatus = "UnLock";

            }
            LogGridIsReadOnly = !LogGridIsReadOnly;
        }

        public DelegateCommand OnSaveCommonConfigStatusCommand => new DelegateCommand(OnSaveCommonConfigStatus);
        public void OnSaveCommonConfigStatus()
        {
            var rst = commonConfig.Save(CommonConfig);
            if (rst.IsSuccess)
            {
                MessageBox.Show("保存成功");

            }
            else
            {
                MessageBox.Show("保存失败\r\n" + rst.Message);
            }
        }
        #endregion

        #region 产品配置
        private List<T_ProductConfig> _ActiveProductionConfigs;
        public List<T_ProductConfig> ActiveProductionConfigs
        {
            get { return _ActiveProductionConfigs; }
            set { _ActiveProductionConfigs = value; }
        }

        private List<T_ProductConfig> GetActiveProductionConfigs()
        {
           
            var   rst = productConfigService.GetAllProductConfigs();
            if (rst.IsSuccess)
            {
                runLogBLL.AddInfoLog("搜索激活产品配置成功");
                return rst.Content.Where(x=>x.IsActive && !x.IsSelected).ToList();
            }
            else 
            {
                runLogBLL.AddInfoLog("搜索激活产品配置失败");
                return new List<T_ProductConfig>();
            }
        }
        #endregion

        #region 视觉引导
        public ShapeTemplateSearcherService Service { get; set; }
           
        private ObservableCollection<RunResault> _RunResaults=new ObservableCollection<RunResault>();
        public ObservableCollection<RunResault> RunResaults
        {
            get { return _RunResaults; }
            set
            {
                _RunResaults = value;

            }
        }

        public DelegateCommand RunCommand { get; set; }
        public DelegateCommand LoadImageCommand { get; set; }
        public DelegateCommand LoadModeCommand { get; set; }


        public DelegateCommand SaveImageCommand { get; set; }


        private HOperateResult _HResult;

        public HOperateResult HResult
        {
            get { return _HResult; }
            set { _HResult = value; RaisePropertyChanged(); }
        }
        private MatchResult matchResult;
        public MatchResult MatchResults
        {
            get { return matchResult; }
            set { matchResult = value; RaisePropertyChanged(); }
        }

        private HObject image;
        public HObject Image
        {
            get { return image; }
            set 
            {
                image = value;
                RaisePropertyChanged(); 
            }
        }

        private string _CurrentModePath;
        public string CurrentModePath
        {
            get { return _CurrentModePath; }
            set { _CurrentModePath = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<DrawingObjectInfo> drawObjectList;

        public ObservableCollection<DrawingObjectInfo> DrawObjectList
        {
            get { return drawObjectList; }
            set { drawObjectList = value; RaisePropertyChanged(); }
        }

        private HObject maskObject;

        public HObject MaskObject
        {
            get { return maskObject; }
            set { maskObject = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 加载图像
        /// </summary>
        private void LoadImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (MatchResults != null)
            {
                MatchResults.Results.Clear();
                MatchResults.Message = "";

            }
            var result = (bool)dialog.ShowDialog();
            if (result)
            {
                var img = new HImage();
                img.ReadImage(dialog.FileName);
                Image = img;
            }
        }


        public void Save()
        {
            var rst= SaveCurrentImage();
            if (rst.IsSuccess) 
            {
                runLogBLL.AddSuccessLog("保存成功");
            }
            else
            {
                runLogBLL.AddSuccessLog("保存图片失败"+ rst.Message);

            }
        }
        private OperateResult SaveCurrentImage()
        {
            return SaveErrorImage(0, "保存");
        }
        private OperateResult SaveErrorImage(int Count, string Mode)
        {
            // 获取当前程序所在目录
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 获取上一级目录
            string parentDirectory = Directory.GetParent(currentDirectory).Parent.FullName;

            // 创建 ErrorImage 文件夹和日期分类文件夹路径
            string errorImageFolder = Path.Combine(parentDirectory, "ErrorImage");
            string dateFolder = Path.Combine(errorImageFolder, DateTime.Now.ToString("yyyy-MM-dd"));

            // 确保文件夹存在
            Directory.CreateDirectory(dateFolder);

            // 创建完整的文件路径
            string ErrorImagePath = Path.Combine(dateFolder, $"{DateTime.Now:yyyyMMdd_HHmmss}_{Mode}_Count{Count}.bmp");

            try
            {
                HOperatorSet.WriteImage(Image, "bmp", 0, ErrorImagePath);
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {

                return OperateResult.CreateFailResult(ex.ToString());
            }
        }
        private void LoadMode()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = (bool)dialog.ShowDialog();
            if (result)
            {
                HResult = Service.LoadShapeModelFromPath(dialog.FileName);
                if (HResult.IsSuccess)
                {
                    CurrentModePath = dialog.FileName;
                }
            }
        }

        private void LoadMode(string FileName)
        {
            HResult = Service.LoadShapeModelFromPath(FileName);

        }
       

        private void RunByHand()
        {
            Run();
        }

        /// <summary>
        /// 执行
        /// </summary>
        private OperateResult Run()
        {
            // 加载模板
            if (CurrentModePath != ProductConfig.ModelPath)
            {
                HResult = Service.LoadShapeModelFromPath(ProductConfig.ModelPath);
                if (!HResult.IsSuccess)
                {
                    runLogBLL.AddWarningLog($"加载模板【{ProductConfig.ModelPath}】失败");
                    SetCaptureResultSignal("9");
                    cts.Cancel();
                    runLogBLL.AddErrorLog("加载模板失败 - 软件停止");
                    return OperateResult.CreateFailResult("加载模板失败 - 软件停止");
                }
                runLogBLL.AddInfoLog($"加载模板【{ProductConfig.ModelPath}】成功");
                CurrentModePath = ProductConfig.ModelPath;

                var rstSetHandWidth = SetHandWidth(ProductConfig.GrbWidth);
                if (rstSetHandWidth.IsSuccess)
                {
                    runLogBLL.AddSuccessLog($"设置对应宽度【{ProductConfig.GrbWidth}】完成");
                }
                else
                {
                    cts.Cancel();
                    runLogBLL.AddErrorLog($"设置对应宽度【{ProductConfig.GrbWidth}】失败-软件调整");
                }
            }

            RunResaults.Clear();
            Service.RunParameter.NumMatches = 0;
            Service.RunParameter.MaxOverlap = 0.1;
            Service.RunParameter.MinScore = ProductConfig.MinScore;
            MatchResults = Service.Run(image);
            if (!MatchResults.Message.Contains("匹配"))
            {
                HResult = new HOperateResult() { IsSuccess = false, Message = matchResult.Message };
                return OperateResult.CreateFailResult(matchResult.Message);
            }

            HResult = new HOperateResult() { IsSuccess = true, Message = matchResult.Message };
            DisplayMatchRender();

            // 处理匹配结果
            int i = 1;
            int PassCount= MatchResults.Results.Where(x=>x.Score> ProductConfig.MinScoreForCheck).Count();

            foreach (var item in MatchResults.Results.OrderByDescending(x => x.Column))
            {

                if (item.Score> ProductConfig.MinScoreForCheck)
                {
                    var runResault = new RunResault
                    {
                        Index = i++,
                        Angle = item.Angle * (180 / Math.PI),
                        Score = item.Score,
                        Column = item.Column,
                        Row = item.Row
                    };
                    CalculateRobotCoordinates(CommonConfig, runResault); // 计算机器人坐标
                    RunResaults.Add(runResault);
                }
                else
                {
                    //if(PassCount>=2)
                    //{
                    //    if(item.Score > 0.3)
                    //    {
                    //        var runResault = new RunResault
                    //        {
                    //            Index = i++,
                    //            Angle = item.Angle * (180 / Math.PI),
                    //            Score = item.Score,
                    //            Column = item.Column,
                    //            Row = item.Row
                    //        };
                    //        CalculateRobotCoordinates(CommonConfig, runResault); // 计算机器人坐标
                    //        RunResaults.Add(runResault);
                    //        runLogBLL.AddWarningLog($"产品序号{runResault.Index} 分数低于0.5； 高于 0.3 且合格数大于等于2-合格");
                    //    }
                    //}
                }
            }
            // 设置夹爪宽度
            var rst = SetHandWidth(ProductConfig.GrbWidth);
          
            if (rst.IsSuccess)
            {
                runLogBLL.AddSuccessLog($"设置对应宽度【{ProductConfig.GrbWidth}】完成");
            }
            else
            {
                cts.Cancel();
                runLogBLL.AddErrorLog($"设置对应宽度【{ProductConfig.GrbWidth}】失败-软件调整");
                return OperateResult.CreateFailResult();

            }
            // 设置坐标
            var result = SetReault(RunResaults);
            if (!result.IsSuccess)
            {
                cts?.Cancel();
                runLogBLL.AddErrorLog("设置坐标失败 - 软件停止");
                return OperateResult.CreateFailResult("设置坐标失败 - 软件停止");
            }

            // 设置产品数量
            result = SetProductCountOnTray(RunResaults.Count.ToString());
            if (!result.IsSuccess)
            {
                cts?.Cancel();
                runLogBLL.AddErrorLog("设置产品数量失败 - 软件停止");
                return OperateResult.CreateFailResult("设置产品数量失败 - 软件停止");
            }

            // 如果未匹配到产品
            if (RunResaults.Count == 0)
            {
                bool isNoProduct = NoProductCheck(image);
                if (!isNoProduct)
                {
                    runLogBLL.AddWarningLog("未识别到产品，且未通过无料检测");

                    // 自动切换配方
                    if (CommonConfig.AutoSwitchRecipeEnabled)
                    {
                        if (ActiveProductionConfigs == null)
                            ActiveProductionConfigs = GetActiveProductionConfigs();

                        bool recipeSwitched = false;
                        for (int j = 0; j < ActiveProductionConfigs.Count; j++)
                        {
                            var prod = ActiveProductionConfigs[j];

                            if (!File.Exists(prod.ModelPath))
                            {
                                runLogBLL.AddWarningLog($"配方模板文件不存在: {prod.ModelPath}");
                                ActiveProductionConfigs.RemoveAt(j);
                                j--; // 删除元素后索引调整
                                continue;
                            }

                            HResult = Service.LoadShapeModelFromPath(prod.ModelPath);
                            if (HResult.IsSuccess)
                            {
                                runLogBLL.AddInfoLog($"加载模板成功: {prod.ModelPath}");
                                CurrentModePath = prod.ModelPath;

                                ProductConfig.IsSelected = false;
                                prod.IsSelected = true;
                                var update1 = productConfigService.UpdateProductConfig(ProductConfig);
                                var update2 = productConfigService.UpdateProductConfig(prod);
                                if (update1.IsSuccess && update2.IsSuccess)
                                {
                                    ProductConfig = prod; // 切换到新配方
                                    ActiveProductionConfigs.RemoveAt(j); // 移除已切换的配方
                                    runLogBLL.AddInfoLog($"配方切换成功: {prod.Name}");
                                    recipeSwitched = true;
                                    break; // 成功切换配方，跳出循环
                                }
                                else
                                {
                                    runLogBLL.AddErrorLog($"配方切换失败: {prod.Name}");
                                    cts.Cancel();
                                    break; // 配方切换失败，停止操作
                                }
                            }
                            else
                            {
                                runLogBLL.AddWarningLog($"加载模板失败: {prod.ModelPath}");
                                ActiveProductionConfigs.RemoveAt(j); // 移除当前配方
                                j--; // 删除元素后索引调整
                            }
                        }

                        // 如果没有可用的配方，则报警并停止
                        if (!recipeSwitched)
                        {
                            ActiveProductionConfigs = null;
                            SetCaptureResultSignal("9"); // 设置报警信号
                            runLogBLL.AddWarningLog("未找到有效配方，报警");
                            return OperateResult.CreateFailResult("未找到有效配方");
                        }

                        // 重新执行Run方法，应用新的配方
                        return Run();
                    }

                    // 自动切换配方未启用
                    SetCaptureResultSignal("9");
                    runLogBLL.AddWarningLog("识别失败 - 自动切换配方未启用");
                    return OperateResult.CreateFailResult("未启用自动切换配方");
                }

                // 没有料的情况下，设置拍照结果
                SetCaptureResultSignal("2");
                runLogBLL.AddWarningLog("设置拍照结果[没料]完成");
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                if(ActiveProductionConfigs != null)
                {
                    ActiveProductionConfigs = null;
                }

            }
            // 如果产品数量异常
            if (RunResaults.Count >0 && RunResaults.Count < ProductConfig.TargetCount)
            {
                
                var rstSave = SaveErrorImage(RunResaults.Count, ProductConfig.Name);
                if (rstSave?.IsSuccess == true)
                {
                    runLogBLL.AddSuccessLog("报错异常图片完成");
                }
                else
                {
                    runLogBLL.AddWarningLog($"报错异常图片失败: {rstSave?.Message}");
                }
                SetCaptureResultSignal("8");
                runLogBLL.AddWarningLog("设置拍照结果[数量异常]完成");
                return OperateResult.CreateSuccessResult();
            }

            // 一切正常
            SetCaptureResultSignal("1");
            runLogBLL.AddSuccessLog("设置拍照结果[正常]完成");

            return OperateResult.CreateSuccessResult();
        }


        // 根据已知的Score、Angle等计算RobotX, RobotY, RobotAngle
        public void CalculateRobotCoordinates(T_CommonConfig config, RunResault runResault)
        {
            // 将图像坐标转换为毫米坐标（假设Row和Column是像素值）
            double imageXmm = 5472 / 8.1;  // 图像列坐标转换为毫米
            double imageYmm = 3638 / 8.1;     // 图像行坐标转换为毫米

            // 计算从图像基准点到图像坐标的差值
            double deltaX = (runResault.Row - config.ImageBaseX) / 8.1;
            double deltaY = (runResault.Column - config.ImageBaseY) / 8.1;


            // 旋转变换，将坐标从图像坐标系转换到机器人坐标系
            runResault.RobotX = Math.Round(config.RobotBaseX + deltaX + ProductConfig.OffsetX, 3);
            runResault.RobotY = Math.Round(config.RobotBaseY + deltaY + ProductConfig.OffsetY, 3);
            runResault.RobotAngle= Math.Round(config.RobotBaseAngle + runResault.Angle + ProductConfig.OffsetR, 3);
        }
        public void DisplayMatchRender()
        {
            if (Image != null)
            {
                //Display(Image); 
            }
            if (MatchResults.Results != null)
            {
                foreach (var item in MatchResults.Results)
                {
                    if (MatchResults.Setting.IsShowCenter)
                    {
                        Service.HWindow.SetColor("red");

                        Service.HWindow.DispCross(item.Row, item.Column, 40, item.Angle);
                    }

                    if (MatchResults.Setting.IsShowDisplayText)
                    {
                        Service.HWindow.disp_message($"({item.Row},{item.Column}) Score:{item.Score}", "image", item.Row, item.Column, "black", "true");
                    }

                    if (MatchResults.Setting.IsShowMatchRange)
                    {
                        if(item.Score> ProductConfig.MinScoreForCheck)
                        {
                            Service.HWindow.SetColor("green");
                        }
                        else
                        {
                            Service.HWindow.SetColor("red");

                        }

                        Service.HWindow.DispObj(item.Contours);
                    }
                }
            }

        }

        public bool NoProductCheck(HObject image)
        {
            HObject imageReduced, region, roi;
            HTuple area, row, column;

            // 设置ROI（可以根据需求调整坐标）
            HTuple hv_ROI1_X1 = CommonConfig.ROI_X1, hv_ROI1_Y1 = CommonConfig.ROI_Y1;
            HTuple hv_ROI1_X2 = CommonConfig.ROI_X2, hv_ROI1_Y2 = CommonConfig.ROI_Y2;

            // 显示原图像
            HOperatorSet.DispImage(image, Service.HWindow);

            // 定义区域并减小图像范围
            HOperatorSet.GenRectangle1(out roi, hv_ROI1_X1, hv_ROI1_Y1, hv_ROI1_X2, hv_ROI1_Y2);
            HOperatorSet.ReduceDomain(image, roi, out imageReduced);

            // 应用阈值操作提取区域
            HOperatorSet.Threshold(imageReduced, out region, CommonConfig.ROI_MinGray, CommonConfig.ROI_MaxGray);

            // 判断区域是否为空
            HOperatorSet.AreaCenter(region, out area, out row, out column);

            // 绘制矩形区域的边缘
            HOperatorSet.SetColor(Service.HWindow, "green");
            HOperatorSet.DispRectangle1(Service.HWindow, hv_ROI1_X1, hv_ROI1_Y1, hv_ROI1_X2, hv_ROI1_Y2);

            // 显示搜索出来的Region边缘（即使region为空，仍显示空白区域的边缘）
            HOperatorSet.SetColor(Service.HWindow, "yellow");
            HOperatorSet.DispRegion( region, Service.HWindow);

            // 绘制面积信息
            HTuple hv_AreaMessage = "Area: " + area;
            HOperatorSet.SetColor(Service.HWindow, "red");
            Service.HWindow.disp_message(hv_AreaMessage, "image", row, column, "black", "true");

            // 如果区域为空，则返回 true（没有产品）
            if (area == 0)
            {
                return true;
            }

            // 如果区域的面积大于某个阈值（例如，1000），则认为有产品
            if (area > CommonConfig.ROI_MaxArea)
            {
                return false;
            }

            // 否则没有产品
            return true;
        }

        #endregion

        #region 相机
        CameraController cameraController;
        public new DelegateCommand CaptureImageCommand { get; set; }

        private void CaptureImage()
        {
            // 开始计时
            Stopwatch stopwatch = Stopwatch.StartNew();

            // 调用方法
            OperateResult result = cameraController.CaptureImage(out HObject capturedImage);
            Image = capturedImage;

            // 停止计时
            stopwatch.Stop();
            if (result.IsSuccess)
            {
                runLogBLL.AddSuccessLog($"Image captured successfully. Time taken: {stopwatch.ElapsedMilliseconds} ms");
            }
            else
            {
                runLogBLL.AddWarningLog($"Failed to capture image: {result.Message}. Time taken: {stopwatch.ElapsedMilliseconds} ms" + "");

            }
        }



        #endregion

        #region PLC
        private void LoadInfo()
        {
            // 获取当前程序所在目录
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 获取上一级目录
            string parentDirectory = Directory.GetParent(currentDirectory).Parent.FullName;

            //加载PLC产数
            string devicePath = Path.Combine(parentDirectory, @"HaierConfig\Device.ini");
            string groupPath = Path.Combine(parentDirectory, @"HaierConfig\Group.xlsx");
            string variablePath = Path.Combine(parentDirectory, @"HaierConfig\Variable.xlsx");
            var loadRst = DeviceConfigDAL.LoadDeviceInfo(devicePath, groupPath, variablePath);
            if (loadRst.IsSuccess)
            {
                Device = loadRst.Content;
                runLogBLL.AddSuccessLog("PLC配置信息加载成功");
                Task.Run(() =>
                {
                    PLCCommunication(loadRst.Content);
                }, cts.Token);
            }

            else
            {
                runLogBLL.AddErrorLog("软件启动条件不足-PLC配置信息加载失败/r/n" + loadRst.Message);
                return;
            }
        }
        #region PLC
        private DeviceInfo device;
        public DeviceInfo Device
        {
            get { return device; }
            set { device = value; RaisePropertyChanged(); }
        }

        ModbusTCP modbusTCP { get; set; }
        Device.DataConvertLib.DataFormat dataFormat { get; set; }
        #region Property_PLC
        //连接状态
        private string _ConnectionStatus;
        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set { _ConnectionStatus = value; RaisePropertyChanged(); }
        }

        //请求拍照信号
        private string _CaptureRequestSignal;
        public string CaptureRequestSignal
        {
            get { return _CaptureRequestSignal; }
            set { _CaptureRequestSignal = value; RaisePropertyChanged(); }
        }


        //拍照的结果
        private string _CaptureResultSignal;
        public string CaptureResultSignal
        {
            get { return _CaptureResultSignal; }
            set { _CaptureResultSignal = value; }
        }


        //取料方式 PickupMethodSignal
        private string _PickupMethodSignal;
        public string PickupMethodSignal
        {
            get { return _PickupMethodSignal; }
            set { _PickupMethodSignal = value; RaisePropertyChanged(); }
        }

        //产品配方编号
        private string _ProductFormulaID;
        public string ProductFormulaID
        {
            get { return _ProductFormulaID; }
            set { _ProductFormulaID = value; RaisePropertyChanged(); }
        }

        //D507载盘产品数量
        private string _ProductCountOnTray;
        public string ProductCountOnTray
        {
            get { return _ProductCountOnTray; }
            set { _ProductCountOnTray = value; RaisePropertyChanged(); }
        }


        #endregion

        #region Methods
        //取消线程源
        private CancellationTokenSource cts = new CancellationTokenSource();
        public OperateResult SetCaptureResultSignal(string resault)
        {
            //1 = 拍照正常完成,2 = 没料,8 = 数量异常,9 = 报警
            short sRst = short.Parse(resault);
            byte[] bytes = ByteArrayLib.GetByteArrayFromShort(sRst);
            bool rst = modbusTCP.PreSetSingleRegister(502, bytes);
            if (rst)
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailResult();
            }

        }
        public OperateResult SetProductCountOnTray(string resault)
        {
            short sRst = short.Parse(resault);
            byte[] bytes = ByteArrayLib.GetByteArrayFromShort(sRst);
            bool rst = modbusTCP.PreSetSingleRegister(507, bytes);
            if (rst)
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailResult();
            }

        }

        bool Heart = true;
        DateTime LastHeatTime = DateTime.MinValue;
        public OperateResult SetHeart()
        {
            short sRst = Heart ? (short)1 : (short)0;
            byte[] bytes = ByteArrayLib.GetByteArrayFromShort(sRst);
            bool rst = modbusTCP.PreSetSingleRegister(508, bytes);
            if (rst)
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailResult();
            }

        }

        public OperateResult SetReault(ObservableCollection<RunResault> runResaults)
        {
            byte[] result = new byte[56 * 2];

            for (int i = 0; i < runResaults.Count; i++)
            {
                int X = (int)(runResaults[i].RobotX * 1000);
                byte[] Xbytes = ByteArrayLib.GetByteArrayFromInt(X);
                int Y = (int)(runResaults[i].RobotY * 1000);
                byte[] Ybytes = ByteArrayLib.GetByteArrayFromInt(Y);
                int Phi = (int)(runResaults[i].RobotAngle * 1000);
                byte[] Phibytes = ByteArrayLib.GetByteArrayFromInt(Phi);
                int j = 20 * i;
                result[j] = Xbytes[0];
                result[j + 1] = Xbytes[1];
                result[j + 2] = Xbytes[2];
                result[j + 3] = Xbytes[3];
                result[j + 4] = Ybytes[0];
                result[j + 5] = Ybytes[1];
                result[j + 6] = Ybytes[2];
                result[j + 7] = Ybytes[3];
                result[j + 8] = Phibytes[0];
                result[j + 9] = Phibytes[1];
                result[j + 10] = Phibytes[2];
                result[j + 11] = Phibytes[3];
            }

            bool rst = modbusTCP.PreSetMultiRegisters(520, result);
            if (rst)
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailResult();
            }
        }
        
        public OperateResult SetHandWidth(double width)
        {
            int IRst=(int)(width * 1000);
            byte[] result = ByteArrayLib.GetByteArrayFromInt(IRst);

            bool rst = modbusTCP.PreSetMultiRegisters(3030, result);
            if (rst)
            {
                return OperateResult.CreateSuccessResult();
            }
            else
            {
                return OperateResult.CreateFailResult();
            }
        }
        
        /// <summary>
        /// PLC通信连接方法
        /// </summary>
        /// <param name="device"></param>
        private void PLCCommunication(DeviceInfo device)
        {
            string DeviceName = "PLC";
            int ReConnectCount = 0;//重连次数
            dataFormat = DataFormat.CDAB;

            //加载PLC产数
            while (!cts.IsCancellationRequested)
            {
                if (device.IsConnected)
                {
                    foreach (var gp in device.GroupList)
                    {
                        byte[] data = null;
                        int reqLength = 0;
                        if (gp.StoreArea == "输入线圈" || gp.StoreArea == "输出线圈")
                        {
                            switch (gp.StoreArea)
                            {
                                case "输入线圈":
                                    data = modbusTCP.ReadInputCoils(gp.Start, gp.Length);
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                case "输出线圈":
                                    data = modbusTCP.ReadOutputCoils(gp.Start, gp.Length);
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                default:
                                    break;
                            }

                            if (data != null && data.Length == reqLength)
                            {
                                foreach (var variable in gp.VarList)
                                {
                                    int start = variable.Start - gp.Start;

                                    DataType dataType = (DataType)Enum.Parse(typeof(DataType), variable.DataType, true);

                                    switch (dataType)
                                    {
                                        case DataType.Bool:
                                            variable.VarValue = BitLib.GetBitFromByteArray(data, start, variable.OffsetOrLength);
                                            break;
                                        default:
                                            break;
                                    }

                                    device.UpdateVariable(variable);
                                }
                            }
                            else
                            {
                                device.IsConnected = false;
                            }
                        }
                        else
                        {
                            switch (gp.StoreArea)
                            {
                                case "输入寄存器":
                                    data = modbusTCP.ReadInputRegisters(gp.Start, gp.Length);
                                    reqLength = gp.Length * 2;
                                    break;
                                case "输出寄存器":
                                    data = modbusTCP.ReadOutputRegisters(gp.Start, gp.Length);
                                    reqLength = gp.Length * 2;
                                    break;
                                default:
                                    break;
                            }
                            if (data != null && data.Length == reqLength)
                            {
                                foreach (var variable in gp.VarList)
                                {
                                    int start = variable.Start - gp.Start;

                                    start *= 2;

                                    DataType dataType = (DataType)Enum.Parse(typeof(DataType), variable.DataType, true);

                                    switch (dataType)
                                    {
                                        case DataType.Bool:
                                            variable.VarValue = BitLib.GetBitFrom2BytesArray(data, start, variable.OffsetOrLength, dataFormat == DataFormat.BADC || dataFormat == DataFormat.DCBA);
                                            break;
                                        case DataType.Byte:
                                            variable.VarValue = ByteLib.GetByteFromByteArray(data, start);
                                            break;
                                        case DataType.Short:
                                            variable.VarValue = ShortLib.GetShortFromByteArray(data, start);
                                            break;
                                        case DataType.UShort:
                                            variable.VarValue = UShortLib.GetUShortFromByteArray(data, start);
                                            break;
                                        case DataType.Int:
                                            variable.VarValue = IntLib.GetIntFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.UInt:
                                            variable.VarValue = UIntLib.GetUIntFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.Float:
                                            variable.VarValue = FloatLib.GetFloatFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.Double:
                                            variable.VarValue = DoubleLib.GetDoubleFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.Long:
                                            variable.VarValue = LongLib.GetLongFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.ULong:
                                            variable.VarValue = ULongLib.GetULongFromByteArray(data, start, dataFormat);
                                            break;
                                        case DataType.String:
                                            variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(data, start, variable.OffsetOrLength * 2, Encoding.ASCII);
                                            break;
                                        case DataType.ByteArray:
                                            variable.VarValue = ByteArrayLib.GetByteArrayFromByteArray(data, start, variable.OffsetOrLength * 2);
                                            break;
                                        case DataType.HexString:
                                            variable.VarValue = StringLib.GetHexStringFromByteArray(data, start, variable.OffsetOrLength * 2);
                                            break;
                                        default:
                                            break;
                                    }
                                    variable.VarValue = MigrationLib.GetMigrationValue(variable.VarValue, variable.Scale.ToString(), variable.Offset.ToString()).Content;

                                    device.UpdateVariable(variable);
                                    if (variable.VarName.Contains("D500请求拍照信号"))
                                    {
                                        if (CaptureRequestSignal != variable.VarValue.ToString())
                                        {
                                            CaptureRequestSignal = variable.VarValue.ToString();

                                            Application.Current.Dispatcher.Invoke(() =>
                                            {
                                                CaptureRequestSignal = variable.VarValue.ToString();
                                                if (CaptureRequestSignal != "0")//视觉读取，有值就进行拍照
                                                {
                                                    runLogBLL.AddInfoLog($"接收到拍照请求【D500 ：{CaptureRequestSignal}】");
                                                    // 调用方法
                                                    OperateResult result =null;
                                                    HObject capturedImage=new HObject();
                                                    bool IsDebug = false;//如果是Debug则不拍照
                                                    if (IsDebug)
                                                    {
                                                        result = OperateResult.CreateSuccessResult();
                                                    }
                                                    else
                                                    {
                                                        result = cameraController.CaptureImage(out  capturedImage);
                                                        Image = capturedImage;

                                                    }

                                                    if (result.IsSuccess)
                                                    {
                                                        runLogBLL.AddInfoLog($"Image captured successfully. ");


                                                        Application.Current.Dispatcher.Invoke
                                                       (() =>
                                                       {

                                                           OperateResult rst = null;
                                                           try
                                                           {
                                                               rst = Run();
                                                           }
                                                           catch (Exception ex)
                                                           {
                                                               cts.Cancel();
                                                               runLogBLL.AddErrorLog("软件停止-执行Run方法发生未知错误" + ex.ToString());

                                                           }

                                                       });

                                                    }
                                                    else
                                                    {
                                                        runLogBLL.AddWarningLog($"Failed to capture image: {result.Message}. ");
                                                        var rst = SetCaptureResultSignal("9");
                                                        if (!rst.IsSuccess)
                                                        {
                                                            cts.Cancel();
                                                            runLogBLL.AddErrorLog("设置失败，软件停止");
                                                        }
                                                    }
                                                }

                                            });
                                         
                                        }
                                    }


                                    if ((DateTime.Now - LastHeatTime).TotalSeconds > 1)
                                    {
                                        Heart = !Heart;
                                        SetHeart();
                                    }
                                }
                            }
                            else
                            {
                                device.IsConnected = false;
                            }
                        }
                    }

                }
                else
                {
                    if (device.ReConnectSign)
                    {
                        modbusTCP.DisConnect();
                        //重连
                        Thread.Sleep(device.ReConnectTime);
                    }

                    modbusTCP = new ModbusTCP();

                    device.IsConnected = modbusTCP.Connect(device.IPAddress, device.Port);

                    if (device.ReConnectSign == false)
                    {
                        device.ReConnectSign = true;
                    }
                    if (device.IsConnected)
                    {
                        ReConnectCount = 0;
                        ConnectionStatus = "连接正常";
                        runLogBLL.AddSuccessLog(DeviceName + "连接成功");
                    }
                    else
                    {
                        ConnectionStatus = "已断开";
                        ReConnectCount += 1;
                        runLogBLL.AddWarningLog(DeviceName + $"第{ReConnectCount}次连接失败");
                        if (ReConnectCount > 3)
                        {
                            cts.Cancel();
                            runLogBLL.AddErrorLog(DeviceName + $"重连达到上限-程序停止");

                        }
                    }
                }
                if (ConnectionStatus == null)
                {
                    ConnectionStatus = "已断开";
                }
                else if (device.IsConnected && ConnectionStatus == "已断开")
                {
                    ReConnectCount = 0;
                    ConnectionStatus = "连接正常";

                }
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
