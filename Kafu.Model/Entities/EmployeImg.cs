using System.ComponentModel.DataAnnotations.Schema;

namespace Kafu.Model.Entities
{
    public partial class EmployeImg
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        [Column(TypeName = "image")]
        public byte[]? EmpImage { get; set; }
    }
}
