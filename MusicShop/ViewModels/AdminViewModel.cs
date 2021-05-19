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
        public ClientViewModel cvm { get; set; }
        public AuthorViewModel avm { get; set; }
        public AccountViewModel accountvm { get; set; }
        public AdminViewModel()
        {
            cvm = new ClientViewModel();
            avm = new AuthorViewModel();
            accountvm = new AccountViewModel(new MusicShop.Views.MainWindow());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
