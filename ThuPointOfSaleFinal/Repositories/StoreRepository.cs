using Microsoft.EntityFrameworkCore;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.App.Models;
using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Repositories
{
    public class StoreRepository : IStoreRepository
    {
       private  MyDbContext _db;

        public StoreRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<Store> AddStore(Store store)
        {
            await _db.Stores.AddAsync(store);
            await _db.SaveChangesAsync();
            return store;
        }

        public async Task DeleteStore(int id)
        {
            var store = await GetStoreById(id);
            _db.Stores.Remove(store);
           await  _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            return await _db.Stores.ToListAsync();
        }

        public async Task<Store> GetStoreById(int id)
        {
            return await _db.Stores.FindAsync(id);
        }

        public async Task<Store> UpdateStore(Store store)
        {
            _db.Stores.Update(store);
            await _db.SaveChangesAsync();
            return store;
        }
    }
}
