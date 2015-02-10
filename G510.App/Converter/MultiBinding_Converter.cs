using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace G510.App.Converter
{
    public class MultiBinding_Converter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new MultiValueHolder(values, parameter);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            return (value as MultiValueHolder).Values.ToArray();
        }
    }

    public class MultiValueHolder
    {
        public MultiValueHolder(IEnumerable<Object> _values, Object _parameter)
        {
            Values = new List<Object>();

            if (_values != null)
                foreach (var item in _values)
                {
                    Values.Add(item);
                }

            Parameter = _parameter;
        }

        public Boolean HasValue
        {
            get { return Values.Count > 0; }
        }

        private List<Object> m_Values;

        public List<Object> Values
        {
            get { return m_Values; }
            private set { m_Values = value; }
        }

        private Object m_Parameter;

        public Object Parameter
        {
            get { return m_Parameter; }
            private set { m_Parameter = value; }
        }


    }
}
