using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kafu.Model.Enum;
using Kafu.Model.Repository;
using Kafu.Model.Entities;
using Kafu.Model.ViewModel;

namespace Kafu.Model.Services
{

    public class KafuServices : IKafuServices
    {
        private readonly UnitOfWorkBase _unitOfWork;

        public KafuServices(Kafu_SystemContext context)
        {
            _unitOfWork = new UnitOfWorkBase(context);
        }

        public async Task<Employee> GetEmployee(string Email)
        {
            var emp = _unitOfWork.EmployeeRepositoryBase.Find(x => x.Email == Email);
            if (emp.Count != 0)
                return emp.First();
            else
                return null;
        }
        public Employee GetEmployee(int ID)
        {
            try
            {
                return _unitOfWork.EmployeeRepositoryBase.FindById(ID);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Employee GetEmployeeByNumber(string EmployeeNumber)
        {
            try
            {
                return _unitOfWork.context.Employee.FirstOrDefault(x => x.EmployeeNumber.Trim() == EmployeeNumber.Trim());
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<EmployeeItem>> GetEmployeesItem(string search = "")
        {
            var items = _unitOfWork.EmployeeRepositoryBase.FindIQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower().Replace("أ", "ا");
                var words = search.Split(' ');

                if (words?.Any() == true)
                {
                    foreach (string keyword in words)
                    {
                        string word = keyword;
                        items = items.Where(x => x.Email.Contains(word) ||
                                                 x.PhoneExtention.Contains(word) ||
                                                 x.ArFullName.ToLower().Replace("أ", "ا").Contains(word) ||
                                                 x.EnFullName.Contains(word));


                    }


                }

            }

            return await
                  (from employee in items
                   join employeeImages in _unitOfWork.context.EmployeDetails
                          on employee.Email equals employeeImages.Email into resultTable
                   from r in resultTable.DefaultIfEmpty()
                   select new EmployeeItem
                   {
                       Value = employee.EmployeeNumber,
                       Text = employee.ArFullName,
                       Description = $"{employee.ArPosition} - {employee.PhoneExtention}",
                       Email = employee.Email,
                      ImageSrc = "/userimages/logo.png"
                       // in case employee's image is not avaliable use the default image
                      /* ImageSrc = r.EmpImage != null ? $"/userimages/{employee.Email}.jpeg" :
                         $"/userimages/logo.png"*/
                   }).Take(15).ToListAsync();


        }
        public ReasonLookup GetReasonById(int Id)
        {
            return _unitOfWork.ReasonepositoryBase.FindById(Id);
        }
        public Employee GetMangerEmployee(string ID)
        {
            try
            {
                var emp = _unitOfWork.EmployeeRepositoryBase
                    .Find(filter: x => x.EmployeeNumber == ID)
                    .First();

                var manger = _unitOfWork.EmployeeRepositoryBase.Find(filter: x => x.Email == emp.MangerEmail && emp.MangerEmail != null).FirstOrDefault();
                return manger;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<SelectListItem> GetEmployeeSelectListItems()
        {
            try
            {
                return _unitOfWork.EmployeeRepositoryBase.Find().Where(emp => emp?.Email != null).Select(x => new SelectListItem(value: x.EmployeeNumber.ToString(), text: x.ArFullName)).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // Kafu Services
        public KafuRecord GetKafuRecord()
        {
            throw new NotImplementedException();
        }

        public int InsertKafuRecord(KafuRecord kafuRecord)
        {
            _unitOfWork.KafuRecordRepositoryBase.Insert(kafuRecord);
            _unitOfWork.Save();
            return kafuRecord.Id;
        }
        public List<Tuple<Employee, int>> GetTopKafuEmployeeQuarter(int number, DateTime today)
        {

            List<Tuple<Employee, int>> employees = new List<Tuple<Employee, int>>();
            try
            {
                // get tables we want 
                var employeesQ = _unitOfWork.EmployeeRepositoryBase.FindIQueryable();
                var records = _unitOfWork.KafuRecordRepositoryBase.FindIQueryable();


                // get the top employees based on total count then creative count then alphabet 
                var topOnes = (from record in records
                               join employee in employeesQ
                               on record.RecipientId equals employee.EmployeeNumber

                               into kafu
                               select new
                               {

                                   arabicName = kafu.ElementAt(0).ArFullName,
                                   engName = kafu.ElementAt(0).EnFullName,

                                   Email = kafu.ElementAt(0).Email,
                                   Ext = kafu.ElementAt(0).PhoneExtention,
                                   sector = kafu.ElementAt(0).Department,
                                   executive = kafu.ElementAt(0).Location,
                                   recipientId = kafu.ElementAt(0).EmployeeNumber,
                                   reasonID = record.ReasonId,
                                   dates = record.CreatedOn

                               }).Where(x => (x.dates.Value.Month - 1) / 3 == (today.Month - 1) / 3).GroupBy(x => x.recipientId).OrderByDescending(x => x.Count()).ThenByDescending(x => x.Where(t => t.reasonID == 2).Count()).ThenBy(x => x.ElementAt(0).engName).Take(number).ToList();


                DateTime start;
                DateTime end;
                var month = today.Month;
                switch (today.Month)
                {

                    case 1:
                    case 2:
                    case 3:
                        start = new DateTime(today.Year, 1, 1);
                        end = new DateTime(today.Year, 3, 1);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        start = new DateTime(today.Year, 4, 1);
                        end = new DateTime(today.Year, 6, 1);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        start = new DateTime(today.Year, 7, 1);
                        end = new DateTime(today.Year, 9, 1);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        start = new DateTime(today.Year, 10, 1);
                        end = new DateTime(today.Year, 12, 1);
                        break;
                    default:
                        start = new DateTime(today.Year, 1, 1);
                        end = new DateTime(today.Year, 12, 1);
                        break;

                }
                // fill the list
                for (var i = 0; i <= topOnes.Count; i++)
                {
                    employees.Add(Tuple.Create(GetEmployeeByNumber(topOnes.ElementAt(i).ElementAt(0).recipientId), GetKafuCount(topOnes.ElementAt(i).ElementAt(0).recipientId, start, end)));

                }

                return employees;
            }
            catch (Exception e)
            {
                return employees;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">number of records to return</param>
        /// <returns> tuple (Employee, total number of Kafu)</returns>
        public List<Tuple<Employee, int>> GetTopKafuEmployeeYearly(int number, DateTime today)
        {

            List<Tuple<Employee, int>> employees = new List<Tuple<Employee, int>>();
            try
            {
                // get tables we want 
                var employeesQ = _unitOfWork.EmployeeRepositoryBase.FindIQueryable();
                var records = _unitOfWork.KafuRecordRepositoryBase.FindIQueryable();


                // get the top employees based on total count then creative count then alphabet 
                var topOnes = (from record in records
                               join employee in employeesQ
                               on record.RecipientId equals employee.EmployeeNumber
                               into kafu
                               select new
                               {
                                   arabicName = kafu.ElementAt(0).ArFullName,
                                   engName = kafu.ElementAt(0).EnFullName,
                                   ImageSrc = "sdsd",
                                   Email = kafu.ElementAt(0).Email,
                                   Ext = kafu.ElementAt(0).PhoneExtention,
                                   sector = kafu.ElementAt(0).Department,
                                   executive = kafu.ElementAt(0).Location,
                                   recipientId = kafu.ElementAt(0).EmployeeNumber,
                                   reasonID = record.ReasonId,
                                   dates = record.CreatedOn

                               }).Where(x => x.dates.Value.Year == today.Year).GroupBy(x => x.recipientId).OrderByDescending(x => x.Count()).ThenByDescending(x => x.Where(t => t.reasonID == 2).Count()).ThenBy(x => x.ElementAt(0).engName).Take(number).ToList();

                DateTime start = new DateTime(today.Year, 1, 1);
                DateTime end = new DateTime(today.Year, 12, 1);


                // fill the list
                for (var i = 0; i <= topOnes.Count; i++)
                {
                    employees.Add(Tuple.Create(GetEmployeeByNumber(topOnes.ElementAt(i).ElementAt(0).recipientId), GetKafuCount(topOnes.ElementAt(i).ElementAt(0).recipientId, start, end)));

                }

                return employees;
            }
            catch (Exception e)
            {
                return employees;
            }
        }




        // count services
        public int GetKafuCount(string RecipientId)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId).Count;
        }
        public int GetKafuCount(string RecipientId, DateTime start, DateTime end)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                               && (x.CreatedOn.Value.Month >= start.Month && x.CreatedOn.Value.Month <= end.Month)
                                                               && x.CreatedOn.Value.Year == start.Year).Count;
        }

        public int KafuGivenCountInMonths(string FromID, DateTime date)
        {
            var y = _unitOfWork.KafuRecordRepositoryBase.Find(x => x.CreatorId == FromID
                                                                && x.CreatedOn.Value.Month == date.Month
                                                                && x.CreatedOn.Value.Year == date.Year).Count;
            return y;
        }

        public int GetHelpCount(string RecipientId, DateTime date)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                               && x.CreatedOn.Value.Month == date.Month
                                                               && x.CreatedOn.Value.Year == date.Year
                                                               && x.ReasonId == (int)ReasonEnum.Helpful).Count;
        }
        public int GetHelpCount(string RecipientId)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                                && x.ReasonId == (int)ReasonEnum.Helpful).Count;
        }

        public int GetHelpfulCount(string RecipientId, DateTime date)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                    && x.CreatedOn.Value.Month == date.Month
                                                    && x.CreatedOn.Value.Year == date.Year
                                                    && x.ReasonId == (int)ReasonEnum.Cooperative).Count;
        }
        public int GetHelpfulCount(string RecipientId)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                               && x.ReasonId == (int)ReasonEnum.Cooperative).Count;
        }

        public int GetCreativeCount(string RecipientId, DateTime date)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                    && x.CreatedOn.Value.Month == date.Month
                                                    && x.CreatedOn.Value.Year == date.Year
                                                    && x.ReasonId == (int)ReasonEnum.Creative).Count;
        }
        public int GetCreativeCount(string RecipientId)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                              && x.ReasonId == (int)ReasonEnum.Creative).Count;
        }
        public int GetCreativeCount(string RecipientId, DateTime start, DateTime end)
        {
            return _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId == RecipientId
                                                               && (x.CreatedOn.Value.Month >= start.Month && x.CreatedOn.Value.Month <= end.Month)
                                                               && x.CreatedOn.Value.Year == start.Year
                                                               && x.ReasonId == (int)ReasonEnum.Creative).Count;
        }

        public int GetNumberOfDayssinceLastKafuForSelectedEmployee(string to, string from)
        {
            if (HasPreviousKafuFromThisEmp(to, from))
            {
                var list = _unitOfWork.KafuRecordRepositoryBase.Find(x => x.CreatorId.Trim() == from.Trim() && x.RecipientId.Trim() == to.Trim()).OrderByDescending(x => x.CreatedOn).First();

                var create = (DateTime)list.CreatedOn;
                var now = DateTime.Now.Date;
                var days = (int)(now - create).TotalDays;
                return days;

            }
            else
                return -1;
        }
        public bool HasPreviousKafuFromThisEmp(string to, string from)
        {
            var list = _unitOfWork.KafuRecordRepositoryBase.Find(x => x.CreatorId.Trim() == from.Trim() && x.RecipientId.Trim() == to.Trim());
            if (list.Count > 0)
                return true;

            return false;
        }


        //-------- View Receirved and sent Kafu --------------------
        public List<KafuRecord> GetReciervedKafu(string empNum)
        {
            var ReciervedKafuList = _unitOfWork.KafuRecordRepositoryBase.Find(x => x.RecipientId.Trim() == empNum.Trim()).OrderByDescending(x => x.Id).ToList(); ;
            return ReciervedKafuList;
        }
        public List<KafuRecord> GetSentKafu(string empNum)
        {
            var ReciervedKafuList = _unitOfWork.KafuRecordRepositoryBase.Find(x => x.CreatorId.Trim() == empNum.Trim()).OrderByDescending(x => x.Id).ToList();
            return ReciervedKafuList;
        }
    }
}
