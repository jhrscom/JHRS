using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JHRS.Shell
{
    /// <summary>
    /// 窗体切换
    /// </summary>
    public static class ShellSwitcher
    {
        /// <summary>
        /// 窗体切换，显示一个，关闭一个
        /// </summary>
        /// <typeparam name="TClosed">待关闭窗体对象</typeparam>
        /// <typeparam name="TShow">待显示窗体对象</typeparam>
        public static void Switch<TClosed, TShow>() where TClosed : Window where TShow : Window, new()
        {
            Show<TShow>();
            Closed<TClosed>();
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <typeparam name="T">窗体对象</typeparam>
        /// <param name="window">待显示窗体</param>
        public static void Show<T>(T window = null) where T : Window, new()
        {
            var shell = Application.Current.MainWindow = window ?? new T();
            shell?.Show();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <typeparam name="T">要关闭的窗体</typeparam>
        public static void Closed<T>() where T : Window
        {
            var shell = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is T);
            shell?.Close();
        }
    }
}
