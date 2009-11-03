using System.Collections.Generic;
using PhoneCallCenter.Domain;

namespace PhoneCallCenter.Services
{
    public class MyService : IService
    {
        #region Methods

        public IEnumerable<Party> GetParties()
        {
            return new List<Party>
                       {
                           new Party {Id = 1, Name = "Sandy Squirrel"},
                           new Party {Id = 2, Name = "Patrick Star"},
                           new Party {Id = 3, Name = "Sheldon Plankton"},
                       };
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>
                       {
                           new Employee {Id = 1, Name = "Sponge Bob Squarepants"},
                           new Employee {Id = 2, Name = "Squidward Tentacles"},
                           new Employee {Id = 3, Name = "Eugene Krabs"},
                       };
        }

        #endregion
    }
}