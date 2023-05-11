namespace Alpha.MemberInfo.Tests.Models.CreditBalance
{
    using System;
    using AppResources;
    using MemberInfo.Models.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceItemTests
    {
        [Fact]
        public void DefaultConstructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var model = new CreditBalanceItem();

            // Act & Assert
            Assert.True(
                model.RowNumber == 0
                && model.Enabled == false
                && model.Closed == false
                && model.ClosedReason == 0
                && model.Suspended == false
                && model.CustId == 0
                && model.UserName == null
                && model.FirstName == null
                && model.LastName == null
                && model.Credit == 0
                && model.BetCredit == 0
                && model.Balance == 0
                && model.Yesterday_Balance == 0
                && model.Outstanding == 0
                && model.LastLogged == null
                && model.IPLogin == null
                && model.Status == Resources.Disabled);
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var lastLogged = new DateTime(2018, 8, 7, 4, 47, 0);
            var model = new CreditBalanceItem(
                1,
                true,
                false,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                lastLogged,
                "192.168.1.1");
            model.ClosedReason = 2;

            // Act & Assert
            Assert.True(
                model.RowNumber == 1
                && model.Enabled == true
                && model.Closed == false
                && model.Suspended == false
                && model.CustId == 1176070
                && model.UserName == "master1"
                && model.FirstName == "---"
                && model.LastName == "---"
                && model.Credit == 900000002
                && model.BetCredit == 826715185
                && model.Balance == -20399.61m
                && model.Yesterday_Balance == -20399.61m
                && model.Outstanding == 371662
                && model.LastLogged == lastLogged
                && model.IPLogin == "192.168.1.1"
                && model.Status == Resources.Open
                && model.ClosedReason == 2);
        }

        [Fact]
        public void Constructor_Disabled_ReturnsStatusIsDisabled()
        {
            // Arrange
            var model = new CreditBalanceItem(
                1,
                false,
                false,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act & Assert
            Assert.True(model.Status == Resources.Disabled);
        }

        [Fact]
        public void Constructor_Closed_ReturnsStatusIsClosed()
        {
            // Arrange
            var model = new CreditBalanceItem(
                1,
                true,
                true,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act & Assert
            Assert.True(model.Status == Resources.Closed);
        }

        [Fact]
        public void Constructor_Suspended_ReturnsStatusIsSuspended()
        {
            // Arrange
            var model = new CreditBalanceItem(
                1,
                true,
                false,
                true,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act & Assert
            Assert.True(model.Status == Resources.Suspended);
        }

        [Fact]
        public void Constructor_Open_ReturnsStatusIsOpen()
        {
            // Arrange
            var model = new CreditBalanceItem(
                1,
                true,
                false,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act & Assert
            Assert.True(model.Status == Resources.Open);
        }
    }
}
