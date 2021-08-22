using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KenAllCsv.Converters
{
    /// <summary>
    /// 町域の付加情報を削除する。
    /// </summary>
    internal class StripAdditionalInfoConverter : IConverter
    {
        private readonly IEnumerable<string> _wordList = new[]
        {
            "以下に掲載がない場合",
            "（全域）",
            "（丁目）",
            "（各町）",
            "（番地）",
            "（大字）",
            "（大字、番地）",
            "（無番地）",
            "（その他）",
            "（○○屋敷）",
            "（成田国際空港内）"
        };

        private readonly IEnumerable<string> _suffixList = new[]
        {
            "を除く）",
            "を含む）",
            "以下）",
            "以上）",
            "以内）",
            "以降）",
            "以外）"
        };

        private readonly Regex _reBrackets1 = new(@"「.+?」(?:以外)?", RegexOptions.Compiled);
        private readonly Regex _reBrackets2 = new(@"〔.+?〕", RegexOptions.Compiled);
        private readonly Regex _reSonota = new(@"、?その他）$", RegexOptions.Compiled);

        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            var town = address.Town;

            if (town.EndsWith("一円") && town != "一円")
            {
                town = town.Remove(town.Length - 2);
            }
            else if (town.EndsWith("の次に番地がくる場合"))
            {
                town = "";
            }
            else if (town.Contains("（高層棟）"))
            {
                town = town.Replace("（高層棟）", "");
            }
            else if (town.StartsWith("甲、乙"))
            {
                town = "甲、乙";
            }
            else
            {
                foreach (var word in _wordList)
                {
                    if (town.Contains(word))
                    {
                        town = town.Replace(word, "");
                    }
                }
                foreach (var suffix in _suffixList)
                {
                    if (town.EndsWith(suffix))
                    {
                        town = StringUtils.RemoveLastParentheses(town);
                    }
                }
                if (town.Contains("その他）"))
                {
                    town = _reSonota.Replace(town, "）");
                }
                town = _reBrackets1.Replace(town, "");
                town = _reBrackets2.Replace(town, "");
            }

            return new[] { address with { Town = town } };
        }
    }
}
