namespace Alpha.Products.AccountInfo.Tests.TestCases
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using NSubstitute;
    using Utility;

    public class InvalidRequestsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { RequestHelper.CreateRequest(3, 4) };
            yield return new object[] { RequestHelper.CreateRequest(2, 4) };
            yield return new object[] { RequestHelper.CreateRequest(1, 4) };
            yield return new object[] { RequestHelper.CreateRequest(2, 3) };
            yield return new object[] { RequestHelper.CreateRequest(1, 3) };
            yield return new object[] { RequestHelper.CreateRequest(1, 2) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class SettingViewModelFactoryTestCases : IEnumerable<object[]>
    {
        private static readonly IEnumerable<ISearchAccountInfoRequest> ValidRequests =
             new List<ISearchAccountInfoRequest>()
              {
                    RequestHelper.CreateRequest(4, 3),
                    RequestHelper.CreateRequest(4, 2),
                    RequestHelper.CreateRequest(4, 1),
                    RequestHelper.CreateRequest(3, 2),
                    RequestHelper.CreateRequest(3, 1),
                    RequestHelper.CreateRequest(2, 1),
              };

        public IEnumerator<object[]> GetEnumerator() => ProductViewModels().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static IEnumerable<object[]> ProductViewModels()
        {
            var productViewModels = Enumerable.Empty<object[]>();

            foreach (var request in ValidRequests)
            {
                productViewModels = productViewModels.Concat(GetProductViewModels(request));
            }

            return productViewModels;
        }

        private static IConfigurationItem CreateDynamicProduct(int productId)
        {
            var dynamicProduct = Substitute.For<IConfigurationItem>();
            dynamicProduct.ProductId.Returns(productId);
            return dynamicProduct;
        }

        private static IEnumerable<object[]> GetProductViewModels(ISearchAccountInfoRequest request)
        {
            var sportsbook = Substitute.For<IConfigurationItem>();
            sportsbook.ProductId.Returns(1);
            return new List<object[]>()
            {
                new object[] { request, 1, new SportsbookViewModel(request, sportsbook) },
                new object[] { request, 5, new DynamicProductViewModel(request, CreateDynamicProduct(5)) },
                new object[] { request, 16, new DynamicProductViewModel(request, CreateDynamicProduct(16)) },
                new object[] { request, 21, new DynamicProductViewModel(request, CreateDynamicProduct(21)) },
                new object[] { request, 22, new DynamicProductViewModel(request, CreateDynamicProduct(22)) },
                new object[] { request, 23, new DynamicProductViewModel(request, CreateDynamicProduct(23)) },
                new object[] { request, 24, new DynamicProductViewModel(request, CreateDynamicProduct(24)) },
                new object[] { request, 28, new DynamicProductViewModel(request, CreateDynamicProduct(28)) },
                new object[] { request, 31, new DynamicProductViewModel(request, CreateDynamicProduct(31)) },
                new object[] { request, 32, new DynamicProductViewModel(request, CreateDynamicProduct(32)) }
            };
        }
    }
}