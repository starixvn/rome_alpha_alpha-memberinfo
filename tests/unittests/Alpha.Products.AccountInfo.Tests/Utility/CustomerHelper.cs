namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using Alpha.Products.Core.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class CustomerHelper
    {
        public static IEnumerable<CustomerProductStatus> CreateCustomerProductStatus()
          => new ReadOnlyCollection<CustomerProductStatus>(
                  new List<CustomerProductStatus>()
                  {
                        new CustomerProductStatus(1),
                        new CustomerProductStatus(3),
                        new CustomerProductStatus(5),
                        new CustomerProductStatus(6),
                        new CustomerProductStatus(8),
                        new CustomerProductStatus(12),
                        new CustomerProductStatus(15),
                        new CustomerProductStatus(16),
                        new CustomerProductStatus(21),
                        new CustomerProductStatus(22),
                        new CustomerProductStatus(23, true, true, true),
                        new CustomerProductStatus(24, true),
                        new CustomerProductStatus(26, true),
                        new CustomerProductStatus(28),
                        new CustomerProductStatus(30),
                        new CustomerProductStatus(31),
                        new CustomerProductStatus(32),
                        new CustomerProductStatus(41),
                        new CustomerProductStatus(44),
                  });

        public static IEnumerable<CustomerProductStatus> CreateCustomerProductStatus(params int[] productIds)
        {
            var customerProductStatus = new List<CustomerProductStatus>();

            foreach (var productId in productIds)
            {
                customerProductStatus.Add(new CustomerProductStatus(productId));
            }

            return customerProductStatus;
        }

        public static ICustomer CreateCustomer(
            int custId = 1,
            byte customerLevelValue = 4,
            int subAccountId = 0,
            IEnumerable<CustomerProductStatus> customerProductStatus = null)
        {
            customerProductStatus = customerProductStatus ?? CreateCustomerProductStatus();
            var upline = new Customer(custId, subAccountId, $"username{1}", (byte)(customerLevelValue + 1), customerProductStatus);
            var customer = new Customer(custId, subAccountId, $"username{1}", customerLevelValue, customerProductStatus, customerLevelValue.Equals(4) ? null : upline);

            return customer;
        }

        public static ICustomer CreateCustomer(
           int custId = 1,
           byte customerLevelValue = 4,
           int subAccountId = 0,
           params int[] productIds)
        {
            var customerProductStatus = CreateCustomerProductStatus(productIds);
            var upline = new Customer(custId, subAccountId, $"username{1}", (byte)(customerLevelValue + 1), customerProductStatus);
            var customer = new Customer(custId, subAccountId, $"username{1}", customerLevelValue, customerProductStatus, customerLevelValue.Equals(4) ? null : upline);

            return customer;
        }

        public static ICustomer CreateSuper(int custId = 1)
        {
            return CreateCustomer(custId, 4);
        }

        public static ICustomer CreateSuperSubAccount(int custId = 1, int subAccountId = 10)
        {
            return CreateCustomer(custId, 4, subAccountId);
        }

        public static ICustomer CreateMaster(int custId = 2)
        {
            return CreateCustomer(custId, 3);
        }

        public static ICustomer CreateMasterSubAccount(int custId, int subAccountId = 11)
        {
            return CreateCustomer(custId, 3, subAccountId);
        }

        public static ICustomer CreateAgent(int custId = 3)
        {
            return CreateCustomer(custId, 2);
        }

        public static ICustomer CreateAgentSubAccount(int custId, int subAccountId = 12)
        {
            return CreateCustomer(custId, 2, subAccountId);
        }

        public static ICustomer CreateMember(int custId = 4)
        {
            return CreateCustomer(custId, 2);
        }

        //blic IEditSettingRequest
    }
}