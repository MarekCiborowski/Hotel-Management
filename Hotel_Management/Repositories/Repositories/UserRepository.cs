using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace Repositories.Repositories
{
    public class UserRepository
    {
        private DatabaseContext db;

        public UserRepository (DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public User GetUser(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.Users.FirstOrDefault(a => a.Identity == id);
        }

        public List<User> GetAccountsToList()
        {
            return db.Users.ToList();
        }

        public User GetUser(string login, string password)
        {
            string hashedPassword = HashPassword(password);
            User user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == hashedPassword);

            return user;
        }


        public User AddUser(User user)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    user.Password = HashPassword(user.Password);
                    User createdUser = db.Users.Add(user);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return createdUser;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public User EditUser(User editedUser)
        {

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedUser).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return editedUser;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return editedUser;
                }
            }

        }

        public void RemoveUser(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    User user = db.Users.FirstOrDefault(u => u.Identity == id);

                    //db.Reservations.RemoveRange(db.Reservations.Where(r => r.UserId == id));

                    db.Users.Remove(user);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public string HashPassword(string password)
        {
            MD5 hash = MD5.Create();

            return GetMd5Hash(hash, password);
        }

        private static string GetMd5Hash(MD5 hash, string input)
        {
            // Konwertowanie stringa do tablicy bajtów i wyliczanie hashowania
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Stworzenie StringBuildera do przechowywania bajtów i stworzenie stringa
            StringBuilder sBuilder = new StringBuilder();

            // Każdy bajt jest haszowany i formatowany do postaci ciągu szesnastkowego
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Zwracanie zahaszowanego stringa
            return sBuilder.ToString();
        }

        public bool IsEmailCorrect(string email)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                if (IsValidEmail(email))
                    return true;
            return false;


        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var check = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsLoginFree(string login)
        {
            var user = db.Users.FirstOrDefault(t => t.Login == login);
            if (user == null)
                return true;
            return false;
        }

        public User ConfirmUser(int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentNullException("Null argument");
            }

            var userToValidate = db.Users.FirstOrDefault(u => u.Identity == id);
            userToValidate.IsConfirmed = true;
            this.EditUser(userToValidate);
            return userToValidate;
        }

        public List<User> GetUnconfirmedUsers()
        {
            var unconfirmedUsers = db.Users.Where(u => !u.IsConfirmed).ToList();

            return unconfirmedUsers;
        }

        public List<User> GetConfirmedUsers()
        {
            var confirmedUsers = db.Users.Where(u => u.IsConfirmed).ToList();

            return confirmedUsers;
        }

        public List<User> GetAllUsers()
        {
            return this.db.Users.ToList();
        }

        public User GetAdminUserWithLeastUserConversations()
        {
            var adminUserWithLeastUserConversations = db.Users.FirstOrDefault(u => db.UserConversations
                .Where(uc => db.Users.Where(us => us.RoleId == RolesEnum.Admin).Select(us => us.Identity).Contains(uc.UserID)).GroupBy(uc => uc.UserID)
                    .Select(group => new
                    {
                        UserId = group.Key,
                        Count = group.Count()
                    }).OrderByDescending(g => g.Count).First().UserId == u.Identity);

            return adminUserWithLeastUserConversations;
        }

    }

}
