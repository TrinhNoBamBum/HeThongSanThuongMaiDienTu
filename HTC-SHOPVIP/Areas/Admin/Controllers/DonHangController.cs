using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;

namespace HTC_SHOPVIP.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        public ActionResult DSDonHang()
        {
            using (var con = new Model1())
            {
                List<DonHang> listDH=con.DonHangs.ToList();

                return View(listDH) ;

            }
            
        }
        public ActionResult Xoa(int id)
        {
            using (var con=new Model1()) 
            {
                List<CTDonHang> ctdh = (from ct in con.CTDonHangs
                                        where ct.MaDon == id
                                        select ct).ToList();
                foreach(var item in ctdh)
                {
                    con.CTDonHangs.Remove(item);
                }
                var donhang = con.DonHangs.Find(id);
                foreach(var item in ctdh)
                {
                    con.CTDonHangs.Remove(item);
                }
                con.DonHangs.Remove(donhang);
                con.SaveChanges();
                return Redirect("/Admin/DonHang/DSDonHang");
            }
            
        }
        public ActionResult CapNhat(int id)
        {
            using (var con= new Model1())
            {
                var dh = con.DonHangs.Find(id);
                if( dh != null)
                {
                    dh.status = "Đã hoàn thành";
                }
                con.SaveChanges();
                return Redirect("/Admin/DonHang/DSDonHang");
            }
        }
        
    }
}