using FPS.Common;
using FPS.DataModel;
using FPS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FPS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Shipper()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Carrier()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Termsandconditions()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FreightQuoteHome()
        {
            if (ReferenceEquals(Session["UserID"], null))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
                System.Web.HttpContext.Current.User = null;
                CacheManager.LogoffCurrentUser();
                return RedirectToAction("Login", "Account");
            }

            var uow = new FPSDatabaseEntities();
            var userName = User.Identity.Name;
            QuoteResultModels _objuserloginmodel = new QuoteResultModels();
            List<CarriersDetail> objItem = new List<CarriersDetail>();

            var alreadypurchaseID = from f in uow.PaymentsDetails
                                    select f.QuoteID;

            var QuoteResultModelsData = (from qdls in uow.QuickQuoteDetails
                                         join crdtls in uow.CarriersDetails on qdls.CarrierID
                                            equals crdtls.Id
                                         where qdls.UserName == userName && !alreadypurchaseID.Contains(qdls.Id.ToString())
                                         select new { Id = crdtls.Id, CarriersDetails = crdtls.CarriersDetails, TransitTime = crdtls.TransitTime, DiscountedPrice = crdtls.DiscountedPrice, CarriersLogo = crdtls.CarriersLogo, CreatedDAte = crdtls.CreatedDAte, QuiteID = qdls.Id });

            foreach (var item in QuoteResultModelsData)
            {
                objItem.Add(new CarriersDetail { Id = item.Id, CarriersDetails = item.CarriersDetails, TransitTime = item.TransitTime, DiscountedPrice = item.DiscountedPrice, CarriersLogo = item.CarriersLogo, CreatedDAte = item.CreatedDAte, QuoteID = item.QuiteID.ToString() });
                TempData["quiteID"] = "123456";
            }
            
            _objuserloginmodel.CarriersDetails = objItem;
            return View(_objuserloginmodel);
        }

        public ActionResult QuickQuoteContinue(int id,string QuoteID)
        {
            if (ReferenceEquals(TempData["quiteID"], null))
            {
                return RedirectToAction("FreightQuoteHome");
            }
            else
            {
                TempData["QuoteID"] = QuoteID;
                TempData["carriarID"] = id.ToString();

                TempData.Keep("carriarID");
                return RedirectToAction("QuickQuoteReview", "Quote");
            }
        }

        public ActionResult QuickQuoteDelete(int id, string QuoteID)
        {
            if (ReferenceEquals(TempData["quiteID"], null))
            {
                return RedirectToAction("FreightQuoteHome");
            }
            else
            {
                var userName = User.Identity.Name;
                int ItemquoteID = Convert.ToInt32(QuoteID);
                TempData["quiteID"] = QuoteID;
                TempData.Keep("QuoteID");

                using (var uow = new FPSDatabaseEntities())
                {
                    var ItemdetailsData = (from qdls in uow.ItemDetails
                                                where qdls.QuoteID == ItemquoteID
                                           select qdls).Distinct().ToList();

                    if (!ReferenceEquals(ItemdetailsData, null))
                    {
                        for (int i = 0; i < ItemdetailsData.Count; i++)
                        {
                            int itemid = ItemdetailsData[i].Id;
                            var QuoteItemData = uow.ItemDetails.SingleOrDefault(x => x.Id == itemid);

                            if (!ReferenceEquals(QuoteItemData, null))
                            {
                                uow.ItemDetails.Remove(QuoteItemData);
                                uow.SaveChanges();
                            }
                        }
                    }
                    
                    var ItemdropData = uow.ItemDropoffDetails.SingleOrDefault(x => x.QuoteID == QuoteID);

                    if (!ReferenceEquals(ItemdropData, null))
                    {
                        uow.ItemDropoffDetails.Remove(ItemdropData);
                        uow.SaveChanges();
                    }

                    var ItempickupData = uow.ItemPickupDetails.SingleOrDefault(x => x.QuoteID == QuoteID);

                    if (!ReferenceEquals(ItempickupData, null))
                    {
                        uow.ItemPickupDetails.Remove(ItempickupData);
                        uow.SaveChanges();
                    }

                    var ShipmentData = (from qdls in uow.ShipmentDetails
                                        where qdls.QuoteID == QuoteID
                                        select qdls).Distinct().ToList();

                    if (!ReferenceEquals(ShipmentData, null))
                    {
                        for (int i = 0; i < ShipmentData.Count; i++)
                        {
                            int Shipmentid = ShipmentData[i].ShipmentId;
                            var ShipmentItemData = uow.ShipmentDetails.SingleOrDefault(x => x.ShipmentId == Shipmentid);

                            if (!ReferenceEquals(ShipmentItemData, null))
                            {
                                uow.ShipmentDetails.Remove(ShipmentItemData);
                                uow.SaveChanges();
                            }
                        }
                    }
                    
                    var QuoteData = uow.QuickQuoteDetails.SingleOrDefault(x => x.Id == ItemquoteID);
                    if (!ReferenceEquals(QuoteData, null))
                    {
                        uow.QuickQuoteDetails.Remove(QuoteData);
                        uow.SaveChanges();
                    }
                }

                return RedirectToAction("FreightQuoteHome");
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}