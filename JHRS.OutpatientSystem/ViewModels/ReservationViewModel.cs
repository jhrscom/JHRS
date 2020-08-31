using JHRS.Core.Apis;
using JHRS.Core.Controls.DropDown;
using JHRS.Core.Enums;
using JHRS.Core.Models;
using JHRS.Core.ViewModels;
using JHRS.OutpatientSystem.Models;
using JHRS.OutpatientSystem.Views;
using KWT.Core.Aop;
using Prism.Commands;
using Prism.Ioc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JHRS.OutpatientSystem.ViewModels
{
    /// <summary>
    /// 預約掛號業務處理
    /// </summary>
    public class ReservationViewModel : BaseManagePageViewModel
    {
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="container"></param>
        public ReservationViewModel(IContainerExtension container) : base(container)
        {
            PagingData.PageSize = 10;
        }

        private ReservationCondition query = new ReservationCondition();

        /// <summary>
        /// 查詢條件
        /// </summary>
        public ReservationCondition Query
        {
            get { return query; }
            set { SetProperty(ref query, value); }
        }

        /// <summary>
        /// 處理新增事件
        /// </summary>
        public override DelegateCommand AddCommand => new DelegateCommand(() =>
        {
            this.ShowDialog(typeof(AddOrEditReservation).FullName, IconEnum.新增頁面圖標, "新增預約掛號記錄", callback: async (d) =>
            {
                if (d.Parameters.GetValue<bool>("success"))
                    await BindPagingData();
            });
        });

        /// <summary>
        /// 編輯事件
        /// </summary>
        public DelegateCommand<object> EditCommand => new DelegateCommand<object>((item) =>
        {
            this.ShowDialog(typeof(AddOrEditReservation).FullName, IconEnum.編輯頁面圖標, "修改預約掛號記錄", args: item, callback: async (d) =>
              {
                  if (d.Parameters.GetValue<bool>("success"))
                      await BindPagingData();
              });
        });

        /// <summary>
        /// 查看詳情
        /// </summary>
        public DelegateCommand<object> ViewDetailsCommand => new DelegateCommand<object>((item) =>
        {
            this.ShowDialog(typeof(ReservationDetails).FullName, IconEnum.詳情頁面圖標, "掛號記錄詳細信息", args: item, disableArea: true);
        });

        /// <summary>
        /// 頁面加載事件
        /// </summary>
        /// <param name="page"></param>
        public async override void PageLoaded(Page page)
        {
            await BindPagingData();
        }

        /// <summary>
        /// 綁定分頁數據
        /// </summary>
        [WaitComplete]
        protected async override Task<object> BindPagingData()
        {
            if (!IsDevelopment)
            {
                var request = this.GetQueryRules(Query);
                var response = await RestService.For<IReservationApi>(AuthClient).GetPageingData(request);
                if (response.Succeeded)
                {
                    PageData = response.Data.Rows;
                    this.PagingData.Total = response.Data.Total;
                }
                return response;
            }
            else
            {
                int counter = 20;
                var list = new List<ReservationOutputDto>();
                for (int i = 0; i < counter; i++)
                {
                    list.Add(new ReservationOutputDto
                    {
                        BusinessNumber = DateTime.Now.ToString($"GH-yyyyMMdd{i + 1}"),
                        DepartmentID = i + 1,
                        DepartmentName = $"測試科室{i}",
                        DoctorName = $"趙不治{i}",
                        Expire = i % 2 == 0 ? "上午有效" : "下午有效",
                        Id = i + 1,
                        Index = $"第{i + 1}號",
                        Name = $"柳{i}刀",
                        ReservationTime = DateTime.Now.AddSeconds(i),
                        Status = i % 5 == 0 ? EntityStatus.停用 : EntityStatus.正常
                    });
                }
                PageData = list;
                this.PagingData.Total = counter;
                return true;
            }
        }

        /// <summary>
        /// 更改狀態
        /// </summary>
        public DelegateCommand<StatusComboBox> SelectionChangedCommand => new DelegateCommand<StatusComboBox>((c) =>
        {
            this.ChangeStatus<StatusComboBox, ReservationOutputDto>(c, "預約掛號記錄", p => nameof(p.Status));
        });

        /// <summary>
        /// 通用更改狀態方法
        /// </summary>
        /// <typeparam name="TEntity">待更改狀態實體</typeparam>
        /// <param name="entity">當前對象</param>
        [WaitComplete]
        protected override async Task<object> UpdateDataStatus<TEntity>(TEntity entity)
        {
            if (!IsDevelopment)
            {
                var model = entity as ReservationOutputDto;
                var response = await RestService.For<IReservationApi>(AuthClient).ChangeStatus(model.Id, model.Status);
                AlertPopup(response.Message, response.Succeeded ? MessageType.Success : MessageType.Error, async (d) =>
                {
                    if (response.Succeeded) await BindPagingData();
                });
                return response.Succeeded;
            }
            return null;
        }
    }
}
