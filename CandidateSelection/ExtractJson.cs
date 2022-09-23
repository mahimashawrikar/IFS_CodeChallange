using CandidateSelection.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace CandidateSelection
{
    public class ExtractJson
    {
        private IConfiguration _configuration;
        public IEnumerable<CandidateDetails> candidates { get; set; }
        public IEnumerable<TechnologyDetails> technology { get; set; }
        public string storedDataPath { get; set; }
        public ExtractJson()
        {

            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        }

        public async Task GetDetailsFromJson()
        {
           
            string canConnection = _configuration.GetValue<string>("MatchJson:CandidateDetailsFile");
            var canJson = await (new HttpClient()).GetStringAsync(canConnection);
            candidates = JsonConvert.DeserializeObject<IEnumerable<CandidateDetails>>(canJson);
            string tecConnection = _configuration.GetValue<string>("MatchJson:TechnologyDetailsFile");
            var techJson = await (new HttpClient()).GetStringAsync((tecConnection));
            technology = JsonConvert.DeserializeObject<IEnumerable<TechnologyDetails>>(techJson);
            storedDataPath = _configuration.GetValue<string>("StoredDataFile");
        }
    }
}

