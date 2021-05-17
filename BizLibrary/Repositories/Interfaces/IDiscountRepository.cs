using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models;

namespace BizLibrary.Repositories.Interfaces
{
    interface IDiscountRepository
    {
        IEnumerable<Discount> GetAllDiscountsByEndDate(DateTime endDate);
        IEnumerable<Discount> GetAllDiscountsByStartDate(DateTime startDate);
        void AddDiscount(Discount addedDiscount);
        void ChangeDiscount(Discount changedDiscount);
        void DelDiscount(int discountId);
    }
}