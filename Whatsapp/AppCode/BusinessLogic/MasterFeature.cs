using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;

namespace Whatsapp.AppCode.BusinessLogic
{
    public class MasterFeature
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MasterFeature(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Response> InsertMasterFeature(MasterServiceFeatures req)
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
                    unitofwork.Repository().Add(req);
                    int i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Successfull.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }
        public async Task<Response> UpdateMasterFeature(MasterServiceFeatures req)
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
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }


        public async Task<List<MasterServiceFeatures>> GetMasterFeatureById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<MasterServiceFeatures>(x => x.FeatureID == id);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<IEnumerable<MasterServiceFeatures>> GetAllFeature()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAllRecords<MasterServiceFeatures>();
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
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
                    var data = await unitofwork.Repository().FindAsync<MasterServiceFeatures>(x => x.FeatureID == id);
                    unitofwork.Repository().Delete(data.FirstOrDefault());
                    int i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Deleted Successfull.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return res;
        }


        public async Task<List<MasterServiceFeatures>> GetFeatureList()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().Get<MasterServiceFeatures>(orderBy: q => q.OrderBy(d => d.FeatureName), includeProperties: "MasterService");
                    return data.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
