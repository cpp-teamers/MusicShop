using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BizLibrary.Repositories.Implementations;
using System.Windows;


namespace BizLibrary.ViewModels
{
    public class PlateViewModel : INotifyPropertyChanged
    {
        private AllRepositories rep = new AllRepositories();
        
        private Plate _plate;
        public PlateViewModel(Plate plate)
        {
            _plate = plate;
            _plate.Tracks = rep.TrackRepository.GetAllTracksByPlateId(_plate.Id);
        }

        public int Id
        {
            get { return _plate.Id; }
            set
            {
                _plate.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return _plate.Name; }
            set
            {
                _plate.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public DateTime PublishDate
        {
            get { return _plate.PublishDate; }
            set
            {
                _plate.PublishDate = value;
                OnPropertyChanged("PublishDate");
            }
        }

        public string AlbumImagePath
        {
            get { return _plate.AlbumImagePath; }
            set
            {
                _plate.AlbumImagePath = value;
                OnPropertyChanged("AlbumImagePath");
            }
        }

        public int Amount
        {
            get { return _plate.Amount; }
            set
            {
                _plate.Amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public decimal Cost
        {
            get { return _plate.Cost; }
            set
            {
                _plate.Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public int AuthorId
        {
            get { return _plate.AuthorId; }
            set
            {
                _plate.AuthorId = value;
                OnPropertyChanged("AuthorId");
            }
        }

        public int GenreId
        {
            get { return _plate.GenreId; }
            set
            {
                _plate.GenreId = value;
                OnPropertyChanged("GenreId");
            }
        }

        public int PublisherId
        {
            get { return _plate.PublisherId; }
            set
            {
                _plate.PublisherId = value;
                OnPropertyChanged("PublisherId");
            }
        }

        public decimal RealCost
        {
            get { return _plate.RealCost; }
            set
            {
                _plate.RealCost = value;
                OnPropertyChanged("RealCost");
            }
        }

        public Author Author
        {
            get { return _plate.Author; }
            set
            {
                _plate.Author = value;
                OnPropertyChanged("Author");
            }
        }

        public ModelsLibrary.Models.Publisher Publisher
        {
            get { return _plate.Publisher; }
            set
            {
                _plate.Publisher = value;
                OnPropertyChanged("Publisher");
            }
        }

        public IEnumerable<Track> Tracks
        {
            get { return _plate.Tracks; }
            set
            {
                _plate.Tracks = value;
                OnPropertyChanged("Tracks");
            }
        }

        private void LoadTracks()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
