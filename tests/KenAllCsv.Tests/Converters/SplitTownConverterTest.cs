using System.Collections.Generic;
using System.Linq;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests.Converters
{
    public class SplitTownConverterTest
    {
        private readonly KenAllAddress _emptyAddress = new("", "", "", "", "");

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[]
            {
                "添川（渡戸沢）",
                new []{"添川", "添川渡戸沢"}
            };
            yield return new object[]
            {
                "山田町下谷上（菊水山、高座川向、中一里山）",
                new []{ "山田町下谷上", "山田町下谷上菊水山", "山田町下谷上高座川向", "山田町下谷上中一里山" }
            };
            yield return new object[]
            {
                "甲、乙",
                new []{ "甲", "乙" }
            };
        }

        [Theory(DisplayName = "町域が分割される"),
        MemberData(nameof(TestData))]
        public void ConvertTest(string town, string[] expected)
        {
            var converter = new SplitTownConverter();
            var addresses = converter.Convert(_emptyAddress with { Town = town }).ToList();
            var towns = addresses.Select(addr => addr.Town);
            Assert.Equal(expected, towns);
        }

        public static IEnumerable<object[]> KyotoTestData()
        {
            // 東入 西入
            yield return new object[]
            {
                "京都市中京区",
                "和久屋町（竹屋町通柳馬場西入、竹屋町通堺町東入、竹屋町通堺町西入、竹屋町通高倉東入）",
                new []{ "和久屋町", "竹屋町通柳馬場西入和久屋町", "竹屋町通堺町東入和久屋町", "竹屋町通堺町西入和久屋町", "竹屋町通高倉東入和久屋町" }
            };
            // 下る
            yield return new object[]
            {
                "京都市東山区",
                "五軒町（大和大路通三条下る、大和大路通三条下る西入）",
                new []{ "五軒町", "大和大路通三条下る五軒町", "大和大路通三条下る西入五軒町" }
            };
            // 上る 
            yield return new object[]
            {
                "京都市上京区",
                "一町目（上長者町通堀川東入、東堀川通上長者町上る、東堀川通中立売通下る）",
                new []{ "一町目", "上長者町通堀川東入一町目", "東堀川通上長者町上る一町目", "東堀川通中立売通下る一町目" }
            };
            // 三条大橋東４丁目
            yield return new object[]
            {
                "京都市東山区",
                "七軒町（三条大橋東４丁目、三条大橋東入４丁目）",
                new []{ "七軒町", "三条大橋東４丁目七軒町", "三条大橋東入４丁目七軒町" }
            };
        }

        [Theory(DisplayName = "京都の通り名は町名の前に付く"),
        MemberData(nameof(KyotoTestData))]
        public void KyotoTownTest(string city, string town, string[] expected)
        {
            var converter = new SplitTownConverter();
            var addresses = converter.Convert(_emptyAddress with { City = city, Town = town }).ToList();
            var towns = addresses.Select(addr => addr.Town);
            Assert.Equal(expected, towns);
        }
    }
}
