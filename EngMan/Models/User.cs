﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EngMan.Models
{
    [DataContract]
    public class User
    {
        public User()
        {

        }

        public User(UserView _user)
        {
            Id = _user.Id;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            Email = _user.Email;
            Password = "";
            Role = _user.Role;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Role { get; set; }
    }

    [Serializable]
    [DataContract]
    public class UserLogin
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }

    [DataContract]
    public class UserView
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Role { get; set; }
    }

    [DataContract]
    public class UserConnect
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ConnectionId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Role { get; set; }
    }
}