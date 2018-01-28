using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EngMan.Models
{
    [DataContract]
    public class RuleModel
    {
        [Key]
        [DataMember]
        public int RuleId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Category { get; set; }
    }
}