using Microsoft.AspNetCore.Mvc.Rendering;
using Kafu.Model.Entities;
using Kafu.Model.ViewModel;

namespace Kafu.Model.Services
{
    public interface IKafuServices
    {
        // kafu services
        int InsertKafuRecord(KafuRecord kafuRecord);
        KafuRecord GetKafuRecord();

        List<Tuple<Employee, int>> GetTopKafuEmployeeQuarter(int number, DateTime today);
        List<Tuple<Employee, int>> GetTopKafuEmployeeYearly(int number, DateTime today);
        public ReasonLookup GetReasonById(int Id);
        //Kafu Count Services
        int KafuGivenCountInMonths(string FromID, DateTime date);
        int GetKafuCount(string RecipientId);
        int GetKafuCount(string RecipientId, DateTime start, DateTime end);
        int GetHelpCount(string RecipientId, DateTime date);
        int GetHelpCount(string RecipientId);

        int GetHelpfulCount(string RecipientId, DateTime date);
        int GetHelpfulCount(string RecipientId);

        int GetCreativeCount(string RecipientId, DateTime date);
        int GetCreativeCount(string RecipientId);

        // Empolyee services
        Task<Employee> GetEmployee(string Email);
        Employee GetEmployee(int ID);
        Employee GetEmployeeByNumber(string EmployeeNumber);
        Employee GetMangerEmployee(string ID);

        // BawabtyEmployee GetBawabtyEmployee(int ID);
        Task<List<EmployeeItem>> GetEmployeesItem(string term);
        List<SelectListItem> GetEmployeeSelectListItems();
        int GetNumberOfDayssinceLastKafuForSelectedEmployee(string to, string from);

        List<KafuRecord> GetReciervedKafu(string empNum);
        List<KafuRecord> GetSentKafu(string empNum);
    }
}
