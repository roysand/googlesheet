using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace googlesheet
{
    class Program
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets};
        static readonly string ApplicationName = "EikaRenter";
        static readonly string SpreadsheetId = "1R7N6H3VZftt-jtX5EGpYM8zD7UTzG4IsGeMKwAf1chU";
        static readonly string sheetName = "Renter";
        static SheetsService service;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, starting ...!!!");
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);

                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });
            }
            
            ReadEntries();
        }

        static void ReadEntries()
        {
            var range = $"{sheetName}!A1:E10";
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    Console.WriteLine("Hello!!");                    
                }
            }
        }
    }
}