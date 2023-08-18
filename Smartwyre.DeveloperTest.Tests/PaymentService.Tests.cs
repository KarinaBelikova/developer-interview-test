using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.AmountCalculationStrategy;
using Xunit;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {     
        [Fact]
        public void FixedCashAmountStrategy_CalculationResult_Valid()
        {
            // Arrange
            var strategy = new FixedCashAmountStrategy();
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 100 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest { Volume = 10, ProductIdentifier = "prod123", RebateIdentifier = "reb111" };
            var expectedCalculationResult = 100;
            
            // Act
            var calculationResult = strategy.CalculateAmount(rebate, product, request);
            
            // Assert
            Assert.Equal(expectedCalculationResult, calculationResult);
        }

        [Fact]
        public void FixedRateRebateStrategy_IsNotValide()
        {
            var strategy = new FixedRateRebateStrategy();
            var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.1m };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
            var request = new CalculateRebateRequest { Volume = 10, ProductIdentifier = "prod123" };

            var result = strategy.IsValide(rebate, product, request);

            Assert.False(result);
        }

        [Fact]
        public void AmountPerUomStrategy_IsValid()
        {
            // Arrange
            var strategy = new AmountPerUomStrategy();
            var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 50 };
            var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
            var request = new CalculateRebateRequest { Volume = 5, ProductIdentifier = "prod123" };

            // Act
            var result = strategy.IsValide(rebate, product, request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Calculate_ProductWithoutFlag_IsNotValid()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "rebate123", ProductIdentifier = "prod123", Volume = 10 };

            var strategies = new Dictionary<IncentiveType, IAmountCalculationStrategy>
            {
                { IncentiveType.FixedCashAmount, new FixedCashAmountStrategy() },
                { IncentiveType.FixedRateRebate, new FixedRateRebateStrategy() },
                { IncentiveType.AmountPerUom, new AmountPerUomStrategy() },
            };

            var rebateService = new RebateService(strategies);

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }
    }
}
