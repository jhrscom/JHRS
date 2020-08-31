using JHRS.Core.Apis;
using JHRS.Core.Enums;
using JHRS.Core.Identity;
using JHRS.Core.Models;
using JHRS.Core.ViewModels;
using JHRS.Extensions;
using JHRS.Http;
using JHRS.Shell.Views;
using JHRS.Shell.Views.Login;
using Prism.Commands;
using Prism.Ioc;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JHRS.Shell.ViewModels.Login
{
    /// <summary>
    /// 用戶登錄
    /// </summary>
    public class LoginPageViewModel : BaseViewModel
    {
        /// <summary>
        /// 登录界面构造函数
        /// </summary>
        /// <param name="container"></param>
        public LoginPageViewModel(IContainerExtension container) : base(container)
        {

        }

        private LoginDto _dto = new LoginDto();

        /// <summary>
        /// 当前登录用户信息（界面输入的）
        /// </summary>
        public LoginDto CurrentUser
        {
            get { return _dto; }
            set { SetProperty(ref _dto, value); }
        }

        public DelegateCommand PageLoadCommand => new DelegateCommand(delegate
        {
        });

        public DelegateCommand PreviewMouseDownCommand => new DelegateCommand(delegate
        {
            Alert("忘記密碼了該咋辦?", (d) =>
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "msedge";
                process.StartInfo.Arguments = @"https://jhrs.com";
                process.Start();
            });
        });

        /// <summary>
        /// 登錄命令
        /// </summary>
        public DelegateCommand<PasswordBox> LoginCommand => new DelegateCommand<PasswordBox>(async (b) =>
        {
            if (string.IsNullOrWhiteSpace(CurrentUser.Name))
            {
                Alert("請輸入用戶名！");
                return;
            }
            if (string.IsNullOrWhiteSpace(b.Password))
            {
                AlertPopup("請輸入密碼！");
                return;
            }
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl)) throw new Exception("未配置BaseUrl節點！");

            //開發環境模擬登錄，正式環境調接口
            if (ConfigurationManager.AppSettings["Development"].CastTo<bool>())
            {
                UserContext = new UserContext
                {
                    Token = new UserToken { AccessToken = "這是訪問token", RefreshToken = "這是刷新token" }
                };
                AuthHttpClient.SetHttpClient(baseUrl, UserContext.Token.AccessToken);
            }
            else
            {
                var response = await RestService.For<ILoginApi>(baseUrl).Login(CurrentUser);
                if (!response.Succeeded)
                {
                    Alert(response.Message);
                    return;
                }
                UserContext = response.Data;
                AuthHttpClient.SetHttpClient(baseUrl, response.Data.Token.AccessToken);
            }
            ShellSwitcher.Switch<LoginWindow, MainWindow>();
        });
    }
}
