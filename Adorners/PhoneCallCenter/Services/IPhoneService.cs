using System.Collections.Generic;
using PhoneCallCenter.Domain;

namespace PhoneCallCenter.Services
{
    public interface IPhoneService
    {
        #region Methods

        IEnumerable<PhoneLine> GetPhoneLines();

        void TransferCall(PhoneLine phoneLine, Employee employee);

        void SendToVoiceMail(PhoneLine phoneLine, Employee employee);

        #endregion
    }
}