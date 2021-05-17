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
    class AllRepositories
    {
        public AccountRepository AccountRepository = new AccountRepository();
        public AuthorRepository AuthorRepository = new AuthorRepository();
        public DiscountRepository DiscountRepository = new DiscountRepository();
        public GenreRepository GenreRepository = new GenreRepository();
        public PlateRepository PlateRepository = new PlateRepository();
        public PublisherRepository PublisherRepository = new PublisherRepository();
        public RoleRepository RoleRepository = new RoleRepository();
        public SaleRepository SaleRepository = new SaleRepository();
        public TrackRepository TrackRepository = new TrackRepository();
    }
}
