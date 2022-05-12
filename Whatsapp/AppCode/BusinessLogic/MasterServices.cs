﻿using System;
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }
            return res;
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
