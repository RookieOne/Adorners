using PhoneCallCenter.Services;

namespace PhoneCallCenter.Domain
{
    public class Employee : Entity
    {
        #region Properties
        
        public string Name { get; set; }

        public IPhoneService PhoneService { get; set; }


        public void SendToVoiceMail(PhoneLine phoneLine)
        {
            if (phoneLine == null) return;

            PhoneService.SendToVoiceMail(phoneLine, this);
        }

        public void Transfer(PhoneLine phoneLine)
        {
            if (phoneLine == null) return;

            PhoneService.TransferCall(phoneLine, this);    
        }

        #endregion
    }
}