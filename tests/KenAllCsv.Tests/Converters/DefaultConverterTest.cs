using System.Collections.Generic;
using System.Linq;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests.Converters
{
    public class DefaultConverterTest
    {
        private readonly KenAllAddress _emptyAddress = new("", "", "", "", "");

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] {
                "名古屋市中村区",
                "名駅ミッドランドスクエア（高層棟）（１階）",
                new [] {"名駅ミッドランドスクエア　１階"}
            };
            yield return new object[]
            {
                "上北郡六戸町",
                "折茂（今熊「２１３～２３４、２４０、２４７、２６２、２６６、２７５、２７７、２８０、２９５、１１９９、１２０６、１５０４を除く」、大原、沖山、上折茂「１－１３、７１－１９２を除く」）",
                new []{"折茂", "折茂今熊", "折茂大原", "折茂沖山", "折茂上折茂"}
            };
            yield return new object[]
            {
                "京都市上京区",
                "神明町（御前通寺之内下る、御前通今出川上る、御前通今出川上る２丁目、御前通今出川上る３丁目、寺之内通御前西入）",
                new []{ "神明町", "御前通寺之内下る神明町", "御前通今出川上る神明町", "御前通今出川上る２丁目神明町", "御前通今出川上る３丁目神明町", "寺之内通御前西入神明町" }
            };
            yield return new object[]{
                "和賀郡西和賀町",
                "越中畑６４地割～越中畑６６地割",
                new []{"越中畑"}
            };
            yield return new object[]
            {
                "吾川郡仁淀川町",
                "土居（甲・乙）",
                new [] {"土居", "土居甲", "土居乙"}
            };
        }

        [Theory,
        MemberData(nameof(TestData))]
        public void ConvertTest(string city, string town, string[] expected)
        {
            var converter = new DefaultConverter();
            var list = converter.Convert(_emptyAddress with { City = city, Town = town }).ToList();
            Assert.Equal(expected, list.Select(addr => addr.Town));
        }
    }
}
