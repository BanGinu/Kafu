using System.ComponentModel.DataAnnotations.Schema;

namespace Kafu.Model.Entities
{
    public partial class Employee
    {
        [Column("ID")]
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string? Department { get; set; }
        // optional email because its optional in Prod DB
        public string? Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? Flags { get; set; }
        public string? ArFullName { get; set; }
        public string? EnFullName { get; set; }
        public string? AR_F_NAME { get; set; }
        public string? AR_L_NAME { get; set; }
        public string? EN_F_NAME { get; set; }
        public string? EN_L_NAME { get; set; }
        public string? PhoneExtention { get; set; }
        public string? ArPosition { get; set; }
        public string? EnPosition { get; set; }
        public string? Location { get; set; }
        [Column("MangerID")]
        public string? MangerID { get; set; }
        [Column("MangerName")]
        public string? MangerName { get; set; }
        [Column("MangerEmail")]
        public string? MangerEmail { get; set; }
    }
}
