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
    public class AccountViewModel2 : INotifyPropertyChanged
    {
        private Account _account;
        public AccountViewModel2(Account account)
        {
            _account = account;
        }

        public int Id
        {
            get { return _account.Id; }
            set
            {
                _account.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Login
        {
            get { return _account.Login; }
            set
            {
                _account.Login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _account.Password; }
            set
            {
                _account.Password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Email
        {
            get { return _account.Email; }
            set
            {
                _account.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get { return _account.Phone; }
            set
            {
                _account.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
