using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicShop.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private Account _defaultAcc = new Account()
        {
            Login = "Login",
            Password = "Password",
            Email = "Email",
            Phone = "Phone",
        };
        public Account DefaultAcc 
        {
			get
			{
                return _defaultAcc;
            }
            set
			{
                _defaultAcc = value;
                OnPropertyChanged(nameof(DefaultAcc));
			}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
