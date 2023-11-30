

namespace FinancialAssetManager.Tests.Services
{
    public class AssetManagerServiceTest
    {

        private static string _ChartUrl = @"https://query2.finance.yahoo.com/v8/finance/chart/";
        private static string _DataBase = @"FinancialDataBase";
        private static string _ConnectionString = @"mongodb://localhost:27017";

        private AssetManagerService GetAssetManagerService()
        {
            // Configuração do mock de IOptions<T>
            var credentialsOptions = new Mock<IOptions<CredentialsOptions>>();
            credentialsOptions.Setup(options => options.Value).Returns(new CredentialsOptions { ChartUrl = _ChartUrl });

            var mongoSettings = new Mock<IOptions<MongoSettings>>();
            mongoSettings.Setup(options => options.Value).Returns(new MongoSettings { ConnectionString = _ConnectionString, DataBase = _DataBase });

            var assetManagerRepository = new AssetManagerRepository(mongoSettings.Object);

            return new AssetManagerService(credentialsOptions.Object, assetManagerRepository);
        }

        [Fact]
        public void GetChartBySymbol_ValidateWhenSendNull()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.GetChartBySymbol(null).Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void GetChartBySymbol_ValidateWhenSendEmpty()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.GetChartBySymbol(string.Empty).Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void GetVariationBySimbol_ValidateWhenNotSendAnyValue()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.GetVariationBySimbol(null,0,false).Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void GetVariationBySimbol_ValidateWhenSendSymbolLowerCase()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.GetVariationBySimbol("PeTr4.SA", 0, false).Result;

            //Assert
            Assert.True(result.isSuccessful);
        }

        [Fact]
        public void CreateChartOnlineBySymbol_ValidateWhenNotSendSymbol()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.CreateChartOnlineBySymbol("").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void CreateChartOnlineBySymbol_ValidateWhenSendSymbolNotExistent()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.CreateChartOnlineBySymbol("NAOEXISTENTE").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void UpdateChartOnlineBySymbol_ValidateWhenNotSendSymbol()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.UpdateChartOnlineBySymbol("").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void UpdateChartOnlineBySymbol_ValidateWhenSendSymbolNotExistent()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.UpdateChartOnlineBySymbol("NAOEXISTENTE").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void DeleteChartById_ValidateWhenSendSymbolNull()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.DeleteChartById(null).Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void DeleteChartById_ValidateWhenSendSymbolEmpty()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.DeleteChartById("").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }

        [Fact]
        public void DeleteChartById_ValidateWhenSendSymbolNotExistent()
        {
            //Act
            var service = GetAssetManagerService();
            var result = service.DeleteChartById("345235sgsd").Result;

            //Assert
            Assert.False(result.isSuccessful);
        }
    }
}
