using System.Windows;

namespace PhoneCallCenter.Domain
{
    public class PhoneLine : Entity
    {        

        private Party _party;
        public Party Party
        {
            get { return _party; }
            set 
            {
                _party = value;
                OnPropertyChanged("Party"); 
            }
        }

        public void SetParty(Party party)
        {
            if (party == null) return;

            Party = party;
            MessageBox.Show(string.Format("Phone {0} Set Party {1}", Id, party.Name));            
        }
    }
}