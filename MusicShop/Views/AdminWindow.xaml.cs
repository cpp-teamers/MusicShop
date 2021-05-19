using MusicShop.ViewModels;
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

namespace MusicShop.Views
{
	/// <summary>
	/// Логика взаимодействия для ModeratorWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		public AdminWindow()
		{
			InitializeComponent();
			this.DataContext = new AdminViewModel();
		}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			if(authorsList.Items.Count > 0)
				authorsList.SelectedIndex = 0;
			if (genresList.Items.Count > 0)
				genresList.SelectedIndex = 0;
        }
    }
}
