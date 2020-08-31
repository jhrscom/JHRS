// -------------------------------------------------------------------------
//  <copyright file="IReservationApi.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的客户端接口类，可供Refit调用</last-editor>
//  <last-date>2020-08-24</last-date>
// -------------------------------------------------------------------------
using JHRS.Core.Enums;
using JHRS.Core.Models;
using JHRS.Data;
using JHRS.Filter;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.Core.Apis
{
	/// <summary>
	/// 預約掛號接口
	/// </summary>
	[Headers("User-Agent: JHRS-WPF-Client")]
	public interface IReservationApi
	{
		/// <summary>
		/// 新增紐約掛號記錄
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[Post("/api/Reservation/Add")]
		Task<OperationResult> Add(ReservationInputDto dto);

		/// <summary>
		/// 修改預約掛號記錄
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[Post("/api/Reservation/Update")]
		Task<OperationResult> Update(ReservationInputDto dto);

		/// <summary>
		/// 更改預約掛號記錄狀態
		/// </summary>
		/// <param name="id">掛號記錄ID</param>
		/// <param name="status">狀態</param>
		/// <returns></returns>
		[Post("/api/Reservation/ChangeStatus")]
		Task<OperationResult> ChangeStatus(int id, EntityStatus status);

		/// <summary>
		/// 批量刪除掛號記錄
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Post("/api/Reservation/Delete")]
		Task<OperationResult> Delete(int[] id);

		/// <summary>
		/// 預約掛號分頁查詢接口
		/// </summary>
		/// <param name="request">請求規則</param>
		/// <returns></returns>
		[Post("/api/Reservation/GetPageingData")]
		Task<OperationResult<PageData<ReservationOutputDto>>> GetPageingData(PageRequest request);
	}
}
