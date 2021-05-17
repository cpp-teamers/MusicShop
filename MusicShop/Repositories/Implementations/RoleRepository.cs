using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLibrary.Repositories.Interfaces;
using ModelsLibrary.Models;
using ModelsLibrary.EF;

namespace MusicShop.Repositories.Implementations
{
    class RoleRepository : IRoleRepository
    {
        private ModelsManager _modelManager = new ModelsManager();
        public IEnumerable<Role> GetAllRoles()
        {
            return _modelManager.Roles.ToList();
        }
    }
}
