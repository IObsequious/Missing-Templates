using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace $rootnamespace$
{
    public class $safeitemrootname$ : INotifyPropertyChanged
    {
        private $fileinputname$ model;

        public $safeitemrootname$($fileinputname$ model)
        {
            this.model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
