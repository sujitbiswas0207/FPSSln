using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPSEntity;
using FPSServie;

namespace FPSServie
{
    public interface IQuickQuoteDetailsManager
    {
        QuickQuoteDetails Insert(QuickQuoteDetails oQuickQuoteDetails);
    }
    public class QuickQuoteDetailsManager : IQuickQuoteDetailsManager
    {
        QuickQuoteDetails IQuickQuoteDetailsManager.Insert(QuickQuoteDetails oQuickQuoteDetails)
        {
            IQuickQuoteDetailsRepository oTranCatRepo = new QuickQuoteDetailsRepository();
            return oTranCatRepo.Insert(oQuickQuoteDetails);
        }
    }
}
