using Prism.Services.Dialogs;
using System.Windows;

namespace JHRS.Shell.Views.Dialogs
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 弹窗处理结果
        /// </summary>
        public IDialogResult Result { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Width = Owner.Width;
            Height = Owner.Height;
            Top = Owner.Top;
            Left = Owner.Left;
        }
    }
}
