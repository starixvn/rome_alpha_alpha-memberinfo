namespace Alpha.MemberInfo.Site.Tests.Features.MyWatchlist.ReachedThreshold
{
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using NSubstitute;
    using ONEbook.SpreadSheets;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Category", "ReachedThreshold")]
    public class ReachedThresholdWorkbookImplTests
    {
        private readonly ReachedThresholdWorkbookImplForTesting workbook;
        private readonly Sheet sheet;

        public ReachedThresholdWorkbookImplTests()
        {
            workbook = new ReachedThresholdWorkbookImplForTesting();
            sheet = Substitute.For<Sheet>();
        }

        [Fact]
        public void SetData_Always_SetsCorrectData()
        {
            // Arrange
            var reachlistAccounts = Enumerable.Empty<ReachlistAccountModel>();

            // Act
            workbook.SetData(reachlistAccounts);

            // Assert
            Assert.True(reachlistAccounts == workbook.Data);
        }

        [Fact]
        public void SetRowMappers_Always_SetsCorrectValue()
        {
            // Arrange
            var reachlistAccountMapper = new Mapper<ReachlistAccountModel>();

            // Act
            workbook.SetRowMappers(reachlistAccountMapper);

            // Assert
            Assert.True(reachlistAccountMapper == workbook.RowMapper);
        }

        [Fact]
        public void FillBody_NullSheet_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => workbook.CallFillBody(null));
        }

        //[Fact]
        //public void FillNoInfoRow_Always_FillsDataWithCorrectColumnLength()
        //{
        //    // Arrange & Act
        //    workbook.CallFillNoInfoRow(sheet);

        //    // Assert
        //    sheet.Received().SetCellRangeValue(3, 0, 3, -1, "No information available.");
        //}

        //[Fact]
        //public void FillNoInfoRow_Always_AlignCenterText()
        //{
        //    // Arrange
        //    var cell = Substitute.For<Cell>();
        //    sheet.GetCell(3, 0).Returns(cell);

        //    // Act
        //    workbook.CallFillNoInfoRow(sheet);

        //    // Assert
        //    cell.Received().SetCenterHorizontalAlignment();
        //}
    }
}
