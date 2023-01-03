using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;

namespace HTC_SHOPVIP.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        public ActionResult DSKhachHang()
        {
            return View();
        }


        public ActionResult XoaKH(int id)
        {
            using(var con =new Model1())
            {
                var kh = con.Khachhangs.Find(id);
                //xóa khuyễn mãi khách hàng
                var km = (from s in con.KM_SPs
                          where s.MaKM == id
                          select s).ToList();
                foreach(var item in km)
                {
                    con.KM_SPs.Remove(item);
                }
                con.SaveChanges();
                //xóa blog đánh giá 
                var bl = (from s in con.Blog_Danhgia
                          where s.MaKH == id
                          select s).ToList();
                foreach(var item in bl)
                {
                    con.Blog_Danhgia.Remove(item);
                }
                con.SaveChanges();
                //xóa sản phẩm đánh giá 
                var sp = (from s in con.SP_Danhgia
                          where s.MaKH == id
                          select s).ToList();
                foreach (var item in sp)
                {
                    con.SP_Danhgia.Remove(item);
                }
                con.SaveChanges();
                //xóa chi tiết đơn hàng
                var dh = (from s in con.DonHangs
                          where s.MaKH == id
                          select s).ToList();
                //mã khách hàng  ->đơn hàng ->chi tiết đơn hàng


                foreach(var i in dh)
                {
                    var ct = (from s in con.CTDonHangs
                              where s.MaDon == i.MaDon
                              select s).ToList();
                    foreach (var item in ct)
                    {
                        con.CTDonHangs.Remove(item);
                    }
                }

                con.SaveChanges();
                //xóa đơn hàng 
                var dh1 = (from s in con.DonHangs
                           where s.MaKH == id
                           select s).ToList();

                foreach(var item in dh1)
                {
                    con.DonHangs.Remove(item);
                }
                con.SaveChanges();
                con.Khachhangs.Remove(kh);
                con.SaveChanges();
                return Redirect("/Admin/KhachHang/DSKhachHang");
            }
        }
    }
}