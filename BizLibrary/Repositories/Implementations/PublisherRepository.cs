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
    class PublisherRepository : IPublisherRepository
    {
        private ModelsManager _modelManager = new ModelsManager();
        public void AddPublisher(Publisher addedPublisher)
        {
            _modelManager.Publishers.Add(addedPublisher);
            _modelManager.SaveChanges();
        }

        public void ChangePublisher(Publisher changedPublisher)
        {
            var publisher = _modelManager.Publishers.Find(changedPublisher.Id);
            publisher.Name = changedPublisher.Name;
            _modelManager.Entry(publisher).State = EntityState.Modified;
        }

        public void DelPublisher(int publisherId)
        {
            var publisher = _modelManager.Publishers.Find(publisherId);
            _modelManager.Publishers.Remove(publisher);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            return _modelManager.Publishers.ToList();
        }

        public Publisher GetPublisherById(int publisherId)
        {
            return _modelManager.Publishers.Find(publisherId);
        }
    }
}
