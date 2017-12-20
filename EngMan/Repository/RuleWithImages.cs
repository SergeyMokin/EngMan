using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EngMan.Repository
{
    [DataContract]
    public class RuleWithImages
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Rule Rule { get; set; }
        [DataMember]
        public IEnumerable<RulesImage> RulesImages { get; set; }
    }
}