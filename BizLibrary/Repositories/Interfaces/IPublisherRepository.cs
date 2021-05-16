using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IPublisherRepository
    {
        IEnumerable<Publisher> GetAllPublishers();
        Publisher GetPublisherById(int publisherId);
        void AddPublisher(Publisher addedPublisher);
        void ChangePublisher(Publisher changedPublisher);
        void DelPublisher(int publisherId);
    }
}
