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
    public class AuthorViewModel : INotifyPropertyChanged
    {
        private Author _author;
        public AuthorViewModel(Author author)
        {
            _author = author;
        }

        public int Id
        {
            get { return _author.Id; }
            set
            {
                _author.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _author.Name; }
            set
            {
                _author.Name = value;
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
