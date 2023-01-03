using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;

namespace HTC_SHOPVIP.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: Admin/NhanVien
        public ActionResult DSNhanVien(string tb1)
        {
            ViewBag.tb1 = tb1;
            return View();
        }

        public ActionResult ViewThemNhanVien(string tb)
        {
            ViewBag.tb = tb;
            return View();
        }
        public ActionResult ThemNhanVien(string hoten, string username, string pass , string sdt, string diachi, string email, DateTime ngaysinh, string repass)
        {
            var nv = new NhanVien();
            nv.Hoten = hoten;
            nv.ngaysinh = ngaysinh;
            nv.username = username;
            nv.email = email;
            nv.sdt = sdt;
            nv.diachi = diachi;
            nv.matkhau = pass;
            using( var con = new Model1())
            {
                int dem = (from s in con.NhanViens
                           where s.username.Trim().ToLower() == username.Trim().ToLower()
                           select s).Count();
                if (username == "" || pass == "" || repass == "" || sdt == "" || hoten == "")
                {
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Điền đầy đủ thông tin bắt buộc" });
                }
                if (dem > 0)
                {
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Tên đăng nhập đã tồn tại" });
                }
               
                else
                {
                    con.NhanViens.Add(nv);
                    con.SaveChanges();
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Thêm nhân viên thành công" });
                }
            }
        }

        public ActionResult XoaNV(int id) 
        {
            using (var con=new Model1())
            {
                var m = con.NhanViens.Find(id);
                con.NhanViens.Remove(m);
                con.SaveChanges();
                return RedirectToAction("/DSNhanVien", new { tb1 = "Xóa nhân viên thành công" });
            }
        }
        public ActionResult ViewCapNhat(int id)
        {

            using (var con = new Model1())
            {
                NhanVien m = con.NhanViens.Find(id);
                return View(m);
            }
        }

        public ActionResult CapNhat1(string hoten, string username, string pass, string sdt, string diachi, string email, DateTime ngaysinh, string repass, int manv)
        {
           
            using (var con = new Model1())
            {

                NhanVien nv=con.NhanViens.Find(manv);
            nv.Hoten = hoten;
                nv.ngaysinh = ngaysinh;
                nv.username = username;
                nv.email = email;
                nv.sdt = sdt;
                nv.diachi = diachi;
                nv.matkhau = pass;
                int dem = (from s in con.NhanViens
                           where s.username.Trim().ToLower() == username.Trim().ToLower()
                           select s).Count();
                if (username == "" || pass == "" || repass == "" || sdt == "" || hoten == "")
                {
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Điền đầy đủ thông tin bắt buộc" });
                }
            
                if (pass != repass)
                {
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Thông tin không chính xác" });
                }
                else
                {
                    
                    con.SaveChanges();
                    return RedirectToAction("/ViewThemNhanVien", new { tb = "Cập nhật thành công" });
                }
            }
        }
        
    

    }
}