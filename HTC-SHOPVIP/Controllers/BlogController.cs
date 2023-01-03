using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;
using System.Data.Entity;
using PagedList;

namespace HTC_SHOPVIP.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            using (var con = new Model1())
            {
                var model = con.Blogs.ToList();
                return View(model);
            }
        }

        public ActionResult ChiTietTinTuc(int id)
        {
            using (var con = new Model1())
            {
                var model = con.Blogs.Find(id);
                return View(model);
            }
        }
        public PartialViewResult DsBlog(int? pageIndex, int? MaTheLoai, string ChuoiTimKiem)
        {
            var db = new Model1();
            int _pageIndex = pageIndex ?? 1;
            var DsBlog = new List<Blog>();
            if (MaTheLoai == null && ChuoiTimKiem == null)
            {
                DsBlog = db.Blogs.ToList();
            }
            else if (MaTheLoai != null)
            {
                DsBlog = db.Blogs.Include(s => s.Blog_Theloai).Where(s => s.MaTheloai == MaTheLoai).ToList();
            }
            else if (ChuoiTimKiem != null)
            {
                DsBlog = db.Blogs.Where(s => s.TenBlog.Contains(ChuoiTimKiem)).ToList();

            }


            return PartialView(DsBlog.OrderBy(s => s.MaTheloai).ToPagedList(_pageIndex, 4));

        }


    }
}