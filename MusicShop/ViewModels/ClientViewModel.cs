using ModelsLibrary.Models;
using MusicShop.Commands;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using MusicShop.Repositories.Implementations;
using System.Collections.Generic;
using System;
using Microsoft.Win32;

namespace MusicShop.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlateViewModel> Plates { get; set; }
        public ObservableCollection<GenreViewModel> Genres { get; set; }
        public ObservableCollection<AuthorViewModel> Authors { get; set; }
        public ObservableCollection<PublisherViewModel> Publishers { get; set; }
        private AllRepositories rep = new AllRepositories();

        private AccountViewModel _selectedAccount;
        public AccountViewModel SelectedAccount 
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
            }
        }
        
        private GenreViewModel _selectedGenre;
        public GenreViewModel SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                if (SelectedAuthor == null)
                    LoadPlatesByAuthorIdAndGenreId(-1, SelectedGenre.Id);
                else
                    LoadPlatesByAuthorIdAndGenreId(SelectedAuthor.Id, SelectedGenre.Id);
                OnPropertyChanged("SelectedGenre");
            }
        }

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                LoadPlatesByAuthorIdAndGenreId(SelectedAuthor.Id, SelectedGenre.Id);
                OnPropertyChanged("SelectedAuthor");
            }
        }
        private PublisherViewModel _selectedPublisher;
        public PublisherViewModel SelectedPublisher
        {
            get { return _selectedPublisher; }
            set
            {
                _selectedPublisher = value;
                OnPropertyChanged("SelectedPublisher");
            }
        }
        private PlateViewModel _selectedPlate;
        public PlateViewModel SelectedPlate
        {
            get { return _selectedPlate; }
            set
            {
                _selectedPlate = value;
                Plate plate = _selectedPlate.GetPlate;
                if(plate.Id == -1)
                {
                    /* OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                    if (!ofd.ShowDialog().HasValue)
                    {
                        string sourcePath = ofd.FileName;
                        string nameImage = ofd.SafeFileName;
                        string destinationPath = $@"..\Pictures\Images\{nameImage}.jpg";
                        System.IO.File.Copy(sourcePath, destinationPath);
                        plate.AlbumImagePath = destinationPath;
                        MessageBox.Show("Image was successfully added");
                    } */
                }
                OnPropertyChanged("SelectedPlate");
            }
        }
        private void LoadPublishers()
        {
            Publishers.Clear();
            var publishers = rep.PublisherRepository.GetAllPublishers();
            foreach (var p in publishers)
                Publishers.Add(new PublisherViewModel(p));
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
                    Plate plate = new Plate()
                    {
                        Id = -1,
                        AlbumImagePath = @"..\Pictures\Images\defaultAlbumImage.png",
                        Cost = 0,
                        PublishDate = date,
                        Amount = 0,
                        AuthorId = SelectedAuthor.Id,
                        GenreId = SelectedGenre.Id,
                        Name = "New Plate",
                        PublisherId = -1,
                        RealCost = 0
                    };
                    Plates.Add(new PlateViewModel(plate));
                    OnPropertyChanged("Plates");
                }
            ));
            }
        }
        private RelayCommand _delPlate;
        public RelayCommand DeletePlate
        {
            get
            {
                return _delPlate ?? (new RelayCommand(obj =>
                {
                    if(SelectedPlate.Id != -1)
                    {
                        string name = SelectedPlate.Name;
                        rep.PlateRepository.DelPlate(SelectedPlate.Id);
                        MessageBox.Show($"{name} was successfully deleted!");
                        LoadPlates();
                    }
                }
            ));
            }
        }
        private RelayCommand _savePlate;
        public RelayCommand SavePlate
        {
            get
            {
                return _savePlate ?? (new RelayCommand(obj =>
                {
                   // try
                   // {
                        if (SelectedAuthor.Id == -1)
                            throw new Exception("You didn't choose author!");
                        else if (SelectedGenre.Id == -1)
                            throw new Exception("You didn't choose genre!");
                        else if (SelectedPublisher.Id == -1)
                            throw new Exception("You didn't choose publisher!");
                        else 
                        {
                            SelectedPlate.GetPlate.GenreId = SelectedGenre.Id;
                            SelectedPlate.GetPlate.PublisherId = SelectedPublisher.Id;
                            SelectedPlate.GetPlate.AuthorId = SelectedAuthor.Id;

                            rep.PlateRepository.AddPlate(SelectedPlate.GetPlate);
                            MessageBox.Show($"{SelectedPlate.GetPlate.Name} was successfully added!");
                            OnPropertyChanged("Plates");
                        }

                   // }
                //    catch(Exception err)
                //    {
                 //       MessageBox.Show(err.Message, "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                //    }
                }
            ));
            }
        }
        private RelayCommand _searchPlate;
        public RelayCommand SearchPlate
        {
            get
            {
                return _searchPlate ?? (_searchPlate = new RelayCommand(obj =>
                {
                    string text = obj.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        var plate = rep.PlateRepository.GetPlateByName(text);
                        Plates.Clear();
                        Plates.Add(new PlateViewModel(plate));
                        if (plate == null)
                        {
                            MessageBox.Show($"'{text}' doesn`t find", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            LoadPlates();
                        }
                    }
                }));
            }
        }

        private void LoadPlates()
        {
            Plates.Clear();
            var platesFromDb = rep.PlateRepository.GetAllPlates();
            foreach (var plate in platesFromDb)
            {
                Plates.Add(new PlateViewModel(plate));
            }
        }

        private void LoadPlatesByAuthorIdAndGenreId(int aid, int gid)
        {
            Plates.Clear();
            IEnumerable<Plate> platesFormDbFiltered;
            if (aid != -1 && gid != -1)
                platesFormDbFiltered = rep.PlateRepository.GetAllPlatesByAuthorIdAndGenreId(gid, aid);
            else if (aid == -1 && gid != -1)
                platesFormDbFiltered = rep.PlateRepository.GetAllPlatesByGenreId(gid);
            else if (gid == -1 && aid != -1)
                platesFormDbFiltered = rep.PlateRepository.GetAllPlatesByAuthorId(aid);
            else
                platesFormDbFiltered = rep.PlateRepository.GetAllPlates();
            foreach (var plate in platesFormDbFiltered)
            {
                Plates.Add(new PlateViewModel(plate));
            }
        }

        public ClientViewModel(Account account)
        {
            Plates = new ObservableCollection<PlateViewModel>();
            Genres = new ObservableCollection<GenreViewModel>();
            Authors = new ObservableCollection<AuthorViewModel>();
            Publishers = new ObservableCollection<PublisherViewModel>();
            SelectedAccount = new AccountViewModel(account);
            LoadGenres();
            LoadAuthors();
            LoadPlates();
            LoadPublishers();
        }

        public ClientViewModel()
        {
            Plates = new ObservableCollection<PlateViewModel>();
            Genres = new ObservableCollection<GenreViewModel>();
            Authors = new ObservableCollection<AuthorViewModel>();
            Publishers = new ObservableCollection<PublisherViewModel>();
            LoadGenres();
            LoadAuthors();
            LoadPlates();
            LoadPublishers();
        }

        private void LoadGenres()
        {
            Genres.Clear();
            var genresFromDb = rep.GenreRepository.GetAllGenres();
            Genre allGenres = new Genre() { Id = -1, Name = "All Genres" };
            allGenres.Plates = rep.PlateRepository.GetAllPlates();
            Genres.Add(new GenreViewModel(allGenres));
            foreach (var genre in genresFromDb)
            {
                Genres.Add(new GenreViewModel(genre));
            }
        }

        private void LoadAuthors()
        {
            Authors.Clear();
            var authorsFromDb = rep.AuthorRepository.GetAllAuthors();

            Author allAuthors = new Author() { Id = -1, Name = "All Authors" };
            allAuthors.Plates = rep.PlateRepository.GetAllPlates();
            Authors.Add(new AuthorViewModel(allAuthors));

            foreach (var author in authorsFromDb)
            {
                Authors.Add(new AuthorViewModel(author));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
