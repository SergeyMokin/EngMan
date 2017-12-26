using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace EngMan.Models
{
    [DataContract]
    public class SentenceTask
    {
        [Key]
        [DataMember]
        public int SentenceTaskId { get; set; }
        [DataMember]
        public string Sentence { get; set; }
        [DataMember]
        public string Category { get; set; }
    }
}