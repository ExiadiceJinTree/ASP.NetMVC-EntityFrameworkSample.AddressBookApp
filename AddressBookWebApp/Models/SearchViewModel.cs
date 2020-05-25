using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookWebApp.Models
{
    /// <summary>
    /// 検索ワードを受け取って、結果を格納する検索ビューモデルクラス。
    /// </summary>
    public class SearchViewModel
    {
        /// <summary>
        /// 検索ワード。
        /// </summary>
        [Display(Name = "カナ")]
        [RegularExpression(pattern: @"[゠-ヿ 　]+")]  // 正規表現:全角片仮名/半角空白/全角空白 1文字以上 *片仮名の正規表現はUnicode一覧での片仮名の範囲より
        public string Kana { get; set; }

        /// <summary>
        /// 検索結果リスト。
        /// </summary>
        public List<Address> Addresses { get; set; }
    }
}