using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;

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
    [DataContract]
    public class RuleImage
    {
        [Key]
        [DataMember]
        public int ImageId { get; set; }
        [DataMember]
        public int RuleId { get; set; }
        [DataMember]
        public byte[] ImageData { get; set; }
        [DataMember]
        public string ImageType { get; set; }
    }
    [DataContract]
    public class Rule
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public RuleModel RuleModel { get; set; }
        [DataMember]
        public IEnumerable<RuleImage> RulesImages { get; set; }
    }
}