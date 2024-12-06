using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using Base.Client.Entity;
using HalconDotNet;

namespace MvCamCtrl.NET
{
    public class CameraController:IDisposable
    {
        private MyCamera m_MyCamera = null;
        private bool m_IsInitialized = false;

        public CameraController()
        {
            MyCamera.MV_CC_Initialize_NET();
        }

        /// <summary>
        /// 初始化相机，返回是否成功
        /// </summary>
        public OperateResult InitializeCamera()
        {
            // 初始化设备列表
            MyCamera.MV_CC_DEVICE_INFO_LIST deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);
            if (nRet != MyCamera.MV_OK || deviceList.nDeviceNum == 0)
            {
                return OperateResult.CreateFailResult("No devices found.");
            }

            // 获取第一个设备的信息
            IntPtr deviceInfoPtr = deviceList.pDeviceInfo[0];
            MyCamera.MV_CC_DEVICE_INFO deviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceInfoPtr, typeof(MyCamera.MV_CC_DEVICE_INFO));

            // 创建相机实例
            m_MyCamera = new MyCamera();
            if (m_MyCamera == null)
            {
                return OperateResult.CreateFailResult("Failed to create camera instance.");
            }

            try
            {
                // 创建设备
                nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref deviceInfo);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to create device.");
                }

                // 打开设备
                nRet = m_MyCamera.MV_CC_OpenDevice_NET();
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to open device.");
                }

                // 设置最佳包大小（仅限GigE设备）
                if (deviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    int packetSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                    if (packetSize > 0)
                    {
                        nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("GevSCPSPacketSize", packetSize);
                        if (nRet != MyCamera.MV_OK)
                        {
                            return OperateResult.CreateFailResult("Failed to set packet size.");
                        }
                    }
                }

                // 设置为触发模式
                nRet = m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to set acquisition mode.");
                }

                //设置开启触发模式
                nRet=m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to set triggerMode.");
                }
                //设置开启软触发模式
                nRet=m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to set triggerSource.");
                }
                //设置曝光
                nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", 20000);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to set ExposureTime.");
                }
                m_IsInitialized = true;
                return OperateResult.CreateSuccessResult("Camera initialized successfully.");
            }
            catch
            {
                CleanupCamera();
                return OperateResult.CreateFailResult("Camera initialization failed.");
            }
        }

        /// <summary>
        /// 获取单张图像
        /// </summary>
        public OperateResult CaptureImage(out HObject capturedImage)
        {
            capturedImage = null;

            if (!m_IsInitialized || m_MyCamera == null)
            {
                return OperateResult.CreateFailResult("Camera is not initialized.");
            }

            try
            {
                //int nRet = m_MyCamera.MV_CC_OpenDevice_NET();
                //if (nRet != MyCamera.MV_OK)
                //{
                //    return OperateResult.CreateFailResult("Failed to open device.");
                //}
                // 开始抓图
                int nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to start grabbing.");
                }

                // 发送软触发命令
                nRet = m_MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to execute software trigger.");
                }

                // 获取图像
                MyCamera.MV_FRAME_OUT frame = new MyCamera.MV_FRAME_OUT();
                nRet = m_MyCamera.MV_CC_GetImageBuffer_NET(ref frame, 1000);
                if (nRet != MyCamera.MV_OK)
                {
                    return OperateResult.CreateFailResult("Failed to capture image.");
                }

                // 转换为 HImage
                HImage captured = new HImage();
                captured.GenImage1("byte", (int)frame.stFrameInfo.nWidth, (int)frame.stFrameInfo.nHeight, frame.pBufAddr);
                capturedImage = new HObject(captured);

                // 释放图像缓冲区
                m_MyCamera.MV_CC_FreeImageBuffer_NET(ref frame);

                return OperateResult.CreateSuccessResult("Image captured successfully.");
            }
            catch
            {
                return OperateResult.CreateFailResult("Failed to capture image.");
            }
            finally
            {
                // 停止抓图
                m_MyCamera.MV_CC_StopGrabbing_NET();
                //m_MyCamera.MV_CC_CloseDevice_NET();
                //m_MyCamera.MV_CC_DestroyDevice_NET();
            }
        }

        /// <summary>
        /// 清理相机资源
        /// </summary>
        public void CleanupCamera()
        {
            if (m_MyCamera != null)
            {
                m_MyCamera.MV_CC_StopGrabbing_NET();
                m_MyCamera.MV_CC_CloseDevice_NET();
                m_MyCamera.MV_CC_DestroyDevice_NET();
                m_MyCamera = null;
            }

            m_IsInitialized = false;
        }

        public void Dispose()
        {
            CleanupCamera();
        }
        ~CameraController()
        {
            CleanupCamera();

        }
    }

}
