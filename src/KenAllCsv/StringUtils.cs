namespace KenAllCsv
{
    internal static class StringUtils
    {
        /// <summary>
        /// （.*）を削除
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveLastParentheses(string value)
        {
            return value[..value.LastIndexOf("（", System.StringComparison.Ordinal)];
        }
    }
}
