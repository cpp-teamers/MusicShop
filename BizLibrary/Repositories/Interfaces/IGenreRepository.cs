using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGenres();
        void AddGenre(Genre addedGenre);
        void ChangeGenre(Genre changedGenre);
        void DelGenre(int genreId);
    }
}