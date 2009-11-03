using System.Collections.Generic;
using PhoneCallCenter.Domain;
using PhoneCallCenter.Services;

namespace PhoneCallCenter.ViewModels
{
    public class CallCenterModel
    {
        #region Properties

        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Party> Parties { get; set; }
        public IEnumerable<PhoneLine> PhoneLines { get; set; }
        private IPhoneService PhoneService { get; set; }
        private IService Service { get; set; }

        #endregion

        #region Constructors

        public CallCenterModel()
        {
            Service = new MyService();
            PhoneService = new PhoneService();

            Parties = Service.GetParties();
            Employees = Service.GetEmployees();            

            PhoneLines = PhoneService.GetPhoneLines();

            foreach(Employee employee in Employees)
            {
                employee.PhoneService = PhoneService;
            }
        }

        #endregion
    }
}