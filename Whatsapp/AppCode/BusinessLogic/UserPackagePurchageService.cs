using System;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.HelperClass;
using Whatsapp.Models;
using Whatsapp.Models.UtilityModel;

namespace Whatsapp.AppCode.BusinessLogic
{
    public class UserPackagePurchageService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UserPackagePurchageService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<Response> BuyPackage(Int64 id, string userId, string loggedInUser)
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
                    var PackageDetails = await unitofwork.Repository().FindAsync<MasterPackage>(x => (x.Id.Equals(id)));
                    MasterPackage masterPackage = PackageDetails.FirstOrDefault();
                    var IsPuchasedData = await unitofwork.Repository().FindAsync<UserPackageDetail>(x => (x.MasterPackageId.Equals(id) && x.UserId == Convert.ToInt32(userId)));
                    UserPackageDetail userPackageDetail = IsPuchasedData.FirstOrDefault();
                    if (userPackageDetail == null)
                    {
                        var userBlnc = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == Convert.ToInt32(userId));
                        UserBalance balance = userBlnc.FirstOrDefault();
                        if(masterPackage.Cost > balance.Balance)
                        {
                            res.StatusCode = (int)ResponseStatus.warning;
                            res.ResponseText = "Balance not sufficient. Please add fund.";
                            return res;
                        }
                        else
                        {
                            var helperService = new HelperService();
                            balance.PreviousBalance = balance.Balance;
                            balance.Balance = balance.Balance - masterPackage.Cost;
                            balance.ModifiedDate = DateTime.Now;
                            balance.ModifyBy = loggedInUser;
                            Ledger ledger = helperService.CalculateDebitLedger(balance, masterPackage.Cost);
                            userPackageDetail = new UserPackageDetail()
                            {
                                UserId = Convert.ToInt32(userId),
                                MasterPackageId = id,
                                EntryBy = loggedInUser,
                                CreatedDate = DateTime.Now
                            };
                            unitofwork.Repository().Add(userPackageDetail);
                            unitofwork.Repository().Add(ledger);
                            unitofwork.Repository().Update(balance);
                        }
                    }
                    else
                    {
                        res.StatusCode = (int)ResponseStatus.warning;
                        res.ResponseText = "Package already purchased.";
                        return res;
                    }
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
    }
}
