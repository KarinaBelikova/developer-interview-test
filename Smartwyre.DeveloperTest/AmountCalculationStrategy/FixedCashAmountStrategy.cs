using System.Net.Http.Headers;
using System.Threading.Channels;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.AmountCalculationStrategy;

public class FixedCashAmountStrategy : IAmountCalculationStrategy
{
    public decimal CalculateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount;
    }

    public bool IsValide(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null && rebate.Incentive == IncentiveType.FixedCashAmount
            && product != null && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
            && rebate.Amount != 0;
    }
}