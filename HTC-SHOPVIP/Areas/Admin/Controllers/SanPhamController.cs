using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;
using HTC_SHOPVIP.App_Start;
using System.Net.Http;
using System.Net.Http.Headers;



using Newtonsoft.Json;
using System.Drawing;


namespace HTC_SHOPVIP.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham


        //[QuyenNhanVien(idChucNang = 1)]
        public ActionResult DSSanPham()
        {
            if (Session["admin"]==null)
            {
                return RedirectToAction("AdminLoginView", "TaiKhoan");
            }


          return View();

        }
     

       
        public ActionResult ThemSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DonSanPham(Sanpham sp, HttpPostedFileBase anhsp)
        {
            if (ModelState.IsValid)
            {
                var newsp = new Sanpham()
                {
                    TenSP = sp.TenSP,
                    Gia = sp.Gia,
                    MaloaiSP = sp.MaloaiSP,
                    MaHangSx = sp.MaHangSx,
                    trongluong = sp.trongluong,
                    Memory = sp.Memory,
                    Ram = sp.Ram,
                    Manhinh = sp.Manhinh,
                    Chip = sp.Chip,
                    mota = sp.mota,
                    Pin = sp.Pin,
                };
                if (anhsp != null && anhsp.ContentLength > 0)
                {
                    string rootFile = Server.MapPath("/Content/img/sanpham/");
                    string pathImage = rootFile + anhsp.FileName;
                    anhsp.SaveAs(pathImage);
                }


                using (var con = new Model1())
                {
                    con.Sanphams.Add(newsp);
                    con.SaveChanges();




                }
                using (var con1 = new Model1())
                {
                    var slidenew = new SP_anh();
                    slidenew.Linkanh = anhsp.FileName;
                    slidenew.MaSP = newsp.MaSP;
                    con1.SP_anh.Add(slidenew);
                    con1.SaveChanges();

                }
                return Redirect("/Admin/SanPham/DSSanPham");
            }
            else
            {
                return Redirect("/Admin/SanPham/ThemSP");
            }
            
        }
        [QuyenNhanVien(idChucNang = 1)]
        public ActionResult Xoasp(int id)
        {
            using (var con = new Model1())
            {


                var spham = con.Sanphams.Find(id);
                //Xóa sản phẩm khuyến mãi
                List<KM_SP> km_sp = (from s in con.KM_SPs
                                     where s.MaSP == id
                                     select s).ToList();
                foreach (var item in km_sp)
                {

                    con.KM_SPs.Remove(item);
                }


                // đơn hàng đang xử lý lại có sản phẩm muốn xóa ?
                // xóa sản phẩm trong đơn hàng
                List<CTDonHang> ctdh = (from s in con.CTDonHangs
                                        where s.MaSP == id
                                        select s).ToList();

                foreach (var item in ctdh)
                {
                    con.CTDonHangs.Remove(item);
                }

                //xóa các bài viết đánh giá của sản phẩm cần xóa
                List<SP_Danhgia> spdg = (from s in con.SP_Danhgia
                                         where s.MaSP == id
                                         select s).ToList();

                foreach (var item in spdg)
                {
                    con.SP_Danhgia.Remove(item);
                }

                List<SP_anh> spa = (from s in con.SP_anh
                                    where s.MaSP == id
                                    select s).ToList();
                foreach (var item in spa)
                {
                    con.SP_anh.Remove(item);
                }
                //var anh = con.SP_anh.Find(spham.MaSP);
                //con.SP_anh.Remove(anh);


                con.Sanphams.Remove(spham);
                con.SaveChanges();
                return Redirect("/Admin/SanPham/DSSanPham");
            }

        }



        public ActionResult CapNhat(int id)
        {
            
            using(var con = new Model1())
            {
                var spn = con.Sanphams.Find(id);
                return View(spn);
                
            }
        }

        public ActionResult CapNhat1(int masp, string tensp, int giasp, int soluongsp, int hangsx, int danhmuc, string trongluong, string bonho, string ram, string pin, string manhinh, string chip, string mota)
        {
            using(var con=new Model1()) 
            {
                var newsp = con.Sanphams.Find(masp);
                newsp.TenSP = tensp;
                newsp.Gia = giasp;
                newsp.MaloaiSP = danhmuc;
                newsp.MaHangSx = hangsx;
                newsp.trongluong = trongluong;
                newsp.Memory = bonho;
                newsp.Ram = ram;
                newsp.Manhinh = manhinh;
                newsp.Chip = chip;
                newsp.mota = mota;
                newsp.Pin = pin;
                con.SaveChanges();
                return Redirect("/Admin/SanPham/DSSanPham");
            }
            
        }

        public ActionResult TimKiem(string search)
        {
            ViewBag.Title = search;
            using (var con= new Model1())
            {
                var db = (from s in con.Sanphams
                          where s.TenSP.Contains(search)
                          select s).ToList();
                return View(db);
            }

        }
        public ActionResult BaoLoi()
        {
            return View();
        }
             


        public ActionResult testAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44355/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/load").Result;
            if (response.IsSuccessStatusCode)
            {
                var model = response.Content.ReadAsAsync<Slide_anh>().Result;
                return View(model);
            }
            return View();

        }


        //CALL API thêm sản phẩm
        //[HttpPost]
        //public ActionResult DonSanPham(Sanpham model)
        //{
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("https://localhost:44355/");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = client.PostAsJsonAsync("themsp", model).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return Redirect("/Admin/SanPham/DSSanPham");
        //        }
        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //call API XÓA SẢN PHẨM
        //public ActionResult Xoasp(int id)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44355/");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = client.GetAsync("api/Values/" + id).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var model = response.Content.ReadAsAsync<Sanpham>().Result;
        //        return Redirect("/Admin/SanPham/DSSanPham");
        //    }
        //    return Redirect("/Admin/SanPham/DSSanPham");
        //}

    }
    
}