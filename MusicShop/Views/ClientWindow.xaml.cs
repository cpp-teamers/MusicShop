using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MusicShop.ViewModels;
using ModelsLibrary.Models;

namespace MusicShop.Views
{
	public partial class ClientWindow : Window
	{
		public ClientWindow(Account account)
		{
			InitializeComponent();
			this.DataContext = new ClientViewModel(account);
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			genresList.SelectedIndex = 0;
			authorsList.SelectedIndex = 0;
        }
    }
}
