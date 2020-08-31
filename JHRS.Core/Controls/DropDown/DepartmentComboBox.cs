using JHRS.Core.Controls.Common;
using JHRS.Core.Models;
using System;
using System.Collections.Generic;

namespace JHRS.Core.Controls.DropDown
{
    /// <summary>
    /// 科室下拉框
    /// </summary>
    public class DepartmentComboBox : BaseComboBox
	{
		/// <summary>
		/// 初始化科室數據，可調用接口獲取數據。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnInitialized(object sender, EventArgs e)
		{
			this.DisplayMemberPath = "Name";
			this.SelectedValuePath = "Id";

			//var response = await RestService.For<IDepartmentApi>(AuthHttpClient.Instance).GetAll();
			//if (response.Succeeded)
			//{
			//	Data = response.Data as IList;
			//}

			List<DepartmentOutputDto> list = new List<DepartmentOutputDto>();
			for (int i = 0; i < 20; i++)
			{
				list.Add(new DepartmentOutputDto
				{
					Id = i + 1,
					Name = $"測試科室{i + 1}"
				});
			}
			base.Data = list;
		}
	}

}
