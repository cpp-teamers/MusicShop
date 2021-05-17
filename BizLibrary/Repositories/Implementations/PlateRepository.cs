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
    class PlateRepository : IPlateRepository
    {
        private ModelsManager _modelManager = new ModelsManager();
        public void AddPlate(Plate addedPlate)
        {
            _modelManager.Plates.Add(addedPlate);
            _modelManager.SaveChanges();
        }

        public void ChangePlate(Plate changedPlate)
        {
            var plate = _modelManager.Plates.Find(changedPlate.Id);
            plate.Amount = changedPlate.Amount;
            plate.AuthorId = changedPlate.AuthorId;
            plate.Cost = changedPlate.Cost;
            plate.GenreId = changedPlate.GenreId;
            plate.Name = changedPlate.Name;
            plate.PublishDate = changedPlate.PublishDate;
            plate.PublisherId = changedPlate.PublisherId;
            plate.RealCost = changedPlate.RealCost;
            _modelManager.Entry(plate).State = EntityState.Modified;
        }

        public void DelPlate(int plateId)
        {
            var plate = _modelManager.Plates.Find(plateId);
            _modelManager.Plates.Remove(plate);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Plate> GetAllPlates()
        {
            return _modelManager.Plates.ToList();
        }

        public IEnumerable<Plate> GetAllPlatesByAuthorId(int authorId)
        {
            return _modelManager.Plates.Where(p => p.AuthorId == authorId).ToList();
        }

        public IEnumerable<Plate> GetAllPlatesByAuthorIdAndGenreId(int genreId, int authorId)
        {
            return _modelManager.Plates.Where(p => p.GenreId == genreId && p.AuthorId == authorId).ToList();
        }

        public IEnumerable<Plate> GetAllPlatesByGenreId(int genreId)
        {
            return _modelManager.Plates.Where(p => p.GenreId == genreId).ToList();
        }

        public IEnumerable<Plate> GetAllPlatesByPublisherId(int publisherId)
        {
            return _modelManager.Plates.Where(p => p.PublisherId == publisherId).ToList();
        }

        public Plate GetPlateById(int plateId)
        {
            return _modelManager.Plates.Find(plateId);
        }

        public Plate GetPlateByName(string plateName)
        {
            return _modelManager.Plates.Where(p => p.Name == plateName).FirstOrDefault();
        }
    }
}
