using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;
using MusicShop.Commands;

namespace MusicShop.ViewModels
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Track> Tracks { get; set; }
        public TrackViewModel()
        {
            Tracks = new ObservableCollection<Track>();
        }
        private Track _selectedTrack;
        public Track SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                _selectedTrack = value;
                OnPropertyChanged("SelectedTrack");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
