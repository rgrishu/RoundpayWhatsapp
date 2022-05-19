using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Services.Interface
{
    public interface IMasterWebsiteService
    {
        Task<MasterWebsiteViewModel> masterWebsite();
    }
}
