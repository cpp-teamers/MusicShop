using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicShop.Repositories.Interfaces;
using ModelsLibrary.Models;
using ModelsLibrary.EF;
using System.Data.Entity;

namespace MusicShop.Repositories.Implementations
{
    class DiscountRepository : IDiscountRepository    
    {
        private ModelsManager _modelManager = new ModelsManager();

        public void AddDiscount(Discount addedDiscount)
        {
            _modelManager.Discounts.Add(addedDiscount);
        }

        public void ChangeDiscount(Discount changedDiscount)
        {
            var discount = _modelManager.Discounts.Find(changedDiscount.Id);
            discount.Comment = changedDiscount.Comment;
            discount.EndDate = changedDiscount.EndDate;
            discount.Percent = changedDiscount.Percent;
            discount.StartDate = changedDiscount.StartDate;
            _modelManager.Entry(discount).State = EntityState.Modified;
        }

        public void DelDiscount(int discountId)
        {
            var discount = _modelManager.Discounts.Find(discountId);
            _modelManager.Discounts.Remove(discount);
            _modelManager.SaveChanges();
        }

        public IEnumerable<Discount> GetAllDiscountsByEndDate(DateTime endDate)
        {
            return _modelManager.Discounts.Where(d => d.EndDate == endDate).ToList();
        }

        public IEnumerable<Discount> GetAllDiscountsByStartDate(DateTime startDate)
        {
            return _modelManager.Discounts.Where(d => d.StartDate == startDate).ToList();
        }
    }
}
