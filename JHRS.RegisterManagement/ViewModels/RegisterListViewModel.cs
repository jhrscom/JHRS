using JHRS.Core.ViewModels;
using JHRS.Data;
using KWT.Core.Aop;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JHRS.RegisterManagement.ViewModels
{
    /// <summary>
    /// 掛號時段業務
    /// </summary>
    public class RegisterListViewModel : BaseManagePageViewModel
    {
        /// <summary>
        /// 掛號時段業務構造函數
        /// </summary>
        /// <param name="container"></param>
        public RegisterListViewModel(IContainerExtension container) : base(container)
        {
            PagingData.PageSize = 20;
        }


        /// <summary>
        /// 處理新增事件
        /// </summary>
        public override DelegateCommand AddCommand => new DelegateCommand(() =>
        {
            //this.ShowDialog(typeof(AddOrEditReservation).FullName, IconEnum.新增頁面圖標, "新增預約掛號記錄", callback: async (d) =>
            //{
            //    if (d.Parameters.GetValue<bool>("success"))
            //        await BindPagingData();
            //});
        });

        /// <summary>
        /// 頁面加載事件
        /// </summary>
        /// <param name="page"></param>
        public async override void PageLoaded(Page page)
        {
            await BindPagingData();
        }


        //private ObservableCollection<Account> listGrid = new ObservableCollection<Account>();
        ///// <summary>
        ///// 表格數據源
        ///// </summary>
        //public ObservableCollection<Account> ListGrid
        //{
        //    get { return listGrid; }
        //    set { SetProperty(ref listGrid, value); }
        //}

        /// <summary>
        /// 綁定分頁數據
        /// </summary>
        [WaitComplete]
        protected async override Task<object> BindPagingData()
        {
            List<Account> list = new List<Account>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(new Account
                {
                    Name = "趙佳仁" + i,
                    RegTime = DateTime.Now.AddDays(i),
                    RoleName = "管理員" + i,
                    Title = "無職" + i,
                    UserID = 100 + i
                });
            }
            PageData = list;
            await Task.Delay(200);
            return true;
        }

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
                await Task.Delay(300);
                //var model = entity as ReservationOutputDto;
                //var response = await RestService.For<IReservationApi>(AuthClient).ChangeStatus(model.Id, (EntityStatus)model.Status);
                //AlertPopup(response.Message, response.Succeeded ? MessageType.Success : MessageType.Error, async (d) =>
                //{
                //    if (response.Succeeded) await BindPagingData();
                //});
                //return response.Succeeded;
            }
            return null;
        }
    }

    public class Account
    {
        [BindDescription("用戶ID")]
        public int UserID { get; set; }
        [BindDescription("用戶名")]
        public string Name { get; set; }
        [BindDescription("註冊時間")]
        public DateTime RegTime { get; set; }
        [BindDescription("角色名穩")]
        public string RoleName { get; set; }
        [BindDescription("職級")]
        public string Title { get; set; }
    }
}
