using System.Linq;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests.Converters
{
    public class BuildingAddressConverterTest
    {
        private readonly KenAllAddress _emptyAddress = new("", "", "", "", "");

        [Theory(DisplayName = "ビルの階数表記から丸括弧が削除される")]
        [InlineData("赤坂赤坂アークヒルズ・アーク森ビル（地階・階層不明）", "赤坂赤坂アークヒルズ・アーク森ビル")]
        [InlineData("赤坂赤坂アークヒルズ・アーク森ビル（１階）", "赤坂赤坂アークヒルズ・アーク森ビル　１階")]
        [InlineData("赤坂赤坂アークヒルズ・アーク森ビル（３７階）", "赤坂赤坂アークヒルズ・アーク森ビル　３７階")]
        public void ConvertTest(string town, string expected)
        {
            var converter = new BuildingAddressConverter();
            var list = converter.Convert(_emptyAddress with { Town = town }).ToList();
            Assert.Single(list);
            Assert.Equal(expected, list.First().Town);
        }
    }
}
