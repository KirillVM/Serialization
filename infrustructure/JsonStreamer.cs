using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SerializationTask.infrustructure
{
    class JsonStreamer
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        public static void Write<T>(string path, Person[] persons)
        {
            var write = WriterAsync<T>(path, persons);
        }
        public static Person[] Read<T>(string path)
        {
            var read = ReaderAsync<T>(path);
            return read.Result;
        }
        private static async Task WriterAsync<T>(string path, Person[] persons)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                await JsonSerializer.SerializeAsync(fs,persons,options);
            }
        }
        private static async Task<Person[]> ReaderAsync<T>(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return await JsonSerializer.DeserializeAsync<Person[]>(fs, options);
            }
        }
    }
}
