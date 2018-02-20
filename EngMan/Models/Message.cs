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

        [DataMember]
        public bool CheckReadMes { get; set; }
    }

    public class ReturnMessageWithTheQueryBD
    {
        [Key]
        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public int SenderId { get; set; }
        [DataMember]
        public string SenderFirstName { get; set; }
        [DataMember]
        public string SenderLastName { get; set; }
        [DataMember]
        public string SenderEmail { get; set; }
        [DataMember]
        public string SenderRole { get; set; }

        [DataMember]
        public int BeneficiaryId { get; set; }
        [DataMember]
        public string BeneficiaryFirstName { get; set; }
        [DataMember]
        public string BeneficiaryLastName { get; set; }
        [DataMember]
        public string BeneficiaryEmail { get; set; }
        [DataMember]
        public string BeneficiaryRole { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public System.DateTime Time { get; set; }

        [DataMember]
        public bool CheckReadMes { get; set; }
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

        [DataMember]
        public bool CheckReadMes { get; set; }
    }
}