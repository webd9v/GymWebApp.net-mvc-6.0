using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymApp.Data
{
    //inheriting from identitydbcontext instead of dbcontext enables login,register...
    //and identitydbcontext inherits already from dbcontext
    //we also need to use: using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MembershipTypeModel> MembershipTypes { get; set; }
        public DbSet<PurchasedMembershipModel> PurchasedMemberships { get; set; }
        public DbSet<GymApp.Models.RegisterModel> RegisterModel { get; set; }
        public DbSet<GymApp.Models.LoginModel> LoginModel { get; set; }
        public DbSet<GymApp.Models.CreateRoleModel> CreateRoleModel { get; set; }
        public DbSet<GymApp.Models.UserRoleModel> UserRoleModel { get; set; }

       
    }
}
