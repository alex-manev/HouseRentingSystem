using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastucture.Constants.DataConstants;

namespace HouseRentingSystem.Infrastucture.Data.Models
{
    [Comment("House to rent")]
    public class House
    {
        [Key]
        [Comment("House identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        [Comment("Title")]
        public string Title { get; set; } = string.Empty;


        [Required]
        [MaxLength(HouseAddressMaxLength)]
        [Comment("Address")]
        public string Address { get; set; } = string.Empty;



        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        [Comment("House description")]
        public string Description { get; set; } = string.Empty;



        [Required]
        [Comment("House image url")]
        public string ImageURL { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Monthly price")]
        //[Range(typeof(decimal), HouseRentingPriceMinimum, HouseRentingPriceMaximum, ConvertValueInInvariantCulture = true)]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Comment("Categor identifier")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;


        [Required]
        [Comment("Agent identifier")]
        public int AgentId { get; set; }

        public Agent Agent { get; set; } = null!;


        [Comment("Renter identifier")]
        public string? RenterId { get; set; }
    }
}
