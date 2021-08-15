using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ShowDevices.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private delegate void NotifyPropertyChangedDeleagte(string propertyName);

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (GetType().GetProperty(propertyName) == null)
            {
                throw new ArgumentOutOfRangeException("propertyName");
            }

            if (Dispatcher.CurrentDispatcher.CheckAccess())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.DataBind, new NotifyPropertyChangedDeleagte(NotifyPropertyChanged), propertyName);
            }
        }

        protected virtual void NotifyAllPropertiesChanged()
        {
            if (Dispatcher.CurrentDispatcher.CheckAccess())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
            else
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.DataBind, new NotifyPropertyChangedDeleagte(NotifyPropertyChanged), null);
            }
        }

        #endregion
    }
}
