using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.AmountCalculationStrategy;

public class FixedRateRebateStrategy : IAmountCalculationStrategy
{
    public decimal CalculateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return product.Price * rebate.Percentage * request.Volume;
    }

    public bool IsValide(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null && rebate.Incentive == IncentiveType.FixedRateRebate
            && product != null && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
            && rebate.Percentage != 0 && product.Price != 0 && request.Volume != 0;
    }
}