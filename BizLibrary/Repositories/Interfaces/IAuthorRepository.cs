using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorById(int authorId);
        void AddAuthor(Author addedAuthor);
        void ChangeAuthor(Author changedAuthor);
        void DelAuthor(int authorId);
    }
}
