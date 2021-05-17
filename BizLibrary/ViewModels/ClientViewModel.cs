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
using BizLibrary.Repositories.Implementations;
using System.Collections.Generic;

namespace BizLibrary.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlateViewModel> Plates { get; set; }
        public ObservableCollection<GenreViewModel> Genres { get; set; }
        public ObservableCollection<AuthorViewModel> Authors { get; set; }
        private AllRepositories rep = new AllRepositories();

        private GenreViewModel _selectedGenre;
        public GenreViewModel SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                if(SelectedAuthor == null)
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

        private PlateViewModel _selectedPlate;
        public PlateViewModel SelectedPlate
        {
            get { return _selectedPlate; }
            set
            {
                _selectedPlate = value;
                OnPropertyChanged("SelectedPlate");
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
            foreach(var plate in platesFormDbFiltered)
            {
                Plates.Add(new PlateViewModel(plate));
            }
        }

        public ClientViewModel()
        {
            Plates = new ObservableCollection<PlateViewModel>();
            Genres = new ObservableCollection<GenreViewModel>();
            Authors = new ObservableCollection<AuthorViewModel>();
            LoadGenres();
            LoadAuthors();
            LoadPlates();
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
