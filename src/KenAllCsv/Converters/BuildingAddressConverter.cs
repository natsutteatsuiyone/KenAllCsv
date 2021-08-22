using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KenAllCsv.Converters
{
    /// <summary>
    /// ビルの階数表記から丸括弧を削除する。<br />
    /// （地層・階層不明）は丸ごと削除する。
    /// </summary>
    internal class BuildingAddressConverter : IConverter
    {
        private readonly Regex _reFloors = new(@"（([０-９]+階)）", RegexOptions.Compiled);

        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            var town = address.Town;
            if (town.Contains("（地階・階層不明）"))
            {
                town = town.Replace("（地階・階層不明）", "");
            }
            else
            {
                var match = _reFloors.Match(town);
                if (match.Success)
                {
                    town = $"{_reFloors.Replace(town, "")}　{match.Groups[1].Value}";
                }
            }

            return new[] { address with { Town = town } };
        }
    }
}
