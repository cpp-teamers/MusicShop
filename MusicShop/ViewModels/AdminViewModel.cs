using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        public Account account { get; set; }
        public ClientViewModel cvm { get; set; }
        public AuthorViewModel avm { get; set; }
        public AccountViewModel2 accountvm { get; set; }
        public DiscountViewModel disc { get; set; }
        public AdminViewModel(Account account)
        {
            cvm = new ClientViewModel(account);
            avm = new AuthorViewModel();
            accountvm = new AccountViewModel2(account);
            disc = new DiscountViewModel();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
