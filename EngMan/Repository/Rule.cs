using System.Runtime.Serialization;

namespace EngMan.Repository
{
    [DataContract]
    public class Rule
    {
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