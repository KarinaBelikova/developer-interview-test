using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
    void StoreRebate(Rebate rebate, decimal rebateAmount);
}
