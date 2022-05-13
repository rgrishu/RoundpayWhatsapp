using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class UsersService 
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly UserManager<WhatsappUser> _userManager;
        private ApplicationContext _appcontext;

        public UsersService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<WhatsappUser> userManager, ApplicationContext appcontext)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
            _appcontext = appcontext;
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
            catch (Exception)
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response> AddUser(Users user)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                var newUser = new WhatsappUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.PhoneNumber, Name = user.Name, EmailConfirmed = true };
                var password = "Aaz@asda89";
                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {

                    var IsSucceeded = await _userManager.AddToRoleAsync(newUser, user.Role);
                    if (IsSucceeded.Succeeded)
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

        public async Task<Response> UpdateUsers(Users user)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            try
            {
                WhatsappUser ws = new WhatsappUser();
                ws = await _userManager.FindByIdAsync(user.Id.ToString());
                ws.Email = user.Email;
                ws.UserName = user.Email;
                ws.Name = user.Name;
                ws.PhoneNumber = user.PhoneNumber;
                var IsSucceeded = await _userManager.UpdateAsync(ws);
                var rolename = await _userManager.GetRolesAsync(ws);
                if (rolename[0].ToString().ToLower() != user.Role.ToString().ToLower())
                {
                    await _userManager.RemoveFromRoleAsync(ws, rolename[0].ToString());
                    await _userManager.AddToRoleAsync(ws, user.Role);
                }
                _appcontext.SaveChanges();
                if (IsSucceeded.Succeeded)
                {
                    res.StatusCode = (int)ResponseStatus.Success;
                    res.ResponseText = "Update Successfull.";
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
                WhatsappUser ws = new WhatsappUser();
                ws = await _userManager.FindByIdAsync(id.ToString());
                var response = await _userManager.DeleteAsync(ws);
                if (response.Succeeded)
                {
                    res.StatusCode = (int)ResponseStatus.Success;
                    res.ResponseText = "Deleted Successfull.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }
    }
}
