using System.ComponentModel.DataAnnotations;
using Kafu.Model.Entities;

namespace Kafu.Model.ViewModel
{
    public class GiveKafuViewModel
    {
        public GiveKafuViewModel()
        {
        
        }

        //public string From { set; get; }
        public string To { set; get; }
        public string Comment { set; get; }
        [Required]
        public List<int> Reasons { set; get; }
        public bool isMangerCC { set; get; }
        public List<Tuple<Employee, int>> TopKafuEmployeesYearly{ set; get; }
        public List<Tuple<Employee, int>> TopKafuEmployeesQuartley { set; get; }
        public List<KafuRecord> ReciervedKafu { get; set; }
        public List<KafuRecord> SentKafu { get; set; }
    }

}
