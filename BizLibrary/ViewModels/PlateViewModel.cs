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

namespace MusicShop.ViewModels
{
    public class PlateViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Plate> Plates { get; set; }
        public PlateViewModel()
        {
            Plates = new ObservableCollection<Plate>();
        }
        private Plate _selectedPlate;
        public Plate SelectedPlate
        {
            get { return _selectedPlate; }
            set
            {
                _selectedPlate = value;
                OnPropertyChanged("SelectedPlate");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
