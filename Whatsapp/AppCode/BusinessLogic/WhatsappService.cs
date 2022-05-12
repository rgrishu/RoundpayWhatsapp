using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class WhatsappService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public WhatsappService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<List<WhatsappUser>> GetAllUsersById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<WhatsappUser>(x => x.Id == id);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<WhatsappUser>> GetAllUsers ()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAllRecords<WhatsappUser>();
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
