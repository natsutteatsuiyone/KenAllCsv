using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests
{
    public class KenAllCsvParserTest
    {
        private readonly string _filePath = "./Data/ParserTest.CSV";
        private readonly Encoding _encoding;

        public KenAllCsvParserTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding("Shift_JIS");
        }

        [Fact(DisplayName = "CSV同期読み込みテスト(File)")]
        public void ReadSyncFromFileTest()
        {
            var parser = new KenAllCsvParser();
            var records = parser.Read(_filePath, _encoding).ToList();
            AssertForReadTest(records);
        }

        [Fact(DisplayName = "CSV非同期読み込みテスト(File)")]
        public async Task ReadAsyncFromFileTest()
        {
            var parser = new KenAllCsvParser();
            var records = await parser.ReadAsync(_filePath, _encoding).ToListAsync();
            AssertForReadTest(records);
        }

        [Fact(DisplayName = "CSV同期読み込みテスト(TextReader)")]
        public void ReadSyncFromTextReaderTest()
        {
            using var reader = new StreamReader(_filePath, _encoding);
            var parser = new KenAllCsvParser();
            var records = parser.Read(reader).ToList();
            AssertForReadTest(records);
        }

        [Fact(DisplayName = "CSV非同期読み込みテスト(TextReader)")]
        public async Task ReadASyncFromTextReaderTest()
        {
            using var reader = new StreamReader(_filePath, _encoding);
            var parser = new KenAllCsvParser();
            var records = await parser.ReadAsync(reader).ToListAsync();
            AssertForReadTest(records);
        }

        private void AssertForReadTest(List<KenAllRecord> records)
        {
            Assert.Equal(3, records.Count);

            var expected = new KenAllRecord(
                RegionCode: "02405",
                ZipCode5: "033  ",
                ZipCode7: "0330072",
                PrefectureKana: "ｱｵﾓﾘｹﾝ",
                CityKana: "ｶﾐｷﾀｸﾞﾝﾛｸﾉﾍﾏﾁ",
                TownKana: "ｵﾘﾓ(ｲﾏｸﾏ<213-234､240､247､262､266､275､277､280､295､1199､1206､1504ｦﾉｿﾞｸ>､ｵｵﾊﾗ､ｵｷﾔﾏ､ｶﾐｵﾘﾓ<1-13､71-192ｦﾉｿﾞｸ>)",
                Prefecture: "青森県",
                City: "上北郡六戸町",
                Town: "折茂（今熊「２１３～２３４、２４０、２４７、２６２、２６６、２７５、２７７、２８０、２９５、１１９９、１２０６、１５０４を除く」、大原、沖山、上折茂「１－１３、７１－１９２を除く」）",
                IsMultiMap: 1,
                HasKoazaBanchi: 1,
                HasChome: 0,
                IsMultiTown: 0,
                UpdateStatus: 0,
                UpdateReason: 0
            );
            Assert.Equal(expected, records[0]);

            expected = new KenAllRecord(
                RegionCode: "26102",
                ZipCode5: "602  ",
                ZipCode7: "6028454",
                PrefectureKana: "ｷｮｳﾄﾌ",
                CityKana: "ｷｮｳﾄｼｶﾐｷﾞｮｳｸ",
                TownKana: "ｲﾏﾃﾞｶﾞﾜﾁｮｳ",
                Prefecture: "京都府",
                City: "京都市上京区",
                Town: "今出川町（元誓願寺通浄福寺西入、元誓願寺通浄福寺東入、浄福寺通元誓願寺上る、浄福寺通元誓願寺下る）",
                IsMultiMap: 0,
                HasKoazaBanchi: 0,
                HasChome: 0,
                IsMultiTown: 0,
                UpdateStatus: 0,
                UpdateReason: 0
            );
            Assert.Equal(expected, records[1]);

            expected = new KenAllRecord(
                RegionCode: "32201",
                ZipCode5: "690  ",
                ZipCode7: "6900000",
                PrefectureKana: "ｼﾏﾈｹﾝ",
                CityKana: "ﾏﾂｴｼ",
                TownKana: "ｲｶﾆｹｲｻｲｶﾞﾅｲﾊﾞｱｲ",
                Prefecture: "島根県",
                City: "松江市",
                Town: "以下に掲載がない場合",
                IsMultiMap: 0,
                HasKoazaBanchi: 0,
                HasChome: 0,
                IsMultiTown: 0,
                UpdateStatus: 0,
                UpdateReason: 0
            );
            Assert.Equal(expected, records[2]);
        }

        [Fact(DisplayName = "同期パーステスト(File)")]
        public void ParseSyncFromFileTest()
        {
            var parser = new KenAllCsvParser(new NopConverter());
            var addresses = parser.Parse(_filePath, _encoding).ToList();

            AssertForParseTest(addresses);
        }

        [Fact(DisplayName = "非同期パーステスト(File)")]
        public async Task ParseAsyncFromFileTest()
        {
            var parser = new KenAllCsvParser(new NopConverter());
            var addresses = await parser.ParseAsync(_filePath, _encoding).ToListAsync();

            AssertForParseTest(addresses);
        }

        [Fact(DisplayName = "同期パーステスト(TextReader)")]
        public void ParseSyncFromTextReaderTest()
        {
            using var reader = new StreamReader(_filePath, _encoding);
            var parser = new KenAllCsvParser(new NopConverter());
            var addresses = parser.Parse(reader).ToList();

            AssertForParseTest(addresses);
        }

        [Fact(DisplayName = "非同期パーステスト(TextReader)")]
        public async Task ParseAsyncFromTextReaderTest()
        {
            using var reader = new StreamReader(_filePath, _encoding);
            var parser = new KenAllCsvParser(new NopConverter());
            var addresses = await parser.ParseAsync(reader).ToListAsync();

            AssertForParseTest(addresses);
        }

        private void AssertForParseTest(List<KenAllAddress> addresses)
        {
            Assert.Equal(3, addresses.Count);

            var expected = new KenAllAddress(
                ZipCode: "0330072",
                Prefecture: "青森県",
                City: "上北郡六戸町",
                Town: "折茂（今熊「２１３～２３４、２４０、２４７、２６２、２６６、２７５、２７７、２８０、２９５、１１９９、１２０６、１５０４を除く」、大原、沖山、上折茂「１－１３、７１－１９２を除く」）",
                RawTown: "折茂（今熊「２１３～２３４、２４０、２４７、２６２、２６６、２７５、２７７、２８０、２９５、１１９９、１２０６、１５０４を除く」、大原、沖山、上折茂「１－１３、７１－１９２を除く」）"
            );
            Assert.Equal(expected, addresses[0]);

            expected = new KenAllAddress(
                ZipCode: "6028454",
                Prefecture: "京都府",
                City: "京都市上京区",
                Town: "今出川町（元誓願寺通浄福寺西入、元誓願寺通浄福寺東入、浄福寺通元誓願寺上る、浄福寺通元誓願寺下る）",
                RawTown: "今出川町（元誓願寺通浄福寺西入、元誓願寺通浄福寺東入、浄福寺通元誓願寺上る、浄福寺通元誓願寺下る）"
            );
            Assert.Equal(expected, addresses[1]);

            expected = new KenAllAddress(
                ZipCode: "6900000",
                Prefecture: "島根県",
                City: "松江市",
                Town: "以下に掲載がない場合",
                RawTown: "以下に掲載がない場合"
            );
            Assert.Equal(expected, addresses[2]);
        }
    }
}
