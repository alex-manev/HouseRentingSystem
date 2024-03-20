using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastucture.Data.Common;
using HouseRentingSystem.Infrastucture.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly IRepository repository;

        public HouseService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<HouseQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var housesToShow = repository.AllReadOnly<House>();

            if (category != null)
            {
                housesToShow = housesToShow
                    .Where(h => h.Category.Name == category);
            }

            if (searchTerm != null)
            {
                string normalized = searchTerm.ToLower();
                housesToShow = housesToShow
                    .Where(h => (h.Title.ToLower().Contains(normalized)  ||
                    h.Address.ToLower().Contains(normalized) || h.Description.ToLower().Contains(normalized)));
            }

            housesToShow = sorting switch
            {
                HouseSorting.Price => housesToShow.OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesToShow
                .OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.Id),
                _ => housesToShow
                .OrderByDescending(h => h.Id)
            };

            var houses = await housesToShow
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .ProjectToHouseServiceModel()
                .ToListAsync();

            int totalHouses = await housesToShow.CountAsync();

            return new HouseQueryServiceModel()
            {
                Houses = houses,
                TotalHouseCount = totalHouses
            };
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => new HouseCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
        {
            return await repository.AllReadOnly<House>()
                .Where(h => h.AgentId == agentId)
                .ProjectToHouseServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<House>()
                 .Where(h => h.RenterId == userId)
                .ProjectToHouseServiceModel()
                .ToListAsync();

        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateAsync(HouseFormModel model, int agentId)
        {
            House house = new House()
            {
                Address= model.Address,
                AgentId = agentId,
                Description= model.Description,
                ImageURL = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                Title = model.Title,
            };

            await repository.AddAsync(house);
            await repository.SaveChangesAsync();

            return house.Id;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await repository.AllReadOnly<House>().AnyAsync(h => h.Id == id);
        }

        public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<House>()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsServiceModel()
                {
                    Id= id,
                    Address = h.Address,
                    Agent = new AgentServiceModel()
                    {
                        Email = h.Agent.User.Email,
                        PhoneNumber = h.Agent.User.PhoneNumber
                    },
                    Category = h.Category.Name,
                    Description = h.Description,
                    ImageUrl = h.ImageURL,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .FirstAsync();

        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
        {
            return await repository.
                AllReadOnly<House>()
                .OrderByDescending(h => h.Id)
                .Take(3)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id = h.Id,
                    ImageUrl = h.ImageURL,
                    Title = h.Title,
                }).ToListAsync();
        }

        
    }
}
