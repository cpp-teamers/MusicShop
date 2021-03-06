using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace MusicShop.Repositories.Interfaces
{
    interface ITrackRepository
    {
        IEnumerable<Track> GetAllTracksByPlateId(int plateId);
        void AddTrack(Track track);
        void ChangeTrack(Track changedTrack);
        void DelTrack(int trackId);
    }
}
