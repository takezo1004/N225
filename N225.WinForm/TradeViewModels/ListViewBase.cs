using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace N225.WinForm.TradeViewModels
{
    public class ListViewBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetPropety<T>(ref T field,
                    T value, [CallerMemberName] string propetyName = null)
        {
            try
            {
                if (Equals(field, value))
                {
                    return false;
                }
                field = value;
                var h = this.PropertyChanged;
                if (h != null)
                {
                    h(this, new PropertyChangedEventArgs(propetyName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return true;
        }
    }
}
