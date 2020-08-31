using JHRS.Core.ViewModels;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JHRS.SystemManage.ViewModels
{
    /// <summary>
    /// 賬號管理業務
    /// </summary>
    public class AccountListViewModel : BaseManagePageViewModel
    {
        /// <summary>
        /// 賬號管理業務構造函數
        /// </summary>
        /// <param name="container"></param>
        public AccountListViewModel(IContainerExtension container) : base(container)
        {
            PagingData.PageSize = 20;
        }


        public override DelegateCommand AddCommand => new DelegateCommand(()=> { });

        public override void PageLoaded(Page page)
        {
           
        }

        protected override Task<object> BindPagingData()
        {
            throw new NotImplementedException();
        }

        protected override Task<object> UpdateDataStatus<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
