using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BizLibrary.ViewModels
{
    public class GenreViewModel : INotifyPropertyChanged
    {
        private Genre _genre;
        public GenreViewModel(Genre genre)
        {
            _genre = genre;
        }

        public int Id
        {
            get { return _genre.Id; }
            set
            {
                _genre.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _genre.Name; }
            set
            {
                _genre.Name = value;
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
