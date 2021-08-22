using System.Collections.Generic;

namespace KenAllCsv.Converters
{
    public interface IConverter
    {
        IEnumerable<KenAllAddress> Convert(KenAllAddress address);
    }
}
