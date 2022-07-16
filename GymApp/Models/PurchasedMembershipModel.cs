using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.Models
{
    public class PurchasedMembershipModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public string StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]

        public string EndDate { get; set; }
        [Required]
        public float Amount { get; set; }
        

        [ForeignKey("MembershipTypeModel")]
        public int MembershipTypeId { get; set; }
        public MembershipTypeModel MembershipTypeModel { get; set; }
      

    }
}
