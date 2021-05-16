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
    public class RoleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Role> Roles { get; set; }
        public RoleViewModel()
        {
            Roles = new ObservableCollection<Role>();
        }
        private Role _selectedRole;
        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged("SelectedSale");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
