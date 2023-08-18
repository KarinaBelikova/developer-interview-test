using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.AmountCalculationStrategy;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IDictionary<IncentiveType, IAmountCalculationStrategy> _calculationStrategies;

    public RebateService(IDictionary<IncentiveType, IAmountCalculationStrategy> calculationStrategies)
    {
        _calculationStrategies = calculationStrategies;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        decimal rebateAmount = 0m;

        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        var rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = productDataStore.GetProduct(request.ProductIdentifier);

        if (_calculationStrategies.TryGetValue(rebate.Incentive, out var strategy))
        {
            if (strategy.IsValide(rebate, product, request))
            {
                rebateAmount = strategy.CalculateAmount(rebate, product, request);
                result.Success = true;
            }
        }

        if (result.Success)
            StoreRebate(rebate, rebateAmount);

        return result;
    }

    public void StoreRebate(Rebate rebate, decimal rebateAmount) 
    {
        var storeRebateDataStore = new RebateDataStore();
        storeRebateDataStore.StoreCalculationResult(rebate, rebateAmount);
    }
}
