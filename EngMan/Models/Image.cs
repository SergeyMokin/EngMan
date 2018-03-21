using System.Runtime.Serialization;

namespace EngMan.Models
{
    public class Image
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Data { get; set; }
    }
}