using System.Collections.Generic;
using System.Linq;

namespace KenAllCsv.Converters
{
    /// <summary>
    /// ・以下に掲載がない場合、（全域）等の付加情報を削除 <br />
    /// ・ビルの階数表記から括弧を削除 <br />
    /// ・丁目、番地、地割を削除 <br />
    /// ・括弧内の地名を複数行に分割
    /// </summary>
    public class DefaultConverter : IConverter
    {
        private readonly IEnumerable<IConverter> _converters = new List<IConverter>()
        {
            new StripAdditionalInfoConverter(),
            new BuildingAddressConverter(),
            new RemoveChomeBanchiConverter(),
            new SplitTownConverter()
        };

        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            return _converters.Aggregate(
                    new List<KenAllAddress>() { address },
                    (current, converter) => current.SelectMany(converter.Convert).ToList()
                    ).ToArray();
        }
    }
}
