using hotelEase.Model;
using hotelEase.Model.Requests;
using hotelEase.Model.SearchObjects;
using hotelEase.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hotelEase.Services
{
    public class UsersService : BaseCRUDService<Model.User, UsersSearchObject, Database.User, UsersInsertRequest, UsersUpdateRequest>, IUsersService
    {
        ILogger<UsersService> _logger;
        public INotificationsService _notificationsService { get; set; }
        public UsersService(HotelEaseContext context, IMapper mapper, ILogger<UsersService> logger, INotificationsService notificationsService ) : base(context, mapper)
        {
            _logger = logger;
            _notificationsService = notificationsService;
        }

        public override IQueryable<Database.User> AddInclude(UsersSearchObject search, IQueryable<Database.User> query)
        {
            if (search.IncludeRoles)
            {
                query = query.Include(u => u.Roles);
            }

            return query;
        }
        public override IQueryable<Database.User> AddFilter(UsersSearchObject searchObject, IQueryable<Database.User> query)
        {
           query =  base.AddFilter(searchObject, query);

            if (!string.IsNullOrWhiteSpace(searchObject?.FirstNameGTE))
            {
                query = query.Where(x => x.FirstName.StartsWith(searchObject.FirstNameGTE));
            }

            if (!string.IsNullOrWhiteSpace(searchObject?.LastNameGTE))
            {
                query = query.Where(x => x.LastName.StartsWith(searchObject.LastNameGTE));
            }

            if (!string.IsNullOrWhiteSpace(searchObject?.Email))
            {
                query = query.Where(x => x.Email == searchObject.Email);
            }

            if (!string.IsNullOrWhiteSpace(searchObject.Username))
            {
                query = query.Where(x => x.Username == searchObject.Username);
            }

            if (searchObject?.Page.HasValue == true && searchObject?.PageSize.HasValue == true)
            {
                query = query.Skip(searchObject.Page.Value * searchObject.PageSize.Value).Take(searchObject.PageSize.Value);
            }

            if (searchObject?.IncludeRoles == true)
                query = query.Include(u => u.Roles);

            if (searchObject.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == searchObject.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchObject.Role))
            {
                query = query.Where(x => x.Roles.Any(r => r.Name == searchObject.Role));
            }

            return query;

        }

        //public List<Model.User> GetList(UsersSearchObject searchObject)
        //{

        //    List<Model.User> result = new List<Model.User>();


        //    var query = Context.Users.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(searchObject?.FirstNameGTE))
        //    {
        //        query = query.Where(x=> x.FirstName.StartsWith(searchObject.FirstNameGTE));
        //    }

        //    if (!string.IsNullOrWhiteSpace(searchObject?.LastNameGTE))
        //    {
        //        query = query.Where(x => x.LastName.StartsWith(searchObject.LastNameGTE));
        //    }

        //    if (!string.IsNullOrWhiteSpace(searchObject?.Email))
        //    {
        //        query = query.Where(x => x.Email == searchObject.Email);
        //    }

        //    if (!string.IsNullOrWhiteSpace(searchObject.Username))
        //    {
        //        query = query.Where(x=> x.Username == searchObject.Username);
        //    }

        //    if(searchObject?.Page.HasValue == true && searchObject?.PageSize.HasValue == true)
        //    {
        //        query = query.Skip(searchObject.Page.Value * searchObject.PageSize.Value).Take(searchObject.PageSize.Value);
        //    }

        //    var list = query.ToList();

        //    //list.ForEach(x => result.Add( new Model.User()
        //    //{
        //    //    Email = x.Email,
        //    //    FirstName = x.FirstName,
        //    //    LastName = x.LastName,
        //    //    Username = x.Username,
        //    //    PhoneNumber = x.PhoneNumber,
        //    //    IsActive = x.IsActive,
        //    //} ));

        //    result = Mapper.Map(list, result);


        //    return result;
        //}

        public override void BeforeInsert(UsersInsertRequest request, Database.User entity)
        {
            _logger.LogInformation($"Adding user: {entity.Username}");
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Password and CofirmPassword must be same");
            }

            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);

            base.BeforeInsert(request, entity);
        }

        public override void BeforeUpdate(UsersUpdateRequest request, Database.User entity)
        {
            base.BeforeUpdate(request, entity);

            if (!string.IsNullOrEmpty(request.Password))
            {
                if (request.Password != request.ConfirmPassword)
                {
                    throw new Exception("Password and CofirmPassword must be same");
                }

                if (!string.IsNullOrEmpty(request.OldPassword))
                {
                    var currentHash = GenerateHash(entity.PasswordSalt, request.OldPassword);
                    if (currentHash != entity.PasswordHash)
                    {
                        throw new UnauthorizedAccessException("Incorrect old password");
                    }
                }

                entity.PasswordSalt = GenerateSalt();
                entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);
            }
        }

        //public Model.User Insert(UsersInsertRequest request)
        //{
        //    if(request.Password != request.ConfirmPassword)
        //    {
        //        throw new Exception("Password and CofirmPassword must be same");
        //    }

        //    Database.User entity = new Database.User();
        //    Mapper.Map(request, entity);

        //    entity.PasswordSalt = GenerateSalt();
        //    entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);

        //    Context.Add(entity);
        //    Context.SaveChanges();

        //    return Mapper.Map<Model.User>(entity);
        //}
        //public Model.User Update(int id, UsersUpdateRequest request)
        //{
        //    var entity = Context.Users.Find(id);

        //    Mapper.Map(request, entity);

        //    if(request.Password != null)
        //    {
        //        if (request.Password != request.ConfirmPassword)
        //        {
        //            throw new Exception("Password and CofirmPassword must be same");
        //        }
        //        entity.PasswordSalt = GenerateSalt();
        //        entity.PasswordHash = GenerateHash(entity.PasswordSalt, request.Password);
        //    }

        //    Context.SaveChanges();

        //    return Mapper.Map<Model.User>(entity);
        //}


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

        public Model.User Login(string username, string password)
        {
            var entity = Context.Users.Include(x=>x.Roles).FirstOrDefault(x => x.Username == username);

            if(entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.PasswordSalt, password);

            if(hash != entity.PasswordHash)
            {
                return null; 
            }

            return Mapper.Map<Model.User>(entity);
        }

        public Model.User Register(UsersInsertRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new Exception("Passwords do not match!");

            if (Context.Users.Any(u => u.Username == request.Username || u.Email == request.Email))
                throw new Exception("User with this username or email already exists!");

            // koristi Insert() iz BaseCRUDService (hash/salt ide u BeforeInsert)
            var user = Insert(request);

            try
            {
                // 📧 priprema email notifikacije
                var message = new NotificationMessage
                {
                    Type = "email",
                    To = user.Email,
                    Subject = "Welcome to HotelEase 🎉",
                    Body = $"Hello {user.FirstName},<br/><br/>Your registration was successful!<br/>Welcome to HotelEase 🏨."
                };

                _notificationsService.SendAndStoreNotificationAsync(message, user.Id).Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send welcome email for user {Username}", user.Username);
            }

            return user;
        }
    

        public Model.User GetCurrentUser(string username)
        {
            var entity = Context.Users
                .Include(x => x.Roles)
                .FirstOrDefault(x => x.Username == username);

            if (entity == null)
                return null;

            return Mapper.Map<Model.User>(entity);
        }

        public void ChangeUserRole(int userId, string newRole)
        {
            var userEntity = Context.Users
                                    .Include(u => u.Roles) // učitaj kolekciju uloga
                                    .FirstOrDefault(u => u.Id == userId);

            if (userEntity == null)
                throw new Exception("User not found");

            // ukloni sve postojeće uloge
            userEntity.Roles.Clear();

            // pronađi ili kreiraj novu rolu
            var roleEntity = Context.Roles.FirstOrDefault(r => r.Name == newRole);
            if (roleEntity == null)
            {
                roleEntity = new Database.Role { Name = newRole };
                Context.Roles.Add(roleEntity);
            }

            // dodaj novu rolu korisniku
            userEntity.Roles.Add(roleEntity);

            Context.SaveChanges();
        }

        public void ChangeStatus(int id, bool isActive)
        {
            var user = Context.Users.Find(id);
            if (user == null)
                throw new Exception("User not found");

            user.IsActive = isActive;
            Context.SaveChanges();
        }
    }
}
