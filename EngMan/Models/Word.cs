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
}