using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvImport
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = args[0];

            List<Address> addressesToImport = ReadCsv(csvFilePath);

            ImportAddressesToDb(addressesToImport);
        }

        static List<Address> ReadCsv(string csvFilePath)
        {
            List<Address> resAddresses = new List<Address>();

            var encode = new System.Text.UTF8Encoding(false);  // BOMなしUTF8エンコード(読み込むCSVファイルに合わせている)

            using (var reader = new System.IO.StreamReader(csvFilePath, encode))
            using (var csvReader = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                bool isHeaderSkipped = false;
                while (csvReader.Read())
                {
                    // 1行目はヘッダー行なのでスキップ
                    if (!isHeaderSkipped)
                    {
                        isHeaderSkipped = true;
                        continue;
                    }

                    //====================================================================================================
                    // CSVレコードからAddressモデルデータを生成。
                    //****************************************************************************************************
                    // * 以下、CSVファイルフォーマット(1行目:ヘッダ,2行目:データ行例):
                    //----------------------------------------------------------------------------------------------------
                    //氏名,氏名（カタカナ）,携帯電話,メールアドレス,郵便番号,住所,,,,
                    //大沼淳三,オオヌマジュンゾウ,08035899727,uoonuma@yjrgtmp.as.fd,989-0914,宮城県,刈田郡蔵王町,遠刈田温泉旭町,4-16-4,
                    //====================================================================================================
                    Address address = new Address()
                    {
                        Name = csvReader.GetField<string>(0),
                        Kana = csvReader.GetField<string>(1),
                        Telephone = csvReader.GetField<string>(2),
                        Mail = csvReader.GetField<string>(3),
                        ZipCode = csvReader.GetField<string>(4).Replace("-", ""),  // CSVデータはハイフンを含むがDBには含めないため
                        Prefecture = csvReader.GetField<string>(5),
                        StreetAddress = csvReader.GetField<string>(6) + csvReader.GetField<string>(7) + csvReader.GetField<string>(8),
                    };

                    resAddresses.Add(address);
                }
            }

            return resAddresses;
        }

        static void ImportAddressesToDb(List<Address> addressesToImport)
        {
            using (var db = new AddressBookInfoEntities())
            {
                db.Addresses.AddOrUpdate(x => x.Name, addressesToImport.ToArray());
                db.SaveChanges();
            }
        }
    }
}
