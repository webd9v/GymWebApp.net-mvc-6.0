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

        public MembershipTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<MembershipTypeModel> objMembershipsList = _db.MembershipTypes;
            return View(objMembershipsList);
        }
        
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return View("Index", await _db.MembershipTypes.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());
        }

    }
}
