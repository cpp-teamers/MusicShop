using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.ViewModels
{
    public class DiscountViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Discount> Discounts { get; set; }
        public DiscountViewModel()
        {
            Discounts = new ObservableCollection<Discount>();
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
