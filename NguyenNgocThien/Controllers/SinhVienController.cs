using NguyenNgocThien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NguyenNgocThien.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listsinhvien()
        {
            var all_sinhvien = from ss in data.SinhViens select ss;
            return View(all_sinhvien);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_MaSV = collection["MaSV"];
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_Hinh = collection["Hinh"];



            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.MaSV = E_MaSV;
                s.HoTen = E_HoTen;
                s.NgaySinh = E_NgaySinh;
                s.GioiTinh = E_GioiTinh;
                s.Hinh = E_Hinh.ToString();


                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Listsinhvien");
            }
            return this.Create();
        }

        public ActionResult Edit(String id)
        {
            var E_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sinhvien);
        }
        [HttpPost]
        public ActionResult Edit(String id, FormCollection collection)
        {
            var E_SinhVien = data.SinhViens.First(m => m.MaSV == id);
            var E_HoTen = collection["HoTen"];
            var E_GioiTinh = collection["GioiTinh"];
            var E_NgaySinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["hinh"];
            var E_MaNganh = collection["MaNganh"];
            E_SinhVien.MaSV = id;
            if (string.IsNullOrEmpty(E_HoTen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_SinhVien.HoTen = E_HoTen;
                E_SinhVien.GioiTinh = E_GioiTinh;
                E_SinhVien.NgaySinh = E_NgaySinh;
                E_SinhVien.Hinh = E_hinh;
                E_SinhVien.MaNganh = E_MaNganh;


                UpdateModel(E_SinhVien);
                data.SubmitChanges();
                return RedirectToAction("listsinhvien");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult Delete(String id)
        {
            var E_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sinhvien);
        }
        [HttpPost]
     
        public ActionResult Delete(String id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(E_sinhvien);
            data.SubmitChanges();
            return RedirectToAction("ListSach");
        }

        public ActionResult Detail(String id)
        {
            var E_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(E_sinhvien);
        }
    }
}