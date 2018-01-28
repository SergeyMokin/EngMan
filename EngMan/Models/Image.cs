using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;

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