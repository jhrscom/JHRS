using JHRS.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JHRS.Core.Controls.Common
{
    /// <summary>
    /// 下拉框控件基类
    /// </summary>
    public abstract class BaseComboBox : ComboBox
    {
        /// <summary>
        /// 下拉框数据源
        /// </summary>
        public IList Data
        {
            get { return (IList)GetValue(DataProperty); }
            set
            {
                AddDefault(value);
                SetValue(DataProperty, value);
            }
        }

        /// <summary>
        /// 已登录获取到Token客户端对象
        /// </summary>
        protected HttpClient AuthClient => AuthHttpClient.Instance;

        /// <summary>
        /// 服务器配置
        /// </summary>
        protected string BaseUrl => AuthHttpClient.Instance.BaseAddress!.ToString();

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(IList), typeof(BaseComboBox), new PropertyMetadata(null, (d, e) =>
            {
                BaseComboBox c = (BaseComboBox)d;
                var list = e.NewValue as IList;
                if (list != null)
                    c.AddDefault(list);
                c.Data = list;
                c.ItemsSource = list;
            }));

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseComboBox()
        {
            this.Initialized += OnInitialized;
        }

        /// <summary>
        /// 下拉框初始化事件，子类实现，可以加载各自数据。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnInitialized(object sender, EventArgs e);

        private string DefaultSelectedValue = "-1";
        private string DefaultSelectedText = "—請選擇—";

        /// <summary>
        /// 添加默认项：请选择
        /// </summary>
        /// <param name="data"></param>
        private void AddDefault(IList data)
        {
            if (data == null || data.Count == 0) return;
            var pros = data[0].GetType().GetProperties();
            bool hasSelect = false;
            var s = pros.FirstOrDefault(x => x.Name == SelectedValuePath);
            var d = pros.FirstOrDefault(x => x.Name == DisplayMemberPath);
            if (s == null) throw new Exception("未給ComboBox指定SelectedValuePath屬性，注意：屬性區分大小寫！");
            if (d == null) throw new Exception("未给ComboBox指定DisplayMemberPath屬性，注意：屬性區分大小寫！");
            foreach (var item in data)
            {
                if (s == d && (s.GetValue(item, null) + "") == DefaultSelectedText)
                {
                    hasSelect = true;
                    break;
                }
                else if ((s.GetValue(item, null) + "") == DefaultSelectedValue && (d.GetValue(item, null) + "") == DefaultSelectedText)
                {
                    hasSelect = true;
                    break;
                }
            }
            if (hasSelect == false)
            {
                var subType = data.GetType().GenericTypeArguments[0];
                if (subType.Name.StartsWith("<>f__AnonymousType")) return;
                var m = Activator.CreateInstance(subType);
                if (s != d)
                {
                    s.SetValue(m, Convert.ChangeType(DefaultSelectedValue, s.PropertyType), null);
                    d.SetValue(m, Convert.ChangeType(DefaultSelectedText, d.PropertyType), null);
                }
                else
                {
                    d.SetValue(m, Convert.ChangeType(DefaultSelectedText, d.PropertyType), null);
                }
                data.Insert(0, m);
            }
        }
    }
}