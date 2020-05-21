# Overview
A Address book app that is a sample of development using ASP.NET MVC and Entity Framework, etc.

# Technology Used/Focused
- ASP.NET MVC (Not ASP.NET Core MVC)
  - Webサイト全体に適用するのに用いる独自CSSファイル"Site.css"と独自JavaScriptファイル"app.js"を作成し、共通レイアウトで適用・ロードする。
  - ドロップダウンリスト
- Entity Framework
  - Database First
    - ADO.NET Entity Data Model
    - Model's Metadata class  
      EntityFramework DB Firstで自動生成されたモデルクラス(Entity,POCO)に属性を定義するための、メタデータクラスファイルを追加し、属性を設定。
- Bootstrap
  - トップナビバーを表示、狭い画面時のハンバーガーボタンメニュー化に対応

# Implementation Function
