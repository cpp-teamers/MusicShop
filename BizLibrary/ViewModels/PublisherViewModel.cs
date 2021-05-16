using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.ViewModels
{
    public class PublisherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Publisher> Publishers { get; set; }
        public PublisherViewModel()
        {
            Publishers = new ObservableCollection<Publisher>();
        }
        private Publisher _selectedPublisher;
        public Publisher SelectedPublisher
        {
            get { return _selectedPublisher; }
            set                 
            {
                _selectedPublisher = value;
                OnPropertyChanged("SelectedPublisher");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
