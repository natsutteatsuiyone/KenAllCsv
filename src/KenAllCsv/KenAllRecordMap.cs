using CsvHelper.Configuration;

namespace KenAllCsv
{
    internal sealed class KenAllRecordMap : ClassMap<KenAllRecord>
    {
        public KenAllRecordMap()
        {
            Parameter(nameof(KenAllRecord.RegionCode)).Index(0);
            Parameter(nameof(KenAllRecord.ZipCode5)).Index(1);
            Parameter(nameof(KenAllRecord.ZipCode7)).Index(2);
            Parameter(nameof(KenAllRecord.PrefectureKana)).Index(3);
            Parameter(nameof(KenAllRecord.CityKana)).Index(4);
            Parameter(nameof(KenAllRecord.TownKana)).Index(5);
            Parameter(nameof(KenAllRecord.Prefecture)).Index(6);
            Parameter(nameof(KenAllRecord.City)).Index(7);
            Parameter(nameof(KenAllRecord.Town)).Index(8);
            Parameter(nameof(KenAllRecord.IsMultiMap)).Index(9);
            Parameter(nameof(KenAllRecord.HasKoazaBanchi)).Index(10);
            Parameter(nameof(KenAllRecord.HasChome)).Index(11);
            Parameter(nameof(KenAllRecord.IsMultiTown)).Index(12);
            Parameter(nameof(KenAllRecord.UpdateStatus)).Index(13);
            Parameter(nameof(KenAllRecord.UpdateReason)).Index(14);
        }
    }
}
