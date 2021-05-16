using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
    }
}
