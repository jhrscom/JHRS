using JHRS.Core.Modules;
using JHRS.Extensions;
using JHRS.Filter;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace JHRS.Core.ViewModels
{
    /// <summary>
    /// 管理功能页面(Page)ViewModel基类
    /// </summary>
    public abstract class BaseManagePageViewModel : BaseViewModel
    {
        /// <summary>
        /// 管理功能页面(Page)ViewModel基类构造函数
        /// </summary>
        /// <param name="container">注入容器</param>
        public BaseManagePageViewModel(IContainerExtension container) : base(container)
        {

        }

        #region 公共属性
        private PagingData pagingData = new PagingData();
        /// <summary>
        /// 分页数据对象
        /// </summary>
        public PagingData PagingData
        {
            get { return pagingData; }
            set { SetProperty(ref pagingData, value); }
        }

        private IEnumerable<object> _pageData;
        /// <summary>
        /// 通用分页表格数据，如果子页面有多个要分页的，子viewmodel里面自己定义
        /// </summary>
        public IEnumerable<object> PageData
        {
            get { return _pageData; }
            set { SetProperty(ref _pageData, value); }
        }

        private IEnumerable<object> _selectedList;

        /// <summary>
        ///  启用复选框功能后，所选中的数据集合
        /// </summary>
        public IEnumerable<object> SelectedList
        {
            get { return _selectedList; }
            set { SetProperty(ref _selectedList, value); }
        }
        #endregion

        /// <summary>
        /// 处理窗体或Page加载事件
        /// </summary>
        public virtual DelegateCommand<Page> LoadedCommand => new DelegateCommand<Page>((p) =>
        {
            if (p == null) throw new Exception("未获取到当前Page对象，请Page页面上实现Loaded命令！");
            var manager = Container.Resolve<UserControlManager>();
            var controls = manager.GetControls(p.GetType().FullName);
            controls.ForEach(x =>
            {
                this.Navigate(x.RegionName, x.ControlType.FullName);
            });
            PageLoaded(p);
        });

        /// <summary>
        /// 查询命令
        /// </summary>
        public DelegateCommand QueryCommand => new DelegateCommand(async () => { await BindPagingData(); });

        /// <summary>
        /// 分页查询事件
        /// </summary>
        public DelegateCommand ChangePageIndexCommand => new DelegateCommand(async () => { await BindPagingData(); });

        /// <summary>
        /// 新增方法
        /// </summary>
        public abstract DelegateCommand AddCommand { get; }

        /// <summary>
        /// 当前页面加载事件
        /// </summary>
        /// <param name="page"></param>
        public abstract void PageLoaded(Page page);

        /// <summary>
        /// 默认绑定分页数据
        /// </summary>
        /// <returns></returns>
        protected abstract Task<object> BindPagingData();

        /// <summary>
        /// 通用更改状态方法
        /// </summary>
        /// <typeparam name="TEntity">待更改实体</typeparam>
        /// <param name="entity">当前对象</param>
        protected abstract Task<object> UpdateDataStatus<TEntity>(TEntity entity) where TEntity : class, new();

        private bool CanHandle = true;
        /// <summary>
        /// 通用更改状态方法
        /// </summary>
        /// <typeparam name="T">复选框类型</typeparam>
        /// <typeparam name="TEntity">实体类型，表示当前分页表格绑定的对象类型</typeparam>
        /// <param name="combox">状态相关联的下拉框</param>
        /// <param name="statusName">状态名称，用于提示</param>
        /// <param name="fieldName">状态字段名称</param>
        protected virtual void ChangeStatus<T, TEntity>(T combox, string statusName, Expression<Func<TEntity, string>> fieldName)
            where T : ComboBox
            where TEntity : class, new()
        {
            if (CanHandle)
            {
                var t = combox.DataContext as TEntity;
                if (t == null) return;
                var selectedValue = t.GetType().GetProperty(fieldName.Body.ToString().Replace("\"", "")).GetValue(t);
                if (combox.SelectedValue.CastTo<int>() == -1)
                {
                    AlertPopup($"请选择有效的{statusName}状态！");
                    CanHandle = false;
                    combox.SelectedValue = selectedValue;
                    return;
                }

                Confirm($"请确认是否修改{statusName}状态？", (d) =>
                {
                    if (d.Result == ButtonResult.Yes)
                    {
                        BindingOperations.GetBindingExpression(combox, ComboBox.SelectedValueProperty).UpdateSource();
                        UpdateDataStatus(t);
                    }
                    else
                    {
                        CanHandle = false;
                        combox.SelectedValue = selectedValue;
                    }
                });
            }
            else
            {
                CanHandle = true;
            }
        }


        /// <summary>
        /// 跨行,跨列表格通用更改状态方法
        /// </summary>
        /// <typeparam name="T">复选框类型</typeparam>
        /// <typeparam name="TEntity">实体类型，表示当前分页表格绑定的对象类型</typeparam>
        /// <param name="combox">状态相关联的下拉框</param>
        /// <param name="statusName">状态名称，用于提示</param>
        /// <param name="fieldName">状态字段名称</param>
        protected virtual void ChangeStatus<T, TEntity>(T combox, object model, string statusName, Expression<Func<TEntity, string>> fieldName)
            where T : ComboBox
        {
            if (CanHandle)
            {
                if (model == null) return;
                var selectedValue = model.GetType().GetProperty(fieldName.Body.ToString().Replace("\"", "")).GetValue(model);
                if (combox.SelectedValue.CastTo<int>() == -1)
                {
                    AlertPopup($"请选择有效的{statusName}状态！");
                    CanHandle = false;
                    combox.SelectedValue = selectedValue;
                    return;
                }

                Confirm($"请确认是否修改{statusName}状态？", (d) =>
                {
                    if (d.Result == ButtonResult.Yes)
                    {
                        BindingOperations.GetBindingExpression(combox, ComboBox.SelectedValueProperty).UpdateSource();
                        UpdateDataStatus(model);
                    }
                    else
                    {
                        CanHandle = false;
                        combox.SelectedValue = selectedValue;
                    }
                });
            }
            else
            {
                CanHandle = true;
            }
        }

        /// <summary>
        /// 获取查询规则
        /// </summary>
        /// <param name="query">当前查询对象</param>
        /// <returns></returns>
        protected override PageRequest GetQueryRules(object query)
        {
            var pageIndex = 0 >= PagingData.PageIndex ? 1 : PagingData.PageIndex;
            var pageSize = 0 >= PagingData.PageSize ? 10 : PagingData.PageSize;

            var request = base.GetQueryRules(query);

            request.PageCondition = new PageCondition
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SortConditions = new SortCondition[] { new SortCondition { ListSortDirection = ListSortDirection.Descending, SortField = "Id" } }
            };
            return request;
        }
    }

}
