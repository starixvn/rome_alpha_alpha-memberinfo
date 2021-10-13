namespace Alpha.MemberInfo.Tests.Feature.DormantAccountList.Models
{
    using System;
    using Alpha.MemberInfo.Features.DormantAccountList.Models;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class DormantAccountTests
    {
        [Fact]
        public void Status_Disabled_ReturnsDisabled()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                4,
                1,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.Status;

            // Assert
            Assert.Equal("Disabled", actual);
        }

        [Fact]
        public void Status_Closed_ReturnsClosed()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                3,
                1,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                true,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.Status;

            // Assert
            Assert.Equal("Closed (*)", actual);
        }

        [Fact]
        public void Status_Invalid_ReturnsEmpty()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                0,
                1,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.Status;

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void ArchiveStage_AccountClosed_ReturnsAccountClosed()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                4,
                1,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.ArchiveStage;

            // Assert
            Assert.Equal("Close Account", actual);
        }

        [Fact]
        public void ArchiveStage_AccountClosed_ReturnsAccountDisabled()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                4,
                2,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.ArchiveStage;

            // Assert
            Assert.Equal("Disable Account", actual);
        }

        [Fact]
        public void ArchiveStage_AccountClosed_ReturnsAccountSettingDeleted()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                4,
                3,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.ArchiveStage;

            // Assert
            Assert.Equal("Delete Settings", actual);
        }

        [Fact]
        public void ArchiveStage_Invalid_ReturnsEmpty()
        {
            // Arrange
            byte invalidStageId = 10;
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                4,
                invalidStageId,
                1000,
                null,
                new DateTime(2020, 6, 5),
                new DateTime(2020, 9, 15, 15, 33, 0),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.ArchiveStage;

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void IsRedDateTime_Null_ReturnsFalse()
        {
            // Arrange
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                1,
                1,
                1000,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.IsRedDateTime;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsRedDateTime_LastLoggedLessThanClosedDate_ReturnsFalse()
        {
            // Arrange
            var current = DateTime.Now;
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                1,
                1,
                1000,
                null,
                current,
                current.AddHours(1),
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.IsRedDateTime;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void IsRedDateTime_LastLoggedGreaterThanClosedDate_ReturnsTrue()
        {
            // Arrange
            var current = DateTime.Now;
            var entity = new DormantAccountEntity(
                1,
                "SP0101",
                3,
                1,
                1,
                1000,
                null,
                current.AddHours(1),
                current,
                null,
                null,
                null,
                false,
                0);
            var dormantAccount = new DormantAccount(entity, 0);

            // Act
            var actual = dormantAccount.IsRedDateTime;

            // Assert
            Assert.True(actual);
        }
    }
}
