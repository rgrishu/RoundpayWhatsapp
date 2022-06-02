using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.UtilityModel;

namespace Whatsapp.AppCode.BusinessLogic
{
    public class PackageService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public PackageService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Response> InsertPackage(Package req)
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
        public async Task<Response> UpdatePackage(Package req)
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
                    var data = await unitofwork.Repository().FindAsync<Package>(x => x.Id == id);
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

        public async Task<List<Package>> GetPackageById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<Package>(x => x.Id == id);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<List<Package>> GetPackageList()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().Get<Package>(includeProperties: "MasterPackage,MasterService,MasterServiceFeatures");
                    foreach (var item in data.ToList())
                    {
                        item.MasterService = item.MasterService == null ? new MasterService() : item.MasterService;
                        item.MasterServiceFeatures = item.MasterServiceFeatures == null ? new MasterServiceFeatures() : item.MasterServiceFeatures;
                        item.MasterPackage = item.MasterPackage == null ? new MasterPackage() : item.MasterPackage;
                    }
                    return data.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<PackageView> GetPackageView(string userId)
        {
            PackageView packageView = new PackageView();
            Package package = new Package();
            var ms = new MasterPackageService(_unitOfWorkFactory);
            var mf = new MasterServices(_unitOfWorkFactory);
            var md = new MasterFeature(_unitOfWorkFactory);
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data1 = await unitofwork.Repository().Get<Package>(includeProperties: "MasterPackage,MasterService,MasterServiceFeatures");
                    var data2 = await unitofwork.Repository().Get<MasterService>(filter: a => a.IsFeature.Equals(false));
                    var data3 = await unitofwork.Repository().Get<MasterPackage>();
                    var data4 = await unitofwork.Repository().Get<MasterServiceFeatures>();
                    var data5 = await unitofwork.Repository().Get<UserPackageDetail>();
                    List<UserPackageDetail> UserPackageDetails = data5.ToList() ?? new List<UserPackageDetail>();
                    packageView.UserPuchasedPackageIds = UserPackageDetails ?? new List<UserPackageDetail>();
                    //foreach (var item in UserPackageDetails)
                    //{
                    //    if (item.UserId == Convert.ToInt32(userId))
                    //        packageView.UserPuchasedPackageIds.Add(item.MasterPackageId);
                    //}
                    packageView.Packages = data1.ToList();
                    packageView.MasterPackages = data3.ToList();
                    packageView.MasterServices = GetNewServicesList(data2.ToList(), data4.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }


            return packageView;
        }

        private List<MasterService> GetNewServicesList(List<MasterService> masterServices, List<MasterServiceFeatures> masterServiceFeatures)
        {
            if(masterServiceFeatures != null)
            {
                foreach (var item in masterServiceFeatures)
                {
                    MasterService masterService = new MasterService()
                    {
                        ServiceID = item.FeatureID,
                        ServiceName = item.FeatureName,
                        CheckIsFeature = true
                    };
                    masterServices.Add(masterService);
                }
            }
            return masterServices;
        }
    }
}
