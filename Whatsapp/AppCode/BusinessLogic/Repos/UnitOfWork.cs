using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;

namespace WAEFCore22.AppCode.BusinessLogic.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _databaseContext;

        public UnitOfWork(ApplicationContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IGenericRepository Repository()
        {
            return new GenericRepository(_databaseContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }

        public Task CommitAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }
    }
}
