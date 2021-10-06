using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ppedv.Laureatus.UI.Wpf.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        protected void OnMyPropertyChanged([CallerMemberName] string propName = "") => OnPropertyChanged(propName);
}
}
