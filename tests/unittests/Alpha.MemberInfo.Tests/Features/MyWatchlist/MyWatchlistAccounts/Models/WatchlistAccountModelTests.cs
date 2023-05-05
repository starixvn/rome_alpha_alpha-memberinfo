namespace Alpha.MemberInfo.Tests.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using System;
    using Xunit;

    [Trait("Category", "MyWatchlistAccounts")]
    public class WatchlistAccountModelTests
    {
        [Fact]
        public void Constructor_Always_AssignCorrectProperties()
        {
            // Arrange 
            int rowNumber = 1;
            int custId = 1234;
            int sporttype = 0;
            string username = "agent0101";
            DateTime lastLogged = new DateTime(2022, 1, 1);
            string status = "Open";
            decimal? winningThreshold = 1000;
            decimal? losingThreshold = 2000;
            byte closedReason = 0;
            byte locked = 0;
            bool isPA = true;

            // Act
            var model = new WatchlistAccountModel(rowNumber, custId, sporttype, username, lastLogged, status, winningThreshold, losingThreshold, closedReason, locked, isPA);

            // Assert
            Assert.True(model.RowNumber == rowNumber
                && model.CustId == custId
                && model.Sporttype == sporttype
                && model.Username == username
                && model.LastLogged == lastLogged
                && model.Status == status
                && model.WinningThreshold == winningThreshold
                && model.LosingThreshold == losingThreshold
                && model.ClosedReason == closedReason
                && model.Locked == locked
                && model.IsPA == isPA);
        }
    }
}
