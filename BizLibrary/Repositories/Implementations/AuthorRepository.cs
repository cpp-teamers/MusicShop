using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLibrary.Repositories.Interfaces;
using ModelsLibrary.Models;
using ModelsLibrary.EF;
using System.Data.Entity;

namespace BizLibrary.Repositories.Implementations
{
    class AuthorRepository : IAuthorRepository
    {
        private ModelsManager _modelManager = new ModelsManager();

        public void AddAuthor(Author addedAuthor)
        {
            _modelManager.Authors.Add(addedAuthor);
            _modelManager.SaveChanges();
        }

        public void ChangeAuthor(Author changedAuthor)
        {
            var author = _modelManager.Authors.Find(changedAuthor.Id);
            author.Name = changedAuthor.Name;
            _modelManager.Entry(author).State = EntityState.Modified;
        }

        public void DelAuthor(int authorId)
        {
            var author = _modelManager.Authors.Find(authorId);
            _modelManager.Authors.Remove(author);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _modelManager.Authors.ToList();
        }

        public Author GetAuthorById(int authorId)
        {
           return _modelManager.Authors.Find(authorId);
        }
    }
}
