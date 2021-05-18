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
        private ModelsLibrary.Models.Publisher _publisher;
        public PublisherViewModel(ModelsLibrary.Models.Publisher publisher)
        {
            _publisher = publisher;
        }
        public int Id
        {
            get { return _publisher.Id; }
            set
            {
                _publisher.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _publisher.Name; }
            set
            {
                _publisher.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
