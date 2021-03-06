﻿using DataAccessLayer;
using DomainObjects.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(DatabaseContext context)
        {
            this.userRepository = new UserRepository(context);
        }

        public User AddUser(User user)
        {
            user.RoleId = RolesEnum.RegularUser;
            this.userRepository.AddUser(user);
            return user;
        }

        public User AddAdmin(User user)
        {
            user.RoleId = RolesEnum.Admin;
            user.IsConfirmed = true;
            this.userRepository.AddUser(user);
            return user;
        }

        public User GetUser(string login, string password)
        {
            var user = this.userRepository.GetUser(login, password);
            return user;
        }

        public User EditUser(User editedUser)
        {
            this.userRepository.EditUser(editedUser);
            return editedUser;
        }

        public void RemoveUser(int? id)
        {
            this.userRepository.RemoveUser(id);
        }

        public bool IsLoginFree (string login)
        {
            return this.userRepository.IsLoginFree(login);
        }

        public bool IsEmailCorrect (string email)
        {
            return this.userRepository.IsEmailCorrect(email);
        }

        public void ConfirmUser(int userId)
        {
            this.userRepository.ConfirmUser(userId);
        }

        public List<User> GetUnconfirmedUsers()
        {
            return this.userRepository.GetUnconfirmedUsers();
        }

        public List<User> GetConfirmedUsers()
        {
            return this.userRepository.GetConfirmedUsers();
        }

        public List<User> GetAllUsers()
        {
            return this.userRepository.GetAllUsers();
        }

        public List<User> GetAllConfirmedUsersOfType(RolesEnum role)
        {
            var allUsers = this.userRepository.GetAllUsers();
            return allUsers.Where(au => au.RoleId == role && au.IsConfirmed).ToList();
        }

        public User GetUserById(int userId)
        {
            return this.userRepository.GetUser(userId);
        }


        public User GetAdminUserWithLeastUserConversations()
        {
            return this.userRepository.GetAdminUserWithLeastUserConversations();
        }

        public string HashPassword(string password)
        {
            return this.userRepository.HashPassword(password);
        }

    }
}
