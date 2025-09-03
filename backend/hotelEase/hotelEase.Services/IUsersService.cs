using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IUsersService : ICRUDService<Model.User, UsersSearchObject, UsersInsertRequest, UsersUpdateRequest>
    {
        //List<Model.User> GetList(UsersSearchObject searchObject);
        //Model.User Insert(UsersInsertRequest request);
        //Model.User Update(int id,UsersUpdateRequest request);
        Model.User Login(string username, string password);
        Model.User Register(UsersInsertRequest request);
        Model.User GetCurrentUser(string username);
        void ChangeUserRole(int userId, string newRole);
    }
}
