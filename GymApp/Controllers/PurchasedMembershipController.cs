using GymApp.Data;
using GymApp.Models;
using GymApp.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GymApp.Controllers
{
    public class PurchasedMembershipController : Controller
    {
        private readonly ApplicationDbContext _db;
        private static string name;
        private static string duration;
        private static string price;

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public PurchasedMembershipController(ApplicationDbContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [Authorize]
        public IActionResult Index(MembershipTypeModel myvar)
        {
            ViewBag.id=myvar.Id;
            ViewBag.name=myvar.Name;
            ViewBag.duration = myvar.Duration;
            ViewBag.price = myvar.Price;
            name = myvar.Name;
            duration=myvar.Duration;
            price = myvar.Price.ToString();
            return View("Index");
        }
        //[Authorize(Roles ="Admin,User")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchasedMembershipModel purchasedMembership)
        {
            int diff = 0;
            int var1, var2;
            if (purchasedMembership.MembershipTypeId == 1)
            {
                if (!(purchasedMembership.Amount.ToString().Equals("9.99")))
                {
                    Console.WriteLine(purchasedMembership.Amount.ToString());
                    ModelState.AddModelError("amount", "Amount is Incorrect");
                }
                diff = Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) - Int32.Parse(purchasedMembership.StartDate.Split("/")[1]);
                if (!(diff == 1))
                {
                    if(!(diff==-1 && purchasedMembership.StartDate.Split("/")[1].Equals("12") && purchasedMembership.EndDate.Split("/")[1].Equals("1")))
                    {
                        ModelState.AddModelError("startDate", "Date is Incorrect");
                        ModelState.AddModelError("endDate", "Date is Incorrect");

                    }
                    if((purchasedMembership.EndDate.Split("/")[0]!= purchasedMembership.StartDate.Split("/")[0]) || (purchasedMembership.EndDate.Split("/")[2] != purchasedMembership.StartDate.Split("/")[2] && diff!=-1))
                    {
                        ModelState.AddModelError("startDate", "Date is Incorrect");
                        ModelState.AddModelError("endDate", "Date is Incorrect");
                    }
                }
                if (purchasedMembership.EndDate.Split("/")[0].Length > 2 || purchasedMembership.EndDate.Split("/")[0].Length == 0 || purchasedMembership.EndDate.Split("/")[1].Length > 2 || purchasedMembership.EndDate.Split("/")[1].Length == 0 || purchasedMembership.EndDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.EndDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if (purchasedMembership.StartDate.Split("/")[0].Length > 2 || purchasedMembership.StartDate.Split("/")[0].Length == 0 || purchasedMembership.StartDate.Split("/")[1].Length > 2 || purchasedMembership.StartDate.Split("/")[1].Length == 0 || purchasedMembership.StartDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.StartDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
            }
            else if (purchasedMembership.MembershipTypeId == 2)
            {
                if (!(purchasedMembership.Amount.ToString().Equals("15.99")))
                {
                    ModelState.AddModelError("amount", "Amount is Incorrect");
                }
                diff = Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) - Int32.Parse(purchasedMembership.StartDate.Split("/")[1]);
                if (!(diff == 1))
                {
                    if (!(diff == -1 && purchasedMembership.StartDate.Split("/")[1].Equals("12") && purchasedMembership.EndDate.Split("/")[1].Equals("1")))
                    {
                        ModelState.AddModelError("startDate", "Date is Incorrect");
                        ModelState.AddModelError("endDate", "Date is Incorrect");

                    }
                    if ((purchasedMembership.EndDate.Split("/")[0] != purchasedMembership.StartDate.Split("/")[0]) || (purchasedMembership.EndDate.Split("/")[2] != purchasedMembership.StartDate.Split("/")[2] && diff!=-1))
                    {
                        ModelState.AddModelError("startDate", "Date is Incorrect");
                        ModelState.AddModelError("endDate", "Date is Incorrect");
                    }
                }
                if (purchasedMembership.EndDate.Split("/")[0].Length > 2 || purchasedMembership.EndDate.Split("/")[0].Length == 0 || purchasedMembership.EndDate.Split("/")[1].Length > 2 || purchasedMembership.EndDate.Split("/")[1].Length == 0 || purchasedMembership.EndDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.EndDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if (purchasedMembership.StartDate.Split("/")[0].Length > 2 || purchasedMembership.StartDate.Split("/")[0].Length == 0 || purchasedMembership.StartDate.Split("/")[1].Length > 2 || purchasedMembership.StartDate.Split("/")[1].Length == 0 || purchasedMembership.StartDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.StartDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
            }
            else if (purchasedMembership.MembershipTypeId==3)
            {
                if (!(purchasedMembership.Amount.ToString().Equals("89.99")))
                {
                    ModelState.AddModelError("amount", "Amount is Incorrect");
                }
                diff = Int32.Parse(purchasedMembership.EndDate.Split("/")[2]) - Int32.Parse(purchasedMembership.StartDate.Split("/")[2]);
                if (!(diff == 1))
                {
                    
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");

                    
                }
                if ((purchasedMembership.EndDate.Split("/")[0] != purchasedMembership.StartDate.Split("/")[0]) || (purchasedMembership.EndDate.Split("/")[1] != purchasedMembership.StartDate.Split("/")[1]))
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if (purchasedMembership.EndDate.Split("/")[0].Length > 2 || purchasedMembership.EndDate.Split("/")[0].Length == 0 || purchasedMembership.EndDate.Split("/")[1].Length > 2 || purchasedMembership.EndDate.Split("/")[1].Length == 0 || purchasedMembership.EndDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.EndDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if (purchasedMembership.StartDate.Split("/")[0].Length > 2 || purchasedMembership.StartDate.Split("/")[0].Length == 0 || purchasedMembership.StartDate.Split("/")[1].Length > 2 || purchasedMembership.StartDate.Split("/")[1].Length == 0 || purchasedMembership.StartDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.StartDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
            }
            else if (purchasedMembership.MembershipTypeId == 4)
            {
                if (!(purchasedMembership.Amount.ToString().Equals("159.99")))
                {
                    ModelState.AddModelError("amount", "Amount is Incorrect");
                }
                diff = Int32.Parse(purchasedMembership.EndDate.Split("/")[2]) - Int32.Parse(purchasedMembership.StartDate.Split("/")[2]);
                if (!(diff == 1))
                {

                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");


                }
                if ((purchasedMembership.EndDate.Split("/")[0] != purchasedMembership.StartDate.Split("/")[0]) || (purchasedMembership.EndDate.Split("/")[1] != purchasedMembership.StartDate.Split("/")[1]))
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if(purchasedMembership.EndDate.Split("/")[0].Length>2 || purchasedMembership.EndDate.Split("/")[0].Length == 0 || purchasedMembership.EndDate.Split("/")[1].Length > 2 || purchasedMembership.EndDate.Split("/")[1].Length == 0|| purchasedMembership.EndDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.EndDate.Split("/")[2])<2022|| Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.EndDate.Split("/")[1]) >31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
                if (purchasedMembership.StartDate.Split("/")[0].Length > 2 || purchasedMembership.StartDate.Split("/")[0].Length == 0 || purchasedMembership.StartDate.Split("/")[1].Length > 2 || purchasedMembership.StartDate.Split("/")[1].Length == 0 || purchasedMembership.StartDate.Split("/")[2].Length > 4 || Int32.Parse(purchasedMembership.StartDate.Split("/")[2]) < 2022 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 12 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[0]) < 0 || Int32.Parse(purchasedMembership.StartDate.Split("/")[1]) > 31)
                {
                    ModelState.AddModelError("startDate", "Date is Incorrect");
                    ModelState.AddModelError("endDate", "Date is Incorrect");
                }
            }
            if (ModelState.IsValid)
            {
                var userInfo = await userManager.FindByNameAsync(User.Identity.Name);
                var idUser = userInfo.Id;
                PurchasedMembershipModel p1 = new PurchasedMembershipModel
                {
                    StartDate = purchasedMembership.StartDate,
                    EndDate = purchasedMembership.EndDate,
                    Amount = purchasedMembership.Amount,
                    MembershipTypeId = purchasedMembership.MembershipTypeId,
                    UserId = idUser
                };
                _db.PurchasedMemberships.Add(p1);
                _db.SaveChanges();
                TempData["success"] = "Membership Purchased Successfully!";
                return RedirectToAction("Index", "MembershipType");
            }
            else
            {
                /*MembershipTypeModel m = new MembershipTypeModel();
                m.Duration = duration;
                m.Price = purchasedMembership.Amount;
                m.Id = purchasedMembership.MembershipTypeId;
                m.Name = name;*/
                
                MembershipTypeModel myvar = new MembershipTypeModel(purchasedMembership.MembershipTypeId, name, duration,float.Parse(price));
                TempData["error"] = "Unvalid information entered! Please leave default values unedited!";

                return Index(myvar);
            }
        }
    }
}
