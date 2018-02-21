using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace EngMan.Models
{
    [DataContract]
    public class Word
    {
        [Key]
        [DataMember]
        public int WordId { get; set; }
        [DataMember]
        public string Original { get; set; }
        [DataMember]
        public string Translate { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Transcription { get; set; }
}

    public class MapWord
    {
        [Key]
        [DataMember]
        public int WordId { get; set; }
        [DataMember]
        public string Original { get; set; }
        [DataMember]
        public IEnumerable<string> Translate { get; set; }
        [DataMember]
        public string Category { get; set; }
    }

    public class UserWord
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int WordId { get; set; }
    }

    public class UserDictionary
    {
        [DataMember]
        public UserView User { get; set; }
        [DataMember]
        public IEnumerable<Word> Words { get; set; }
    }

    public class UserWordSqlScript
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public int WordId { get; set; }
        [DataMember]
        public string Original { get; set; }
        [DataMember]
        public string Translate { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Transcription { get; set; }
    }
}