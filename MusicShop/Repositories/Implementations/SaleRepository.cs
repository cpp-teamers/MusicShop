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
    class SaleRepository : ISaleRepository
    {
        private ModelsManager _modelManager = new ModelsManager();
        public void AddSale(Sale sale)
        {
            _modelManager.Sales.Add(sale);
            _modelManager.SaveChanges();
        }

        public void ChangeSale(Sale changedSale)
        {
            var sale = _modelManager.Sales.Find(changedSale.Id);
            sale.AccountId = changedSale.AccountId;
            sale.AmountOfSales = changedSale.AmountOfSales;
            sale.DateOfSale = changedSale.DateOfSale;
            sale.PlateId = changedSale.PlateId;
            sale.Price = changedSale.Price;
            _modelManager.Entry(sale).State = EntityState.Modified;
        }

        public void DelSale(int saleId)
        {
            var sale = _modelManager.Sales.Find(saleId);
            _modelManager.Sales.Remove(sale);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Sale> GetAllSalesByAccoutId(int accountId)
        {
            return _modelManager.Sales.Where(s => s.AccountId == accountId).ToList();
        }

        public IEnumerable<Sale> GetAllSalesByPlateId(int plateId)
        {
            return _modelManager.Sales.Where(s => s.PlateId == plateId).ToList();
        }
    }
}
