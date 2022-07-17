using GymApp.Data;
using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Controllers
{
    public class MembershipTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        string duration;
        string searchPhrase;
        public MembershipTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<MembershipTypeModel> objMembershipsList = _db.MembershipTypes;
            ViewBag.Duration = this.duration;
            ViewBag.SearchPhrase = this.searchPhrase;
            return View(objMembershipsList);
        }
        
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase, String durationFilter)
        {
            var membershipTypes=  _db.MembershipTypes;
            if (!(string.IsNullOrEmpty(SearchPhrase)) && !(string.IsNullOrEmpty(durationFilter)))
            {

                this.searchPhrase = SearchPhrase;
                this.duration = durationFilter;

                if (durationFilter.Equals("All"))
                {
                    return View("Index", await membershipTypes.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());

                }
                return View("Index", await membershipTypes.Where(mmtype=>mmtype.Name.Contains(SearchPhrase) && mmtype.Duration.Contains(durationFilter)).ToListAsync());

            }
            else if (!(string.IsNullOrEmpty(SearchPhrase)))
            {
                this.searchPhrase = SearchPhrase;

                return View("Index", await membershipTypes.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());

            }else if (!(string.IsNullOrEmpty(durationFilter)))
            {
                this.duration = durationFilter;

                if (durationFilter.Equals("All"))
                {
                    return View("Index", await membershipTypes.ToListAsync());
                }
                return View("Index", await membershipTypes.Where(j => j.Duration.Contains(durationFilter)).ToListAsync());

            }

            return View("Index", await membershipTypes.ToListAsync());
        }

    }
}
