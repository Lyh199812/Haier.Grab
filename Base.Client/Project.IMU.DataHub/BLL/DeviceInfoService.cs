using Base.Client.Entity;
using Project.IMU.DataHub.DAL;
using Project.IMU.DataHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Project.IMU.DataHub.BLL
{
    public class DeviceInfoService
    {
        private readonly DeviceInfoDAL deviceInfoDAL;

        public DeviceInfoService(DeviceInfoDAL _deviceInfoDAL)
        {
            deviceInfoDAL = _deviceInfoDAL;
        }

        public OperateResult<List<TDeviceInfo>> LoadDevices()
        {
            OperateResult<List<TDeviceInfo>> rst = new OperateResult<List<TDeviceInfo>>();
            rst.IsSuccess = false;
            rst.Content = null;
            try
            {
                rst.Content=deviceInfoDAL.Query<TDeviceInfo>(x=>true).ToList();
                rst.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rst.Message = "An error occurred while setting device online." + ex.ToString();
            }
            return rst;
        }


        public OperateResult ResetAllConnectionStates()
        {
            OperateResult result = new OperateResult();
            try
            {
                // 查询所有设备
                var devices = deviceInfoDAL.Query<TDeviceInfo>(x => true).ToList();

                // 遍历设备列表，复位每个设备的连接状态
                foreach (var device in devices)
                {
                    device.ConnectionStartTime = DateTime.MinValue;
                    device.ConnectionEndTime = DateTime.MinValue;
                    device.State = "未连接"; // 设置状态为未连接
                    deviceInfoDAL.Update(device); // 更新设备信息到数据库
                }

                result.IsSuccess = true;
                result.Message = "All device connection states have been reset.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "An error occurred while resetting all connection states.\r\n" + ex.ToString();
            }

            return result;
        }


        // 复位设备连接状态
        public OperateResult ResetConnectionState(TDeviceInfo device)
        {
            try
            {
                // 设置设备的初始状态
                device.State = "未连接"; // 状态设为未连接
                device.ConnectionStartTime = DateTime.MinValue;
                device.ConnectionEndTime   = DateTime.MinValue;
                deviceInfoDAL.Update(device); // 更新到数据库

                return OperateResult.CreateSuccessResult($"Device {device.DeviceName} connection state has been reset.");
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult("An error occurred while resetting connection state.\r\n" + ex.ToString());
            }
        }

        // 设备上线
        public OperateResult DeviceOnline(TDeviceInfo device)
        {
            var result = new OperateResult<TDeviceInfo>() { IsSuccess = false };
            try
            {
                device.ConnectionStartTime = DateTime.Now;
                device.State = "连接中";
                deviceInfoDAL.Update(device); // 更新到数据库
                return OperateResult.CreateSuccessResult($"Device {device.DeviceName} is now online.");

            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult("An error occurred while setting device online.\r\n"+ex.ToString());
            }
        }

        // 设备下线
        public OperateResult DeviceOffline(TDeviceInfo device)
        {


            try
            {
                device.ConnectionStartTime = DateTime.Now;
                device.State = "断开";
                deviceInfoDAL.Update(device); // 更新到数据库

                return OperateResult.CreateSuccessResult($"Device {device.DeviceName} is  offline.");
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult("An error occurred while setting device offline.\r\n" + ex.ToString());
            }
        }
    }
}
