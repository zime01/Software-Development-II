using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public interface IUsersService
    {
        List<Model.User> GetList();
        Model.User Insert(UsersInsertRequest request);
        Model.User Update(int id,UsersUpdateRequest request);
    }
}
