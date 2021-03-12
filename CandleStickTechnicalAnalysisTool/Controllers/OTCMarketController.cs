using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CandleStickTechnicalAnalysisTool.Core;
using CandleStickTechnicalAnalysisTool.Core.Models;
using CandleStickTechnicalAnalysisTool.Core.Models.PatternScanning;
using CandleStickTechnicalAnalysisTool.Helpers;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TicTacTec.TA.Library;
using static TicTacTec.TA.Library.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CandleStickTechnicalAnalysisTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTCMarketController : ControllerBase
    {
        private HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        const string FileLocation = @"\MarketData\";

        public OTCMarketController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task<string> WriteFile(string jsonString, string fileName, string subDirectory)
        {
            string contentRootPath = _env.ContentRootPath;
            var logPath = contentRootPath + FileLocation + "\\" + fileName;

            logPath = string.IsNullOrWhiteSpace(subDirectory) ? logPath : contentRootPath + FileLocation + "\\Daily\\" + fileName;

            using (StreamWriter streamwriter = System.IO.File.CreateText(logPath))
            {
                await streamwriter.WriteLineAsync(jsonString);
            }

            return jsonString;
        }

        

        // GET: api/<OTCMarketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //https://query1.finance.yahoo.com/v7/finance/download/AABB?period1=912556800&period2=1615161600&interval=1d&events=history&includeAdjustedClose=true

        [HttpGet("/downloadHistoricDataByTicker/{tickerSymbol}")]
        public async Task<string> DownloadHistoricDataByTicker(string tickerSymbol)
        {
            string contentRootPath = _env.ContentRootPath;
            _client = GetHttpClient();
            string json = System.IO.File.ReadAllText(contentRootPath + "/MarketData/OTCMarketDirectory.json");
            var records = JsonConvert.DeserializeObject<List<Record>>(json);
            var numOfCompaniesSuccessfullyRetrieved = 0;

            try
            {
                var yfinanceAPIURL = $"https://query1.finance.yahoo.com/v7/finance/download/{tickerSymbol}?period1=912556800&period2=1615161600&interval=1d&events=history&includeAdjustedClose=true";
                var otcCompaniesInitialResponse = await _client.GetAsync(yfinanceAPIURL);
                var historicalDataForTicker = await otcCompaniesInitialResponse.Content.ReadAsStringAsync();
                await WriteFile(historicalDataForTicker, $"{tickerSymbol}.csv", "/Daily");

                numOfCompaniesSuccessfullyRetrieved++;
            }
            catch (Exception ex)
            {

            }


            return $"# of tickers data was retrieved for: {numOfCompaniesSuccessfullyRetrieved} of {records.Count()} = err % {(records.Count() - numOfCompaniesSuccessfullyRetrieved) / (records.Count())}";
        }

        [HttpGet("/getHistoricTickerData")]
        public async Task<string> GetHistoricDataByTicker()
        {
            _client = GetHttpClient();
            var records = await GetListOfOTCCompanyRecords();
            var numOfCompaniesSuccessfullyRetrieved = 0;

            foreach (var company in records)
            {
                try {
                    var yfinanceAPIURL = $"https://query1.finance.yahoo.com/v7/finance/download/{company.Symbol}?period1=912556800&period2=1615334400&interval=1d&events=history&includeAdjustedClose=true";
                    var otcCompaniesInitialResponse = await _client.GetAsync(yfinanceAPIURL);
                    var historicalDataForTicker = await otcCompaniesInitialResponse.Content.ReadAsStringAsync();
                    await WriteFile(historicalDataForTicker, $"{company.Symbol}.csv", "/Daily");

                    numOfCompaniesSuccessfullyRetrieved++;
                }
                catch (Exception ex)
                {

                }
            }


            return $"# of tickers data was retrieved for: {numOfCompaniesSuccessfullyRetrieved} of {records.Count()} = err % {(records.Count() - numOfCompaniesSuccessfullyRetrieved) / (records.Count())}";
        }

        [HttpGet("/getCandleStickChartByTicker/{ticker}")]
        public async Task<string> GetHistoricDataByTicker(string ticker)
            => await OutputFileContentAsJSONString(ticker);

        private async Task<string> OutputFileContentAsJSONString(string ticker)
        {
            string contentRootPath = _env.ContentRootPath;
            var csvDataFromDisk = System.IO.File.ReadLines(contentRootPath + $"/MarketData/Daily/{ticker}.csv");
            var i = 0;

            List<CandleStick> candleSticks = new List<CandleStick>();

            foreach (var each in csvDataFromDisk)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                try
                {
                    var candleStickObj = CandleStick.FromCsv(each.ToString());

                    if (candleStickObj != null)
                        candleSticks.Add(candleStickObj);
                }
                catch (Exception ex)
                {

                }

                i++;
            }

            return JsonConvert.SerializeObject(candleSticks.OrderBy(x => x.DateTime));
        }

        private async Task<List<Record>> GetListOfOTCCompanyRecords()
        {
            string contentRootPath = _env.ContentRootPath;
            string json = System.IO.File.ReadAllText(contentRootPath + "/MarketData/OTCMarketDirectory.json");
            return JsonConvert.DeserializeObject<List<Record>>(json);
        }

        private async Task<List<Record>> GetPortfolio()
        {
            string contentRootPath = _env.ContentRootPath;
            string json = System.IO.File.ReadAllText(contentRootPath + "/MarketData/NoahsPortfolio.json");
            return JsonConvert.DeserializeObject<List<Record>>(json);
        }

        //Retrieves all current company names and tickers listed on the OTC Market
        // GET api/<OTCMarketController>/5
        [HttpGet("/companiesDirectory")]
        public async Task<string> GetCompaniesDirectory()
        {
            _client = GetHttpClient();
            var otcCompaniesInitialResponse = await _client.GetAsync($"https://backend.otcmarkets.com/otcapi/company-directory?page=1&pageSize=50");
            var yeet = await otcCompaniesInitialResponse.Content.ReadAsStringAsync();
            var yote = JsonConvert.DeserializeObject<OTCMarketsCompanyDirectory>(yeet);

            var currentPage = yote.CurrentPage;
            var totalPages = yote.Pages;

            List<Record> companyRecords = new List<Record>();

            companyRecords.AddRange(yote.Records);

            while (yote.CurrentPage < yote.Pages)
            {
                otcCompaniesInitialResponse = await _client.GetAsync($"https://backend.otcmarkets.com/otcapi/company-directory?page={yote.CurrentPage + 1}&pageSize=50");
                yeet = await otcCompaniesInitialResponse.Content.ReadAsStringAsync();

                yote = JsonConvert.DeserializeObject<OTCMarketsCompanyDirectory>(yeet);

                companyRecords.AddRange(yote.Records);
            }

            await WriteFile(JsonConvert.SerializeObject(companyRecords).ToString(), "OTCMarketDirectory.json", "");

            return JsonConvert.SerializeObject(companyRecords).ToString();
        }

        [HttpGet("/getListOfPatterns")]
        public async Task<string> GetListOfPatternsAsync()
        {
            var patterns = Enum.GetNames(typeof(CandleStickPattern));
            var patternDescriptions = HelperMethods.GetListOfDescription<CandleStickPattern>();
            List<PatternObj> patternObjs = new List<PatternObj>();
            var i = 0;
            foreach (var pattern in patterns)
            {
                var currentPattern = patternDescriptions[i];
                var patternObj = PatternObj.Create(pattern, currentPattern);
                patternObjs.Add(patternObj);
                i++;
            }


            return JsonConvert.SerializeObject(patternObjs);
        }

        [HttpPost("/api/RunPatternDetection")]
        public async Task<string> FindPatternScanResults([FromBody] ScanRequest scanRequest)
        {
            var records = await GetPortfolio();
            List<CompanyPatternResults> companyPatternResults = new List<CompanyPatternResults>();

            var patternEnum = Enum.Parse<CandleStickPattern>(scanRequest.Pattern);
            //var patternEnum = scanRequest.Pattern;

            foreach (var company in records)
            {
                var tickerHistoricJSONString = await OutputFileContentAsJSONString(company.Symbol);
                List<CandleStick> candleSticks = JsonConvert.DeserializeObject<List<CandleStick>>(tickerHistoricJSONString);
                var patternScanResultsList = await GetPatternScanResultsAsync(candleSticks, patternEnum);
                var companyPatternResult = CompanyPatternResults.Create(patternEnum, company, patternScanResultsList, candleSticks, (bool)patternScanResultsList.Any(x => x.IsPatternTriggered == true));
                
                

                var minDateFrequency = DateTime.Now.AddDays(-int.Parse(scanRequest.MinimumFrequency));
                var blah1 = (bool)patternScanResultsList.Any(x => x.IsPatternTriggered == true);
                var blah2 = patternScanResultsList.Any(y => DateTime.Compare(y.CandleStick.DateTime, minDateFrequency) > 0);
                
                if (blah1 && blah2)
                    companyPatternResults.Add(companyPatternResult);
            }

            return JsonConvert.SerializeObject(companyPatternResults).ToString();
        }

        private async Task<List<PatternScanResults>> GetPatternScanResultsAsync(List<CandleStick> candleSticks, CandleStickPattern patternToScanFor)
        {
            List<PatternScanResults> patternScanResults = new List<PatternScanResults>();

            int[] patternIndexLoc = new int[candleSticks.Count()];
            int out1 = 0;
            int out2 = 0;

            object[] parameters = new object[] { 1, candleSticks.Count() - 1, candleSticks.Select(x => (float)x.Open).ToArray(),
                candleSticks.Select(x => (float)x.High).ToArray(), candleSticks.Select(x => (float)x.Low).ToArray(), candleSticks.Select(x => (float)x.Close).ToArray(), 0, out1, out2, patternIndexLoc};
            
            Type thisType = typeof(TicTacTec.TA.Library.Core);
            
            try
            {
                MethodInfo theMethod = thisType.GetMethod("CdlMorningStar");
                var result = theMethod.Invoke(this, parameters);
                
            }
            catch (AmbiguousMatchException)
            {
                MethodInfo[] methods = thisType.GetMethods();

                var result = methods[0].Invoke(this, parameters);

                if ((RetCode)result ==  RetCode.Success)
                {
                    out1 = (int)parameters[7];
                    out2 = (int)parameters[8];
                }
            }

            //Create PatternScanResults
            int iterator = 0;
            foreach (var candle in candleSticks)
            {
                var isPatternTriggered = patternIndexLoc[iterator] != 0 ? true : false;
                var patternScanResult = PatternScanResults.Create(candle, isPatternTriggered);

                if(isPatternTriggered)
                    patternScanResults.Add(patternScanResult);

                iterator++;
            }

            return patternScanResults;
        }

        private HttpClient GetHttpClient()
                => _client == null ? new HttpClient() : _client;

    }

    public class ScanRequest
    {
        public string Pattern { get; set; }
        public string MinimumFrequency { get; set; }
    }

    public class PatternObj
    {
        public string PatternName { get; set; }
        public string PatternDisplayName { get; set; }

        public static PatternObj Create(string patternName, string patternDisplayName)
            => new PatternObj { PatternDisplayName = patternDisplayName, PatternName = patternName};
    }

}
