namespace Alpha.MemberInfo.Site.Tests.Features.DormantAccountList
{
    using System;
    using System.Linq;
    using Alpha.MemberInfo.Features.DormantAccountList.Models;
    using NSubstitute;
    using ONEbook.SpreadSheets;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class DormantAccountWorkbookImplTests
    {
        private readonly DormantAccountWorkbookImplForTesting workbook;
        private readonly Sheet sheet;

        public DormantAccountWorkbookImplTests()
        {
            workbook = new DormantAccountWorkbookImplForTesting();
            sheet = Substitute.For<Sheet>();
        }

        [Fact]
        public void SetData_Always_SetsCorrectData()
        {
            // Arrange
            var dormantAccounts = Enumerable.Empty<DormantAccount>();
            var total = new DormantAccount();

            // Act
            workbook.SetData(dormantAccounts, total);

            // Assert
            Assert.True(dormantAccounts == workbook.DormantAccounts && total == workbook.Total);
        }

        [Fact]
        public void SetRowMappers_Always_SetsCorrectValue()
        {
            // Arrange
            var dormantAccountMapper = new Mapper<DormantAccount>();
            var totalMapper = new Mapper<DormantAccount>();

            // Act
            workbook.SetRowMappers(dormantAccountMapper, totalMapper);

            // Assert
            Assert.True(dormantAccountMapper == workbook.DormantAccountMapper && totalMapper == workbook.TotalMapper);
        }

        [Fact]
        public void FillBody_NullSheet_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => workbook.CallFillBody(null));
        }

        [Fact]
        public void TotalRowTemplate_Always_ReturnsCorrectValue()
        {
            // Arrange
            var row = Substitute.For<Row>();

            // Act
            workbook.TotalRowTemplate = row;

            // Assert
            Assert.Equal(row, workbook.TotalRowTemplate);
        }

        [Fact]
        public void FillNoInfoRow_Always_FillsDataWithCorrectColumnLength()
        {
            // Arrange & Act
            workbook.CallFillNoInfoRow(sheet);

            // Assert
            sheet.Received().SetCellRangeValue(3, 0, 3, -1, "No information available.");
        }

        [Fact]
        public void FillNoInfoRow_Always_AlignCenterText()
        {
            // Arrange
            var cell = Substitute.For<Cell>();
            sheet.GetCell(3, 0).Returns(cell);

            // Act
            workbook.CallFillNoInfoRow(sheet);

            // Assert
            cell.Received().SetCenterHorizontalAlignment();
        }
    }
}
