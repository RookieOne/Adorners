using System.Collections.Generic;
using System.Windows;
using PhoneCallCenter.Domain;

namespace PhoneCallCenter.Services
{
    public class PhoneService : IPhoneService
    {
        #region IPhoneService

        public IEnumerable<PhoneLine> GetPhoneLines()
        {
            return new List<PhoneLine>
                       {
                           new PhoneLine {Id = 1},
                           new PhoneLine {Id = 2},
                           new PhoneLine {Id = 3},
                           new PhoneLine {Id = 4},
                           new PhoneLine {Id = 5},
                           new PhoneLine {Id = 6}
                       };
        }

        public void TransferCall(PhoneLine phoneLine, Employee employee)
        {
            MessageBox.Show(string.Format("Transfer {0} to {1}", phoneLine.Id, employee.Name));
        }

        public void SendToVoiceMail(PhoneLine phoneLine, Employee employee)
        {
            MessageBox.Show(string.Format("Send {0} to {1}'s voice mail", phoneLine.Id, employee.Name));
        }

        #endregion
    }
}