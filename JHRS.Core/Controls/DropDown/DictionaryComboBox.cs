using JHRS.Core.Controls.Common;
using JHRS.Core.Enums;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Controls.DropDown
{
    /// <summary>
    /// 通用字典業務
    /// </summary>
    public class DictionaryComboBox : BaseComboBox
    {
        /// <summary>
        /// 初始化字典下拉框數據源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnInitialized(object sender, EventArgs e)
        {
            this.SelectedValuePath = "ID";
            this.DisplayMemberPath = "Name";

            if (DictionaryType == 0) throw new ArgumentException("未指定字典类型！");

            //var response = await RestService.For<IBasicDataApi>(AuthClient).GetDicItemListByType((int)DictionaryType);
            //if (response.Succeeded)
            //{
            //    Data = response.Data;
            //}
            if (DictionaryType == DictionaryType.性別)
            {
                List<DictionaryData> list = new List<DictionaryData>();
                list.Add(new DictionaryData { ID = 234, Name = "男" });
                list.Add(new DictionaryData { ID = 235, Name = "女" });
                list.Add(new DictionaryData { ID = 236, Name = "未知" });
                Data = list;
            }
        }

        /// <summary>
        /// 字典类型
        /// </summary>
        public DictionaryType DictionaryType { get; set; }
    }

    public class DictionaryData
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
