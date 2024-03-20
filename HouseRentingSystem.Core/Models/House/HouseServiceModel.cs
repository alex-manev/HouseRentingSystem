using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Core.Constants.MessageConstants;
using static HouseRentingSystem.Infrastucture.Constants.DataConstants;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseServiceModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(HouseAddressMaxLength,
            MinimumLength = HouseAddressMinLength,
            ErrorMessage = LengthMessage)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal), HouseRentingPriceMinimum, HouseRentingPriceMaximum,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Price must be a positive number and less than {2} leva")]
        [Display(Name = "Price per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
        

    }
}