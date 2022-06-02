using Microsoft.SqlServer.Management.Smo;
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
    public class UserBalanceService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public UserBalanceService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<Response> InsertUserBalance(UserBalance req)
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
                    req.CreatedDate = DateTime.Now;
                    unitofwork.Repository().Add(req);
                    int i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
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
        public async Task<Response> UpdateUserBalance(UserBalance userBalance, int loggedInUserId)
        {
            var helperService = new HelperService();
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var currentBalance = userBalance.Balance;
                    userBalance.Balance = userBalance.PreviousBalance + currentBalance;
                    userBalance.ModifiedDate = DateTime.Now;
                    var data = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == loggedInUserId);
                    UserBalance adminbalance = data.FirstOrDefault();
                    adminbalance.PreviousBalance = adminbalance.Balance;
                    adminbalance.Balance = adminbalance.Balance - (currentBalance);
                    adminbalance.ModifiedDate = DateTime.Now;
                    Ledger userLedger = helperService.CalculateCreditLedger(userBalance, currentBalance);
                    Ledger adminLedger = helperService.CalculateDebitLedger(adminbalance, currentBalance);
                    unitofwork.Repository().Update(userBalance);
                    unitofwork.Repository().Update(adminbalance);
                    unitofwork.Repository().Add(adminLedger);
                    unitofwork.Repository().Add(userLedger);
                    var i = await unitofwork.SaveChangesAsync();
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
        public async Task<Response> UpdateUserForAddFund(UserBalance userBalance)
        {
            var helperService = new HelperService();
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var getUserData = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == userBalance.UserId);
                    UserBalance adminBalance = getUserData.FirstOrDefault();
                    adminBalance.PreviousBalance = adminBalance.Balance;
                    adminBalance.Balance = userBalance.Balance + adminBalance.Balance;
                    adminBalance.ModifiedDate = DateTime.Now;
                    adminBalance.ModifyBy = userBalance.ModifyBy;
                    Ledger adminLedger = helperService.CalculateCreditLedger(adminBalance, userBalance.Balance);
                    unitofwork.Repository().Update(adminBalance);
                    unitofwork.Repository().Add(adminLedger);
                    var i = await unitofwork.SaveChangesAsync();
                    if (i >= 0 && i < 20)
                    {
                        res.StatusCode = (int)ResponseStatus.Success;
                        res.ResponseText = "Update Successfull.";
                        res.ResponseAmt = adminBalance.Balance.ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }
        public async Task<List<UserBalance>> GetUserBalanceById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<UserBalance>(x => x.UserId == id);
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
