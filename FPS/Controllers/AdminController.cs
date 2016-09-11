using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FPS.Models;
using System.Collections.Generic;
using System.Text;
using System;
using System.Security.Cryptography;
using FPS.DataModel;
using FPS.Common;
using System.Web.Security;

namespace FPS.Controllers
{
    public class AdminController : Controller
    {

        #region Admin
        public static string HashPassword(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult AdminLogin(string returnUrl)
        {
            Session["UserType"] = "Admin";
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var uow = new FPSDatabaseEntities())
            {
                var userName = model.UserName;
                var password = HashPassword(model.Password);
                uow.Configuration.LazyLoadingEnabled = true;

                var userDataTemp =
                    uow.Users.FirstOrDefault(
                        st => st.UserName == userName && st.Password == password);

                if (userDataTemp != null)
                {
                    Session["UserID"] = userDataTemp.Id;
                    Session["UserType"] = userDataTemp.UserType;
                }
            }
           
            return RedirectToAction("Addcarriers", "Admin");            
        }

        //
        // POST: /Admin/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
            System.Web.HttpContext.Current.User = null;
            CacheManager.LogoffCurrentUser();
            return RedirectToAction("AdminLogin", "Admin");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        #endregion

        #region Carriers
        [HttpGet]
        public PartialViewResult CreateCarrier()
        {
            return PartialView(new FPS.DataModel.CarriersDetail());
        }

        [AllowAnonymous]
        public ActionResult Addcarriers()
        {
            List<CarriersDetail> objItem = new List<CarriersDetail>();
            if (ReferenceEquals(Session["UserType"], null))
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var uow = new FPSDatabaseEntities();

            List<CarriersDetail> carriersDetail = (from f in uow.CarriersDetails
                                                   select f).ToList<CarriersDetail>();
            
            return View(carriersDetail);
        }

