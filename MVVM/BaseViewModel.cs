using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vardirsoft.Shared.MVVM
{
    [Serializable]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        public BaseViewModel() { }

        protected void ForceSetWithNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            NotifyPropertyChanged(propertyName);
        }
        protected void SetWithNotify<T>(ref T field, T value, bool disallowNull = true, [CallerMemberName] string propertyName = null)
            where T: class
        {
            if (value == null && disallowNull || value == field)
                return;
            
            ForceSetWithNotify(ref field, value, propertyName);
        }
        protected void SetWithNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            SetWithNotify(ref field, value, Comparer<T>.Default, propertyName);
        }
        protected void SetWithNotify<T>(ref T field, T value, IComparer<T> comparer, [CallerMemberName] string propertyName = null)
        {
            if (comparer.Compare(field, value) != 0)
            {
                ForceSetWithNotify(ref field, value, propertyName);
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}