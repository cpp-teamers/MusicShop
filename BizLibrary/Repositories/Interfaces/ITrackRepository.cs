using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface ITrackRepository
    {
        IEnumerable<Track> GetAllTracksByPlateId(int plateId);
        void AddTrack(Track track);
        void ChangeTrack(Track track);
        void DelTrack(int trackId);
    }
}
