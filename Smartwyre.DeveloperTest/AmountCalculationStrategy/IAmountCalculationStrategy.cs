using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.AmountCalculationStrategy;

public interface IAmountCalculationStrategy
{
    bool IsValide(Rebate rebate, Product product, CalculateRebateRequest request);
    decimal CalculateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
}