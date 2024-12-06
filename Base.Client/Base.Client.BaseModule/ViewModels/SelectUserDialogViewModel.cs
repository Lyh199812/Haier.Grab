using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Base.Client.BaseModule.Models;
using Base.Client.IBLL;

namespace Base.Client.BaseModule.ViewModels
{
    public class SelectUserDialogViewModel : IDialogAware
    {
        public string Title => "选择用户";

        public event Action<IDialogResult> RequestClose2;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        List<int> _userIds;
        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 接收参数    已选中的用户ID
            _userIds = parameters.GetValue<List<int>>("ids");
        }

        // 当前作为绑定的列表 
        public ObservableCollection<UserInfoModel> Users { get; set; } = new ObservableCollection<UserInfoModel>();

        IUserBLL _userBLL;
        public SelectUserDialogViewModel(IUserBLL userBLL)
        {
            _userBLL = userBLL;

            this.InitUsers();
        }

        private async void InitUsers()
        {
            var users = await _userBLL.GetAll("");// 添加了过滤条件   获取所有的用户信息  
            users.Where(u => !_userIds.Contains(u.userId)).ToList().
                ForEach(u => Users.Add(new UserInfoModel
                {
                    UserId = u.userId,
                    UserName = u.userName,
                    RealName = u.realName
                }));
        }

        public ICommand ConfirmCommand
        {
            get => new DelegateCommand(() =>
            {
                // 返回选中用户
                DialogParameters param = new DialogParameters();
                param.Add("users", Users.Where(u => u.IsSelected).ToList());

            
            });
        }
        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }

        public DialogCloseListener RequestClose { get; }

    }
}
