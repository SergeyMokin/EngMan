using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EngMan.Models
{
    public class Message
    {
        [Key]
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public int SenderId { get; set; }

        [DataMember]
        public int BeneficiaryId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public System.DateTime Time { get; set; }
    }

    public class ReturnMessage
    {
        [Key]
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public UserView Sender { get; set; }

        [DataMember]
        public UserView Beneficiary { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public System.DateTime Time { get; set; }
    }
}