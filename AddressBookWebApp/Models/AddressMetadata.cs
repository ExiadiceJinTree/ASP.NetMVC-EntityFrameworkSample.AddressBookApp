using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBookWebApp.Models
{
    /// <summary>
    /// Addressモデルクラスに対する追加定義をするためのパーシャルクラス。
    /// </summary>
    [MetadataType(typeof(AddressMetadata))]  // 本モデルクラスに関連付けるメタデータクラスを指定
    public partial class Address
    {

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