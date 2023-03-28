using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace N225.WinForm.ViewModels
{
    /// <summary>
    /// VeiwModelの基底クラス
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public Dispatcher Dispatcher { get; set; } = null;
        /// <summary>
        /// SetPropety
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="field">field</param>
        /// <param name="value">value</param>
        /// <param name="propetyName">propetyName</param>
        /// <returns></returns>
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
                    Dispatcher.Invoke(delegate ()
                    {
                        h(this, new PropertyChangedEventArgs(propetyName));
                    });
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
