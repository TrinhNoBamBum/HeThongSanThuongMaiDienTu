using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;
namespace HTC_SHOPVIP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string user)
        {
        
         
            return View();
        }
   


        [HttpPost]
        public ActionResult TimKiem(FormCollection form)
        {
    
            string key = form["valuekey"];
            ViewBag.key= key;

                return View();
          
        }


        public ActionResult TimKiemDanhMuc(int id)
        {
           
            ViewBag.id = id;
            return View();
        }

        public ActionResult ChiTiet(int id )
        {
            ViewBag.id = id;
        
            return View();
        }

        [ChildActionOnly]
        public ActionResult DanhMuc()
        {
            using (var con = new Model1())
            {
                var dm = con.SP_Theloai.ToList();
                return PartialView("DanhMuc", dm);
            }
        }


        public ActionResult LoginView(string tb, string tb1, string tb2)
        {
            ViewBag.tb1= tb1;
            ViewBag.tb2 = tb2;
            ViewBag.tb = tb;
            return View();
        }

        public ActionResult Login(string tendangnhap, string matkhau)
        {
            ViewBag.tb = "Tên tài khoản hoặc mật khẩu không đúng !";
            using (var con = new Model1())
            {
                var tkmk = (from s in con.Khachhangs
                            select s).ToList();

                foreach (var item in tkmk)
                {

                    if (item.username.ToLower().Trim()==tendangnhap.ToLower().Trim() && item.matkhau.ToLower().Trim()==matkhau.ToLower().Trim())
                    {

                        var kh = (from s in con.Khachhangs
                                  where s.username.ToLower().Trim()==tendangnhap.ToLower().Trim() && s.matkhau.ToLower().Trim()==matkhau.ToLower().Trim()
                                  select s).FirstOrDefault();
                        Session["khachhang"] = kh;
                        return RedirectToAction("Index", "Home");

                    }


                }
                return RedirectToAction("LoginView", "Home", new { tb = "Tên đăng nhập hoặc tài khoản sai rồi bạn ơi" });

            }

        }

        public ActionResult DangKy( string repass, Khachhang khachh) 
        {
            if (ModelState.IsValid)
            {
                var kh = new Khachhang()
                {
                    Hoten = khachh.Hoten,
                    diachi = khachh.diachi,
                    email = khachh.email,
                    sdt = khachh.sdt,
                    matkhau = khachh.matkhau,
                    username = khachh.username,
                    ngaysinh = khachh.ngaysinh,
                };


                using (var con = new Model1())
                {
                    int dem = (from s in con.Khachhangs
                               where s.username.Trim().ToLower() == kh.username.Trim().ToLower()
                               select s).Count();

                    if (kh.matkhau != repass)
                    {
                        return RedirectToAction("LoginView", "Home", new { tb1 = "Thông tin không chính xác" });
                    }
                    else if (dem > 0)
                    {
                        return RedirectToAction("LoginView", "Home", new { tb1 = "Tên đăng nhập đã tồn tại" });
                    }
                    else
                    {
                        con.Khachhangs.Add(khachh);
                        con.SaveChanges();
                        return RedirectToAction("LoginView", "Home", new { tb2 = "Đăng ký thành công" });
                    }

                }
            }
            else
            {
                return RedirectToAction("LoginView", "Home", new { tb1 = "Tên đăng nhập đã tồn tại" });
            }

        }

        public ActionResult Binhluan(int id,string comment)
        {
            using(var con = new Model1())
            {

                if(comment=="" || comment == null)
                {
                    return RedirectToAction("ChiTiet", "Home");
                }
                else
                {
                    var kh = Session["khachhang"] as Khachhang;


                    var spdg = new SP_Danhgia();
                    spdg.MaSP = id;

                    spdg.noidung_danhgia = comment;

                    spdg.MaKH = kh.MaKH;
                    spdg.NgayDanhgia = DateTime.Now;
                    con.SP_Danhgia.Add(spdg);
                    con.SaveChanges();
                    return RedirectToAction("ChiTiet", "Home", new {id = id});
                }
               

            }

        }
    }
}