using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kafu.Model.Entities
{
    public partial class ReasonLookup
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("AR_name")]
        [StringLength(350)]
        public string? ArName { get; set; }
        [Column("EN-name")]
        [StringLength(350)]
        public string? EnName { get; set; }
    }
}