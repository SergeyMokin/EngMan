using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EngMan.Repository
{
    [DataContract]
    public class RulesImage
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
}