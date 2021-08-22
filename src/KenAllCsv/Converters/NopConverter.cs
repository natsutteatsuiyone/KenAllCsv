using System.Collections.Generic;

namespace KenAllCsv.Converters
{
    public class NopConverter : IConverter
    {
        public IEnumerable<KenAllAddress> Convert(KenAllAddress address)
        {
            return new[] { address };
        }
    }
}
