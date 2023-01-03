using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;
namespace HTC_SHOPVIP.Areas.Admin.Controllers
{
    public class TinTucController : Controller
    {
        // GET: Admin/TinTuc
        public ActionResult DSTinTuc()
        {
            return View();
        }
        public ActionResult DonTinTuc(string tieude, DateTime? thoigian, string tacgia, int theloai, string modau, string tags, string tomtat, string noidung)
        {
            using (var con = new Model1())
            {
                var bl = new Blog();

                bl.TenBlog = tieude;
                bl.ngayviet = thoigian;
                bl.tacgia = tacgia;
                bl.MaTheloai = theloai;
                bl.Tags = tags;
                bl.Tomtat = tomtat;
                bl.Modau = modau;
                bl.Noidung = noidung;
                con.Blogs.Add(bl);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/DSTinTuc");
            }

        }


        public ActionResult XoaTinTuc(int id)
        {
            using(var con=new Model1())
            {
                //xóa mã đánh giá
                var bl = con.Blogs.Find(id);
                var mtl = (from s in con.Blog_Danhgia
                           where s.MaBlog == id
                           select s).ToList();
                foreach(var it in mtl)
                {
                    con.Blog_Danhgia.Remove(it);
                }
                con.SaveChanges();
                //xóa blog ảnh
                var bla = (from s in con.Blog_anh
                           where s.MaBlog == id
                           select s).ToList();
                foreach(var it in bla)
                {
                    con.Blog_anh.Remove(it);
                }
                con.SaveChanges();
                con.Blogs.Remove(bl);
                con.SaveChanges();

                return Redirect("/Admin/TinTuc/DSTinTuc");
            }

            
        }


        public ActionResult CapNhat(int id)
        {
            using(var con=new Model1())
            {
                var db = con.Blogs.Find(id);
                return View(db);
            }
        }
        public ActionResult CapNhat1(int id,string tieude, DateTime? thoigian, string tacgia,string modau, int theloai,string tomtat, string tags, string noidung)
        {
            using (var con = new Model1())
            {
                var bl = con.Blogs.Find(id);

                bl.TenBlog = tieude;
                bl.ngayviet = thoigian;
                bl.tacgia = tacgia;
                bl.Modau = modau;
                bl.MaTheloai = theloai;
                bl.Tomtat = tomtat;
                bl.Tags = tags;
                bl.Noidung = noidung;
               
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/DSTinTuc");
            }
        }
        public ActionResult ThemTinTuc() {

            return View();
        }

        public ActionResult AnhSlide()
        {
            return View();
        }

        public ActionResult AnhSanPham()
        {
            return View();
        }

        public ActionResult ThemAnhSanPham(string maanh, int masp)
        {
            {
                using (var con = new Model1())
                {
                    var anhspnew = new SP_anh();
                    anhspnew.MaSP = masp;
                    anhspnew.Linkanh = maanh;

                    con.SP_anh.Add(anhspnew);
                    con.SaveChanges();
                    return Redirect("/Admin/TinTuc/AnhSanPham");
                }
              
            }
        }
        [HttpPost]
        public ActionResult ThemAnhSlide(HttpPostedFileBase link) {





            if (link != null && link.ContentLength > 0)
            {
                string rootFile = Server.MapPath("/Content/img/slide/");
                string pathImage = rootFile + link.FileName;
                link.SaveAs(pathImage);
            }

            using (var con=new Model1())
            {
                var slidenew = new Slide_anh();
                slidenew.Linkanh = link.FileName;
                con.Slide_anh.Add(slidenew);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/AnhSlide");
            }
           
        }


        public ActionResult AnhBlog()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemAnhBlog( string maanhbl, int maspblog)
        {
        
          
            using (var con = new Model1())
            {
                var blognew = new Blog_anh();
                blognew.Linkanh = maanhbl;
                blognew.MaBlog = maspblog;
                con.Blog_anh.Add(blognew);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/AnhBlog");
            }




        }


        public ActionResult XoaAnhSlide(int id)
        {
            using(var con=new Model1())
            {
                var db = con.Slide_anh.Find(id);
                con.Slide_anh.Remove(db);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/AnhSlide");
            }
        }


        public ActionResult XoaAnhBlog(int mablog, string linkanh)
        {
            using(var con=new Model1())
            {
                var db=(from s in con.Blog_anh
                        where s.MaBlog==mablog && s.Linkanh==linkanh 
                        select s).FirstOrDefault();

                con.Blog_anh.Remove(db);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/AnhBlog");
            }
        }

        public ActionResult XoaAnhSanPham(int masp, string linkanh)
        {
            using(var con=new Model1())
            {
                var db=(from s in con.SP_anh
                        where s.MaSP==masp && s.Linkanh==linkanh
                        select s).FirstOrDefault();
                con.SP_anh.Remove(db);
                con.SaveChanges();
                return Redirect("/Admin/TinTuc/AnhSanPham");
            }
        }
        public ActionResult HinhAnh() 
        { return View(); }
    }
}