using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Commands;
using BizLibrary.Repositories.Implementations;
using System.Windows;

namespace MusicShop.ViewModels
{
    public class AuthorViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Author> Authors { get; set; }
        private AuthorRepository _ar { get; set; }
        public AuthorViewModel()
        {
            Authors = new ObservableCollection<Author>();
            _ar = new AuthorRepository();
        }
        private void LoadAuthors()
        {
            Authors.Clear();
            var authors = _ar.GetAllAuthors().ToList();
            foreach (var author in authors)
                Authors.Add(author);
        }
        private Author _selectedAuthor;
        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set 
            {
                _selectedAuthor = value;
                OnPropertyChanged("SelectedAuthor");
            }
        }
        private RelayCommand _addAuthor;
        public RelayCommand AddAuthor
        {
            get
            {
                return _addAuthor ?? (new RelayCommand(obj =>
                {
                    Author author = new Author() { Id = 0, Name="New Author"};
                    Authors.Add(author);
                    LoadAuthors();
                    OnPropertyChanged("Authors");
                }
            ));}
        }
        private RelayCommand _delAuthor;
        public RelayCommand DelAuthor
        {
            get
            {
                return _delAuthor ?? (new RelayCommand(obj =>
                {
                    var mess_result = MessageBox.Show($"Do you really want to remove {_selectedAuthor.Name}?", "Question", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (mess_result == MessageBoxResult.Yes)
                    {
                        int authorId = _selectedAuthor.Id;
                        Authors.RemoveAt(authorId);
                        _ar.DelAuthor(authorId);
                        LoadAuthors();
                        OnPropertyChanged("Authors");
                    }
                }
            ));
            }
        }
        private RelayCommand _saveAuthor;
        public RelayCommand SaveAuthor
        {
            get
            {
                return _saveAuthor ?? (new RelayCommand(obj =>
                {
                    if (_selectedAuthor.Id == 0)
                    {
                        _ar.AddAuthor(_selectedAuthor);
                        MessageBox.Show($"{_selectedAuthor.Name} was successfully added!");
                    }
                    else
                    { 
                        _ar.ChangeAuthor(SelectedAuthor);
                        MessageBox.Show($"{_selectedAuthor.Name} was successfully changed!");
                    }
                }
            ));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
