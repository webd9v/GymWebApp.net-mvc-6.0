using System;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class MembershipTypeModel
    {
        public MembershipTypeModel()
        {

        }
        public MembershipTypeModel(int id, string name, string duration, float price)
        {
            Id = id;
            Name = name;
            Duration = duration;
            Price = price;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }

    }
}
