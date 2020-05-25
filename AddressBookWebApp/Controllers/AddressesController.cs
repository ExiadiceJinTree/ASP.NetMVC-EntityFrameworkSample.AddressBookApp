using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBookWebApp.Models;

namespace AddressBookWebApp.Controllers
{
    /// <summary>
    /// Addresses Controller。
    /// コントローラー生成テンプレート"Entity Frameworkを使用した、ビューがあるMVC5コントローラー"で自動生成。
    /// コントローラー自動生成時のオプション:
    ///   ・モデルクラス=Address  ・データコンテキストクラス=AddressBookInfoEntities  ・非同期コントローラーアクションの使用=true
    ///   ・ビュー:
    ///     ・ビューの生成=true  ・スクリプトライブラリの参照=true  ・レイアウトページの使用=true(~/Views/Shared/_Layout.cshtml)
    /// </summary>
    public class AddressesController : Controller
    {
        private AddressBookInfoEntities db = new AddressBookInfoEntities();

        // GET: Addresses
        public async Task<ActionResult> Index()
        {
            var addresses = db.Addresses.Include(a => a.Group);
            return View(await addresses.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name");
            return View();
        }

        // POST: Addresses/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Kana,ZipCode,PrefectureItem,StreetAddress,Telephone,Mail,Group_Id")] Address address)  // BindパラメータPrefectureをPrefectureItemに変更
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,Kana,ZipCode,Prefecture,StreetAddress,Telephone,Mail,Group_Id")] Address address)
        {
            if (ModelState.IsValid)
            {
                // ビューで選択されたPrefectureItemプロパティ列挙値を文字列化してPrefectureプロパティに設定
                address.Prefecture = address.PrefectureItem.ToString();

                db.Addresses.Add(address);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            // モデルのPrefectureプロパティ値文字列をPrefectures列挙値に変換し、DBマッピングされていないモデルのPrefectureItemプロパティに設定しておく。
            // これにより、ビューのPrefectureItem列挙値プロパティを利用したドロップダウンリストが、現在値が選択された状態になる。
            Prefectures prefectureEnumItem;
            if (Enum.TryParse(address.Prefecture, out prefectureEnumItem))
            {
                address.PrefectureItem = prefectureEnumItem;
            }

            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Kana,ZipCode,PrefectureItem,StreetAddress,Telephone,Mail,Group_Id")] Address address)  // BindパラメータPrefectureをPrefectureItemに変更
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Kana,ZipCode,Prefecture,StreetAddress,Telephone,Mail,Group_Id")] Address address)
        {
            if (ModelState.IsValid)
            {
                // ビューで選択されたPrefectureItemプロパティ列挙値を文字列化してPrefectureプロパティに設定
                address.Prefecture = address.PrefectureItem.ToString();

                db.Entry(address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Group_Id = new SelectList(db.Groups, "Id", "Name", address.Group_Id);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
            db.Addresses.Remove(address);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Addresses/Search
        public async Task<ActionResult> Search([Bind(Include = "Kana")] SearchViewModel searchViewModel)
        {
            if (!string.IsNullOrEmpty(searchViewModel.Kana))
            {
                // 検索ワード(カナ)がAddressesテーブルデータのカナフィールドに含まれているデータ一覧を取得
                var searchedAddresses = db.Addresses.Where(address => address.Kana.Contains(searchViewModel.Kana));
                //検索結果をSearchViewModelの検索結果リストにセット
                searchViewModel.Addresses = await searchedAddresses.ToListAsync();
            }
            else
            {
                // 検索ワードが未指定の場合は、全件を結果とする
                searchViewModel.Addresses = await db.Addresses.ToListAsync();
            }

            return View(searchViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
