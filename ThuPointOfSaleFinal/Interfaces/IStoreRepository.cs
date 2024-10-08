﻿using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllStores();
        Task<Store> GetStoreById(int id);
        Task<Store> AddStore(Store store);
        Task<Store> UpdateStore(Store store);
        Task DeleteStore(int id);
    }
}
