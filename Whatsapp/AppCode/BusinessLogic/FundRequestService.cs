using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.HelperClass;
using Whatsapp.Models;
using Whatsapp.Models.UtilityModel;

namespace Whatsapp.AppCode.BusinessLogic
{
    public class FundRequestService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public FundRequestService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<Response> InsertFundRequest(UserFundRequest req)
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

        public async Task<Response> UpdateUserFundRequest(UserFundRequest req, string LoggedInUserId, string status)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            var helperService = new HelperService();
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var userfundRequest = await unitofwork.Repository().FindAsync<UserFundRequest>(x => x.Id == req.Id);
                    req = userfundRequest.FirstOrDefault();
                    req.LoggedInUserId = LoggedInUserId;
                    req.Status = status;
                    var currentBalance = req.RequestedAmount;
                    var adminBalnce = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == Convert.ToInt32(req.LoggedInUserId));
                    var userBalnce = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == req.UserId);
                    UserBalance userBalance = userBalnce.FirstOrDefault();
                    UserBalance adminBalance = adminBalnce.FirstOrDefault();
                    userBalance.PreviousBalance = userBalance.Balance;
                    adminBalance.PreviousBalance = adminBalance.Balance;
                    Ledger userLedger = new Ledger();
                    Ledger adminLedger = new Ledger();

                    unitofwork.Repository().Update(req);
                    if (req.Status.ToLower() == "approve")
                    {
                        userBalance.Balance = userBalance.Balance + currentBalance;
                        userBalance.ModifiedDate = DateTime.Now;
                        adminBalance.Balance = adminBalance.Balance - currentBalance;
                        adminBalance.ModifiedDate = DateTime.Now;
                        userLedger = helperService.CalculateCreditLedger(userBalance, currentBalance);
                        adminLedger = helperService.CalculateDebitLedger(adminBalance, currentBalance);
                        unitofwork.Repository().Update(userBalance);
                        unitofwork.Repository().Update(adminBalance);
                        unitofwork.Repository().Add(adminLedger);
                        unitofwork.Repository().Add(userLedger);
                    }

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

        public async Task<List<UserFundRequest>> GetAll()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().Get<UserFundRequest>(includeProperties: "WhatsappUser");
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
