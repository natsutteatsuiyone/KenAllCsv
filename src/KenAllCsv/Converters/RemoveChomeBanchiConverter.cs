using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KenAllCsv.Converters
{
    /// <summary>
    /// 丁目・番地・地割等を削除する。
    /// </summary>
    internal class RemoveChomeBanchiConverter : IConverter
    {
        private readonly Regex _reNumber = new(@"[０-９－の]+$", RegexOptions.Compiled);
        private readonly Regex _reGou = new(@"[０-９]+～[０-９]+号$", RegexOptions.Compiled);
        private readonly Regex _reKu = new(@"[０-９]+～[０-９]+区$", RegexOptions.Compiled);
        private const string ChomeBanchi = @"第?[０-９－]+(?:丁目|番地|番|地割|～|線|条)";
        private readonly Regex _reChomeBanchi = new($@"(?:{ChomeBanchi}?[～の・])?{ChomeBanchi}(?:以上)?", RegexOptions.Compiled);
        private readonly Regex _reParentheses = new(@"（(.+)）", RegexOptions.Compiled);

        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            var town = address.Town;
            var townSub = "";

            var match = _reParentheses.Match(town);
            if (match.Success)
            {
                townSub = match.Groups[1].Value;
                // 8700924	大分県	大分市	牧
                townSub = townSub.Replace("白滝Ｂ・Ｃ", "白滝Ｂ、白滝Ｃ");
                var subs = townSub.Split(new[] { "、", "・", "及び" }, System.StringSplitOptions.None)
                                  .Select(sub => address.ContainsKyotoStreetName() ? sub : Remove(sub))
                                  .Where(sub => sub.Length > 0).ToList();
                townSub = subs.Count > 0 ? $"（{string.Join("、", subs)}）" : "";
                town = StringUtils.RemoveLastParentheses(town);
            }
            if (town.Contains("地割"))
            {
                town = town.Split(new[] { "、", "～" }, System.StringSplitOptions.None)
                           .Select(t => _reChomeBanchi.Replace(t, ""))
                           .First(t => t.Length > 0);
            }

            return new[] { address with { Town = $"{town}{townSub}" } };
        }

        private string Remove(string value)
        {
            value = _reKu.Replace(value, "");
            value = _reGou.Replace(value, "");
            value = _reChomeBanchi.Replace(value, "");
            value = _reNumber.Replace(value, "");
            return value;
        }
    }
}
