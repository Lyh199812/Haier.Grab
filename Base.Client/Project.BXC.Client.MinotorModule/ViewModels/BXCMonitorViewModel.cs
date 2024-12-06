using BXC.Client.MinotorModule.BLL;
using BXC.Client.MinotorModule.DAL;
using BXC.Client.MinotorModule.Models;
using Device.Communication;
using Device.DataConvertLib;
using Device.Models.Config;
using Base.Client.BLL;
using Base.Client.DAL;
using Base.Client.Entity;
using Base.Client.Entity.BXC;
using Base.Client.IBLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using Unity;
using Unity.Injection;
using DataFormat = Device.DataConvertLib.DataFormat;
using Base.Client.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Base.Client.BXC.IBLL;
using BXC.Client.DAL;

namespace BXC.Client.MinotorModule.ViewModels
{
    public class BXCMonitorViewModel : BindableBase
    {
        private string _pageTitle = "Main";
        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }
        public bool IsCanClose { get; set; } = false;
        public IBXCRunLogBLL runLogBLL {  get; set; }
        public BXCMonitorViewModel(IBXCRunLogBLL logBLL) 
        {
            var rst = DBDAL.AddOrLoadTodayRecord();
            if (rst != null)
            {
                Statistics = rst.Content;
            }
            runLogBLL =logBLL;
            Recods=new ObservableCollection<BXCRecordEntity> ();
            cts =new CancellationTokenSource();
            WeightReleaseCommand = new DelegateCommand(SetReleaseSignal);
            ScanCommand  = new DelegateCommand(CamScan);
            SetScanSignalCommand=new DelegateCommand(SetScanSignal);
            MESCommand = new DelegateCommand(MesTest);
            SaveDBConfigCommand=new DelegateCommand(SaveDBConfig);
            ReShelveCommand = new DelegateCommand(SetIsReShelve);
            ProductDataClearCommand = new DelegateCommand(ProductDataClear);
            LoadInfo();
            UnlockCommand = new DelegateCommand(UnlockReadOnly);
        }

