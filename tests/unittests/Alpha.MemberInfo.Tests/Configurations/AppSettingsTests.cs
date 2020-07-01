namespace Alpha.MemberInfo.Tests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using Alpha.MemberInfo.Configurations;
    using Alpha.MemberInfo.Tests.Fixtures;
    using Alpha.MemberInfo.Tests.Helpers;
    using Xunit;

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

        [Fact]
        public void MainAppPath_Always_ReturnsValueFromConfig()
        {
            // Arrange
            var mainAppPath = anyValueCreatorFixture.Create<string>();
            var excpectedMainAppPath = mainAppPath;
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["MainAppPath"] = mainAppPath
            };

            // Act
            var actualMainAppPath = AppSettings.MainAppPath;

            // Assert
            Assert.Equal(excpectedMainAppPath, actualMainAppPath);
        }

        [Fact]
        public void IndirectLoginAllowSuperIds_HasConfig_ReturnCorrectValues()
        {
            // Arrange

            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["IndirectLogin:AllowSuperIds"] = "1,2"
            };
            var expectedSetting = new List<int> { 1, 2 };

            // Act
            var actualSetting = AppSettings.IndirectLoginSuperIds;

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [Fact]
        public void IndirectLoginAllowSuperIds_HasConfig_ReturnCorrectSetting()
        {
            // Arrange

            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["IndirectLogin:AllowSuperIds"] = "1,2"
            };
            var expectedSetting = new[] { "1", "2" };

            // Act
            var actualSetting = AppSettings.AppSettingValues["IndirectLogin:AllowSuperIds"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Assert
            Assert.Equal(expectedSetting, actualSetting);
        }

        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, false)]
        public void IndirectLoginIsAllowSuperIds_HasConfig_ReturnCorrectValues(int srecommend, bool expected)
        {
            // Arrange

            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["IndirectLogin:AllowSuperIds"] = "1,2"
            };

            // Act
            var actualSetting = AppSettings.IsAllowedIndirectLogin(srecommend);

            // Assert
            Assert.Equal(expected, actualSetting);
        }

        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, false)]
        public void IndirectLoginIsAllowSuperIds_HasNotConfig_ReturnCorrectValues(int srecommend, bool expected)
        {
            // Arrange

            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["IndirectLogin:AllowSuperIds"] = ""
            };

            // Act
            var actualSetting = AppSettings.IsAllowedIndirectLogin(srecommend);

            // Assert
            Assert.Equal(expected, actualSetting);
        }

        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        public void IndirectLoginIsAllowSuperIds_HasAsteriskConfig_ReturnCorrectValues(int srecommend, bool expected)
        {
            // Arrange

            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["IndirectLogin:AllowSuperIds"] = "*"
            };

            // Act
            var actualSetting = AppSettings.IsAllowedIndirectLogin(srecommend);

            // Assert
            Assert.Equal(expected, actualSetting);
        }
    }
}
