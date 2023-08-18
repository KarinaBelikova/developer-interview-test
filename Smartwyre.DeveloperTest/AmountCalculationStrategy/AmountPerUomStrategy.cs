using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.AmountCalculationStrategy;

public class AmountPerUomStrategy : IAmountCalculationStrategy
{
    public decimal CalculateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount * request.Volume;
    }

    public bool IsValide(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate != null && rebate.Incentive == IncentiveType.AmountPerUom
            && product != null && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
            && rebate.Amount != 0 && request.Volume != 0;
    }
}