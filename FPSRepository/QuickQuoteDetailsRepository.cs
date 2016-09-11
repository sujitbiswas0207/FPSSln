using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FPSEntity.DataModel;
using FPSEntity;

namespace FPSRepository
{
    public interface IQuickQuoteDetailsRepository
    {
        FPSEntity.DataModel.QuickQuoteDetail Insert(FPSEntity.DataModel.QuickQuoteDetail oQuickQuoteDetails);
    }

   public class QuickQuoteDetailsRepository: IQuickQuoteDetailsRepository
    {
        FPSEntity.DataModel.QuickQuoteDetail IQuickQuoteDetailsRepository.Insert(FPSEntity.DataModel.QuickQuoteDetail oQuickQuoteDetails)
        {
            oQuickQuoteDetails.UpdatedDate = DateTime.Now;
            oQuickQuoteDetails.CreatedDate = DateTime.Now;
            FPSDatabaseEntities oFPSContext = new FPSDatabaseEntities();
            oFPSContext.QuickQuoteDetails.Add(oQuickQuoteDetails);
            oFPSContext.SaveChanges();
            return oQuickQuoteDetails;
        }
    }

    public interface IItemDetailsRepository
    {
        FPSEntity.DataModel.ItemDetail Insert(FPSEntity.DataModel.ItemDetail oItemDetails);
    }

    public class ItemDetailsRepository : IItemDetailsRepository
    {
        FPSEntity.DataModel.ItemDetail IItemDetailsRepository.Insert(FPSEntity.DataModel.ItemDetail oItemDetails)
        {
            oItemDetails.UpdatedDate = DateTime.Now;
            oItemDetails.CreatedDate = DateTime.Now;
            FPSDatabaseEntities oFPSContext = new FPSDatabaseEntities();
            oFPSContext.ItemDetails.Add(oItemDetails);
            oFPSContext.SaveChanges();
            return oItemDetails;
        }
    }
}
