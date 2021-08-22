using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KenAllCsv.Converters
{
    /// <summary>
    /// 町域括弧内のカンマ区切りされている地名を分割する。
    /// </summary>
    internal class SplitTownConverter : IConverter
    {
        private readonly Regex _reParentheses = new(@"（(.+)）", RegexOptions.Compiled);

        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            var town = address.Town;

            if (town.StartsWith("甲、乙"))
            {
                return new[]
                {
                    address with { Town = "甲" },
                    address with { Town = "乙" }
                };
            }

            var match = _reParentheses.Match(town);
            if (!match.Success)
            {
                return new[] { address with { Town = town } };
            }

            var townSub = match.Groups[1].Value;
            town = StringUtils.RemoveLastParentheses(town);

            bool isKyotoStreet = address.ContainsKyotoStreetName();

            var result = new List<KenAllAddress>() { address with { Town = town } };
            foreach (var sub in townSub.Split('、'))
            {
                if (isKyotoStreet)
                {
                    result.Add(address with { Town = sub + town });
                }
                else
                {
                    result.Add(address with { Town = town + sub });
                }
            }
            return result;
        }
    }
}
