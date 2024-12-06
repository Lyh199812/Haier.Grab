using Base.Client.Entity;
using Device.Helper;
using Device.Models.Config;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Modules.GrabLocate.DAL
{
    public class DeviceConfigDAL
    {

        /// <summary>
        /// 加载配置信息的方法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static OperateResult<DeviceInfo> LoadDeviceInfo(string devicePath)
        {
            OperateResult<DeviceInfo> result = new OperateResult<DeviceInfo>();
            result.IsSuccess = false;
            if (!File.Exists(devicePath))
            {
                result.Message = $"文件不存在 {devicePath}";
                return result;
            }

            try
            {
                result.IsSuccess = true;
                result.Content = new DeviceInfo()
                {
                    IPAddress = IniConfigHelper.ReadIniData("设备参数", "IP地址", "-1", devicePath),
                    Port = Convert.ToInt32(IniConfigHelper.ReadIniData("设备参数", "端口号", "-1", devicePath)),
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"设备信息加载失败: {ex.Message}";
                return result;
            }
        }



        /// <summary>
        /// 加载配置信息的方法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static OperateResult<DeviceInfo> LoadDeviceInfo(string devicePath, string groupPath, string variablePath)
        {
            OperateResult<DeviceInfo> result = new OperateResult<DeviceInfo>();
            result.IsSuccess = false;
            if (!File.Exists(devicePath))
            {
                result.Message = $"文件不存在 {devicePath}";
                return result;
            }
           
            try
            {
                var loadResault = LoadDeviceGroup(groupPath, variablePath);
                if (!loadResault.IsSuccess)
                {
                    result.Message = loadResault.Message;
                    return result;
                }
                List<DeviceGroup> groupList = loadResault.Content;
                result.IsSuccess = true;
                result.Content = new DeviceInfo()
                {
                    IPAddress = IniConfigHelper.ReadIniData("设备参数", "IP地址", "-1", devicePath),
                    Port = Convert.ToInt32(IniConfigHelper.ReadIniData("设备参数", "端口号", "-1", devicePath)),
                    GroupList = groupList
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"设备信息加载失败: {ex.Message}";
                return result;
            }
        }
        /// <summary>
        /// 加载通信组信息
        /// </summary>
        /// <param name="DeviceGrouppath"></param>
        /// <param name="variablepath"></param>
        /// <returns></returns>
        public static OperateResult<List<DeviceGroup>> LoadDeviceGroup(string groupPath, string variablePath)
        {
            var resault = new OperateResult<List<DeviceGroup>>();
            resault.IsSuccess = false;
            if (!File.Exists(groupPath))
            {
                resault.Message = $"通信组文件不存在 {groupPath}";
                return resault;
            }

            if (!File.Exists(variablePath))
            {
                resault.Message = $"通信变量文件不存在 {variablePath}";
                return resault;
            }

            List<DeviceGroup> GpList = null;

            try
            {
                GpList = MiniExcel.Query<DeviceGroup>(groupPath).ToList();
            }
            catch (Exception ex)
            {
                resault.Message = $"通信组加载失败 {ex.Message}";
                return resault;
            }
            List<DeviceVariable> VarList = null;
            try
            {
                VarList = MiniExcel.Query<DeviceVariable>(variablePath).ToList();
            }
            catch (Exception ex)
            {
                resault.Message = $"通信变量加载失败 {ex.Message}";
                return resault;
            }


            if (GpList != null && VarList != null && GpList.Count > 0 && VarList.Count > 0)
            {
                foreach (var DeviceGroup in GpList)
                {
                    DeviceGroup.VarList = VarList.FindAll(c => c.GroupName == DeviceGroup.GroupName).ToList();
                }
                resault.Content = GpList;
                resault.IsSuccess = true;
                return resault;
            }
            else
            {
                resault.Message = $"通信组或通信变量未配置";
                return resault;
            }
        }


        public static OperateResult<List<T>> LoadObject<T>(string Path) where T : class, new()
        {
            var resault = new OperateResult<List<T>>();
            resault.IsSuccess = false;
            if (!File.Exists(Path))
            {
                resault.Message = $"{typeof(T).Name}文件不存在 {Path}";
                return resault;
            }

            List<T> TList = null;

            try
            {
                TList = MiniExcel.Query<T>(Path).ToList();
            }
            catch (Exception ex)
            {
                resault.Message = $"{typeof(T).Name}文件加载失败 {ex.Message}";
                return resault;
            }

            if (TList != null && TList.Count > 0)
            {
                resault.Content = TList;
                resault.IsSuccess = true;
                return resault;
            }
            else
            {
                resault.Message = $"{typeof(T).Name}文件内容未配置";
                return resault;
            };
        }
    }
}
