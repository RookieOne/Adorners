using System.Collections.Generic;
using PhoneCallCenter.Domain;

namespace PhoneCallCenter.Services
{
    public interface IService
    {
        #region Methods

        IEnumerable<Party> GetParties();
        IEnumerable<Employee> GetEmployees();

        #endregion
    }
}