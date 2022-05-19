using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;
using Whatsapp.Services.Interface;

namespace Whatsapp.Services
{
    public class MasterWebsiteService :  IMasterWebsiteService
    {
        private readonly ApplicationContext _dbContext;
        public MasterWebsiteService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MasterWebsiteViewModel> masterWebsite()
        {
            MasterWebsiteViewModel result = await _dbContext.MasterWebsite
                 .Join(_dbContext.CompanyProfile, x => x.Id, y => y.WID, (x, y) => new { x, y })
                 .Select(m => new MasterWebsiteViewModel
                 {
                     Id = m.x.Id,
                     WebsiteName = m.x.WebsiteName,
                     ComapnyName = m.y.Name,
                     CreatedDate = m.x.CreatedDate
                 }).FirstOrDefaultAsync();
            return result ?? new MasterWebsiteViewModel();
        }
    }
}
