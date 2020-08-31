using JHRS.Core.Controls.Mask;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JHRS.Core.Extensions
{
    /// <summary>
    /// 蒙層效果擴展
    /// </summary>
    public static class MaskExtensions
    {
        private static LoadingWait w = new LoadingWait();
        /// <summary>
        /// 顯示蒙層
        /// </summary>
        public static void Show()
        {
            Window win = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            Grid container = win.GetChildObject<Grid>("maskContainer");
            if (container == null) throw new Exception("界面上未找到名稱爲maskContainer的Grid容器控件！");
            container.Children.Add(w);
        }

        /// <summary>
        /// 關閉蒙層
        /// </summary>
        public static void Close()
        {
            Window win = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            Grid container = win.GetChildObject<Grid>("maskContainer");
            if (container == null) throw new Exception("界面上未找到名稱爲maskContainer的Grid容器控件！");
            container.Children.Remove(w);
        }
    }
}
