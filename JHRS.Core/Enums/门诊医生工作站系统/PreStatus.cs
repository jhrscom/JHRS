// -------------------------------------------------------------------------
//  <copyright file="PreStatus.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的枚举类（CloudH）</last-editor>
//  <last-date>2020-08-26 16:24:22</last-date>
// -------------------------------------------------------------------------

using System.ComponentModel;

namespace JHRS.Core.Enums.门诊医生工作站系统
{
    /// <summary>
    /// 处方状态
    /// </summary>
    public enum PreStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        待审核 = 1,

        /// <summary>
        /// 审核不通过
        /// </summary>
        审核不通过 = 2,

        /// <summary>
        /// 待收费
        /// </summary>
        待收费 = 3,

        /// <summary>
        /// 待发药
        /// </summary>
        待发药 = 4,

        /// <summary>
        /// 拒发
        /// </summary>
        拒发 = 5,

        /// <summary>
        /// 已发药
        /// </summary>
        已发药 = 6,

        /// <summary>
        /// 废止
        /// </summary>
        废止 = 7
    }
}