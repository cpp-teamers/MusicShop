﻿using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Repositories.Implementations;
using System.Windows;
using MusicShop.Commands;

namespace MusicShop.ViewModels
{
    public class PlateViewModel : INotifyPropertyChanged
    {
        private AllRepositories _rep = new AllRepositories();
        ObservableCollection<Plate> Plates { get; set; }
        private Plate _plate1;
        public Plate SelectedPlate
        {
            get { return _plate1; }
            set 
            {
                _plate1 = value;
                OnPropertyChanged("SelectedPlate");
            }
        }
        public PlateViewModel()
        {
            Plates = new ObservableCollection<Plate>();
        }
        private Plate _plate;
        public PlateViewModel(Plate plate)
        {
            Plates = new ObservableCollection<Plate>();
            _plate = new Plate();
            _plate.Tracks = _rep.TrackRepository.GetAllTracksByPlateId(_plate.Id);
            //MessageBox.Show(_plate.Tracks.Count().ToString()); // - Dev Kho changed
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
        private RelayCommand _addPlate;
        public RelayCommand AddPlate
        {
            get
            {
                return _addPlate ?? (new RelayCommand(obj =>
                {
                    var author = obj as Author;
                    DateTime date = new DateTime(1900, 1, 1);
                    Plate plate = new Plate() {
                       AlbumImagePath = @"..\..\MusicShop\Pictures\Images\defaultAlbumImage.png",
                       Cost = 0,
                       PublishDate = date,
                       Amount = 0,
                       AuthorId = author.Id,
                       GenreId = GenreId,
                       Name = "New Plate",
                       PublisherId = PublisherId,
                       RealCost = 0
                    };
                    Plates.Add(plate);
                    MessageBox.Show("!");
                    OnPropertyChanged("Plates");
                }
            ));
            }
        }
        private void LoadPlates()
        {
            Plates.Clear();
            var platesList = _rep.PlateRepository.GetAllPlates().ToList();
            foreach (var plate in platesList)
                Plates.Add(plate);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
