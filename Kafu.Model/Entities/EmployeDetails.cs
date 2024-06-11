using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kafu.Model.Entities
{
    public partial class EmployeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Email { get; set; }
        public byte[]? EmpImage
        {
            get; set;

        }
    
    }
}
