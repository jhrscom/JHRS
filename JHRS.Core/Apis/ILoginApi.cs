// -------------------------------------------------------------------------
//  <copyright file="ILoginApi.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的客户端接口类，可供Refit调用</last-editor>
//  <last-date>2020-08-24</last-date>
// -------------------------------------------------------------------------
using JHRS.Core.Identity;
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
	/// 登錄接口
	/// </summary>
	[Headers("User-Agent: JHRS-WPF-Client")]
	public interface ILoginApi
	{
		/// <summary>
		/// 用戶登錄
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[Post("/api/Login/Login")]
		Task<OperationResult<UserContext>> Login(LoginDto dto);

		/// <summary>
		/// 退出
		/// </summary>
		/// <returns></returns>
		[Post("/api/Login/Logout")]
		Task<OperationResult> Logout();
	}
}
