using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using FPS.Models;
using System.Collections.Generic;
using FPS.DataModel;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Owin.Security;
using System.Web.Security;
using FPS.Common;

namespace FPS.Controllers
{
    [Authorize]
    public class QuoteController : Controller
    {
        //
        // GET: /Manage/Index
        public ActionResult QuickQuote()
        {
            Session["objItem"] = null;
            QuoteViewModel _objuserloginmodel = new QuoteViewModel();

            PopulateViewBagForQuoteView();
            var userId = User.Identity.GetUserId();

            List<ItemDetail> _objItem = new List<ItemDetail>();
            _objItem = null;// GetItemList();
            _objuserloginmodel.ItemDetails = _objItem;
            ViewBag.btnText = "Add";
            return View(_objuserloginmodel);
        }

        private void PopulateViewBagForQuoteView()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Business", Value = "2" });
            li.Add(new SelectListItem { Text = "Freight Carrier Terminal", Value = "4" });
            li.Add(new SelectListItem { Text = "Residential or Non-commercial", Value = "1" });
            ViewData["LocationType"] = li;           
        }

        private void PopulateExDateMonth()
        {
            List <SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "00", Value = "00" });
            li.Add(new SelectListItem { Text = "01", Value = "01" });
            li.Add(new SelectListItem { Text = "02", Value = "02" });
            li.Add(new SelectListItem { Text = "03", Value = "03" });
            li.Add(new SelectListItem { Text = "04", Value = "04" });
            li.Add(new SelectListItem { Text = "05", Value = "05" });
            li.Add(new SelectListItem { Text = "06", Value = "06" });
            li.Add(new SelectListItem { Text = "07", Value = "07" });
            li.Add(new SelectListItem { Text = "08", Value = "08" });
            li.Add(new SelectListItem { Text = "09", Value = "09" });
            li.Add(new SelectListItem { Text = "10", Value = "10" });
            li.Add(new SelectListItem { Text = "11", Value = "11" });
            li.Add(new SelectListItem { Text = "12", Value = "12" });
            ViewData["ExDateMonth"] = li;
        }

        private void PopulateExDateYear()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "YYYY", Value = "0" });
            li.Add(new SelectListItem { Text = "2016", Value = "2016" });
            li.Add(new SelectListItem { Text = "2017", Value = "2017" });
            li.Add(new SelectListItem { Text = "2018", Value = "2018" });
            li.Add(new SelectListItem { Text = "2019", Value = "2019" });
            li.Add(new SelectListItem { Text = "2020", Value = "2020" });
            li.Add(new SelectListItem { Text = "2021", Value = "2021" });
            li.Add(new SelectListItem { Text = "2022", Value = "2022" });
            li.Add(new SelectListItem { Text = "2023", Value = "2023" });
            li.Add(new SelectListItem { Text = "2024", Value = "2024" });
            li.Add(new SelectListItem { Text = "2025", Value = "2025" });
            li.Add(new SelectListItem { Text = "2026", Value = "2026" });
            ViewData["ExDateYear"] = li;
        }

        private void PopulateCardType()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Visa", Value = "Visa" });
            li.Add(new SelectListItem { Text = "Mastercard", Value = "Mastercard" });
            li.Add(new SelectListItem { Text = "Discover", Value = "Discover" });
            li.Add(new SelectListItem { Text = "American Express", Value = "American Express" });
            ViewData["CardType"] = li;
        }

        private void PopulateCommodityCategory()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "General Merchandise", Value = "1" });
            li.Add(new SelectListItem { Text = "Machinery", Value = "2" });
            li.Add(new SelectListItem { Text = "Household Goods", Value = "3" });
            li.Add(new SelectListItem { Text = "Fragile Goods or Glass", Value = "4" });
            li.Add(new SelectListItem { Text = "Computers or Electronics", Value = "5" });
            li.Add(new SelectListItem { Text = "Fine Arts", Value = "6" });
            li.Add(new SelectListItem { Text = "Motorcycles or Automobiles", Value = "7" });
            li.Add(new SelectListItem { Text = "Precision Instruments", Value = "8" });
            li.Add(new SelectListItem { Text = "Musical Instruments", Value = "9" });
            li.Add(new SelectListItem { Text = "Chemicals or Hazardous Materials", Value = "10" });
            li.Add(new SelectListItem { Text = "Non - Perishable Foods", Value = "11" });
            li.Add(new SelectListItem { Text = "Bottled Beverages", Value = "12" });
            li.Add(new SelectListItem { Text = "Non - Meat Frozen Foods", Value = "13" });
            li.Add(new SelectListItem { Text = "Frozen Meats", Value = "14" });
            li.Add(new SelectListItem { Text = "Steel Products", Value = "15" });
            li.Add(new SelectListItem { Text = "Branded Goods", Value = "16" });
            li.Add(new SelectListItem { Text = "Ceramic, Marble, or Granite Tiles", Value = "17" });
            li.Add(new SelectListItem { Text = "Lumber", Value = "18" });
            li.Add(new SelectListItem { Text = "Boats or Yachts", Value = "19" });
            li.Add(new SelectListItem { Text = "TV’s(Plasma, LCD, DLP, etc)", Value = "20" });
            li.Add(new SelectListItem { Text = "Antiques(Items older than 100 years of age)", Value = "21" });
            
            ViewData["CommodityCategory"] = li;
        }

        private void PopulateItemCondition()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "New Goods", Value = "1" });
            li.Add(new SelectListItem { Text = "Used Goods", Value = "2" });
            li.Add(new SelectListItem { Text = "Reconditioned Goods", Value = "3" });
            ViewData["ItemCondition"] = li;
        }

        private void PopulatePackagingList()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Boxed", Value = "1" });
            li.Add(new SelectListItem { Text = "Crated", Value = "2" });
            li.Add(new SelectListItem { Text = "Drums or Barrels", Value = "3" });
            li.Add(new SelectListItem { Text = "Pallets (48” x 48”)", Value = "8" });
            li.Add(new SelectListItem { Text = "Pallets (60” x 48”)", Value = "9" });
            li.Add(new SelectListItem { Text = "Pallets (enter dimensions)", Value = "10" });
            li.Add(new SelectListItem { Text = "Pallets: Standard (40” x 48”)", Value = "7" });
            li.Add(new SelectListItem { Text = "Un-Packaged", Value = "5" });
            ViewData["PackagingList"] = li;
        }

        public List<ItemDetail> GetItemList(QuoteViewModel model)
        {
            List<ItemDetail> objItem = new List<ItemDetail>();
            /*Create instance of entity model*/
            var uow = new FPSDatabaseEntities();
            /*Getting data from database for user validation*/
            //var _objitemdetail =  (from data in uow.ItemDetails select data);
            if (!ReferenceEquals(Session["objItem"], null))
            {
                objItem = (List<ItemDetail>)Session["objItem"];
            }

            if (objItem.Count == 0)
            {
                objItem.Add(new ItemDetail { Id = 0, ItemName = GetItemName(Convert.ToInt32(model.Packaging1)), Quantity = model.quantity, Total = model.Weight1, Dimention_Length = model.Length1, WeightType = model.WeightUOM1.ToString(), Dimention_Width = model.Width1, Dimention_Height = model.Height1, HightUnit = model.DimensionsUOM1, FrightClass = model.Frightclass, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, QuoteID = 1, UserId = "www" });
            }
            else
            {
                var alreadyadded = false;
                foreach (var item in objItem)
                {
                    if (item.ItemName == GetItemName(Convert.ToInt32(model.Packaging1)))
                        alreadyadded = true;
                        
                }
                int id = objItem.Count;
                if (!alreadyadded)
                    objItem.Add(new ItemDetail { Id = id, ItemName = GetItemName(Convert.ToInt32(model.Packaging1)), Quantity = model.quantity, Total = model.Weight1, Dimention_Length = model.Length1, WeightType = model.WeightUOM1.ToString(), Dimention_Width = model.Width1, Dimention_Height = model.Height1, HightUnit = model.DimensionsUOM1, FrightClass = model.Frightclass, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, QuoteID = 1, UserId = "www" });
            }
            Session["objItem"] = objItem;
            return objItem;
        }
        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult truckloadquote()
        {
            Session["objItem"] = null;
            TruckloadQuoteDetail _objTruckloadQuoteDetail = new TruckloadQuoteDetail();
            PopulateViewBagForQuoteView();
            var userId = User.Identity.GetUserId();
           
            ViewBag.btnText = "Add";
            return View(_objTruckloadQuoteDetail);
        }

        [HttpPost]
        public ActionResult truckloadquote(TruckloadQuoteDetail model, string rblTruckType)
        {
            if (ReferenceEquals(Session["UserID"], null))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
                System.Web.HttpContext.Current.User = null;
                CacheManager.LogoffCurrentUser();
                return RedirectToAction("Login", "Account");
            }
            if (ReferenceEquals(rblTruckType, null))
            {
                rblTruckType = "0";
            }
            PopulateViewBagForQuoteView();
            string userID = Session["UserID"].ToString();
            try
            {
                using (var uow = new FPSDatabaseEntities())
                {
                    var userData =
                    uow.TruckloadQuoteDetails.FirstOrDefault(st => st.Id.ToString().Equals(userID, StringComparison.InvariantCultureIgnoreCase));

                    if (ReferenceEquals(userData, null))
                    {
                        var newTruckloadQuoteDetail = new TruckloadQuoteDetail
                        {
                            StartLocationType = model.StartLocationType,
                            EndLocationType = model.EndLocationType,
                            ShipmentType =Convert.ToInt32(rblTruckType),
                            StartLocationZip = model.StartLocationZip,
                            EndLocationZip = model.EndLocationZip,
                            PickupTime = DateTime.Now,// model.PickupTime,
                            CreatedDate = DateTime.Now,// model.PickupTime,
                            UserId = userID,
                            Weight=model.Weight
                        };

                        uow.TruckloadQuoteDetails.Add(newTruckloadQuoteDetail);
                        uow.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return View();
            }

            TempData["StartLocationZiptmp"] = model.StartLocationZip;
            TempData["EndLocationZiptmp"] = model.EndLocationZip;
            TempData["Weighttmp"] = model.Weight;

            return RedirectToAction("TruckloadReview");
        }

        public ActionResult TruckloadReview()
        {
            ViewBag.StartLocationZipBag = TempData["StartLocationZiptmp"];
            TempData.Keep("StartLocationZiptmp");
            ViewBag.EndLocationZipbag = TempData["EndLocationZiptmp"];
            TempData.Keep("EndLocationZiptmp");
            ViewBag.Weightbag = TempData["Weighttmp"];
            TempData.Keep("Weighttmp");

            return View();
        }

        public ActionResult AccountVerification()
        {
            if (ReferenceEquals(Session["UserID"],null))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
                System.Web.HttpContext.Current.User = null;
                CacheManager.LogoffCurrentUser();
                return RedirectToAction("Login", "Account");
            }

            TempData["ItemValue"] = TempData["ItemValue"].ToString();
            TempData.Keep("ItemValue");

            ViewBag.Countrylist = LoadCountries();
            ViewBag.Statelist = LoadStates();
            RegisterViewModel _objuserloginmodel = new RegisterViewModel();

            try
            {
                using (var uow = new FPSDatabaseEntities())
                {
                    string userID = Session["UserID"].ToString();
                    var userData =
                       uow.AspNetUsers.FirstOrDefault(
                           st => st.Id.Equals(userID, StringComparison.InvariantCultureIgnoreCase));

                    if (!ReferenceEquals(userData, null))
                    {
                        _objuserloginmodel = new RegisterViewModel
                        {
                            Fname = userData.FirstName,
                            Lname = userData.LastName,
                            Phone = userData.PhoneNumber,
                            CellPhone = userData.CellPhone,
                            City = userData.City,
                            Ctype = userData.CType,
                            Country = userData.Country,
                            State = userData.State
                        };
                    }
                    @ViewBag.Ctype = userData.CType;
                    // _objuserloginmodel = (RegisterViewModel)userData;

                }
            }

            catch (Exception ex)
            {
                return View(_objuserloginmodel);
            }
            return View(_objuserloginmodel);
        }

        [HttpPost]
        public ActionResult AccountVerification(RegisterViewModel model)
        {
            if (ReferenceEquals(Session["UserID"], null))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
                System.Web.HttpContext.Current.User = null;
                CacheManager.LogoffCurrentUser();
                return RedirectToAction("Login", "Account");
            }

            string userID = Session["UserID"].ToString();
            try
            {
                using (var uow = new FPSDatabaseEntities())
                {
                    var userData =
                    uow.AspNetUsers.FirstOrDefault(
                    st => st.Id.Equals(userID, StringComparison.InvariantCultureIgnoreCase));

                    if (!ReferenceEquals(userData, null))
                    {
                        userData.FirstName = model.Fname;
                        userData.LastName = model.Lname;
                        userData.Company = model.Company;
                        userData.Department = model.Department;
                        userData.Address1 = model.Address1;
                        userData.Address2 = model.Address2;
                        userData.City = model.City;
                        userData.Zip = model.Zip;
                        userData.PhoneNumber = model.Phone;
                        userData.CellPhone = model.CellPhone;
                        userData.sendTextMessg = model.sendTextMessg;
                        userData.State = model.State;
                        userData.isVerified = true;

                        uow.SaveChanges();
                    }                    
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("LocationDetails");
        }

        public ActionResult LocationDetails()
        {
            var uow = new FPSDatabaseEntities();
            string QuickQuoteDetailID = TempData["QuoteID"].ToString();
            TempData.Keep("QuoteID");
            TempData["ItemValue"] = TempData["ItemValue"].ToString();
            TempData.Keep("ItemValue");

            var PickupDetailsData = (from pickupDtles in uow.ItemPickupDetails
                                         where pickupDtles.QuoteID == QuickQuoteDetailID
                                         select pickupDtles).FirstOrDefault();

            ItemPickupDetail _objPickupDetail = new ItemPickupDetail();
            _objPickupDetail.State = PickupDetailsData.State;
            _objPickupDetail.Zip = PickupDetailsData.Zip;
            _objPickupDetail.Country = PickupDetailsData.Country;
            _objPickupDetail.City = PickupDetailsData.City;
            _objPickupDetail.QuoteID = QuickQuoteDetailID;

            _objPickupDetail.PickupDate = PickupDetailsData.PickupDate;
            _objPickupDetail.Company = PickupDetailsData.Company;
            _objPickupDetail.FirstName = PickupDetailsData.FirstName;
            _objPickupDetail.LastName = PickupDetailsData.LastName;
            _objPickupDetail.Department = PickupDetailsData.Department;
            _objPickupDetail.EmailAddress = PickupDetailsData.EmailAddress;
            _objPickupDetail.Phone = PickupDetailsData.Phone;
            _objPickupDetail.AlternatePhone = PickupDetailsData.AlternatePhone;
            _objPickupDetail.Fax = PickupDetailsData.Fax;
            _objPickupDetail.StreetAddress = PickupDetailsData.StreetAddress;
            _objPickupDetail.Apartment = PickupDetailsData.Apartment;

            var DellDetailsData = (from dellDtles in uow.ItemDropoffDetails
                                     where dellDtles.QuoteID == QuickQuoteDetailID
                                     select dellDtles).FirstOrDefault();

            ItemDropoffDetail _objDellDetail = new ItemDropoffDetail();
            _objDellDetail.State = DellDetailsData.State;
            _objDellDetail.Zip = DellDetailsData.Zip;
            _objDellDetail.Country = DellDetailsData.Country;
            _objDellDetail.City = DellDetailsData.City;
            _objDellDetail.QuoteID = QuickQuoteDetailID;
            
            _objDellDetail.Company = DellDetailsData.Company;
            _objDellDetail.FirstName = DellDetailsData.FirstName;
            _objDellDetail.LastName = DellDetailsData.LastName;
            _objDellDetail.Department = DellDetailsData.Department;
            _objDellDetail.EmailAddress = DellDetailsData.EmailAddress;
            _objDellDetail.Phone = DellDetailsData.Phone;
            _objDellDetail.AlternatePhone = DellDetailsData.AlternatePhone;
            _objDellDetail.Fax = DellDetailsData.Fax;
            _objDellDetail.StreetAddress = DellDetailsData.StreetAddress;
            _objDellDetail.Apartment = DellDetailsData.Apartment;

            PickupDropoffModel model = new PickupDropoffModel();
            model.PickupDetail = _objPickupDetail;
            model.DropoffDetail = _objDellDetail;

            return View(model);
        }

        [HttpPost]
        public ActionResult LocationDetails(PickupDropoffModel detailsModel)
        {
            var uow = new FPSDatabaseEntities();
            string QuickQuoteDetailID = TempData["QuoteID"].ToString();
            TempData.Keep("QuoteID");

            var PickupDetailsData = (from pickupDtles in uow.ItemPickupDetails
                                     where pickupDtles.QuoteID == QuickQuoteDetailID
                                     select pickupDtles).FirstOrDefault();

            if (!ReferenceEquals(PickupDetailsData, null))
            {
                PickupDetailsData.PickupDate = detailsModel.PickupDetail.PickupDate;
                PickupDetailsData.Company = detailsModel.PickupDetail.Company;
                PickupDetailsData.FirstName = detailsModel.PickupDetail.FirstName;
                PickupDetailsData.LastName = detailsModel.PickupDetail.LastName;
                PickupDetailsData.Department = detailsModel.PickupDetail.Department;
                PickupDetailsData.EmailAddress = detailsModel.PickupDetail.EmailAddress;
                PickupDetailsData.Phone = detailsModel.PickupDetail.Phone;
                PickupDetailsData.AlternatePhone = detailsModel.PickupDetail.AlternatePhone;
                PickupDetailsData.Fax = detailsModel.PickupDetail.Fax;
                PickupDetailsData.StreetAddress = detailsModel.PickupDetail.StreetAddress;
                PickupDetailsData.Apartment = detailsModel.PickupDetail.Apartment;
            }            

            var DellDetailsData = (from dellDtles in uow.ItemDropoffDetails
                                   where dellDtles.QuoteID == QuickQuoteDetailID
                                   select dellDtles).FirstOrDefault();

            if (!ReferenceEquals(DellDetailsData, null))
            {
                DellDetailsData.Company = detailsModel.DropoffDetail.Company;
                DellDetailsData.FirstName = detailsModel.DropoffDetail.FirstName;
                DellDetailsData.LastName = detailsModel.DropoffDetail.LastName;
                DellDetailsData.Department = detailsModel.DropoffDetail.Department;
                DellDetailsData.EmailAddress = detailsModel.DropoffDetail.EmailAddress;
                DellDetailsData.Phone = detailsModel.DropoffDetail.Phone;
                DellDetailsData.AlternatePhone = detailsModel.DropoffDetail.AlternatePhone;
                DellDetailsData.Fax = detailsModel.DropoffDetail.Fax;
                DellDetailsData.StreetAddress = detailsModel.DropoffDetail.StreetAddress;
                DellDetailsData.Apartment = detailsModel.DropoffDetail.Apartment;
            }

            uow.SaveChanges();

            return RedirectToAction("ShipmentDetails");
        }

        public ActionResult ShipmentDetails()
        {
            PopulateCommodityCategory();
            PopulateItemCondition();
            PopulatePackagingList();
            TempData["ItemValue"] = TempData["ItemValue"].ToString();
            TempData.Keep("ItemValue");

            var uow = new FPSDatabaseEntities();
            int QuickQuoteDetailID = Convert.ToInt32(TempData["QuoteID"].ToString());
            TempData.Keep("QuoteID");
            ViewBag.ShipmentID = QuickQuoteDetailID;
            List<ShipmentDetail> model = new List<ShipmentDetail>();

            var ShipmentDetailsData = (from shipmentDtles in uow.ShipmentDetails
                                     where shipmentDtles.QuoteID == QuickQuoteDetailID.ToString()
                                     select shipmentDtles).Distinct().ToList();

            if (ShipmentDetailsData.Count == 0) {
                var ItemDetailsData = (from itemDtles in uow.ItemDetails
                                       where itemDtles.QuoteID == QuickQuoteDetailID
                                       select new { itemDtles.Id, itemDtles.ItemName, itemDtles.PackgingID, itemDtles.Quantity, itemDtles.Dimention_Width, itemDtles.Dimention_Length, itemDtles.Dimention_Height }).Distinct().ToList();

                for (int i = 0; i < ItemDetailsData.Count; i++)
                {
                    model.Add(new ShipmentDetail { ItemName = ItemDetailsData[i].ItemName, Quantity = ItemDetailsData[i].Quantity.ToString(), Quantity_Length = ItemDetailsData[i].Dimention_Length.ToString(), Quantity_Width = ItemDetailsData[i].Dimention_Width.ToString(), Quantity_Height = ItemDetailsData[i].Dimention_Height.ToString(), ShipmentId = 0 });                    
                }
            }
            else
            {
                for (int i = 0; i < ShipmentDetailsData.Count; i++)
                {
                    model.Add(new ShipmentDetail { ItemName = ShipmentDetailsData[i].ItemName, Quantity = ShipmentDetailsData[i].Quantity.ToString(), Quantity_Length = ShipmentDetailsData[i].Quantity_Length.ToString(), Quantity_Width = ShipmentDetailsData[i].Quantity_Width.ToString(), Quantity_Height = ShipmentDetailsData[i].Quantity_Height.ToString(), Isfreight_Insurance = ShipmentDetailsData[i].Isfreight_Insurance, NMFC_Code = ShipmentDetailsData[i].NMFC_Code, ItemCondition = ShipmentDetailsData[i].ItemCondition, Commodity_Category = ShipmentDetailsData[i].Commodity_Category, ItemdetailsDesc = ShipmentDetailsData[i].ItemdetailsDesc, ShipmentId = ShipmentDetailsData[i].ShipmentId });
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ShipmentDetails(List<ShipmentDetail> model,string QuoteID)
        {
            var uow = new FPSDatabaseEntities();

            for (int i = 0; i < model.Count; i++)
            {
                string shipingID = model[i].ShipmentId.ToString();
                if (model[i].ShipmentId == 0)
                {
                    var newShipmentDetail = new ShipmentDetail
                    {
                        Commodity_Category = model[i].Commodity_Category,
                        ItemCondition=model[i].ItemCondition,
                        ItemName=model[i].ItemName,
                        Isfreight_Insurance=model[i].Isfreight_Insurance,
                        ItemdetailsDesc=model[i].ItemdetailsDesc,
                        NMFC_Code=model[i].NMFC_Code,
                        PackagingID=model[i].PackagingID,
                        Quantity=model[i].Quantity,
                        Quantity_Height=model[i].Quantity_Height,
                        Quantity_Length=model[i].Quantity_Length,
                        Quantity_Width=model[i].Quantity_Width,
                        QuoteID = QuoteID,
                        TotalValue_Shipment=model[i].TotalValue_Shipment
                    };
                    
                    uow.ShipmentDetails.Add(newShipmentDetail);
                    uow.SaveChanges();
                }
                else
                {
                    var ShipmentData =
                    uow.ShipmentDetails.FirstOrDefault(
                    sd => sd.ShipmentId.ToString().Equals(shipingID, StringComparison.InvariantCultureIgnoreCase));

                    if (!ReferenceEquals(ShipmentData, null))
                    {
                        ShipmentData.Commodity_Category = model[i].Commodity_Category;
                        ShipmentData.ItemCondition = model[i].ItemCondition;
                        ShipmentData.ItemName = model[i].ItemName;
                        ShipmentData.Isfreight_Insurance = model[i].Isfreight_Insurance;
                        ShipmentData.ItemdetailsDesc = model[i].ItemdetailsDesc;
                        ShipmentData.NMFC_Code = model[i].NMFC_Code;
                        ShipmentData.PackagingID = model[i].PackagingID;
                        ShipmentData.Quantity = model[i].Quantity;
                        ShipmentData.Quantity_Height = model[i].Quantity_Height;
                        ShipmentData.Quantity_Length = model[i].Quantity_Length;
                        ShipmentData.Quantity_Width = model[i].Quantity_Width;
                        ShipmentData.TotalValue_Shipment = model[i].TotalValue_Shipment;

                        uow.SaveChanges();
                    }
                }
            }

            return RedirectToAction("PaymentDetails");
        }

        public ActionResult PaymentDetails()
        {
            PopulateExDateMonth();
            PopulateCardType();
            PopulateExDateYear();

            var uow = new FPSDatabaseEntities();
            string QuickQuoteDetailID = TempData["QuoteID"].ToString();
            TempData.Keep("QuoteID");
            ViewBag.ShipmentID = QuickQuoteDetailID;
            var userName = User.Identity.Name;
            TempData["ItemValue"] = TempData["ItemValue"].ToString();            
            ViewBag.ItemValue = TempData["ItemValue"].ToString();
            TempData.Keep("ItemValue");

            var userInfo = (from usr in uow.AspNetUsers
                            where usr.UserName == userName
                            select usr).FirstOrDefault();

            AspNetUser _objAspNetUserDetail = new AspNetUser();
            _objAspNetUserDetail.State = userInfo.State;
            _objAspNetUserDetail.Zip = userInfo.Zip;
            _objAspNetUserDetail.Country = userInfo.Country;
            _objAspNetUserDetail.City = userInfo.City;

            _objAspNetUserDetail.Company = userInfo.Company;
            _objAspNetUserDetail.FirstName = userInfo.FirstName;
            _objAspNetUserDetail.LastName = userInfo.LastName;
            _objAspNetUserDetail.Address1 = userInfo.Address1;
            _objAspNetUserDetail.Address2 = userInfo.Address2;
            _objAspNetUserDetail.PhoneNumber = userInfo.PhoneNumber;

            PaymentInformationModel model = new PaymentInformationModel();
            model.AspNetUserDetail = _objAspNetUserDetail;

            return View(model);
        }

        [HttpPost]
        public ActionResult PaymentDetails(PaymentInformationModel model, string QuoteID)
        {
            var uow = new FPSDatabaseEntities();

            var newPaymentsDetail = new PaymentsDetail
            {
                CardNumber = model.PaymentDetail.CardNumber,
                CardType = model.PaymentDetail.CardType,
                ExDate_Month = model.PaymentDetail.ExDate_Month,
                ExDate_Year = model.PaymentDetail.ExDate_Year,
                CVVCode = model.PaymentDetail.CVVCode,
                IsStoredCard = true,
                PaymentDAte = DateTime.Now,
                Address1 = model.AspNetUserDetail.Address1,
                Address2 = model.AspNetUserDetail.Address2,
                City = model.AspNetUserDetail.City,
                Company = model.AspNetUserDetail.Company,
                Country = model.AspNetUserDetail.Country.ToString(),
                QuoteID = QuoteID,
                CouponCode = "",
                FirstName = model.AspNetUserDetail.FirstName,
                LastName = model.AspNetUserDetail.LastName,
                PhoneNumber = model.AspNetUserDetail.PhoneNumber,
                State = model.AspNetUserDetail.State,
                Zip = model.AspNetUserDetail.Zip
            };

            uow.PaymentsDetails.Add(newPaymentsDetail);
            uow.SaveChanges();

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            int QuickQuoteDetailID = Convert.ToInt32(TempData["QuoteID"].ToString());
            TempData.Keep("QuoteID");
            ViewBag.ShipmentID = QuickQuoteDetailID;

            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult QuickQuote(FormCollection form,QuoteViewModel model, string action,string[] StartlocationExtraservice, string[] EndlocationExtraservice,string ItemIDHidden)
        {
            string hdnpaclat = form["hdnpaclat"];
            string hdnpaclong = form["hdnpaclong"];
            string hdndellat = form["hdndellat"];
            string hdndellong = form["hdndellong"];
            string hdnpicklocation = form["hdnpicklocation"];
            string hdndeliverylocation = form["hdndeliverylocation"];
            string hdndeliveryss = form["mapcity"];

            string hdnpickuplocality = form["pickup_locality"];
            string hdnpickupState = form["pickup_administrative_area_level_1"];
            string hdnpickupzip = form["pickup_postal_code"];
            string hdnpickupcountry = form["pickup_country"];

            string hdndellocality = form["delivery_locality"];
            string hdndelState = form["delivery_administrative_area_level_1"];
            string hdndelzip = form["delivery_postal_code"];
            string hdndelcountry = form["delivery_country"];

            if (hdnpicklocation.Length > 0)
            {
                TempData["picklocation"] = hdnpicklocation;
                TempData["pickpaclong"] = hdnpaclong;
                TempData["pickuppaclat"] = hdnpaclat;

                TempData["pickuplocality"] = hdnpickuplocality;
                TempData["pickupState"] = hdnpickupState;
                TempData["pickupzip"] = hdnpickupzip;
                TempData["pickupcountry"] = hdnpickupcountry;                
            }

            if (hdndeliverylocation.Length > 0)
            {
                TempData["deliverylocation"] = hdndeliverylocation;
                TempData["dellong"] = hdndellong;
                TempData["dellat"] = hdndellat;
                TempData["dellocality"] = hdndellocality;
                TempData["delState"] = hdndelState;
                TempData["delzip"] = hdndelzip;
                TempData["delcountry"] = hdndelcountry;
            }
            
            if (hdnpicklocation.Length == 0 && !ReferenceEquals(TempData["picklocation"], null))
            {
                hdnpicklocation = TempData["picklocation"].ToString();
                ViewBag.picklocation = hdnpicklocation;                
            }
            if (hdndeliverylocation.Length == 0 && !ReferenceEquals(TempData["deliverylocation"],null))
            {
                hdndeliverylocation = TempData["deliverylocation"].ToString();
                ViewBag.deliverylocation = hdndeliverylocation;                
            }

            if (hdnpicklocation.Length > 0)
            {
                ViewBag.picklocation = hdnpicklocation;
                TempData["picklocation"] = hdnpicklocation;
            }
            if (hdndeliverylocation.Length > 0)
            {
                ViewBag.deliverylocation = hdndeliverylocation;
                TempData["deliverylocation"] = hdndeliverylocation;
            }

            ViewBag.StartLocationType = model.StartLocationType;
            ViewBag.EndLocationType = model.EndLocationType;

            string strStartlocationExtraservice = "";
            if (!ReferenceEquals(StartlocationExtraservice,null) && StartlocationExtraservice.Length > 0)
            {
                if (StartlocationExtraservice.Contains("Lift Gate at Pickup Point"))
                    ViewBag.vstartid1 = "checked";
                if (StartlocationExtraservice.Contains("Limited Access Pick Up"))
                    ViewBag.vstartid2 = "checked";
                if (StartlocationExtraservice.Contains("Tradeshow Origin"))
                    ViewBag.vstartid3 = "checked";

                foreach (string str in StartlocationExtraservice)
                {
                    strStartlocationExtraservice = strStartlocationExtraservice + "," + str;
                }
            }else
                strStartlocationExtraservice = ",";

            string strEndlocationExtraservice = "";
            if (!ReferenceEquals(EndlocationExtraservice, null) && EndlocationExtraservice.Length > 0)
            {
                if (EndlocationExtraservice.Contains("Call before Delivery"))
                    ViewBag.endid1 = "checked";
                if (EndlocationExtraservice.Contains("Lift Gate at Delivery Point"))
                    ViewBag.endid2 = "checked";
                if (EndlocationExtraservice.Contains("Limited Access Delivery"))
                    ViewBag.endid3 = "checked";
                if (EndlocationExtraservice.Contains("Tradeshow Delivery"))
                    ViewBag.endid4 = "checked";

                foreach (string str in EndlocationExtraservice)
                {
                    strEndlocationExtraservice = strEndlocationExtraservice + "," + str;
                }
            }
            else
                strEndlocationExtraservice = ",";

            string erromsg = "";
            if (ReferenceEquals(TempData["picklocation"], null))
            {
                erromsg = "Start location not entered.";
            }
            if (erromsg == "")
            {
                if (ReferenceEquals(TempData["deliverylocation"], null))
                {
                    erromsg = "Delivery location not entered.";
                }
            }

            TempData.Keep("deliverylocation");
            TempData.Keep("picklocation");

            if (action == "Add")
            {
                List<ItemDetail> _objItem = new List<ItemDetail>();
                _objItem = GetItemList(model);
                model.ItemDetails = _objItem;

                PopulateViewBagForQuoteView();
                var userId = User.Identity.GetUserId();
                ViewBag.btnText = "Add more";

                return View(model);
            }
            else if (action == "Add more")
            {
                List<ItemDetail> _objItem = new List<ItemDetail>();
                _objItem = GetItemList(model);
                model.ItemDetails = _objItem;

                PopulateViewBagForQuoteView();
                var userId = User.Identity.GetUserId();
                ViewBag.btnText = "Add more";

                return View(model);
            }
            else if (action == "Delete")
            {
                List<ItemDetail> _objItem = new List<ItemDetail>();
                _objItem = GetItemList(model);
                model.ItemDetails = _objItem;

                PopulateViewBagForQuoteView();
                var userId = User.Identity.GetUserId();
                ViewBag.btnText = "Add more";

                return View(model);
            }
            else
            {
                if (erromsg == "")
                {
                    model.ItemDetails = (List<ItemDetail>)Session["objItem"];

                    var userId = "";
                    var userName = User.Identity.Name;

                    using (var uow = new FPSDatabaseEntities())
                    {
                        var QuickQuoteDetailData = new QuickQuoteDetail();
                        string itemExist = "";
                        for (int i = 0; i < model.ItemDetails.Count; i++)
                        {
                            string itemname = model.ItemDetails[i].ItemName;
                            QuickQuoteDetailData = (from qdls in uow.QuickQuoteDetails
                                                    join idls in uow.ItemDetails on qdls.Id
                                                       equals idls.QuoteID
                                                    where qdls.UserName == userName && idls.ItemName == itemname
                                                    select qdls).FirstOrDefault();
                            if (!ReferenceEquals(QuickQuoteDetailData, null))
                            {
                                itemExist = itemExist + "," + itemname;
                            }
                        }

                        //if (ReferenceEquals(itemExist, ""))
                        //{
                        if (ReferenceEquals(Session["UserID"], null))
                        {
                            var userDataTemp =
                            uow.AspNetUsers.FirstOrDefault(
                                st => st.UserName == userName);

                            userId = userDataTemp.Id;
                        }
                        else { userId = Session["UserID"].ToString(); }


                        var newQuickQuoteDetail = new QuickQuoteDetail
                        {
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            PickupTime = DateTime.Now,
                            StartLocationExtraService = strStartlocationExtraservice.Substring(1),
                            QuickQuoteType = model.QuickQuoteType,
                            EndLocationType = model.EndLocationType,
                            EndLocationExtraService = strEndlocationExtraservice.Substring(1),
                            UserName = userName,
                            StartLocationType = model.StartLocationType,
                            UserId = userId
                        };

                        uow.QuickQuoteDetails.Add(newQuickQuoteDetail);
                        uow.SaveChanges();
                        QuickQuoteDetailData = newQuickQuoteDetail;
                        for (int i = 0; i < model.ItemDetails.Count; i++)
                        {
                            var newItemDetails = new ItemDetail
                            {
                                Dimention_Height = Convert.ToInt32(model.ItemDetails[i].Dimention_Height),
                                Dimention_Length = Convert.ToInt32(model.ItemDetails[i].Dimention_Length),
                                Dimention_Width = Convert.ToInt32(model.ItemDetails[i].Dimention_Width),
                                FrightClass = Convert.ToInt32(model.ItemDetails[i].FrightClass),
                                HightUnit = model.ItemDetails[i].HightUnit,
                                ItemName = model.ItemDetails[i].ItemName,
                                PackgingID = Convert.ToInt32(model.ItemDetails[i].PackgingID),
                                Quantity = Convert.ToInt32(model.ItemDetails[i].Quantity),
                                Total = Convert.ToInt32(model.ItemDetails[i].Total),
                                UpdatedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                WeightType = model.ItemDetails[i].WeightType,
                                UserId = userId,
                                QuoteID = QuickQuoteDetailData.Id
                            };

                            TempData["QuoteID"] = QuickQuoteDetailData.Id;
                            uow.ItemDetails.Add(newItemDetails);
                            uow.SaveChanges();
                        }

                        if (!ReferenceEquals(TempData["pickuplocality"], null))
                        {
                            var pickupDropoffModel = new ItemPickupDetail
                            {
                                City = TempData["pickuplocality"].ToString(),
                                State = TempData["pickupState"].ToString(),
                                Zip = TempData["pickupzip"].ToString(),
                                Country = TempData["pickupcountry"].ToString(),
                                QuoteID = QuickQuoteDetailData.Id.ToString(),
                                LocationType = model.StartLocationType.ToString()
                            };

                            uow.ItemPickupDetails.Add(pickupDropoffModel);
                            uow.SaveChanges();
                        }

                        if (!ReferenceEquals(TempData["dellocality"], null))
                        {
                            var itemDropoffDetail = new ItemDropoffDetail
                            {
                                City = TempData["dellocality"].ToString(),
                                State = TempData["delState"].ToString(),
                                Zip = TempData["delzip"].ToString(),
                                Country = TempData["delcountry"].ToString(),
                                QuoteID = QuickQuoteDetailData.Id.ToString(),
                                LocationType = model.EndLocationType.ToString()
                            };

                            uow.ItemDropoffDetails.Add(itemDropoffDetail);
                            uow.SaveChanges();
                        }
                        
                        AddItemCollectionAddress(QuickQuoteDetailData.Id.ToString(), TempData["pickuppaclat"].ToString(), TempData["pickpaclong"].ToString(), TempData["picklocation"].ToString());
                        AddItemDeliveryAddress(QuickQuoteDetailData.Id.ToString(), TempData["dellat"].ToString(), TempData["dellong"].ToString(), TempData["deliverylocation"].ToString());

                        TempData.Keep("picklocation");
                        TempData.Keep("pickpaclong");
                        TempData.Keep("pickuppaclat");
                        TempData.Keep("deliverylocation");
                        TempData.Keep("dellong");
                        TempData.Keep("dellat");                       

                        TempData.Keep("dellocality");
                        TempData.Keep("delState");
                        TempData.Keep("delzip");
                        TempData.Keep("delcountry");
                        
                        TempData.Keep("pickuplocality");
                        TempData.Keep("pickupState");
                        TempData.Keep("pickupzip");
                        TempData.Keep("pickupcountry");

                        //}
                        //else
                        //{
                        //    PopulateViewBagForQuoteView();
                        //    return View(model);
                        //}
                    }
                    TempData["carriarID"] = "0";
                    return RedirectToAction("QuickQuoteReview");
                }
                else
                {
                    List<ItemDetail> _objItem = new List<ItemDetail>();
                    _objItem = GetItemList(model);
                    model.ItemDetails = _objItem;

                    PopulateViewBagForQuoteView();
                    var userId = User.Identity.GetUserId();

                    ViewBag.errmsg = erromsg;
                    return View(model);
                }
            }
        }

        public ActionResult Delete(QuoteViewModel model,int id)
        {
            List<ItemDetail> objItem = new List<ItemDetail>();
            var uow = new FPSDatabaseEntities();
            if (!ReferenceEquals(Session["objItem"], null))
            {
                objItem = (List<ItemDetail>)Session["objItem"];
            }

            objItem.RemoveAt(id);

            return RedirectToAction("QuickQuote", new { id = 99 });
        }

        public ActionResult QuickQuoteContinue(int id)
        {
            var userName = User.Identity.Name;
            Int32 QuoteID =Convert.ToInt32(TempData["QuoteID"].ToString());
            TempData.Keep("QuoteID");

            bool isVerified = false;

            using (var uow = new FPSDatabaseEntities())
            {
                var QuickQuoteDetailData = (from qdls in uow.QuickQuoteDetails
                                            where qdls.UserName == userName && qdls.Id == QuoteID
                                            select qdls).FirstOrDefault();

                var userInfo = (from usr in uow.AspNetUsers
                               where usr.UserName == userName
                               select usr).FirstOrDefault();

                var carrierInfo = (from crr in uow.CarriersDetails
                                where crr.Id == id
                                   select crr).FirstOrDefault();

                if (!ReferenceEquals(userInfo, null))
                {
                    isVerified =Convert.ToBoolean(userInfo.isVerified);
                }

                if (!ReferenceEquals(carrierInfo, null))
                {
                    TempData["ItemValue"] = carrierInfo.DiscountedPrice;
                }

                if (!ReferenceEquals(QuickQuoteDetailData, null))
                {
                    QuickQuoteDetailData.CarrierID = id;
                }
                uow.SaveChanges();
            }
            if(isVerified)
                return RedirectToAction("LocationDetails");
            else
                return RedirectToAction("AccountVerification");
        }

        private void AddItemCollectionAddress(string id, string hdnpaclat, string hdnpaclong, string hdnpicklocation)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["FPSDataModelKey"].ConnectionString);
            string sql = @"insert into ItemCollectionAddress([UserShipmentDetailId]
           ,[Latitude]
           ,[Longitude]
            ,[FromDate]
           ,[ToDate]
           ,[SpecificDateAttribute],[SpecificDateTime],[StartCurrentLocation]) values('" + id + "','" + hdnpaclat + "','" +
                                  hdnpaclong + "',getdate(),getdate(),3,getdate(),'" + hdnpicklocation + "' )";
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            if (sqlConn.State != System.Data.ConnectionState.Open)
                sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
        }
        
        private void AddItemDeliveryAddress(string id, string hdndellat, string hdndellong, string hdndeliverylocation)
        {
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["FPSDataModelKey"].ConnectionString);
            string sql = @"insert into ItemDeliveryAddress([UserShipmentDetailId]
           ,[Latitude]
           ,[Longitude]
            ,[FromDate]
           ,[ToDate]
           ,[SpecificDateAttribute],[SpecificDateTime],[DeliveryCurrentLocation]) values('" + id + "','" + hdndellat + "','" +
                                  hdndellong + "',getdate(),getdate(),3,getdate(),'" + hdndeliverylocation + "' )";
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            if (sqlConn.State != System.Data.ConnectionState.Open)
                sqlConn.Open();
            int result = cmd.ExecuteNonQuery();
        }

        public ActionResult QuickQuoteReview()
        {
            var userName = User.Identity.Name;
            List<CarriersDetail> objItem = new List<CarriersDetail>();
            var uow = new FPSDatabaseEntities();
            int QuickQuoteDetailID = Convert.ToInt32(TempData["QuoteID"].ToString());
            TempData.Keep("QuoteID");

            var QuoteResultModelsData = (from qdls in uow.QuickQuoteDetails
                                         join idls in uow.ItemDetails on qdls.Id
                                            equals idls.QuoteID
                                         join startLocation in uow.ItemCollectionAddresses on qdls.Id
                                         equals startLocation.UserShipmentDetailId
                                         join endLocation in uow.ItemDeliveryAddresses on qdls.Id
                                            equals endLocation.UserShipmentDetailId
                                         where qdls.UserName == userName && qdls.Id == QuickQuoteDetailID
                                         select new { startLocation.StartCurrentLocation, endLocation.DeliveryCurrentLocation, qdls.EndLocationExtraService, qdls.StartLocationExtraService, idls.FrightClass }).FirstOrDefault();

            QuoteResultModels _objuserloginmodel = new QuoteResultModels();
            if (!ReferenceEquals(QuoteResultModelsData, null))
            {                
                _objuserloginmodel.startLocation = QuoteResultModelsData.StartCurrentLocation;
                _objuserloginmodel.endLocation = QuoteResultModelsData.DeliveryCurrentLocation;
                _objuserloginmodel.includedstart = QuoteResultModelsData.StartLocationExtraService;
                _objuserloginmodel.includedend = QuoteResultModelsData.EndLocationExtraService;
                _objuserloginmodel.itemInfo = QuoteResultModelsData.FrightClass.ToString();
            }

            int carriarID = Convert.ToInt32(TempData["carriarID"].ToString());
            TempData.Keep("carriarID");

            if (carriarID > 0)
            {
                var _objcarriersdetail = (from data in uow.CarriersDetails where data.Id == carriarID select data);

                foreach (var item in _objcarriersdetail)
                {
                    objItem.Add(new CarriersDetail { Id = item.Id, CarriersDetails = item.CarriersDetails, TransitTime = item.TransitTime, DiscountedPrice = item.DiscountedPrice, CarriersLogo = "~/images2/LogoImage/" + item.CarriersLogo, CreatedDAte = item.CreatedDAte });
                }
            }
            else
            {
                var _objcarriersdetail = (from data in uow.CarriersDetails select data);

                foreach (var item in _objcarriersdetail)
                {
                    objItem.Add(new CarriersDetail { Id = item.Id, CarriersDetails = item.CarriersDetails, TransitTime = item.TransitTime, DiscountedPrice = item.DiscountedPrice, CarriersLogo = "~/images2/LogoImage/"+item.CarriersLogo, CreatedDAte = item.CreatedDAte });
                    }
            }
            
            _objuserloginmodel.CarriersDetails= objItem;
            return View(_objuserloginmodel);
        }

        private string GetItemName(int ItemID)
        {
            string ItemName = "";

            if (ItemID == 1)
            {
                ItemName = "Boxed";
            }else if (ItemID == 2)
            {
                ItemName = "Crated";
            }
            else if (ItemID == 3)
            {
                ItemName = "Drums or Barrels";
            }
            else if (ItemID == 5)
            {
                ItemName = "Un - Packaged";
            }
            else if (ItemID == 7)
            {
                ItemName = "Pallets: Standard(40” x 48”)";
            }
            else if (ItemID == 8)
            {
                ItemName = "Pallets(48” x 48”)";
            }
            else if (ItemID == 9)
            {
                ItemName = "Pallets(60” x 48”)";
            }
            else if (ItemID == 10)
            {
                ItemName = "Pallets(enter dimensions)";
            }
            
            return ItemName;
        }

        private List<SelectListItem> LoadCountries()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            li.Add(new SelectListItem { Text = "India", Value = "1" });
            li.Add(new SelectListItem { Text = "Srilanka", Value = "2" });
            li.Add(new SelectListItem { Text = "China", Value = "3" });
            li.Add(new SelectListItem { Text = "Austrila", Value = "4" });
            li.Add(new SelectListItem { Text = "USA", Value = "5" });
            li.Add(new SelectListItem { Text = "UK", Value = "6" });

            return li;
        }

        private List<SelectListItem> LoadStates()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Alabama", Value = "3" });
            li.Add(new SelectListItem { Text = "Alaska", Value = "4" });
            li.Add(new SelectListItem { Text = "Arizona", Value = "5" });
            li.Add(new SelectListItem { Text = "Arkansas", Value = "6" });
            li.Add(new SelectListItem { Text = "California", Value = "7" });
            li.Add(new SelectListItem { Text = "Colorado", Value = "8" });
            li.Add(new SelectListItem { Text = "Connecticut", Value = "9" });
            li.Add(new SelectListItem { Text = "Delaware", Value = "10" });
            li.Add(new SelectListItem { Text = "District of Columbia", Value = "11" });
            li.Add(new SelectListItem { Text = "Florida", Value = "12" });
            li.Add(new SelectListItem { Text = "Georgia", Value = "13" });
            li.Add(new SelectListItem { Text = "Guam", Value = "14" });
            li.Add(new SelectListItem { Text = "Hawaii", Value = "15" });
            li.Add(new SelectListItem { Text = "Idaho", Value = "16" });
            li.Add(new SelectListItem { Text = "Illinois", Value = "17" });
            li.Add(new SelectListItem { Text = "Indiana", Value = "18" });
            li.Add(new SelectListItem { Text = "Iowa", Value = "19" });
            li.Add(new SelectListItem { Text = "Kansas", Value = "20" });
            li.Add(new SelectListItem { Text = "Kentucky", Value = "21" });
            li.Add(new SelectListItem { Text = "Louisiana", Value = "22" });
            li.Add(new SelectListItem { Text = "Maine", Value = "23" });
            li.Add(new SelectListItem { Text = "Marshall Islands", Value = "24" });
            li.Add(new SelectListItem { Text = "Maryland", Value = "25" });
            li.Add(new SelectListItem { Text = "Massachusetts", Value = "26" });
            li.Add(new SelectListItem { Text = "Michigan", Value = "27" });
            li.Add(new SelectListItem { Text = "Minnesota", Value = "28" });
            li.Add(new SelectListItem { Text = "Mississippi", Value = "29" });
            li.Add(new SelectListItem { Text = "Missouri", Value = "30" });
            li.Add(new SelectListItem { Text = "Montana", Value = "31" });
            li.Add(new SelectListItem { Text = "Nebraska", Value = "32" });
            li.Add(new SelectListItem { Text = "Nevada", Value = "33" });
            li.Add(new SelectListItem { Text = "New Hampshire", Value = "34" });
            li.Add(new SelectListItem { Text = "New Jersey", Value = "35" });
            li.Add(new SelectListItem { Text = "New Mexico", Value = "36" });
            li.Add(new SelectListItem { Text = "New York", Value = "37" });
            li.Add(new SelectListItem { Text = "North Carolina", Value = "38" });
            li.Add(new SelectListItem { Text = "North Dakota", Value = "39" });
            li.Add(new SelectListItem { Text = "Northern Mariana Islands", Value = "40" });
            li.Add(new SelectListItem { Text = "Ohio", Value = "41" });
            li.Add(new SelectListItem { Text = "Oklahoma", Value = "42" });
            li.Add(new SelectListItem { Text = "Oregon", Value = "43" });
            li.Add(new SelectListItem { Text = "Palau", Value = "44" });
            li.Add(new SelectListItem { Text = "Pennsylvania", Value = "45" });
            li.Add(new SelectListItem { Text = "Puerto Rico", Value = "46" });
            li.Add(new SelectListItem { Text = "Rhode Island", Value = "47" });
            li.Add(new SelectListItem { Text = "South Carolina", Value = "48" });
            li.Add(new SelectListItem { Text = "South Dakota", Value = "49" });
            li.Add(new SelectListItem { Text = "Tennessee", Value = "50" });
            li.Add(new SelectListItem { Text = "Texas", Value = "51" });
            li.Add(new SelectListItem { Text = "Utah", Value = "52" });
            li.Add(new SelectListItem { Text = "Vermont", Value = "53" });
            li.Add(new SelectListItem { Text = "Virgin Islands", Value = "54" });
            li.Add(new SelectListItem { Text = "Virginia", Value = "55" });
            li.Add(new SelectListItem { Text = "Washington", Value = "56" });
            li.Add(new SelectListItem { Text = "West Virginia", Value = "57" });
            li.Add(new SelectListItem { Text = "Wisconsin", Value = "58" });
            li.Add(new SelectListItem { Text = "Wyoming", Value = "59" });

            return li;
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