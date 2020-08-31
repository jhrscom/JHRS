// -------------------------------------------------------------------------
//  <copyright file="InpatientStatus.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的枚举类（CloudH）</last-editor>
//  <last-date>2020-08-26 16:24:22</last-date>
// -------------------------------------------------------------------------

using System.ComponentModel;

namespace JHRS.Core.Enums.住院护士站系统
{
    /// <summary>
    /// 住院病人状态
    /// </summary>
    public enum InpatientStatus
    {
        /// <summary>
        /// 新入
        /// </summary>
        新入 = 0,

        /// <summary>
        /// 转入
        /// </summary>
        转入 = 1,

        /// <summary>
        /// 转出
        /// </summary>
        转出 = 2,

        /// <summary>
        /// 出科
        /// </summary>
        出科 = 3,

        /// <summary>
        /// 出院
        /// </summary>
        出院 = 4,

        /// <summary>
        /// 结算出院
        /// </summary>
        结算出院 = 5
    }
}