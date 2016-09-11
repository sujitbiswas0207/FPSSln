using FPSEntity;
using FPSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPSService.DomainService
{
    public interface IQuickQuoteDetailsManager
    {
        FPSEntity.DataModel.QuickQuoteDetail Insert(FPSEntity.DataModel.QuickQuoteDetail oQuickQuoteDetails);
    }

    public class UserShipmentDetailManager : IQuickQuoteDetailsManager
    {
        FPSEntity.DataModel.QuickQuoteDetail IQuickQuoteDetailsManager.Insert(FPSEntity.DataModel.QuickQuoteDetail oQuickQuoteDetails)
        {
            IQuickQuoteDetailsRepository oTranCatRepo = new QuickQuoteDetailsRepository();
            return oTranCatRepo.Insert(oQuickQuoteDetails);
        }
    }

    public interface IItemDetailsManager
    {
        FPSEntity.DataModel.ItemDetail Insert(FPSEntity.DataModel.ItemDetail oItemDetails);
    }

    public class ItemDetailManager : IItemDetailsManager
    {
        FPSEntity.DataModel.ItemDetail IItemDetailsManager.Insert(FPSEntity.DataModel.ItemDetail oItemDetails)
        {
            IItemDetailsRepository oTranCatRepo = new ItemDetailsRepository();
            return oTranCatRepo.Insert(oItemDetails);
        }
    }
}
