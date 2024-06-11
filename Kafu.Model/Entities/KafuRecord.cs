using System.ComponentModel.DataAnnotations.Schema;

namespace Kafu.Model.Entities
{
    public partial class KafuRecord
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("Creator_ID")]
        public string? CreatorId { get; set; }
        [Column("Recipient_ID")]
        public string? RecipientId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column("ReasonID")]
        public int? ReasonId { get; set; }
        //[Column(TypeName = "text")]
        public string? Comment { get; set; }

        [NotMapped]
        public string? CreatorName { get; set; }
        [NotMapped]
        public string? RecName { get; set; }

        public string? Creator_name { get; set; }
        public string? Recipient_name { get; set; }
        public string? isMangerCC { get; set; }
    }
}
