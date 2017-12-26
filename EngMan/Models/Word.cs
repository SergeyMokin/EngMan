using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
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
}