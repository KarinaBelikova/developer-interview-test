using System;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.AmountCalculationStrategy;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var strategies = new Dictionary<IncentiveType, IAmountCalculationStrategy>
        {
            { IncentiveType.FixedCashAmount, new FixedCashAmountStrategy() },
            { IncentiveType.FixedRateRebate, new FixedRateRebateStrategy() },
            { IncentiveType.AmountPerUom, new AmountPerUomStrategy() },
        };

        var rebateService = new RebateService(strategies);

        var request = new CalculateRebateRequest {
            RebateIdentifier = "Test_Rebate_Identifier",
            ProductIdentifier = "Test_Product_Identifier",
            Volume = 100
        };

        var result = rebateService.Calculate(request);

        Console.WriteLine($"Rebate Result: {result.Success}");
        
        if (result.Success) 
            Console.WriteLine($"Rebate Amount: {result}");
        
    }
}
