using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace Importer
{
    class Program
    {

        static List<UserRecommendation> GetNextUserRecommendations(long startId, SqlConnection modelDbConnection)
        {
            return modelDbConnection.Query<UserRecommendation>("SELECT * FROM ReadNextUserRecommendations(@startId) ORDER BY Id", new { startId }).ToList();
        }

        static DataTable CreateRecommendationsTable()
        {
            var result = new DataTable();
            result.Columns.Add("Id", typeof(long));
            result.Columns.Add("UserId", typeof(long));
            result.Columns.Add("ItemId", typeof(int));
            return result;
        }
        static void UpdateUserRecommendations(List<UserRecommendation> recommendations, SqlConnection siteDbConnection)
        {
            var recoTable = CreateRecommendationsTable();

            foreach (var reco in recommendations)
            {
                recoTable.Rows.Add(reco.Id, reco.UserId, reco.ItemId);
            }

            siteDbConnection.Execute("exec RenewUserRecommendations @newRecommendations", new { @newRecommendations = recoTable.AsTableValuedParameter("UserRecommendationsTable") });
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Importer runs!");

            var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();

            Console.WriteLine("Reading database configuration ...");

            var siteConnectionString = config.GetConnectionString("siteDb");
            var modelConnectionString = config.GetConnectionString("modelDb");

            Console.WriteLine("Connecting to databases ...");

            using (var siteConnection = new SqlConnection(siteConnectionString))
            {
                using (var modelConnection = new SqlConnection(modelConnectionString))
                {
                    Console.WriteLine("Getting last imported id from website database ...");
                    var lastImportedId = siteConnection.Query<long>("SELECT TOP 1 Id FROM UserRecommendations ORDER BY Id DESC").FirstOrDefault();
                    Console.WriteLine($"Last imported id = {lastImportedId}");

                    while(true)
                    {
                        Console.WriteLine($"Getting recommendations from model database starting from id = {lastImportedId} ...");
                        var recommendations = GetNextUserRecommendations(lastImportedId, modelConnection);
                        var lastReco = recommendations.LastOrDefault();

                        if (lastReco == null) 
                        {
                            Console.WriteLine("No more recommendation retrieved. Exiting ...");
                            break;
                        };

                        lastImportedId = lastReco.Id;
                        Console.WriteLine($"Updating recommendations for user: {lastReco.UserId}");
                        UpdateUserRecommendations(recommendations, siteConnection);
                    }
                }
            }
        }
    }
}
