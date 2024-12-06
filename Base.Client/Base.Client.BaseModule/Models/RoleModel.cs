using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.Client.BaseModule.Models
{
    public class RoleModel : BindableBase
    {
        public int RoleId { get; set; }// 编辑用户   菜单
        public string RoleName { get; set; }
        //public int State { get; set; }

        private bool _isSelected;
        // 添加用户或菜单     根据当前选中的权限组
        public bool IsSelected
        {
            get => _isSelected;
            set { SetProperty<bool>(ref _isSelected, value); }
        }


        public ICommand DeleteCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
    }
}
