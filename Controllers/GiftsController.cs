using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LG_10_Exercise_01.Models;

namespace LG_10_Exercise_01.Controllers
{
    public class GiftsController : Controller
    {
        private GiftDbContext db = new GiftDbContext();

        public ActionResult ImportGiftItems()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportGiftItems(string tbFilePath)
        {
            string strResponse;
            if (!string.IsNullOrEmpty(tbFilePath))
            {
                try
                {
                    System.Xml.Linq.XDocument xmlDoc = System.Xml.Linq.XDocument.Load(tbFilePath);

                    var gifts = from g in xmlDoc.Descendants("Gift")
                                select g;

                    foreach (var gift in gifts)
                    {
                        Gift newGift = new Gift();
                        newGift.Details = gift.Element("Details").Value;
                        newGift.Gift_Name = gift.Element("Name").Value;
                        newGift.GiftCategoryID = Convert.ToInt32(gift.Element("Category").Value);
                        newGift.Price = Convert.ToInt32(gift.Element("Price").Value);
                        db.Gifts.Add(newGift);
                        db.SaveChanges();
                    }
                    strResponse = "<font style='font-size:18pt;'><b>Gift items added successfully in the database.</b><br><br><br></font>";
                }
                catch (System.Exception ex)
                {
                    strResponse = "<font style='font-size:18pt;color:red;'><b>Invalid file path.</b><br><br><br></font>";
                }
                
            }
            else
                strResponse = "<font style='font-size:18pt;color:red;'><b>Please specify a valid file path.</b><br><br><br></font>";
            ViewBag.response = strResponse;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? GiftList, int? priceRange)
        {
            IEnumerable<Gift> dtGifts = (IEnumerable<Gift>)System.Runtime.Caching.MemoryCache.Default.Get("GiftList", null);

            if (dtGifts == null)
            {
                System.Runtime.Caching.MemoryCache.Default.Add("GiftList", getGifts(), System.DateTime.Now.AddMinutes(5), null);
                dtGifts = (IEnumerable<Gift>)System.Runtime.Caching.MemoryCache.Default.Get("GiftList", null);
            }

            var gifts = dtGifts;
            
            
         
            ViewBag.GiftList = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory");
            if (GiftList != null)
            {
                gifts = gifts.Where(g => g.GiftCategoryID == GiftList);
            }
            if (priceRange != null)
            {
                switch (priceRange)
                {
                    case 1:
                        gifts = gifts.Where(g => g.Price < 50);
                        break;
                    case 2:
                        gifts = gifts.Where(g => g.Price >= 50 && g.Price <= 100);
                        break;
                    case 3:
                        gifts = gifts.Where(g => g.Price > 100);
                        break;
                }
            }
            return PartialView("_FilteredGifts", gifts);
        }

        //
        // GET: /Gifts/
        public IEnumerable<Gift> getGifts()
        {
            var gifts = db.Gifts.Include(g=> g.giftcategory).ToList();
            return (gifts);
        }

        public ActionResult Index()
        {
            IEnumerable<Gift> dtGifts = (IEnumerable<Gift>)System.Runtime.Caching.MemoryCache.Default.Get("GiftList", null);
            ViewBag.method = "get";
                

            if (dtGifts == null)
            {
                System.Runtime.Caching.MemoryCache.Default.Add("GiftList", getGifts(), System.DateTime.Now.AddMinutes(2), null);
                dtGifts = (IEnumerable<Gift>)System.Runtime.Caching.MemoryCache.Default.Get("GiftList", null);

                var gifts = dtGifts;
                ViewBag.GiftList = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory");
                return View(gifts);
            }


            else
            {

                var gifts = dtGifts;
                ViewBag.GiftList = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory");
                return View(gifts);

            }
                    
        }

        //
        // GET: /Gifts/Details/5

        public ActionResult Details(long id = 0)
        {
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        //
        // GET: /Gifts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GiftCategoryID = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory");
            return View();
        }

        //
        // POST: /Gifts/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(Gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Gifts.Add(gift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GiftCategoryID = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory", gift.GiftCategoryID);
            return View(gift);
        }

        //
        // GET: /Gifts/Edit/5
        [Authorize]
        public ActionResult Edit(long id = 0)
        {
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiftCategoryID = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory", gift.GiftCategoryID);
            return View(gift);
        }

        //
        // POST: /Gifts/Edit/5

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiftCategoryID = new SelectList(db.GiftCategories, "GiftCategoryID", "strCategory", gift.GiftCategoryID);
            return View(gift);
        }

        //
        // GET: /Gifts/Delete/5
        [Authorize]
        public ActionResult Delete(long id = 0)
        {
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        //
        // POST: /Gifts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Gift gift = db.Gifts.Find(id);
            db.Gifts.Remove(gift);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        
        public ActionResult ShoppingCart(int id=0)
        {
            Gift gift;
            if (id != 0)
            {
                gift = db.Gifts.Find(id);
            }
            else
            {
                return View();
            }

            var gifts="";


            if (Session["items"] != null)
                gifts = Session["Items"].ToString() + gift.Gift_Name + "<br/>";
            else
            {
                gifts = gift.Gift_Name +  "<br/>";
            }

                      
            Session["Items"] =gifts;
            
            return View();
        }

    }
}