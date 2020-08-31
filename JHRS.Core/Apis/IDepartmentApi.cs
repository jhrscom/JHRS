// -------------------------------------------------------------------------
//  <copyright file="IDepartmentApi.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的客户端接口类，可供Refit调用</last-editor>
//  <last-date>2020-08-24</last-date>
// -------------------------------------------------------------------------

using JHRS.Core.Models;
using JHRS.Data;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Apis
{
	/// <summary>
	/// 科室業務相關接口
	/// </summary>
	[Headers("User-Agent: JHRS-WPF-Client")]
	public interface IDepartmentApi
	{
		/// <summary>
		/// 获取
		/// </summary>
		/// <returns></returns>
		[Post("/api/Department/GetAll")]
		Task<OperationResult<IList<DepartmentOutputDto>>> GetAll();
	}

}
