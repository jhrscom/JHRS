using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JHRS.Core.Apis;
using JHRS.Core.Models;
using JHRS.Core.ViewModels;
using KWT.Core.Aop;
using Prism.Ioc;
using Refit;

namespace JHRS.OutpatientSystem.ViewModels
{
    /// <summary>
    /// 新增預約掛號,編輯預約掛號功能頁面viewmodel,用於處理業務邏輯
    /// 調用web api保存數據
    /// </summary>
    public class AddOrEditReservationViewModel : BaseDialogPageViewModel
    {
        /// <summary>
        /// 新增預約掛號業務處理構造函數
        /// </summary>
        /// <param name="container"></param>
        public AddOrEditReservationViewModel(IContainerExtension container) : base(container)
        {
            InintData();
        }

        /// <summary>
        /// 編輯模式下初始化界面數據
        /// </summary>
        private void InintData()
        {
            ReservationOutputDto current = this.GetContext<ReservationOutputDto>();
            //不爲null表示處於編輯模式
            if (current != null)
            {
                Dto = new ReservationInputDto
                {
                    DepartmentID = current.DepartmentID,
                    DepartmentName = current.DepartmentName,
                    DoctorName = current.DoctorName,
                    Gender = current.Gender,
                    Index = current.Index,
                    BusinessNumber = current.BusinessNumber,
                    Name = current.Name,
                    ReservationTime = current.ReservationTime
                };
            }
        }

        private ReservationInputDto _dto = new ReservationInputDto();

        /// <summary>
        /// 界面上輸入的信息
        /// </summary>
        public ReservationInputDto Dto
        {
            get { return _dto; }
            set { SetProperty(ref _dto, value); }
        }

        /// <summary>
        /// 新增,編輯保存方法,從viewmodel獲取數據保存即可.
        /// </summary>
        /// <returns></returns>
        [WaitComplete]
        protected async override Task SaveCommand()
        {
            var current = this.GetContext<ReservationOutputDto>();
            if (current == null)
            {
                if (!IsDevelopment)
                {
                    var response = await RestService.For<IReservationApi>(AuthClient).Add(Dto);
                    AlertPopup(response.Message, response.Succeeded ? MessageType.Success : MessageType.Error, (d) =>
                    {
                        if (response.Succeeded) 
                            this.CloseDialog(returnValue:"已經添加成功啦，這裏可以是任何參數和對象喲，父窗體可以接收到此回傳參數。");
                    });
                }
            }
            else
            {
                if (!IsDevelopment)
                {
                    var response = await RestService.For<IReservationApi>(AuthClient).Update(Dto);
                    AlertPopup(response.Message, response.Succeeded ? MessageType.Success : MessageType.Error, (d) =>
                    {
                        if (response.Succeeded)
                            this.CloseDialog(returnValue: "已經修改成功啦，這裏可以是任何參數和對象喲，父窗體可以接收到此回傳參數。");
                    });
                }
            }
        }
    }
}
