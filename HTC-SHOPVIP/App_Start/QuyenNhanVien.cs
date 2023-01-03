using HTC_SHOPVIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTC_SHOPVIP.App_Start
{
    public class QuyenNhanVien: AuthorizeAttribute
    {
        public int idChucNang { set; get; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
          
            NhanVien nvSession = (NhanVien)HttpContext.Current.Session["admin"];


            if (nvSession != null)
            {
                using (var con = new Model1())
                {
                    var count = con.PhanQuyens.Count(m => m.idNhanVien ==nvSession.MaNV & m.idChucNang == idChucNang);
                    if (count != 0)
                    {
                        return;
                    }
                    else
                    {
                        var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                            new
                            {
                                controller = "SanPham",
                                action = "BaoLoi",
                                area = "Admin",
                                returnUrl = returnUrl.ToString()
                            }
                            ));
                    }
                    return;
                }
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "TaiKhoan",
                        action = "AdminLoginView",
                        area = "Admin",
                        returnUrl = returnUrl.ToString()
                    }
                    ));
            }


        }
    }
}