using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace G510.App
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged { add { _propertyChanged += value; } remove { _propertyChanged -= value; } }

        public void RaisePorpertyChanged([CallerMemberName]String _propertyName = "")
        {
            if (this._propertyChanged != null)
            {
                this._propertyChanged(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
