using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookWebApp.Models
{
    /// <summary>
    /// 都道府県の列挙体
    /// </summary>
    public enum Prefectures
    {
        北海道,
        青森県,
        岩手県,
        宮城県,
        秋田県,
        山形県,
        福島県,
        東京都,
        神奈川県,
        埼玉県,
        千葉県,
        茨城県,
        栃木県,
        群馬県,
        山梨県,
        新潟県,
        長野県,
        富山県,
        石川県,
        福井県,
        愛知県,
        岐阜県,
        静岡県,
        三重県,
        大阪府,
        兵庫県,
        京都府,
        滋賀県,
        奈良県,
        和歌山県,
        鳥取県,
        島根県,
        岡山県,
        広島県,
        山口県,
        徳島県,
        香川県,
        愛媛県,
        高知県,
        福岡県,
        佐賀県,
        長崎県,
        熊本県,
        大分県,
        宮崎県,
        鹿児島県,
        沖縄県,
    }

    /// <summary>
    /// Addressモデルクラスに対する追加定義をするためのパーシャルクラス。
    /// </summary>
    [MetadataType(typeof(AddressMetadata))]  // 本モデルクラスに関連付けるメタデータクラスを指定
    public partial class Address
    {
        /// <summary>
        /// 登録/編集画面で、選択された都道府県を一時的に保持するためのプロパティ。
        /// * Nullable型にすることで初期値がnullになるため、新規登録ビューでのドロップダウンリスト選択値が空項目にできる。
        /// </summary>
        [DisplayName("都道府県")]
        [Required]
        public Prefectures? PrefectureItem { get; set; }
    }

    /// <summary>
    /// Addressモデルクラスに関連付けるメタデータクラス。
    /// * モデルクラス(Entity,POCO)側にMetadataType属性でメタデータクラスを指定すると、
    ///   両クラスで名前が一致するプロパティについて、メタデータクラスで定義した属性をモデルクラスのプロパティに設定できる。
    /// * EntityFramework DB Firstで自動生成されたモデルクラス(Entity,POCO)に属性を定義するには、メタデータクラスを使用する必要がある。
    /// * メタデータクラス名: 何でもいいが、分かりやすくするために、対象モデルクラス名 + Metadata という命名が慣習である。
    /// </summary>
    public class AddressMetadata
    {
        [DisplayName("氏名")]
        [Required]
        public string Name { get; set; }

        [DisplayName("カナ")]
        [Required]
        public string Kana { get; set; }

        [DisplayName("郵便番号")]
        public string ZipCode { get; set; }

        [DisplayName("都道府県")]
        public string Prefecture { get; set; }

        [DisplayName("住所")]
        public string StreetAddress { get; set; }

        [DisplayName("電話番号")]
        public string Telephone { get; set; }

        [DisplayName("メールアドレス")]
        public string Mail { get; set; }

        [DisplayName("グループ")]
        public Nullable<int> Group_Id { get; set; }
    }
}