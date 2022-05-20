using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class MasterServices
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MasterServices(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public async Task<Response> InsertMasterService(MasterService req)
        {
            var res = new Response()
            {
                StatusCode =(int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    unitofwork.Repository().Add(req);
                    int i = await unitofwork.SaveChangesAsync();
                    if (i>= 0 && i<20)
                    {
                        res.StatusCode =(int)ResponseStatus.Success;
                        res.ResponseText = "Successfull.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res; 
        }
        public async Task<Response> UpdateMasterService(MasterService req)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    unitofwork.Repository().Update(req);
                    int i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Update Successfull.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public async Task<Response> Delete(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<MasterService>(x => x.ServiceID == id);
                    unitofwork.Repository().Delete(data.FirstOrDefault());
                    int i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Deleted Successfull.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }
        public async Task<List<MasterService>> GetMasterServiceById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<MasterService>(x => x.ServiceID == id);
                    return data.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<MasterService>> GetAllService()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().Get<MasterService>();
                    return data.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<CompanyProfile> GetCompanyProfile()
        {
            try
            {
          
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().SingleOrDefaultAsync<CompanyProfile>();
                    return data;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<MessageTemplate> GetMessageTemplate()
        {
            try
            {

                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().SingleOrDefaultAsync<MessageTemplate>();
                    return data;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
