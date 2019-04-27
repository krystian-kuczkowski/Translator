using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Translator.Desktop.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private double _gridContentLength = 300;

        public GridLength GridContentLength
        {
            get
            {
                return new GridLength(_gridContentLength);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
