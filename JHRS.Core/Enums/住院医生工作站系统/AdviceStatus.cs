// -------------------------------------------------------------------------
//  <copyright file="AdviceStatus.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的枚举类（CloudH）</last-editor>
//  <last-date>2020-08-26 16:24:22</last-date>
// -------------------------------------------------------------------------

using System.ComponentModel;

namespace JHRS.Core.Enums.住院医生工作站系统
{
    /// <summary>
    /// 医嘱状态
    /// </summary>
    public enum AdviceStatus
    {
        /// <summary>
        /// 新开
        /// </summary>
        新开 = 1,

        /// <summary>
        /// 待发送
        /// </summary>
        待发送 = 2,

        /// <summary>
        /// 待校对
        /// </summary>
        待校对 = 3,

        /// <summary>
        /// 已校对
        /// </summary>
        已校对 = 4,

        /// <summary>
        /// 停用待确认
        /// </summary>
        停用待确认 = 5,

        /// <summary>
        /// 停用已确认
        /// </summary>
        停用已确认 = 6,

        /// <summary>
        /// 退嘱待确认
        /// </summary>
        退嘱待确认 = 7,

        /// <summary>
        /// 退嘱已确认
        /// </summary>
        退嘱已确认 = 8,

        /// <summary>
        /// 作废
        /// </summary>
        作废 = 9
    }
}