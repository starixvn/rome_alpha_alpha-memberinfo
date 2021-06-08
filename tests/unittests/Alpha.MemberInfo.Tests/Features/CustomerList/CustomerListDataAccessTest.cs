namespace Alpha.MemberInfo.Tests.DataAccesses
{
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Data;
    using NSubstitute;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("ObjectDbCollection")]
    public class CustomerListDataAccessTest
    {
        private readonly CustomerListDataAccessImpl dataAccess;
        private readonly IObjectDb objectDb;
        private readonly IMultiResultReader multiResultReader;

        public CustomerListDataAccessTest()
        {
            DbSettingProviderManager
                .StartTestingSession()
                .Run();

            objectDb = Substitute.For<IObjectDb>();
            multiResultReader = Substitute.For<IMultiResultReader>();

            dataAccess = new CustomerListDataAccessImpl();

            ObjectDbFactory.Mock(
                (key, timeout)
                =>
                objectDb);
        }

        [Fact]
        public void UpdateCustomerStatus_UpdatedSuccessfully_ReturnSuccessResult()
        {
            // Arrange
            var command = new UpdateCustomerStatusCommand(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                statusName: StatusNames.Closed,
                statusValue: true,
                language: "E",
                ip: "192.168.1.1");

            var expectedResult = new Result() { Code = 0, Message = string.Empty };
            objectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 0 }, { "errmsg", string.Empty } });

            // Act
            var actualResult = dataAccess.UpdateCustomerStatus(command);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void UpdateCustomerStatus_UpdateFailed_ReturnErrorResult()
        {
            // Arrange
            var command = new UpdateCustomerStatusCommand(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                statusName: StatusNames.Closed,
                statusValue: true,
                language: "E",
                ip: "192.168.1.1");

            var errorMsg = "Update Failed !";
            var expectedResult = new Result() { Code = 100, Message = "Update Failed !" };
            objectDb.GetOuputParams().Returns(new Hashtable { { "errcode", 100 }, { "errmsg", errorMsg } });

            // Act
            var actualResult = dataAccess.UpdateCustomerStatus(command);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void GetCustomerAllStatus_HasData_ReturnsCustomerStatusModel()
        {
            // Arrange
            var criteria = new SelectCustomerAllStatusModelCriteria(1);
            objectDb
                .QueryMultiple(Arg.Any<Hashtable>())
                .Returns(multiResultReader);

            var nonProduct = StubNonProductStatus();
            multiResultReader
                .Read<NonProductStatusModel>()
                .Returns(new List<NonProductStatusModel> { nonProduct } as IEnumerable<NonProductStatusModel>);

            var nonDCSProduct = StubNonDCSProduct();
            multiResultReader
                .Read<NonDCSProductStatusModel>()
                .Returns(new List<NonDCSProductStatusModel> { nonDCSProduct } as IEnumerable<NonDCSProductStatusModel>);

            var dcsProducts = StubDcsProducts();
            multiResultReader
               .Read<DCSProductStatusModel>()
               .Returns(dcsProducts as IEnumerable<DCSProductStatusModel>);

            // Act
            var actualData = dataAccess.GetCustomerStatus(criteria);

            // Assert
            AssertExtension.Equal(nonProduct, actualData.NonProduct);
            AssertExtension.Equal(nonDCSProduct, actualData.NonDCSProduct);
            AssertExtension.Equal(dcsProducts, actualData.DCSProducts);
        }

        private static NonDCSProductStatusModel StubNonDCSProduct()
            => new NonDCSProductStatusModel(
                             syncCasino: true,
                             disableCasino: true,
                             uplineDisableCS: true,
                             disableNumber: true,
                             uplineDisableNumber: true,
                             disableEsports: true,
                             uplineDisableEsports: true,
                             hideKeno: true,
                             disableKeno: false,
                             uplineHideGoldDeluxe: true,
                             uplineDisableGoldDeluxe: true,
                             disableGoldDeluxe: true,
                             hideGoldDeluxe: true);

        private static NonProductStatusModel StubNonProductStatus()
            => new NonProductStatusModel(
                             hasDownline: true,
                             disableAutoMPT: true,
                             closed: true,
                             isDirectClosed: 1,
                             uplineClosed: true,
                             suspended: true,
                             isDirectSuspend: true,
                             uplineSuspend: true,
                             transferOption: "00000000",
                             doubleCommLosing: true,
                             uplineDoubleCommLosing: true,
                             disableOutright: true,
                             uplineDisableOutright: true,
                             isAllDayBalTransfer: false,
                             locked: 0);

        private static List<DCSProductStatusModel> StubDcsProducts()
            => new List<DCSProductStatusModel>
                 {
                     new DCSProductStatusModel(
                                  productId: 5,
                                  sportType: 12,
                                  betType: 24,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true),
                     new DCSProductStatusModel(
                                  productId: 6,
                                  sportType: 13,
                                  betType: 26,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true)
                    };
    }
}