using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Repositories.Implementations;

namespace MusicShop.ViewModels
{
    public class SaleViewModel : INotifyPropertyChanged
    {
        private Sale _sale;
        private AllRepositories rep = new AllRepositories();
        public SaleViewModel(Sale sale)
        {
            _sale = sale;
            if(_sale.Plate == null)
                Plate = rep.PlateRepository.GetPlateById(sale.PlateId);
        }

        public int Id
        {
            get { return _sale.Id; }
            set
            {
                _sale.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public int PlateId
        {
            get { return _sale.PlateId; }
            set
            {
                _sale.PlateId = value;
                OnPropertyChanged("PlateId");
            }
        }

        public Plate Plate
        {
            get { return _sale.Plate; }
            set
            {
                _sale.Plate = value;
                OnPropertyChanged("Plate");
            }
        }

        public int AmountOfSales
        {
            get { return _sale.AmountOfSales; }
            set
            {
                _sale.AmountOfSales = value;
                OnPropertyChanged("AmountOfSale");
            }
        }

        public DateTime DateOfSale
        {
            get { return _sale.DateOfSale; }
            set
            {
                _sale.DateOfSale = value;
                OnPropertyChanged("DateOfSale");
            }
        }

        public decimal Price
        {
            get { return _sale.Price; }
            set
            {
                _sale.Price = value;
                OnPropertyChanged("Price");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
