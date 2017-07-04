using System;
using System.Collections.Specialized;
using System.Configuration;
using Alpha.MemberInfo.Configurations;
using Alpha.MemberInfo.Tests.Fixtures;
using Alpha.MemberInfo.Tests.Helpers;
using Xunit;

namespace Alpha.MemberInfo.Tests.Configurations
{
    [Trait("Category", "BaseFunction")]
    [Collection("AnyValueCreatorCollection")]
    public class AppSettingsTests
    {
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;

        public AppSettingsTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
        }

        [Fact]
        public void StoreProcedurePath_Always_ReturnsValueFromConfig()
        {
            // Arrange
            var storeProcedurePath = anyValueCreatorFixture.Create<string>();
            var excpectedStoreProcedurePath = storeProcedurePath;
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["StoredProcedurePath"] = storeProcedurePath
            };

            // Act
            var actualStoreProcedurePath = AppSettings.StoreProcedurePath;

            // Assert
            Assert.Equal(excpectedStoreProcedurePath, actualStoreProcedurePath);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void Verify_NotHaveStoreProcedurePathConfig_ThrowsConfigurationErrorsException(string storeProcedurePath)
        {
            // Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["StoredProcedurePath"] = storeProcedurePath
            };

            // Act
            Exception actualExcelption = ExceptionRecorder.Record(() => AppSettings.Verify());

            // Act & Assert
            Assert.True(actualExcelption is ConfigurationErrorsException);
        }

        [Fact]
        public void Verify_HaveAllRequiredConfig_NotThrowsException()
        {
            // Arrange
            var storeProcedurePath = anyValueCreatorFixture.Create<string>();
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["StoredProcedurePath"] = storeProcedurePath
            };

            // Act
            Exception actualExcelption = ExceptionRecorder.Record(() => AppSettings.Verify());

            // Assert
            Assert.Null(actualExcelption);
        }

        [Fact]
        public void OldMainAppPath_Always_ReturnsValueFromConfig()
        {
            // Arrange
            var oldMainAppPath = anyValueCreatorFixture.Create<string>();
            var excpectedOldMainAppPath = oldMainAppPath;
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["OldMainAppPath"] = oldMainAppPath
            };

            // Act
            var actualOldMainAppPath = AppSettings.OldMainAppPath;

            // Assert
            Assert.Equal(excpectedOldMainAppPath, actualOldMainAppPath);
        }
    }
}
