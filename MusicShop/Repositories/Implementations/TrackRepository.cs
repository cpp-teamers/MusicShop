using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLibrary.Repositories.Interfaces;
using ModelsLibrary.Models;
using ModelsLibrary.EF;
using System.Data.Entity;

namespace MusicShop.Repositories.Implementations
{
    class TrackRepository : ITrackRepository
    {
        private ModelsManager _modelManager = new ModelsManager();
        public void AddTrack(Track track)
        {
            _modelManager.Tracks.Add(track);
            _modelManager.SaveChanges();
        }

        public void ChangeTrack(Track changedTrack)
        {
            var track = _modelManager.Tracks.Find(changedTrack.Id);
            track.Duration = changedTrack.Duration;
            track.Name = changedTrack.Name;
            track.PlateId = changedTrack.PlateId;
            _modelManager.Entry(track).State = EntityState.Modified;
        }

        public void DelTrack(int trackId)
        {
            var deletedTrack = _modelManager.Tracks.Find(trackId);
            _modelManager.Tracks.Remove(deletedTrack);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Track> GetAllTracksByPlateId(int plateId)
        {
            return _modelManager.Tracks.Where(t => t.PlateId == plateId).ToList();
        }
    }
}
