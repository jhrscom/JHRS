// -------------------------------------------------------------------------
//  <copyright file="DrugMark.cs" company="江湖人士（jhrs.com）">
//      Copyright (c) 2020-2050 jhrs.com. All rights reserved.
//  </copyright>
//  <site>https://jhrs.com</site>
//  <last-editor>自动化工具生成的枚举类（CloudH）</last-editor>
//  <last-date>2020-08-26 16:24:22</last-date>
// -------------------------------------------------------------------------

using System.ComponentModel;

namespace JHRS.Core.Enums.药库管理系统
{
    /// <summary>
    /// 药品标记
    /// </summary>
    public enum DrugMark
    {
        /// <summary>
        /// 国家基本药物
        /// </summary>
        国家基本药物 = 1,

        /// <summary>
        /// 省基本药物
        /// </summary>
        省基本药物 = 2,

        /// <summary>
        /// 新农合
        /// </summary>
        新农合 = 4,

        /// <summary>
        /// OTC
        /// </summary>
        OTC = 8,

        /// <summary>
        /// 复方
        /// </summary>
        复方 = 16
    }
}