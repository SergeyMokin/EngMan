using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace EngMan.Models
{
    [DataContract]
    public class User
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
        public string Password { get; set; }
        [DataMember]
        public string Role { get; set; }
    }

    [Serializable]
    [DataContract]
    public class UserLogin
    {
        [DataMember]
        public string grant_type { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
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