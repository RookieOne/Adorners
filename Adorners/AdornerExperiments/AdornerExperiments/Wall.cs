using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AdornerExperiments
{
    public class Wall : INotifyPropertyChanged
    {
        private WallDetails _wallInfo;

        public WallDetails WallInfo
        {
            get { return _wallInfo; }
            set 
            {
                _wallInfo = value;
                OnPropertyChanged("WallInfo");
            }
        }
	

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