        [HttpPost]
        public JsonResult CreateCarrier(CarriersDetail carriers)
        {
            var uow = new FPSDatabaseEntities();
            CarriersDetail carrier;

            var carrierDataTemp =
                    uow.CarriersDetails.FirstOrDefault(st => st.CarriersDetails == carriers.CarriersDetails);

            if (ReferenceEquals(carrierDataTemp, null))
            {
                carrier = new CarriersDetail
                {
                    CarriersDetails = carriers.CarriersDetails,
                    DiscountedPrice = carriers.DiscountedPrice,
                    CreatedDAte = DateTime.Now,
                    CarriersLogo = carriers.CarriersLogo,
                    TransitTime = carriers.TransitTime
                };
                uow.CarriersDetails.Add(carrier);
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Already added this carriers details";
            }

            return Json(carriers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EditCarrier(Int32 carrierid)
        {
            var uow = new FPSDatabaseEntities();
            CarriersDetail carrier = new CarriersDetail();

            var carrierDataTemp = uow.CarriersDetails.FirstOrDefault(st => st.Id == carrierid);

            carrier = new CarriersDetail
            {
                CarriersDetails = carrierDataTemp.CarriersDetails,
                DiscountedPrice = carrierDataTemp.DiscountedPrice,
                CarriersLogo = carrierDataTemp.CarriersLogo,
                TransitTime = carrierDataTemp.TransitTime,
                Id= carrierid
            };

            return PartialView(carrier);
        }

        [HttpPost]
        public JsonResult EditCarrier(CarriersDetail carriers)
        {
            var uow = new FPSDatabaseEntities();
           
            var carrierDataTemp = uow.CarriersDetails.FirstOrDefault(st => st.Id == carriers.Id);

            if (!ReferenceEquals(carrierDataTemp, null))
            {
                carrierDataTemp.CarriersDetails = carriers.CarriersDetails;
                carrierDataTemp.DiscountedPrice = carriers.DiscountedPrice;
                carrierDataTemp.CreatedDAte = DateTime.Now;
                carrierDataTemp.CarriersLogo = carriers.CarriersLogo;
                carrierDataTemp.TransitTime = carriers.TransitTime;
                
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Updated carriers details";
            }

            return Json(carrierDataTemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Int32 carrierId)
        {
            var uow = new FPSDatabaseEntities();
            var carrierDataTemp = uow.CarriersDetails.FirstOrDefault(st => st.Id == carrierId);

            uow.CarriersDetails.Remove(carrierDataTemp);
            uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Commodity Category
        [HttpGet]
        public PartialViewResult CreateCommodityCategory()
        {
            return PartialView(new FPS.DataModel.tblCommodityCategory());
        }

        [AllowAnonymous]
        public ActionResult AddCommodityCategory()
        {
            if (ReferenceEquals(Session["UserType"], null))
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var uow = new FPSDatabaseEntities();

            List<tblCommodityCategory> comodityCatg = (from f in uow.tblCommodityCategories
                                                   select f).ToList<tblCommodityCategory>();

            return View(comodityCatg);
        }

        [HttpPost]
        public JsonResult CreateCommodityCategory(tblCommodityCategory comodity)
        {
            var uow = new FPSDatabaseEntities();
            tblCommodityCategory comodityCatg;

            var comodityDataTemp = uow.tblCommodityCategories.FirstOrDefault(st => st.Name == comodity.Name);
            
            var itemList = (from t in uow.tblCommodityCategories
                            select t).OrderByDescending(c => c.Id).First();
            var cnt = 0;
            if (!ReferenceEquals(itemList, null))
            {
                cnt = itemList.Id;
            }
            
            if (ReferenceEquals(comodityDataTemp, null))
            {
                comodityCatg = new tblCommodityCategory
                {
                    Name = comodity.Name,
                    IsDeleted = false,
                    Value = cnt+1
                };
                uow.tblCommodityCategories.Add(comodityCatg);
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Already added this comodity category.";
            }

            return Json(comodity, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EditCommodityCategory(Int32 comodityid)
        {
            var uow = new FPSDatabaseEntities();
            tblCommodityCategory comodity = new tblCommodityCategory();

            var comodityDataTemp = uow.tblCommodityCategories.FirstOrDefault(st => st.Id == comodityid);

            comodity = new tblCommodityCategory
            {
                Name = comodityDataTemp.Name,
                Value = comodityDataTemp.Value
            };

            return PartialView(comodity);
        }

        [HttpPost]
        public JsonResult EditCommodityCategory(tblCommodityCategory comodities)
        {
            var uow = new FPSDatabaseEntities();

            var comodityDataTemp = uow.tblCommodityCategories.FirstOrDefault(st => st.Id == comodities.Id);

            if (!ReferenceEquals(comodityDataTemp, null))
            {
                comodityDataTemp.Name = comodities.Name;
                comodityDataTemp.IsDeleted=false;

                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Updated comodities category.";
            }

            return Json(comodityDataTemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCommodityCategory(Int32 comodityId)
        {
            var uow = new FPSDatabaseEntities();
            var comodityDataTemp = uow.tblCommodityCategories.FirstOrDefault(st => st.Id == comodityId);

            uow.tblCommodityCategories.Remove(comodityDataTemp);
            uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Packaging
        [HttpGet]
        public PartialViewResult CreatePackaging()
        {
            return PartialView(new FPS.DataModel.tblPackaging());
        }

        [AllowAnonymous]
        public ActionResult AddPackaging()
        {
            List<tblPackaging> objItem = new List<tblPackaging>();
            if (ReferenceEquals(Session["UserType"], null))
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var uow = new FPSDatabaseEntities();

            List<tblPackaging> packagingDetail = (from f in uow.tblPackagings
                                                   select f).ToList<tblPackaging>();

            return View(packagingDetail);
        }

        [HttpPost]
        public JsonResult CreatePackaging(tblPackaging packaging)
        {
            var uow = new FPSDatabaseEntities();
            tblPackaging carrier;

            var packagingDataTemp = uow.tblPackagings.FirstOrDefault(st => st.Name == packaging.Name);

            var cnt = 0;
            var items = uow.tblPackagings.Count();
            if (items > 0)
            {
                var itemList = (from t in uow.tblPackagings
                                select t).OrderByDescending(c => c.Id).First();
                
                if (!ReferenceEquals(itemList, null))
                {
                    cnt = itemList.Id;
                }
            }

            if (ReferenceEquals(packagingDataTemp, null))
            {
                packaging = new tblPackaging
                {
                    Name = packaging.Name,
                    IsDeleted = false,
                    Value = cnt + 1
                };
                uow.tblPackagings.Add(packaging);
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Already added this comodity category.";
            }

            return Json(packaging, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EditPackaging(Int32 packagingid)
        {
            var uow = new FPSDatabaseEntities();
            tblPackaging pacaging = new tblPackaging();

            var packagingDataTemp = uow.tblPackagings.FirstOrDefault(st => st.Id == packagingid);

            pacaging = new tblPackaging
            {
                Name = packagingDataTemp.Name,
                Value = packagingDataTemp.Value
            };

            return PartialView(pacaging);
        }

        [HttpPost]
        public JsonResult EditPackaging(tblPackaging packagin)
        {
            var uow = new FPSDatabaseEntities();

            var packaginDataTemp = uow.tblPackagings.FirstOrDefault(st => st.Id == packagin.Id);

            if (!ReferenceEquals(packaginDataTemp, null))
            {
                packaginDataTemp.Name = packagin.Name;
                packaginDataTemp.IsDeleted = false;

                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Updated packaging.";
            }

            return Json(packaginDataTemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePackaging(Int32 packaginId)
        {
            var uow = new FPSDatabaseEntities();
            var packaginIdDataTemp = uow.tblPackagings.FirstOrDefault(st => st.Id == packaginId);

            uow.tblPackagings.Remove(packaginIdDataTemp);
            uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Condition
        [HttpGet]
        public PartialViewResult CreateCondition()
        {
            return PartialView(new FPS.DataModel.tblCondition());
        }

        [AllowAnonymous]
        public ActionResult AddCondition()
        {
            List<tblPackaging> objItem = new List<tblPackaging>();
            if (ReferenceEquals(Session["UserType"], null))
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var uow = new FPSDatabaseEntities();

            List<tblCondition> conditionDetail = (from f in uow.tblConditions
                                                  select f).ToList<tblCondition>();

            return View(conditionDetail);
        }

        [HttpPost]
        public JsonResult CreateCondition(tblCondition condition)
        {
            var uow = new FPSDatabaseEntities();

            var conditionDataTemp = uow.tblConditions.FirstOrDefault(st => st.Name == condition.Name);

            var cnt = 0;
            var items = uow.tblConditions.Count();
            if (items > 0)
            {
                var itemList = (from t in uow.tblConditions
                                select t).OrderByDescending(c => c.Id).First();

                if (!ReferenceEquals(itemList, null))
                {
                    cnt = itemList.Id;
                }
            }

            if (ReferenceEquals(conditionDataTemp, null))
            {
                condition = new tblCondition
                {
                    Name = condition.Name,
                    IsDeleted = false,
                    Value = cnt + 1
                };
                uow.tblConditions.Add(condition);
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Already added this condition.";
            }

            return Json(condition, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EditCondition(Int32 conditionid)
        {
            var uow = new FPSDatabaseEntities();
            tblCondition condition = new tblCondition();

            var conditionDataTemp = uow.tblConditions.FirstOrDefault(st => st.Id == conditionid);

            condition = new tblCondition
            {
                Name = conditionDataTemp.Name,
                Value = conditionDataTemp.Value
            };

            return PartialView(condition);
        }

        [HttpPost]
        public JsonResult EditCondition(tblCondition conditions)
        {
            var uow = new FPSDatabaseEntities();

            var conditionDataTemp = uow.tblConditions.FirstOrDefault(st => st.Id == conditions.Id);

            if (!ReferenceEquals(conditionDataTemp, null))
            {
                conditionDataTemp.Name = conditions.Name;
                conditionDataTemp.IsDeleted = false;

                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Updated condition.";
            }

            return Json(conditionDataTemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCondition(Int32 conditionId)
        {
            var uow = new FPSDatabaseEntities();
            var conditionIdDataTemp = uow.tblConditions.FirstOrDefault(st => st.Id == conditionId);

            uow.tblConditions.Remove(conditionIdDataTemp);
            uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Freight Class
        [HttpGet]
        public PartialViewResult CreateFreightClass()
        {
            return PartialView(new FPS.DataModel.tblFreightClass());
        }

        [AllowAnonymous]
        public ActionResult AddFreightClass()
        {
            List<tblFreightClass> objItem = new List<tblFreightClass>();
            if (ReferenceEquals(Session["UserType"], null))
            {
                return RedirectToAction("AdminLogin", "Admin");
            }

            var uow = new FPSDatabaseEntities();

            List<tblFreightClass> freightClassesDetail = (from f in uow.tblFreightClasses
                                                   select f).ToList<tblFreightClass>();

            return View(freightClassesDetail);
        }

        [HttpPost]
        public JsonResult CreateFreightClass(tblFreightClass freightClasses)
        {
            var uow = new FPSDatabaseEntities();

            var freightClassesDataTemp =
                    uow.tblFreightClasses.FirstOrDefault(st => st.Name == freightClasses.Name);

            var cnt = 0;
            var items = uow.tblFreightClasses.Count();
            if (items > 0)
            {
                var itemList = (from t in uow.tblFreightClasses
                                select t).OrderByDescending(c => c.Id).First();

                if (!ReferenceEquals(itemList, null))
                {
                    cnt = itemList.Id;
                }
            }

            if (ReferenceEquals(freightClassesDataTemp, null))
            {
                freightClasses = new tblFreightClass
                {
                    Name = freightClasses.Name,
                    IsDeleted = false,
                    Value = cnt + 1
                };
                uow.tblFreightClasses.Add(freightClasses);
                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Already added this Freight Class";
            }

            return Json(freightClasses, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult EditFreightClass(Int32 freightClassid)
        {
            var uow = new FPSDatabaseEntities();
            tblFreightClass freightClass = new tblFreightClass();

            var freightClassDataTemp = uow.tblFreightClasses.FirstOrDefault(st => st.Id == freightClassid);

            freightClass = new tblFreightClass
            {
                Name = freightClassDataTemp.Name,
                Value = freightClassDataTemp.Value
            };

            return PartialView(freightClass);
        }

        [HttpPost]
        public JsonResult EditFreightClass(tblFreightClass freightClass)
        {
            var uow = new FPSDatabaseEntities();

            var freightClassDataTemp = uow.tblFreightClasses.FirstOrDefault(st => st.Id == freightClass.Id);

            if (!ReferenceEquals(freightClassDataTemp, null))
            {
                freightClassDataTemp.Name = freightClass.Name;
                freightClassDataTemp.IsDeleted = false;

                uow.SaveChanges();
                ViewBag.errormsg = "";
            }
            else
            {
                ViewBag.errormsg = "Updated FreightClass.";
            }

            return Json(freightClassDataTemp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFreightClass(Int32 freightClassId)
        {
            var uow = new FPSDatabaseEntities();
            var freightClassDataTemp = uow.tblFreightClasses.FirstOrDefault(st => st.Id == freightClassId);

            uow.tblFreightClasses.Remove(freightClassDataTemp);
            uow.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion                      

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}