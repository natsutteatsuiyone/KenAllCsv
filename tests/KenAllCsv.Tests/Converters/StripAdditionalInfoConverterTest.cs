using System.Linq;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests.Converters
{
    public class StripAdditionalInfoConverterTest
    {
        private readonly KenAllAddress _emptyAddress = new("", "", "", "", "");

        [Theory(DisplayName = "不要な付加情報が削除される")]
        [InlineData("以下に掲載がない場合", "")]
        [InlineData("厚内（全域）", "厚内")]
        [InlineData("矢浜（丁目）", "矢浜")]
        [InlineData("東田町（番地）", "東田町")]
        [InlineData("花田町（無番地）", "花田町")]
        [InlineData("三国町新保（その他）", "三国町新保")]
        [InlineData("富岡（○○屋敷）", "富岡")]
        [InlineData("芦田町福田（３７６－１０を除く）", "芦田町福田")]
        [InlineData("立川町（立川山を含む）", "立川町")]
        [InlineData("音江町（国見その他）", "音江町（国見）")]
        [InlineData("竹房（４５０番地以下）", "竹房")]
        [InlineData("島内（９８２０、９８２１、９８２３～９８３０、９８６４番地以上）", "島内")]
        [InlineData("東郷町山陰戊（５１３の１以内）", "東郷町山陰戊")]
        [InlineData("緑ヶ丘町（３５番以降）", "緑ヶ丘町")]
        [InlineData("青木島町青木島乙（９５６番地以外）", "青木島町青木島乙")]
        [InlineData("芦田町福田（３７６−１０「聖宝寺」）", "芦田町福田（３７６−１０）")]
        [InlineData("利島村一円", "利島村")]
        [InlineData("一円", "一円")]
        [InlineData("一鍬田（成田国際空港内）", "一鍬田")]
        [InlineData("境町の次に番地がくる場合", "")]
        [InlineData("名駅ミッドランドスクエア（高層棟）（１階）", "名駅ミッドランドスクエア（１階）")]
        [InlineData("折茂（今熊「２１３～２３４、２４０、２４７、５０４を除く」、大原、沖山、上折茂「１－１３、７１－１９２を除く」）", "折茂（今熊、大原、沖山、上折茂）")]
        [InlineData("箱石（第２地割「７０～１３６」～第４地割「３～１１」）", "箱石（第２地割～第４地割）")]
        [InlineData("大江（１丁目、２丁目「６５１、６６２、６６８番地」以外、３丁目５、１３－４、２０、６７８、６８７番地）", "大江（１丁目、２丁目、３丁目５、１３－４、２０、６７８、６８７番地）")]
        [InlineData("毛萱（前川原２３２～２４４、３１１、３１２、３３７～８６２番地〔東京電力福島第二原子力発電所構内〕）", "毛萱（前川原２３２～２４４、３１１、３１２、３３７～８６２番地）")]
        [InlineData("甲、乙（大木戸）", "甲、乙")]
        [InlineData("鶴見（大字、番地）", "鶴見")]
        [InlineData("橋本（大字）", "橋本")]
        [InlineData("高師町（北原、その他）", "高師町（北原）")]
        public void StripAdditionalInfoTest(string town, string exptected)
        {
            var converter = new StripAdditionalInfoConverter();
            var list = converter.Convert(_emptyAddress with { Town = town }).ToList();
            Assert.Single(list);
            Assert.Equal(exptected, list.First().Town);
        }
    }
}
