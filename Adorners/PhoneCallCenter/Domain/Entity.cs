using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PhoneCallCenter.Domain
{
    public abstract class Entity : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id;}
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
