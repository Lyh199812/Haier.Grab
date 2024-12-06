using Base.Client.Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Client.Common;
using System.Reflection;
using System.Windows;

namespace Base.Client.SystemModule.ViewModels
{
    public class MainHeaderViewModel : BindableBase
    {
        private string _currentUserName;
        public string CurrentUserName
        {
            get { return _currentUserName; }
            set { SetProperty(ref _currentUserName, value); }
        }

        private string _userAvatar;
        public string UserAvatar
        {
            get { return _userAvatar; }
            set { SetProperty(ref _userAvatar, value); }
        }

        private string systemVesion;
        public string SystemVesion
        {
            get { return systemVesion; }
            set { systemVesion = value;RaisePropertyChanged(); }
        }

        public MainHeaderViewModel(GlobalValue globalValue)
        {
            if(globalValue.UserInfo==null)
            {
                globalValue.UserInfo = new Entity.UserEntity() { realName = "本地模式" };

            }
            CurrentUserName = globalValue.UserInfo.realName;
            //http://localhost:22643/api/file/img/1001.png
            // 如果需要Token的话，这里需要下载图片到本地，然后再引用
            UserAvatar = "http://localhost:22643/api/" + globalValue.UserInfo.userIcon;
            SystemVesion = GetAppVersion();
        }

        private string GetAppVersion()
        {
            // 获取当前执行的程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取程序集的版本号
            return assembly.GetName().Version.ToString();


        }
    }
}
