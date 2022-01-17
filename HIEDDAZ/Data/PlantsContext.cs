using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace HIEDDAZ.Data
{
    public interface IPlantsContext
    {
        List<Plant> Plants { get; set; }

        Task<List<Plant>> GetContextAsync();
        Task SaveChangesAsync();
    }

    public class PlantsContext : IPlantsContext
    {
        private readonly string jsonFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "plants.json");

        public PlantsContext()
        {
            Plants = new List<Plant>();
        }

        public List<Plant> Plants { get; set; }

        public async Task SaveChangesAsync()
        {
            using (var fs = new FileStream(jsonFile, FileMode.Create, FileAccess.Write))
                await JsonSerializer.SerializeAsync(fs, Plants);
        }

        public async Task<List<Plant>> GetContextAsync()
        {
            if (File.Exists(jsonFile))
            {
                using (var fs = new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
                    Plants = await JsonSerializer.DeserializeAsync<List<Plant>>(fs);
            }

            return Plants;
        }
    }
}

