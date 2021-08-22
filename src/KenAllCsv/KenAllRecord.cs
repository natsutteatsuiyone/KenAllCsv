namespace KenAllCsv
{
    /// <summary>
    /// KEN_ALLレコード
    /// </summary>
    /// <param name="RegionCode">全国地方公共団体コード（JIS X0401、X0402）</param>
    /// <param name="ZipCode5">（旧）郵便番号（5桁）</param>
    /// <param name="ZipCode7">郵便番号（7桁）</param>
    /// <param name="PrefectureKana">都道府県名 半角カタカナ</param>
    /// <param name="CityKana">市区町村名 半角カタカナ</param>
    /// <param name="TownKana">町域名 半角カタカナ</param>
    /// <param name="Prefecture">都道府県名</param>
    /// <param name="City">市区町村名</param>
    /// <param name="IsMultiMap">一町域が二以上の郵便番号で表される場合の表示（「1」は該当、「0」は該当せず）</param>
    /// <param name="HasKoazaBanchi">小字毎に番地が起番されている町域の表示（「1」は該当、「0」は該当せず）</param>
    /// <param name="HasChome">丁目を有する町域の場合の表示　（「1」は該当、「0」は該当せず）</param>
    /// <param name="IsMultiTown">一つの郵便番号で二以上の町域を表す場合の表示（「1」は該当、「0」は該当せず）</param>
    /// <param name="UpdateStatus">更新の表示（「0」は変更なし、「1」は変更あり、「2」廃止（廃止データのみ使用））</param>
    /// <param name="UpdateReason">変更理由（「0」は変更なし、「1」市政・区政・町政・分区・政令指定都市施行、「2」住居表示の実施、「3」区画整理、「4」郵便区調整等、「5」訂正、「6」廃止（廃止データのみ使用））</param>
    public record KenAllRecord(
        string RegionCode,
        string ZipCode5,
        string ZipCode7,
        string PrefectureKana,
        string CityKana,
        string TownKana,
        string Prefecture,
        string City,
        string Town,
        int IsMultiMap,
        int HasKoazaBanchi,
        int HasChome,
        int IsMultiTown,
        int UpdateStatus,
        int UpdateReason)
    {
        /// <summary>
        /// KenAllAddressに変換する。
        /// </summary>
        /// <param name="town"></param>
        /// <param name="rawTown"></param>
        /// <returns></returns>
        internal KenAllAddress ToZipCodeAddress(string? town = null, string? rawTown = null)
        {
            return new KenAllAddress(
                    ZipCode: ZipCode7,
                    Prefecture: Prefecture,
                    City: City,
                    Town: town ?? Town,
                    RawTown: rawTown ?? Town
                );
        }
    }
}
