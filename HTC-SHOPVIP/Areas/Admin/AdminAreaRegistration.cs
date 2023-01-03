using System.Web.Mvc;

namespace HTC_SHOPVIP.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 "Admin_default",
                 "Admin/{controller}/{action}/{id}",
                
                 new { action = "DSSanPham", id = UrlParameter.Optional },
                 new[] { "HTC_SHOPVIP.Areas.Admin.Controllers" }
             );

           
        }
    }
}