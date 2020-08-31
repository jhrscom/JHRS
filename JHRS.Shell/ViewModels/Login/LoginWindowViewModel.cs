using JHRS.Constants;
using JHRS.Core.ViewModels;
using JHRS.Shell.Views.Login;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Shell.ViewModels.Login
{
    /// <summary>
    /// 登录窗体界面逻辑
    /// </summary>
    public class LoginWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// 登录窗体界面构造函数
        /// </summary>
        /// <param name="container"></param>
        public LoginWindowViewModel(IContainerExtension container) : base(container)
        {

        }

        /// <summary>
        /// LoginWindow窗体加载事件
        /// </summary>
        public DelegateCommand LoadingCommand => new DelegateCommand(() =>
        {
            Navigate(RegionNames.LoginContentRegion, typeof(LoginPage).FullName);
        });
    }
}
