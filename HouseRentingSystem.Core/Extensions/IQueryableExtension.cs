using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastucture.Data.Models;

namespace System.Linq
{
    public static class IQueryableHouseExtension
    {
        public static IQueryable<HouseServiceModel> ProjectToHouseServiceModel(this IQueryable<House> houses)
        {
            return houses
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Address = h.Address,
                    ImageUrl = h.ImageURL,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                }) ;
        }
    }
}
