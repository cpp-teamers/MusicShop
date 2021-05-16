using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IPlateRepository
    {
        IEnumerable<Plate> GetAllPlatesByAuthorId(int authorId);
        IEnumerable<Plate> GetAllPlatesByPublisherId(int publisherId);
        IEnumerable<Plate> GetAllPlatesByGenreId(int genreId);
        Plate GetPlateById(int plateId);
        void AddPlate(Plate addedPlate);
        void ChangePlate(Plate changedPlate);
        void DelPlate(int plateId);
    }
}
