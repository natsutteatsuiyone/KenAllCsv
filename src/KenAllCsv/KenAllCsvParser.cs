using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using KenAllCsv.Converters;

namespace KenAllCsv
{
    /// <summary>
    /// KEN_ALL.CSV Parser
    /// </summary>
    public class KenAllCsvParser
    {
        private readonly IConverter _converter;

        public KenAllCsvParser()
        {
            _converter = new DefaultConverter();
        }

        public KenAllCsvParser(IConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public IEnumerable<KenAllRecord> Read(string path, Encoding encoding)
        {
            using var reader = new StreamReader(path, encoding);
            using var csvReader = CreateCsvReader(reader);
            while (csvReader.Read())
            {
                yield return CreateRecord(csvReader);
            }
        }

        public IEnumerable<KenAllRecord> Read(TextReader reader)
        {
            using var csvReader = CreateCsvReader(reader);
            while (csvReader.Read())
            {
                yield return CreateRecord(csvReader);
            }
        }

        public async IAsyncEnumerable<KenAllRecord> ReadAsync(TextReader reader)
        {
            using var csvReader = CreateCsvReader(reader);
            while (await csvReader.ReadAsync().ConfigureAwait(false))
            {
                yield return CreateRecord(csvReader);
            }
        }

        public async IAsyncEnumerable<KenAllRecord> ReadAsync(string path, Encoding encoding)
        {
            using var reader = new StreamReader(path, encoding);
            using var csvReader = CreateCsvReader(reader);
            while (await csvReader.ReadAsync().ConfigureAwait(false))
            {
                yield return CreateRecord(csvReader);
            }
        }

        public IEnumerable<KenAllAddress> Parse(string path, Encoding encoding)
        {
            using var reader = new StreamReader(path, encoding);
            foreach (var record in Read(reader))
            {
                foreach (var addr in _converter.Convert(record.ToZipCodeAddress()))
                {
                    yield return addr;
                }
            }
        }

        public IEnumerable<KenAllAddress> Parse(TextReader reader)
        {
            return Read(reader).SelectMany(r => _converter.Convert(r.ToZipCodeAddress()));
        }

        public async IAsyncEnumerable<KenAllAddress> ParseAsync(string path, Encoding encoding)
        {
            using var reader = new StreamReader(path, encoding);
            await foreach (var record in ReadAsync(reader))
            {
                foreach (var addr in _converter.Convert(record.ToZipCodeAddress()))
                {
                    yield return addr;
                }
            }
        }

        public async IAsyncEnumerable<KenAllAddress> ParseAsync(TextReader reader)
        {
            await foreach (var record in ReadAsync(reader))
            {
                foreach (var addr in _converter.Convert(record.ToZipCodeAddress()))
                {
                    yield return addr;
                }
            }
        }

        private CsvReader CreateCsvReader(TextReader reader)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
                HasHeaderRecord = false,
            };

            var csvReader = new CsvReader(reader, config);
            csvReader.Context.RegisterClassMap<KenAllRecordMap>();
            return csvReader;
        }

        private KenAllRecord CreateRecord(CsvReader csvReader)
        {
            var record = csvReader.GetRecord<KenAllRecord>();
            if (record.Town.Contains("（") && !record.Town.Contains("）"))
            {
                var splittedRecords = new List<KenAllRecord> { record };
                while (csvReader.Read())
                {
                    var r = csvReader.GetRecord<KenAllRecord>();
                    splittedRecords.Add(r);
                    if (r.Town.Contains("）"))
                    {
                        break;
                    }
                }
                return record with
                {
                    Town = string.Join("", splittedRecords.Select(r => r.Town)),
                    TownKana = string.Join("", splittedRecords.Select(r => r.TownKana).Distinct())
                };
            }
            else
            {
                return record;
            }
        }
    }
}
