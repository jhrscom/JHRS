# JHRS
JHRS WPF框架用于演示如何使用wpf和xamarin构建开发框架。该演示框架仅提供一个想法。如果将其应用于实际项目，则需要自己完成更多功能。<br><br>
JHRS WPF framework is used to demonstrate how to build a development framework using wpf and xamarin. This demonstration framework only provides an idea. If it is applied to a real project, you need to complete more functions by yourself.<br><br>
JHRS WPF框架用於演示如何使用wpf和xamarin構建開發框架。該演示框架僅提供一個想法。如果將其應用於實際項目，則需要自己完成更多功能。
# 

## 资源推荐
1. [国外VPS网](https://guowaivps.org) 、[国外便宜Windows VPS推荐](https://guowaivps.org/guowai-pianyi-windows-vps-tuijian/)
2. [国外VPN](https://gbtcs.com)
3. [Code Life](https://znlive.com)

﻿# JHRS version 0.1

 - [简介][1]
 - [相关技术][2]
 - [Description][3]
 - [Related technology][4]
 - [簡介][5]
 - [相關技術][6]

## <a id="title01"/>简介
1. JHRS是在工作中摸索的，并且搭建的一个WPF开发框架，但不是一个完整的框架，只是一个起步；相较于使用原生的WPF技术直接来开发项目，这个框架引入了一些现成的组件来规范、统一、并且提升开发效率，因为很多传统的管理系统，有着类似的功能，因此就可以做一些基础的封装，这样就可以避免团队成员各自实现相同的功能；同时也做了一些管理系统中使用功能比较多的WPF用户控件的封装，在真实项目中，可以自行扩展用户控件。
2. 官方博客(首发)：[JHRS搭建系列文章](https://jhrs.com/2020/37391.html)
3. 博客园：[JHRS搭建系列文章](https://www.cnblogs.com/jessory/p/13590057.html)

## <a id="title02"/>相关技术
1. 技术框架：.NET 5
2. 涉及技术：WPF + AspectInjector + HandyControl + Prism + Refit
3. 调用web api：Refit
4. 数据序列化：使用JSON.NET作为JSON序列化的主要工具
5. IoC组件：Prism自带（Unity）
6. 日志记录：客户端日志记录可以自行实现，但ViewModel基类公开了日志属性，可用于记录日志。

## <a id="title03"/>Description
1. JHRS was groping at work and built a WPF development framework, but it is not a complete framework, but just a start; compared to using native WPF technology to develop projects directly, this framework introduces some ready-made components to standardize , Unify, and improve development efficiency. Because many traditional management systems have similar functions, some basic encapsulation can be done so as to prevent team members from implementing the same functions. At the same time, they have also been used in some management systems. The encapsulation of WPF user controls with more functions, in real projects, user controls can be extended by themselves.
2. Official blog：[My Blog posts](https://jhrs.com/2020/37391.html)
3. JHRS（cnblogs）：[Blog posts](https://www.cnblogs.com/jessory/p/13590057.html)

## <a id="title04"/>Related technology
1. Technical framework: .NET 5
2. Technology involved: WPF + AspectInjector + HandyControl + Prism + Refit
3. Call web api: Refit
4. Data serialization: use JSON.NET as the main tool for JSON serialization
5. IoC components: Prism comes with (Unity)
6. Logging: Client-side logging can be implemented by itself, but the base class of ViewModel exposes log properties, which can be used to record logs.

## <a id="title05"/>簡介
1. JHRS是在工作中摸索的，並且搭建的一個WPF開發框架，但不是一個完整的框架，只是一個起步；相較於使用原生的WPF技術直接來開發項目，這個框架引入了一些現成的組件來規範、統一、並且提升開發效率，因為很多傳統的管理系統，有著類似的功能，因此就可以做一些基礎的封裝，這樣就可以避免團隊成員各自實現相同的功能；同時也做了一些管理系統中使用功能比較多的WPF用戶控件的封裝，在真實項目中，可以自行擴展用戶控件。
2. 部落格：[JHRS搭建系列文章](https://jhrs.com/2020/37391.html)
3. 博客園：[JHRS搭建系列文章](https://www.cnblogs.com/jessory/p/13590057.html)

## <a id="title06"/>相關技術
1. 技術框架：.NET 5
2. 涉及技術：WPF + AspectInjector + HandyControl + Prism + Refit
3. 調用web api：Refit
4. 數據序列化：使用JSON.NET作為JSON序列化的主要工具
5. IoC組件：Prism自帶（Unity）
6. 日誌記錄：客戶端日誌記錄可以自行實現，但ViewModel基類公開了日誌屬性，可用於記錄日誌。

  [1]: #title01
  [2]: #title02
	[3]: #title03
	[4]: #title04
	[5]: #title05
	[6]: #title06
