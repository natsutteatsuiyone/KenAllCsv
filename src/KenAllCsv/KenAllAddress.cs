using System.Text.RegularExpressions;

namespace KenAllCsv
{
    /// <summary>
    /// 住所
    /// </summary>
    /// <param name="ZipCode">郵便番号</param>
    /// <param name="Prefecture">都道府県名</param>
    /// <param name="City">市区町村名</param>
    /// <param name="Town">町域名</param>
    /// <param name="RawTown">加工前の町域名</param>
    public record KenAllAddress(
        string ZipCode,
        string Prefecture,
        string City,
        string Town,
        string RawTown)
    {
        private static readonly Regex RegexKyotoStreetName = new(@"（.+(?:上る|下る|東入|西入).*）", RegexOptions.Compiled);

        /// <summary>
        /// 京都の通り名を含む住所である場合はtrue、そうでなければfalse。
        /// </summary>
        internal bool ContainsKyotoStreetName()
        {
            return City.StartsWith("京都市")
                && RegexKyotoStreetName.IsMatch(Town);
        }
    }
}
