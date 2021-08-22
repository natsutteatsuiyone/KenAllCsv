using System.Linq;
using KenAllCsv.Converters;
using Xunit;

namespace KenAllCsv.Tests.Converters
{
    public class RemoveChomeBanchiConverterTest
    {
        private readonly KenAllAddress _emptyAddress = new("", "", "", "", "");

        [Theory(DisplayName = "丁目、番地、地割が削除される")]
        [InlineData("土樋（１丁目）", "土樋")]
        [InlineData("大通西（１～１９丁目）", "大通西")]
        [InlineData("中嶋（１－９７～１１６）", "中嶋")]
        [InlineData("花畑（１－７－１）", "花畑")]
        [InlineData("西早稲田（２丁目１番１～２３号、２番）", "西早稲田")]
        [InlineData("塩江町安原下（第１号）", "塩江町安原下（第１号）")]
        [InlineData("ベベルイ（本幸、北１３号沢）", "ベベルイ（本幸、北１３号沢）")]
        [InlineData("大前（細原２２５９～）", "大前（細原）")]
        [InlineData("戸山（３丁目１８・２１番）", "戸山")]
        [InlineData("藤原（１０４７～１２６８及び下平）", "藤原（下平）")]
        [InlineData("藤野（４００、４００－２番地）", "藤野")]
        [InlineData("萩原台西（３丁目２８３番以上）", "萩原台西")]
        [InlineData("萩原台西（１、２丁目、３丁目１番～２８２番）", "萩原台西")]
        [InlineData("野牛（稲崎平３０２番地・３１５番地、トクサ沢）", "野牛（稲崎平、トクサ沢）")]
        [InlineData("西瑞江（４丁目１～２番・１０～２７番、５丁目）", "西瑞江")]
        [InlineData("葛巻（第４０地割～第４５地割）", "葛巻")]
        [InlineData("桂子沢７５地割、桂子沢７６地割", "桂子沢")]
        [InlineData("種市第１５地割～第２１地割（鹿糠、小路合、緑町、大久保、高取）", "種市（鹿糠、小路合、緑町、大久保、高取）")]
        [InlineData("中山（新田１７－２、３７番地、東火行１番地）", "中山（新田、東火行）")]
        [InlineData("矢臼別（４０の１番地、４１の２番地）", "矢臼別")]
        [InlineData("大豆（１の２、３の２～６、４の２・４・６、１１の１番地）", "大豆")]
        [InlineData("唐桑町西舞根（２００番以上）", "唐桑町西舞根")]
        [InlineData("位登（猪位金４～７区、清美町）", "位登（猪位金、清美町）")]
        [InlineData("東藻琴（北１区）", "東藻琴（北１区）")]
        [InlineData("大江（２丁目６５１、６６２、６６８番地、３丁目１０３、１１８、２１０、２５４、２６７、３７２、４４４、４６９番地）", "大江")]
        [InlineData("南山（４３０番地以上、大谷地、折渡、鍵金野、金山、滝ノ沢、豊牧、沼の台、最上郡大蔵村、肘折、平林）",
                    "南山（大谷地、折渡、鍵金野、金山、滝ノ沢、豊牧、沼の台、最上郡大蔵村、肘折、平林）")]
        [InlineData("留萌原野（１～１２線）", "留萌原野")]
        [InlineData("士幌西（１条～３条）", "士幌西")]
        [InlineData("泉沢（烏帽子、烏帽子国有林７７林班）", "泉沢（烏帽子、烏帽子国有林７７林班）")]
        [InlineData("牧（１～３丁目、白滝Ｂ・Ｃ、高見）", "牧（白滝Ｂ、白滝Ｃ、高見）")]
        public void ConvertTest(string town, string exptected)
        {
            var converter = new RemoveChomeBanchiConverter();
            var list = converter.Convert(_emptyAddress with { Town = town }).ToList();
            Assert.Single(list);
            Assert.Equal(exptected, list.First().Town);
        }

        [Theory(DisplayName = "京都の通り名からは丁目が削除されない")]
        [InlineData(
            "京都市上京区",
            "神明町（御前通寺之内下る、御前通今出川上る、御前通今出川上る２丁目、御前通今出川上る３丁目、寺之内通御前西入）",
            "神明町（御前通寺之内下る、御前通今出川上る、御前通今出川上る２丁目、御前通今出川上る３丁目、寺之内通御前西入）")]
        [InlineData(
            "京都市東山区",
            "七軒町（三条大橋東４丁目、三条大橋東入４丁目）",
            "七軒町（三条大橋東４丁目、三条大橋東入４丁目）")]
        public void KyotoTownTest(string city, string town, string exptected)
        {
            var converter = new RemoveChomeBanchiConverter();
            var list = converter.Convert(_emptyAddress with { City = city, Town = town }).ToList();
            Assert.Single(list);
            Assert.Equal(exptected, list.First().Town);
        }
    }
}
