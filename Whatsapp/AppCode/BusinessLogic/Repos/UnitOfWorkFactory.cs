using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;

namespace WAEFCore22.AppCode.BusinessLogic.Repos
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private ApplicationContext _AppDbContext;

        public UnitOfWorkFactory(ApplicationContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_AppDbContext);
        }
    }
}
