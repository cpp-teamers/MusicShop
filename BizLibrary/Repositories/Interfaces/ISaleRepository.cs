using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface ISaleRepository
    {
        IEnumerable<Sale> GetAllSalesByAccoutId(int accountId);
        IEnumerable<Sale> GetAllSalesByPlateId(int plateId);
        void AddSale(Sale sale);
        void ChangeSale(Sale changedSale);
        void DelSale(int saleId);
    }
}
