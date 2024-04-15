using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LG_10_Exercise_01.Models;

namespace LG_10_Exercise_01.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            
            return View();
        }

  [OutputCache(Duration = 10)]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

  public ActionResult ChangeTheme()
  {
      ViewBag.Theme = new SelectList(new[] { "Light Pink", "Light Grey", "Light Blue" });

      return View();
  }


        [HttpPost]
  public ActionResult ChangeTheme(string theme)
  {
      switch (theme)
      {
          case "Light Pink": Response.Cookies["cookie"].Value = "lightpink"; break;
          case "Light Grey": Response.Cookies["cookie"].Value = "lightgrey"; break;
          case "Light Blue": Response.Cookies["cookie"].Value = "lightblue"; break;
          default: Response.Cookies["cookie"].Value = "white"; break;
      }

      Response.Cookies["cookie"].Expires = DateTime.Now.AddDays(2);
      return View("Index");
  }












       [ChildActionOnly]
[OutputCache(Duration=10)]
public ActionResult CategoriesList()
{
           
        GiftDbContext db = new GiftDbContext();
            string strResponse = "<div id=\"giftItemsAcc\" style=\"width:200px; margin:0px; padding:0px;\">";
            
            foreach (var giftcat in db.GiftCategories.ToList<GiftCategory>())
            {
                strResponse += "<h3 style='width:100%; background-color:red; height:25px; color:white;margin:0px; padding:5px;'>" + giftcat.strCategory + "</h3>";
                strResponse += "<div style='width:100%; background-color:#ffcc00; height:100%;margin-left:2px; padding:0px 0px 0px 5px;'>";
                var giftItems = db.Gifts.Where(g => g.GiftCategoryID == giftcat.GiftCategoryID).ToList();
                foreach (var giftItem in giftItems )
                {
                    strResponse += "  <a href=\"/Gifts/Details/"+ giftItem.ID+"\">" + giftItem.Gift_Name + "</a><br>";
                }
                strResponse += "</div>";
            }
            strResponse += "</div>";
            ViewBag.strResponse = strResponse;
            return PartialView();
}

    }
}
