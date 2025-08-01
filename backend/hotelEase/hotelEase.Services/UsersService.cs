using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class UsersService : IUsersService
    {
        public HotelEaseContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public UsersService(HotelEaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        
        public List<Model.User> GetList()
        {

            List<Model.User> result = new List<Model.User>();
            var list = Context.Users.ToList();

            //list.ForEach(x => result.Add( new Model.User()
            //{
            //    Email = x.Email,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    Username = x.Username,
            //    PhoneNumber = x.PhoneNumber,
            //    IsActive = x.IsActive,
            //} ));

            result = Mapper.Map(list, result);
            

            return result;
        }

        public Model.User Insert(UsersInsertRequest request)
        {
            if(request.Password != request.ConfirmPassword)
            {
                throw new Exception("Password and CofirmPassword must be same");
            }

            Database.User entity = new Database.User();
            Mapper.Map(request, entity);

            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);

            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<Model.User>(entity);
        }
        public Model.User Update(int id, UsersUpdateRequest request)
        {
            var entity = Context.Users.Find(id);

            Mapper.Map(request, entity);

            if(request.Password != null)
            {
                if (request.Password != request.ConfirmPassword)
                {
                    throw new Exception("Password and CofirmPassword must be same");
                }
                entity.PasswordSalt = GenerateSalt();
                entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);
            }

            Context.SaveChanges();

            return Mapper.Map<Model.User>(entity);
        }


        public static string GenerateSalt()
        {
            var byteArray = RNGCryptoServiceProvider.GetBytes(16);


            return Convert.ToBase64String(byteArray);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

    }
}