        /// <summary>
        /// 解锁操作
        /// </summary>
        private void UnlockReadOnly()
        {
            if (BtnLockContent == "UnLock")
            {
                // 弹出输入密码对话框
                string inputPassword = ShowInputPasswordDialog();

                // 获取系统生成的密码
                string generatedPassword = GeneratePassword();

                // 校验密码
                //if (inputPassword.Contains(generatedPassword)&& inputPassword.Length>8)
                //{
                //    IsCfgReadOnly = false;
                //    BtnLockContent = "Lock";
                //    MessageBox.Show("已开启编辑模式！", "提示");

                //}
                //else
                //{
                //    MessageBox.Show("密码错误，请重试！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

                if (inputPassword=="1")
                {
                    IsCfgReadOnly = false;
                    BtnLockContent = "Lock";
                    MessageBox.Show("已开启编辑模式！", "提示");

                }
                else
                {
                    MessageBox.Show("密码错误，请重试！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                IsCfgReadOnly = true;
                BtnLockContent = "UnLock";
            }
        }

        /// <summary>
        /// 根据系统事件动态生成密码
        /// </summary>
        /// <returns>动态生成的密码</returns>
        private string GeneratePassword()
        {
            // 获取当前日期和时间
            DateTime now = DateTime.Now;

            // 将日期部分保持为十进制字符串
            string mouthHex = now.Month.ToString("X");

            string dateHex = now.Day.ToString("X");

            // 将小时部分转为十六进制字符串
            string hourHex = now.Hour.ToString("X");

            // 拼接最终密码
            string password = $"{mouthHex}{dateHex}{hourHex}";

            return password;
        }

        /// <summary>
        /// 显示一个输入框让用户输入密码
        /// </summary>
        /// <returns>用户输入的密码</returns>
        private string ShowInputPasswordDialog()
        {
            // 使用 Windows Forms 提供的 InputBox 获取密码
            return Microsoft.VisualBasic.Interaction.InputBox("请输入密码：", "输入密码", "");
        }


        #region View Properties
        private bool _IsCfgReadOnly=true;
        public bool IsCfgReadOnly
        {
            get { return _IsCfgReadOnly; }
            set { _IsCfgReadOnly=  value; RaisePropertyChanged(); }
        }

        private string btnLockContent="UnLock";
        public string BtnLockContent
        {
            get { return btnLockContent; }
            set { btnLockContent = value;RaisePropertyChanged(); }
        }

        public DelegateCommand UnlockCommand { get; set; }
        #endregion
        private void LoadInfo()
        {
            // 获取当前程序所在目录
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 获取上一级目录
            string parentDirectory = Directory.GetParent(currentDirectory).Parent.FullName;

            //加载系统参数
            string cfgPath = Path.Combine(parentDirectory, @"EConfig_Camera\Device.ini");
            var loadRstCmaera = DeviceConfigDAL.LoadDeviceInfo(cfgPath);
            if (!loadRstCmaera.IsSuccess)
            {
                runLogBLL.LogError("加载扫码相机配置信息失败/r/n" + loadRstCmaera.Message);
                return;
            }
            else
            {
                runLogBLL.LogInfo("加载扫码相机配置信息成功");

            }
            CamDevice = loadRstCmaera.Content;

            //加载MES配置文件
            var mesCfgLoadRst=DBDAL.LoadSelectedConfig();
            if(!mesCfgLoadRst.IsSuccess)
            {
                runLogBLL.LogError("加载MES配置信息失败/r/n" + mesCfgLoadRst.Message);
                return;
            }
            else
            {
                runLogBLL.LogInfo("加载MES配置信息成功");
                DbCfg = mesCfgLoadRst.Content;
            }
            //加载PLC产数
            string devicePath = Path.Combine(parentDirectory, @"EConfig_WeightPLC\Device.ini");
            string groupPath = Path.Combine(parentDirectory, @"EConfig_WeightPLC\Group.xlsx");
            string variablePath = Path.Combine(parentDirectory, @"EConfig_WeightPLC\Variable.xlsx");
            var loadRst = DeviceConfigDAL.LoadDeviceInfo(devicePath, groupPath, variablePath);
            if (loadRst.IsSuccess)
            {
                DeviceWeight = loadRst.Content;
                runLogBLL.LogInfo("称重PLC配置信息加载成功");
                devicePath = Path.Combine(parentDirectory, @"EConfig_ScanPLC\Device.ini");
                groupPath = Path.Combine(parentDirectory, @"EConfig_ScanPLC\Group.xlsx");
                variablePath = Path.Combine(parentDirectory, @"EConfig_ScanPLC\Variable.xlsx");
                var loadRst2 = DeviceConfigDAL.LoadDeviceInfo(devicePath, groupPath, variablePath);
                if (loadRst2.IsSuccess)
                {
                    DeviceScan = loadRst2.Content;
                    runLogBLL.LogInfo("扫码PLC配置信息加载成功");
                    cts = new CancellationTokenSource();

                    Task.Run(() =>
                    {
                        PLCCommunication(loadRst.Content);
                    }, cts.Token);

                    Task.Run(() =>
                    {
                        ScanPLCCommunication(loadRst2.Content);
                    }, cts.Token);

                }
                else
                {

                    runLogBLL.LogError("加载扫码PLC配置信息失败/r/n" + loadRst2.Message);
                }

            }

            else
            {

                runLogBLL.LogError("加载称重PLC配置文件失败/r/n" + loadRst.Message);
            }
        }

        static string ExtractReturnContent(string logText)
        {
            // 使用正则表达式提取 ns:return 标签中的内容
            var match = Regex.Match(logText, @"<ns:return>(.*?)</ns:return>", RegexOptions.Singleline);

            if (match.Success)
            {
                // 尝试格式化 JSON 内容
                string jsonContent = match.Groups[1].Value;
                string formattedJson = TryFormatJsonWithLineBreaks(jsonContent);

                // 如果格式化成功，则返回格式化内容；否则返回原始 JSON 内容
                return formattedJson ?? logText;
            }
            else
            {
                // 如果没有找到 ns:return 标签，返回整个内容
                return logText;
            }
        }

        static string TryFormatJsonWithLineBreaks(string json)
        {
            try
            {
                // 尝试解析 JSON 字符串为 JObject
                var parsedJson = JObject.Parse(json);
                // 格式化 JSON，每个属性在新行显示
                return parsedJson.ToString(Formatting.Indented);
            }
            catch (JsonReaderException)
            {
                // 如果解析失败，返回 null 表示不进行任何修改
                return null;
            }
        }


        #region MES
        public DelegateCommand MESCommand {  get; set; }

        public void MesTest()
        {
            WeighData data=new WeighData();
            data.EMP_NO = "ADMIN";
            data.TERMINAL_ID = "Check_Po";
            data.SN = "UEPYU24529000002";
            data.WEIGH = "6.351";
            data.IS_PASS = "NG";
            var rst= MesUpdate(data);
            if(rst.IsSuccess)
            {
                runLogBLL.LogInfo($"MES-Rec-[{rst.Message}]");
            }
            else
            {
                runLogBLL.LogWarning($"MES-Rec失败-[{rst.Message}]");
            }
        }
        public OperateResult MesUpdate(WeighData data)
        {
            string soapRequest = $@"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
    <soapenv:Header/>
    <soapenv:Body>
        <tem:UPDATE_WEIGH>
            <tem:data>
                {{
                    ""EMP_NO"": ""{data.EMP_NO}"",
                    ""TERMINAL_ID"": ""{data.TERMINAL_ID}"",
                    ""SN"": ""{data.SN}"",
                    ""WEIGH"": ""{data.WEIGH}"",
                    ""IS_PASS"": ""Y""
                }}
            </tem:data>
        </tem:UPDATE_WEIGH>
    </soapenv:Body>
</soapenv:Envelope>";
            runLogBLL.LogInfo($"MES-Sen-[{soapRequest}]");
            var rst2= SoapService.UpdateMes(soapRequest);
            if(rst2.IsSuccess)
            {
                runLogBLL.LogInfo($"MES-Rec-[{rst2.Message}]");
                if(rst2.Message.Contains("NG")|| rst2.Message.Contains("false"))
                {
                    rst2.Message = $"MES返回NG或false+{rst2.Message}";
                    rst2.IsSuccess = false;
                }

            }

            return rst2;



        }
        #endregion


        #region 数据库
        public DelegateCommand SaveDBConfigCommand {  get; set; }
        public void SaveDBConfig()
        {
            var rst= DBDAL.UpdateConfig(DbCfg);
            if(rst.IsSuccess)
            {
                MessageBox.Show("保存成功");
            }
            else
            {
                MessageBox.Show("保存失败\r\n"+ rst.Message);
            }
        }
        #endregion



        #region 数据
     

        //测试记录
        private ObservableCollection<BXCRecordEntity> recods;
        public ObservableCollection<BXCRecordEntity> Recods
        {
            get { return recods; }
            set
            { 
                recods = value;
                RaisePropertyChanged(); }
        }
        //产量
        private ProductionStatistics statistics;
        public ProductionStatistics Statistics
        {
            get { return statistics; }
            set { statistics = value; }
        }
        //清空生产数据
        public DelegateCommand ProductDataClearCommand {  get; set; }
        private void ProductDataClear()
        {
            if(MessageBox.Show("是否确认清空今日生产数据", "修改",MessageBoxButton.OKCancel)==MessageBoxResult.OK)
            {
                var rst=DBDAL.ClearProductionStatistics();
                if(rst.IsSuccess)
                {
                    Statistics= rst.Content;
                    MessageBox.Show("清除完成");
                    RaisePropertyChanged("Statistics");
                }
                else
                {
                    MessageBox.Show("清除失败，软件停止");
                    runLogBLL.LogError("清除失败，软件停止"+ rst.Message);
                    cts.Cancel();
                }
            }
        }


        //数据库配置文件
        private BXCConfig   dbCfg;
        public BXCConfig DbCfg
        {
            get { return dbCfg; }
            set { dbCfg = value; RaisePropertyChanged(); }
        }


        //到位后生成
        private BXCRecordEntity recordWeight;
        public BXCRecordEntity RecordWeight
        {
            get { return recordWeight; }
            set { recordWeight = value; RaisePropertyChanged(); }
        }

        //放行时传入
        private BXCRecordEntity recordScan;
        public BXCRecordEntity RecordScan
        {
            get { return recordScan; }
            set { recordScan = value; RaisePropertyChanged(); }
        }
        #endregion




        #region PLC
        #region 相机
        private DeviceInfo camDevice;
        public DeviceInfo CamDevice
        {
            get { return camDevice; }
            set { camDevice = value; RaisePropertyChanged(); }
        }

        ModbusTCP modbusTCP_Cam{ get; set; }= new ModbusTCP();


        #region Property相机
        private string camConnectionStatus;
        public string CamConnectionStatus
        {
            get { return camConnectionStatus; }
            set { camConnectionStatus = value; }
        }

        #endregion

        #region Methods相机
        public DelegateCommand ScanCommand { get; set; }
        /// <summary>
        /// PLC通信连接方法
        /// </summary>
        /// <param name="device"></param>
        private void CamScan()
        {
            var rst = CamScan2();
            if(rst.IsSuccess)
            {
                runLogBLL.LogInfo($"相机-读码成功 Code：{rst.Content}");

            }
            else
            {
                runLogBLL.LogWarning($"相机-读码失败 错误：{rst.Message}");
            }

        }


        private OperateResult<string> CamScan2()
        {
            OperateResult<string> operateResult = new OperateResult<string>();
            operateResult.IsSuccess = false;
            string DeviceName = "相机";
            CamDevice.IsConnected = modbusTCP_Cam.Connect(CamDevice.IPAddress, CamDevice.Port);
            //CamDevice.IsConnected = modbusTCP_Cam.Connect(CamDevice.IPAddress,302);
            if (!CamDevice.IsConnected)
            {
                CamConnectionStatus = "已断开";
                operateResult.Message = DeviceName + "连接失败";
                return operateResult;
            }
            byte[] bytes = ByteArrayLib.GetByteArrayFromString("start", Encoding.ASCII);
            byte[] refBytes = new byte[512];
            modbusTCP_Cam.SendAndReceive(bytes, ref refBytes);
            modbusTCP_Cam.DisConnect();

            string Code = StringLib.GetStringFromByteArrayByEncoding(refBytes, Encoding.ASCII);
            Code= Code.Replace("\0", "");
            if(string.IsNullOrEmpty(Code))
            {
                operateResult.Message = "相机-未接收到数据/无法连接";
                return operateResult;
            }
            if(Code.Contains("NoRead"))
            {
                operateResult.Message = "相机-未扫到条码";
                if(DbCfg.CamIP_Top == CamDevice.IPAddress)
                {
                    CamDevice.IPAddress = DbCfg.CamIP_Left;
                    runLogBLL.LogWarning($"相机-未扫到条码-切换左测相机-{CamDevice.IPAddress}");
                    operateResult=CamScan2Second();
                    return operateResult;

                }
                else
                {
                    CamDevice.IPAddress = DbCfg.CamIP_Top;
                    runLogBLL.LogWarning($"相机-未扫到条码-切换顶部相机-{CamDevice.IPAddress}");
                    operateResult=CamScan2Second();
                    return operateResult;
                }

            }
            operateResult.IsSuccess=true;
            operateResult.Content=Code;
            return operateResult;

        }
        private OperateResult<string> CamScan2Second()
        {
            OperateResult<string> operateResult = new OperateResult<string>();
            operateResult.IsSuccess = false;
            string DeviceName = "相机";
            CamDevice.IsConnected = modbusTCP_Cam.Connect(CamDevice.IPAddress, CamDevice.Port);
            if (!CamDevice.IsConnected)
            {
                CamConnectionStatus = "已断开";
                operateResult.Message = DeviceName + "连接失败";
                return operateResult;
            }
            byte[] bytes = ByteArrayLib.GetByteArrayFromString("start", Encoding.ASCII);
            byte[] refBytes = new byte[512];
            modbusTCP_Cam.SendAndReceive(bytes, ref refBytes);
            modbusTCP_Cam.DisConnect();

            string Code = StringLib.GetStringFromByteArrayByEncoding(refBytes, Encoding.ASCII);
            Code = Code.Replace("\0", "");
            if (string.IsNullOrEmpty(Code))
            {
                operateResult.Message = "相机-未接收到数据";
                return operateResult;
            }
            if (Code.Contains("NoRead"))
            {
                operateResult.Message = "相机-未扫到条码";
                return operateResult;
            }
            operateResult.IsSuccess = true;
            operateResult.Content = Code;
            return operateResult;

        }
        #endregion
        #endregion

        #region 称重机
        private DeviceInfo deviceWeight;
        public DeviceInfo DeviceWeight
        {
            get { return deviceWeight; }
            set { deviceWeight = value; RaisePropertyChanged(); }
        }

        ModbusTCP modbusTCP_Weight { get; set; }
        Device.DataConvertLib.DataFormat dataFormat { get; set; } = DataFormat.ABCD;
        #region Property_称重机
        //连接状态
        private string weightConnectionStatus;
        public string WeightConnectionStatus
        {
            get { return weightConnectionStatus; }
            set { weightConnectionStatus = value; RaisePropertyChanged(); }
        }

        //实时重量
        private double realTimeWeight;
        public double RealTimeWeight
        {
            get { return realTimeWeight; }
            set { realTimeWeight = value; RaisePropertyChanged(); }
        }

        //到位信号
        private string positionSignal;
        public string PositionSignal
        {
            get { return positionSignal; }
            set { positionSignal = value; RaisePropertyChanged(); }
        }

        //ReleaseSignal
        private string releaseSignal;
        public string ReleaseSignal
        {
            get { return releaseSignal; }
            set { releaseSignal = value; RaisePropertyChanged(); }
        }

        //IsProductWaiting
        private string isProductWaiting;
        public string IsProductWaiting
        {
            get { return isProductWaiting; }
            set { isProductWaiting = value; RaisePropertyChanged(); }
        }

        //设置放行时间
        private DateTime setReleaseSignalTime;
        public DateTime SetReleaseSignalTime
        {
            get { return setReleaseSignalTime; }
            set { setReleaseSignalTime = value;RaisePropertyChanged(); }
        }

        #endregion

        #region Methods称重机
        //放行方法
        public DelegateCommand WeightReleaseCommand { get; set; }
        public void SetReleaseSignal()
        {

            byte[] bytes = ByteArrayLib.GetByteArrayFromShort(1200);
            bool rst = modbusTCP_Weight.PreSetSingleRegister(10, bytes);
            if (rst)
            {
                runLogBLL.LogInfo("称重机-放行设置完成");
                SetReleaseSignalTime = DateTime.Now; ;
            }
            else
            {
                runLogBLL.LogError("称重机放行设置失败，软件停止");
                cts.Cancel();
            }

        }


        //获取重量
        public void GetWeight()
        {
            double LastWeight = 0;
            double CurWeight = 0;
            byte[] bytes = modbusTCP_Weight.ReadOutputRegisters(0, 1);
            LastWeight = ShortLib.GetShortFromByteArray(bytes, 0)*0.001;
            runLogBLL.LogInfo($"称重机_当前重量【{LastWeight}】");

            int WeightCounts = 10;
            for (int i = 0; i < WeightCounts; i++)
            {
                Thread.Sleep(DbCfg.WeightWaitingTime);
                byte[] bytes2 = modbusTCP_Weight.ReadOutputRegisters(0, 1);
                CurWeight = ShortLib.GetShortFromByteArray(bytes2, 0) * 0.001;
                runLogBLL.LogInfo($"称重机_当前重量【{CurWeight}】");

                if (CurWeight == LastWeight)
                {
                    runLogBLL.LogInfo($"称重机_重量未发生变化，数据有效");
                    break;
                }
                else if (i != (WeightCounts-1))
                {
                    LastWeight = CurWeight;
                }
            }
            RecordWeight = new BXCRecordEntity();

            if (LastWeight != CurWeight)
            {
                runLogBLL.LogWarning($"称重机_到位后数据稳定不下来，NG");
                RecordWeight.Weight = -999;
                RecordWeight.TestResult = "NG";
            }
            else
            {
                RecordWeight.Weight = CurWeight;
            }

        }


        /// <summary>
        /// PLC通信连接方法
        /// </summary>
        /// <param name="device"></param>
        private void PLCCommunication(DeviceInfo device)
        {
            string DeviceName = "称重机PLC";
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
                                    data = modbusTCP_Weight.ReadInputCoils(gp.Start, gp.Length);
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                case "输出线圈":
                                    data = modbusTCP_Weight.ReadOutputCoils(gp.Start, gp.Length);
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
                                    data = modbusTCP_Weight.ReadInputRegisters(gp.Start, gp.Length);
                                    reqLength = gp.Length * 2;
                                    break;
                                case "输出寄存器":
                                    data = modbusTCP_Weight.ReadOutputRegisters(gp.Start, gp.Length);
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
                                    if (variable.VarName == "D0重量数据")
                                    {
                                        double.TryParse(variable.VarValue.ToString(), out double weight);
                                        RealTimeWeight = weight;
                                    }
                                    if (variable.VarName == "D8到位信号")
                                    {

                                        if (PositionSignal != variable.VarValue.ToString())
                                        {
                                            if (PositionSignal == "100")
                                            {
                                                IsProductWaiting = "无";
                                                runLogBLL.LogInfo("称重PLC_产品已运出称重机");
                                            }
                                            else if (variable.VarValue.ToString() == "100")
                                            {
                                                runLogBLL.LogInfo("称重PLC_产品到位");
                                                //获取重量
                                                GetWeight();
                                                if(ScanPLCIsBusy=="1")
                                                {
                                                    if(SetReleaseSignalTime> ScanPLCIsBusyFreeTime)
                                                    {
                                                        runLogBLL.LogWarning($"扫码位空闲，但是设置放行时间[{SetReleaseSignalTime}]" +
                                                            $"\r\n晚于要料时间[{ScanPLCIsBusyFreeTime}]");
                                                        runLogBLL.LogWarning($"代表：该放行信号已触发称重机运出产品，但是未到扫码位，即上位机认为前一个产品未完成，等待");
                                                        runLogBLL.LogWarning("相关人员请确认，如认为异常，则可手动要料，按钮（重新要料）！！！");

                                                    }

                                                }
                                            }
                                            PositionSignal = variable.VarValue.ToString();
                                        }
                                    }

                                    if (variable.VarName == "D10放行信号")
                                    {
                                        if (ReleaseSignal != variable.VarValue.ToString())
                                        {
                                            ReleaseSignal = variable.VarValue.ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                device.IsConnected = false;
                            }
                        }
                    }

                    Thread.Sleep(500);
                }
                else
                {
                    if (device.ReConnectSign)
                    {
                        modbusTCP_Weight.DisConnect();
                        //重连
                        Thread.Sleep(device.ReConnectTime);
                    }

                    modbusTCP_Weight = new ModbusTCP();

                    device.IsConnected = modbusTCP_Weight.Connect(device.IPAddress, device.Port);

                    if (device.ReConnectSign == false)
                    {
                        device.ReConnectSign = true;
                    }
                    if (device.IsConnected)
                    {
                        runLogBLL.LogInfo(DeviceName + "连接成功");
                    }
                    else
                    {
                        runLogBLL.LogWarning(DeviceName + "连接失败");

                    }
                }
                if (WeightConnectionStatus == null)
                {
                    WeightConnectionStatus = "已断开";
                }
                if (!device.IsConnected && WeightConnectionStatus == "连接正常")
                {
                    WeightConnectionStatus = "已断开";
                    runLogBLL.LogWarning($"称重PLC连接中断 ScanConnectionStatus: {ScanConnectionStatus}");

                }
                else if (device.IsConnected && WeightConnectionStatus == "已断开")
                {
                    WeightConnectionStatus = "连接正常";

                }
            }
        }
        #endregion
        #endregion



        #region 扫码PLC
        private DeviceInfo deviceScan;
        public DeviceInfo DeviceScan
        {
            get { return deviceScan; }
            set { deviceScan = value; RaisePropertyChanged(); }
        }

        ModbusTCP modbusTCP_Scan { get; set; }
        DataFormat dataFormatScan { get; set; } = DataFormat.ABCD;

        #region Property_扫码
        //连接状态
        private string scanConnectionStatus;
        public string ScanConnectionStatus
        {
            get { return scanConnectionStatus; }
            set { scanConnectionStatus = value; RaisePropertyChanged(); }
        }



        private string scanPLCIsBusy;
        public string ScanPLCIsBusy
        {
            get { return scanPLCIsBusy; }
            set { scanPLCIsBusy = value; RaisePropertyChanged(); }
        }


        private string scanRequestSingnal;
        public string ScanRequestSingnal
        {
            get { return scanRequestSingnal; }
            set { scanRequestSingnal = value; }
        }

        private DateTime scanGetDataTime;
        public DateTime ScanGetDataTime
        {
            get { return scanGetDataTime; }
            set { scanGetDataTime = value; RaisePropertyChanged(); }
        }


        #endregion

        #region Methods扫码PLC

        public DelegateCommand SetScanSignalCommand { get; set; }

        public DelegateCommand ReShelveCommand {  get; set; }

        //重新设置放行
        public void SetIsReShelve()
        {
            runLogBLL.LogError("危险操作-重新要板");
            if (ScanPLCIsBusy == "1")
            {
                ScanPLCIsBusy = "0";
                runLogBLL.LogInfo("复位要板信号-触发要板事件");
            }
            else
            {
                runLogBLL.LogWarning("非要板状态");

            }
        }

        private DateTime scanPLCIsBusyFreeChangeTime;
        public DateTime ScanPLCIsBusyFreeTime
        {
            get { return scanPLCIsBusyFreeChangeTime; }
            set { scanPLCIsBusyFreeChangeTime = value;RaisePropertyChanged(); }
        }
        private DateTime scanPLCBusyTime;
        public DateTime ScanPLCBusyTime
        {
            get { return scanPLCBusyTime; }
            set { scanPLCBusyTime = value; RaisePropertyChanged(); }
        }


        public void SetScanSignal()
        {
            SetScanSignal(true);
        }
 
        public void SetScanSignal(bool IsSuccess)
        {

            byte[] bytes = null;
            short rst2;
            if (IsSuccess)
            {
                rst2 = 2;
                bytes = ByteArrayLib.GetByteArrayFromShort(2);
            }
            else
            {
                rst2 = 3;
                bytes = ByteArrayLib.GetByteArrayFromShort(3);

            }
            bool rst = modbusTCP_Scan.PreSetSingleRegister(200, bytes);
            if (rst)
            {
                runLogBLL.LogInfo($"扫码PLC-扫码结果【{rst2}】设置完成");
            }
            else
            {
                runLogBLL.LogError($"扫码PLC-扫码结果{rst2}设置失败，软件停止");
                cts.Cancel();
            }

        }
        /// <summary>
        /// PLC通信连接方法
        /// </summary>
        /// <param name="device"></param>
        private async void ScanPLCCommunication(DeviceInfo device)
        {
            string DeviceName = "扫码PLC";
            while (!cts.IsCancellationRequested)
            {
                ScanGetDataTime = DateTime.Now;
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
                                    data = modbusTCP_Scan.ReadInputCoils(gp.Start, gp.Length);
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                case "输出线圈":
                                    data = modbusTCP_Scan.ReadOutputCoils(gp.Start, gp.Length);
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
                                    data = modbusTCP_Scan.ReadInputRegisters(gp.Start, gp.Length);
                                    reqLength = gp.Length * 2;
                                    break;
                                case "输出寄存器":
                                    data = modbusTCP_Scan.ReadOutputRegisters(gp.Start, gp.Length);
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
                                    if (variable.VarName == "D201设备状态")
                                    {
                                        if(ScanPLCIsBusy!= variable.VarValue.ToString())
                                        {
                                            ScanPLCIsBusy = variable.VarValue.ToString();
                                            if(ScanPLCIsBusy=="1")
                                            {
                                                ScanPLCIsBusyFreeTime = DateTime.Now;
                                                //触发要板
                                                Task.Run(() => 
                                                {
                                                    runLogBLL.LogInfo("扫码PLC-要板中");
                                                    while(RecordWeight == null)
                                                    {
                                                        //有产品已到位
                                                    }
                                                    runLogBLL.LogInfo("扫码PLC-要板成功-代表称重位有产品可运过来");
                                                    SetReleaseSignal();
                                                    RecordScan = new BXCRecordEntity()
                                                    {
                                                        Weight = RecordWeight.Weight,
                                                        Time = DateTime.Now,
                                                        MaxWeight = DbCfg.MaxWeight,
                                                        MinWeight = DbCfg.MinWeight,
                                                        Mode = DbCfg.Mode,
                     
                                                    };
                                                    
                                                    RecordWeight = null;
                                                });
                                            }
                                            else
                                            {
                                                runLogBLL.LogInfo("扫码PLC-停止要板");
                                                ScanPLCBusyTime=DateTime.Now;
                                            }
                                        }
                         
                                       
                                    }

                                    if(variable.VarName == "D200请求扫码")
                                    {
                                        if(ScanRequestSingnal !=variable.VarValue.ToString())
                                        {
                                            ScanRequestSingnal = variable.VarValue.ToString();

                                            if (ScanRequestSingnal == "1")
                                            {
                                                runLogBLL.LogInfo("扫码PLC-接收到扫码请求");
                                                if(RecordScan==null)
                                                {
                                                    runLogBLL.LogError($"扫码PLC-未过板就发送扫码请求,软件停止");
                                                    cts.Cancel();
                                                    return;
                                                }
                                                RecordScan.TestResult = "NG";
                                                

                                                var CamRst = CamScan2();
                                                if(!CamRst.IsSuccess)
                                                {
                                                    runLogBLL.LogWarning($"第一次拍照失败-【{CamRst.Message}】");
                                                    runLogBLL.LogInfo($"二次拍照延时{DbCfg.ScanDelay}");
                                                    await Task.Delay(DbCfg.ScanDelay);
                                                    runLogBLL.LogInfo($"延时完成-进行第二次拍照");
                                                    CamRst = CamScan2();
                                                }
                                                if(CamRst.IsSuccess)
                                                {
                                                    RecordScan.SN= CamRst.Content;
                                                    runLogBLL.LogInfo($"扫码成功：{RecordScan.SN}");
                                                    if(RecordScan.Weight> RecordScan.MaxWeight|| RecordScan.Weight < RecordScan.MinWeight)
                                                    {
                                                        RecordScan.ErrorInfo = "重量不合格";
                                                        if(RecordScan.Weight < 0)
                                                        {
                                                            RecordScan.ErrorInfo += " -重量不稳定赋负值";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        RecordScan.TestResult = "PASS";
                                                    }

                                                    if (RecordScan.TestResult == "PASS" && RecordScan.Mode!="本地模式")
                                                    {
                                                        WeighData data2 = new WeighData();
                                                        data2.EMP_NO = DbCfg.EMP_NO;
                                                        data2.TERMINAL_ID = DbCfg.TERMINAL_ID;
                                                        data2.SN = RecordScan.SN;
                                                        data2.WEIGH = Math.Round(RecordScan.Weight,3).ToString();
                                                        data2.IS_PASS = RecordScan.TestResult;

                                                        RecordScan.MESSendMsg= $@"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
    <soapenv:Header/>
    <soapenv:Body>
        <tem:UPDATE_WEIGH>
            <tem:data>
                {{
                    ""EMP_NO"": ""{data2.EMP_NO}"",
                    ""TERMINAL_ID"": ""{data2.TERMINAL_ID}"",
                    ""SN"": ""{data2.SN}"",
                    ""WEIGH"": ""{data2.WEIGH}"",
                    ""IS_PASS"": ""Y""
                }}
            </tem:data>
        </tem:UPDATE_WEIGH>
    </soapenv:Body>
</soapenv:Envelope>";
                                                        var rst = MesUpdate(data2);
                                                        RecordScan.MESRecMsg = ExtractReturnContent(rst.Message);
                                                        runLogBLL.LogInfo($"Mes返回数据提取节点信息\r\n{RecordScan.MESRecMsg}");
                                                        if (rst.IsSuccess)
                                                        {
                                                            RecordScan.ErrorInfo = "";
                                                            runLogBLL.LogInfo($"MES返回不包括NG或false-合格]");
                                                      
                                                        }
                                                        else
                                                        {
                                                            RecordScan.TestResult = "NG";
                                                            RecordScan.ErrorInfo = $"MES-返回失败";
                                                        }
                                                    }
                                                    
                                                }
                                                else
                                                {
                                                    RecordScan.SN = "-1";
                                                    RecordScan.ErrorInfo = $"扫码失败：{CamRst.Message}";
                                                }
                                                //上传数据库并给PLC结果
                                                var dbRst=DBDAL.AddRecord(RecordScan);
                                                if(dbRst.IsSuccess)
                                                {
                                                    if (RecordScan.TestResult == "NG")
                                                    {
                                                        runLogBLL.LogWarning("ErrorInfo:"+RecordScan.ErrorInfo);
                                                        SetScanSignal(false);
                                                    }
                                                    else
                                                    {
                                                        SetScanSignal(true);
                                                    }
                                                }
                                                else
                                                {
                                                    runLogBLL.LogWarning("添加记录失败"+dbRst.Message);
                                                }
                                                RaisePropertyChanged("RecordScan");
                                                Application.Current.Dispatcher.Invoke(() => 
                                                { 
                                                
                                                    if(Recods.Count>DbCfg.RealTimDataCount)
                                                    {
                                                        Recods.Clear();
                                                    }
                                                    Recods.Add(RecordScan);

                                                });
                                                var rst3 = DBDAL.AddOrUpdateTodayRecordWithTestResult(RecordScan.TestResult == "PASS", !(RecordScan.ErrorInfo!=null &&  RecordScan.ErrorInfo.Contains("扫码失败")));
                                                if (rst3.IsSuccess)
                                                {
                                                    Statistics = rst3.Content;
                                                    runLogBLL.LogInfo($"更新生产数据");

                                                    RaisePropertyChanged("Statistics");
                                                }
                                                else
                                                {
                                                    cts.Cancel();
                                                    runLogBLL.LogError($"获取统计数据失败，软件停止");
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                device.IsConnected = false;

                            }
                        }
                    }

                    Thread.Sleep(500);
                }
                else
                {
                    if (device.ReConnectSign)
                    {
                        modbusTCP_Scan.DisConnect();
                        //重连
                        Thread.Sleep(device.ReConnectTime);
                    }

                    modbusTCP_Scan = new ModbusTCP();

                    device.IsConnected = modbusTCP_Scan.Connect(device.IPAddress, device.Port);

                    if (device.ReConnectSign == false)
                    {
                        device.ReConnectSign = true;
                    }
                    if (device.IsConnected)
                    {
                        runLogBLL.LogInfo(DeviceName + "连接成功");
                    }
                    else
                    {
                        runLogBLL.LogWarning(DeviceName + "连接失败");

                    }
                }
                if (ScanConnectionStatus == null)
                {
                    ScanConnectionStatus = "已断开";
                }
                if (!device.IsConnected && ScanConnectionStatus == "连接正常")
                {
                    ScanConnectionStatus = "已断开";
                    runLogBLL.LogWarning($"扫码PLC连接中断 ScanConnectionStatus: {ScanConnectionStatus}");
                }
                else if (device.IsConnected && ScanConnectionStatus == "已断开")
                {
                    ScanConnectionStatus = "连接正常";

                }
            }
        }
        #endregion
        #endregion


        //取消线程源
        private CancellationTokenSource cts;
        /// <summary>
        /// 在主窗体中加载配置信息
        /// </summary>


        #endregion
    }
}
