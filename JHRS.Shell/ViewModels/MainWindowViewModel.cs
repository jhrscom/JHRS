using JHRS.Core.Events;
using JHRS.Core.Identity;
using JHRS.Core.Modules;
using JHRS.Core.ViewModels;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JHRS.Shell.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// 主界面构造函数
        /// </summary>
        /// <param name="container"></param>
        public MainWindowViewModel(IContainerExtension container) : base(container)
        {
            InintMenus();
        }

        /// <summary>
        /// 當前登錄用戶
        /// </summary>
        public UserContext CurrentUser => UserContext;

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        public DelegateCommand CloseWindowCommand => new DelegateCommand(() =>
        {
            Confirm("您确定要关闭窗体并退出吗？", (d) =>
            {
                if (d.Result == ButtonResult.Yes)
                {
                    Application.Current.Shutdown(0);
                }
            });
        });

        /// <summary>
        /// 窗体主菜单集合
        /// </summary>
        public static ObservableCollection<MenuEntity> MainMenuItemsSource { get; set; }

        /// <summary>
        /// 点击菜单选择页面
        /// </summary>
        public DelegateCommand<MenuEntity> SelectedIntoPage => new DelegateCommand<MenuEntity>((m) =>
        {
            var manager = Container.Resolve<PageManager>();
            var pageType = manager.GetPage(m.Name);
            if (pageType == null)
            {
                MessageBox.Show($"未实现或创建名称为【{m.Name}】的Page对象", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var page = Container.Resolve(pageType) as Page;
            EventAggregator.GetEvent<PageEvent>().Publish(new NavigatePage { Menu = m, Page = page });
        });

        /// <summary>
        /// 初始化功能菜單
        /// </summary>
        private void InintMenus()
        {
            MainMenuItemsSource = new ObservableCollection<MenuEntity>();
            MenuEntity entity4 = new MenuEntity { Id = 1, Name = "門診掛號", IsGroup = true, Children = new List<MenuEntity>() };
            entity4.Children.Add(new MenuEntity { Id = 3, Name = "預約掛號" });
            entity4.Children.Add(new MenuEntity { Id = 4, Name = "現場掛號" });
            MainMenuItemsSource.Add(entity4);
            MenuEntity entity3 = new MenuEntity { Id = 2, Name = "掛號管理", IsGroup = true, Children = new List<MenuEntity>() };
            entity3.Children.Add(new MenuEntity { Id = 5, Name = "掛號時段設置" });
            entity3.Children.Add(new MenuEntity { Id = 6, Name = "掛號醫生設置" });
            entity3.Children.Add(new MenuEntity { Id = 7, Name = "掛號診室" });
            entity3.Children.Add(new MenuEntity { Id = 8, Name = "號源管理" });
            MainMenuItemsSource.Add(entity3);
            MenuEntity entity2 = new MenuEntity { Id = 9, Name = "系統管理", IsGroup = true, Children = new List<MenuEntity>() };
            entity2.Children.Add(new MenuEntity { Id = 10, Name = "賬戶管理" });
            entity2.Children.Add(new MenuEntity { Id = 15, Name = "角色管理" });
            entity2.Children.Add(new MenuEntity { Id = 16, Name = "權限管理" });
            entity2.Children.Add(new MenuEntity { Id = 11, Name = "科室管理" });
            entity2.Children.Add(new MenuEntity { Id = 12, Name = "病區設置" });
            entity2.Children.Add(new MenuEntity { Id = 13, Name = "診室管理" });
            entity2.Children.Add(new MenuEntity { Id = 14, Name = "醫生管理" });
            MainMenuItemsSource.Add(entity2);
        }
    }
}
