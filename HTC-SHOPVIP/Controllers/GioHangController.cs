using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTC_SHOPVIP.Models;

namespace HTC_SHOPVIP.Controllers
{
    public class GioHangController : Controller
    {

        // GET: GioHangC
        public ActionResult ShowToCart()
        {
           
            if (Session["khachhang"]==null)
            {
                return RedirectToAction("ChuaDangNhap", "GioHang", new {tb="Bạn chưa đăng nhập !"});
            }
            if (Session["cart"] == null)
            {
                return RedirectToAction("ChuaDangNhap", "GioHang", new {tb="Gỉỏ hàng trống !"});
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult ChiTietThanhToan()
        {
            return View();
        }

       public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"]==null)
            {
                cart= new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            using(var con=new Model1())
            {
                if (Session["khachhang"] != null)
                {
                    var pro = con.Sanphams.SingleOrDefault(s => s.MaSP == id);
                    if (pro != null)
                    {
                        GetCart().Add(pro);
                    }
                    return RedirectToAction("ShowToCart", "GioHang");
                }
                else
                    return RedirectToAction("LoginView", "Home");
            }
        } 
        public ActionResult Update_Quantity_Cart(int[] Ma, int[] SL)
        {
            Cart cart = Session["Cart"] as Cart;
        
            var list = new List<CartItem>();
            using (var con = new Model1())
            {
                if (cart != null)
                {
                    for (int i = 0; i < Ma.Count(); i++)
                    {
                        
                        var pro = con.Sanphams.Find(Ma[i]);
                        cart.Update_QuantiTy_Shopping(pro.MaSP, SL[i]);
                    }

                    Session["Cart"] = cart;
                }
                return RedirectToAction("ShowToCart", "GioHang");
            }


            //int quantity = int.Parse(form["Quantity"]);
            //int id_pro = int.Parse(form["ID_Product"]);
            //cart.Update_QuantiTy_Shopping(id_pro, quantity);




        }
        public ActionResult ChuaDangNhap(string tb)

        {
            ViewBag.tb = tb;
            return View();
        }

        public ActionResult GioHangNULL()
        {
            return View();
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "GioHang");

        }

        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["cart"] as Cart;
            if (cart != null)
            
                total_item = cart.Total_Quantity_in_Cart();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
            
        }

        public ActionResult Shopping_Success()
        {
            return View();
        }

        public ActionResult CheckOut(FormCollection form)
        {
            
            using (var conx = new Model1())
            {
                Khachhang kh = Session["khachhang"] as Khachhang;
                try
                {
                    Cart cart = Session["Cart"] as Cart;
                    DonHang _order = new DonHang();
                    _order.Ngaydat = DateTime.Now;
                    _order.MaKH = kh.MaKH;
                    _order.DiachiGui = form["Address_Delivery"];
                    //_order.MaKM = int.Parse(form["Makm"]);
                    _order.Tongtien = int.Parse(form["tongtien"]);
                    _order.status = form["status"];
                    conx.DonHangs.Add(_order);
                    conx.SaveChanges();
                    foreach (var item in cart.Items)
                    {
                        CTDonHang _order_Detail = new CTDonHang();
                        _order_Detail.MaDon = _order.MaDon;
                        _order_Detail.MaSP = item._shopping_product.MaSP;
                        _order_Detail.Dongia = item._shopping_product.Gia;
                        _order_Detail.soluong = item._shopping_quantity;
                        conx.CTDonHangs.Add(_order_Detail);
                    }
                    conx.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("Shopping_Success", "GioHang");

            }
                catch
            {
                return Content("Lỗi đặt hàng ! Vui lòng kiểm tra lại thông tin");
            }
        }
        }


        

        
    }
}