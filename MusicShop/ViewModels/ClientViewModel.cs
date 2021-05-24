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
        public ObservableCollection<SaleViewModel> Sales { get; set; }
        public ObservableCollection<PublisherViewModel> Publishers { get; set; }
        public ObservableCollection<Discount> Discounts{ get; set; }

        private AllRepositories rep = new AllRepositories();
        public AccountViewModel2 Account { get; set; }
        public DateTime StartDate { get; set; } = new DateTime(2021, 1, 1);
        public decimal SalesPrice { get; set; }
        
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
        private AccountViewModel2 _selectedAccount;
        public AccountViewModel2 SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                OnPropertyChanged("SelectedAccount");
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
        private Discount _selectedDiscount;
        public Discount SelectedDiscount
        {
            get { return _selectedDiscount; }
            set
            {
                _selectedDiscount = value;
                OnPropertyChanged("SelectedDiscount");
            }
        }
        private PlateViewModel _selectedPlate;
        public PlateViewModel SelectedPlate
        {
            get { return _selectedPlate; }
            set
            {
                _selectedPlate = value;
                var sales = rep.SaleRepository.GetAllSalesByPlateId(_selectedPlate.Id);
                if (sales != null)
                {
                    Sales.Clear();
                    int i = 0;
                    foreach (var sale in sales)
                    {
                        Sales.Add(new SaleViewModel(sale));
                        SalesPrice += Sales[i].Price;
                    }
                    OnPropertyChanged("Sales");
                    OnPropertyChanged("SalesPrice");
                }
                var discounts = rep.DiscountRepository.GetAllDiscounts().ToList();
                //var discounts = rep.DiscountRepository.GetAllDiscountsByStartDate(StartDate).ToList();
                if (discounts != null)
                {
                    Discounts.Clear();
                    foreach (var d in discounts)
                        Discounts.Add(d);
                    OnPropertyChanged("Discounts");
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
        private RelayCommand _addDiscount;
        public RelayCommand AddDiscount
        {
            get
            {
                return _addDiscount ?? (new RelayCommand(obj =>
                {
                    DatePicker date_def =  (DatePicker)obj;
                    DateTime startDate = (DateTime)date_def.SelectedDate;
                    DateTime endDate = startDate.AddDays(30);
                    if (startDate.Date > DateTime.Now)
                    {
                        Discount discount = new Discount()
                        {
                            Id = -1,
                            Comment = "New Discount",
                            StartDate = startDate,
                            EndDate = endDate
                        };
                        rep.DiscountRepository.AddDiscount(discount);
                        LoadDiscounts();
                        OnPropertyChanged("Discounts");
                    }
                    else
                        MessageBox.Show("You input invalid date!");
                }
            ));
            }
        }
        public string Comment
        {
            get { return SelectedDiscount.Comment; }
            set { SelectedDiscount.Comment = value; }
        }
        private void LoadDiscounts()
        {
            var discounts = rep.DiscountRepository.GetAllDiscounts().ToList();
            if (discounts != null)
            {
                Discounts.Clear();
                foreach (var d in discounts)
                    Discounts.Add(d);
                OnPropertyChanged("Discounts");
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
                    try
                    {
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

                    }
                    catch(Exception err)
                    {
                        MessageBox.Show(err.Message, "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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

        private RelayCommand _buyCommand;
        public RelayCommand BuyCommand
        {
            get
            {
                return _buyCommand ?? (_buyCommand = new RelayCommand(obj =>
                {
                    if(SelectedPlate != null)
                    {
                        if(SelectedPlate.Amount != 0)
                        {
                            Sale sale = new Sale()
                            {
                                Id = 0,
                                AccountId = Account.Id,
                                PlateId = SelectedPlate.Id,
                                DateOfSale = DateTime.Now,
                                AmountOfSales = 1,
                                Price = SelectedPlate.Cost
                            };
                            rep.SaleRepository.AddSale(sale);
                            Plate plate = new Plate()
                            {
                                Id = SelectedPlate.Id,
                                Name = SelectedPlate.Name,
                                Amount = SelectedPlate.Amount - 1,
                                PublishDate = SelectedPlate.PublishDate,
                                Cost = SelectedPlate.Cost,
                                AlbumImagePath = SelectedPlate.AlbumImagePath,
                                RealCost = SelectedPlate.RealCost,
                                AuthorId = SelectedPlate.AuthorId,
                                PublisherId = SelectedPlate.PublisherId,
                                GenreId = SelectedPlate.GenreId
                            };
                            rep.PlateRepository.ChangePlate(plate);

                            MessageBox.Show($"Thanks for the buying {SelectedPlate.Name}!!!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadPlates();
                            LoadSales();
                        }
                        else
                        {
                            MessageBox.Show($"Sorry we didn`t have that plates", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"You didn`t select any plates", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }));
            }
        }
        private void LoadSales()
        {
            Sales.Clear();
            var salesFromDb = rep.SaleRepository.GetAllSalesByAccoutId(Account.Id);
            foreach (var sale in salesFromDb)
            {
                Sales.Add(new SaleViewModel(sale));
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
            Discounts = new ObservableCollection<Discount>();
            Plates = new ObservableCollection<PlateViewModel>();
            Genres = new ObservableCollection<GenreViewModel>();
            Authors = new ObservableCollection<AuthorViewModel>();
            Sales = new ObservableCollection<SaleViewModel>();
            Account = new AccountViewModel2(account);
            SelectedAccount = Account;
            Publishers = new ObservableCollection<PublisherViewModel>();
            SelectedAccount = new AccountViewModel2(account);
            LoadGenres();
            LoadAuthors();
            LoadPlates();
            LoadPublishers();
            LoadSales();
        }

        public ClientViewModel()
        {
            Discounts = new ObservableCollection<Discount>();
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
