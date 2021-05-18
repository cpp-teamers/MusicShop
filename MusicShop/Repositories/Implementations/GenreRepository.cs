using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Repositories.Interfaces;
using ModelsLibrary.Models;
using ModelsLibrary.EF;
using System.Data.Entity;

namespace MusicShop.Repositories.Implementations
{
    class GenreRepository : IGenreRepository
    {
        private ModelsManager _modelManager = new ModelsManager();

        public void AddGenre(Genre addedGenre)
        {
            _modelManager.Genres.Add(addedGenre);
            _modelManager.SaveChanges();
        }

        public void ChangeGenre(Genre changedGenre)
        {
            var genre = _modelManager.Genres.Find(changedGenre.Id);
            genre.Name = changedGenre.Name;
            _modelManager.Entry(genre).State = EntityState.Modified;
        }

        public void DelGenre(int genreId)
        {
            var genre = _modelManager.Genres.Find(genreId);
            _modelManager.Genres.Remove(genre);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _modelManager.Genres.ToList();
        }
    }
}
