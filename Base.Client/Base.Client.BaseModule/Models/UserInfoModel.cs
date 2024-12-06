using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.Common;
using Base.Client.Common.Attibutes;

namespace Base.Client.BaseModule.Models
{
    public class UserInfoModel : ModelBase
    {
        public bool IsSelected { get; set; }
        public int UserId { get; set; }
        private string _userIcon;
        public string UserIcon
        {
            get { return _userIcon; }
            set { SetProperty(ref _userIcon, value); }
        }

        //public string UserName { get; set; }// 必须唯一   this["UserName"]
        private string _userName;
        [Uniqueness("用户名称已存在")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        public string Password { get; set; }
        public int Age { get; set; }
        public string RealName { get; set; }

        public ObservableCollection<RoleModel> Roles { get; set; } = new ObservableCollection<RoleModel>();

        public ICommand RoleCommand { get; set; }
        public ICommand PwdCommand { get; set; }
    }
}
