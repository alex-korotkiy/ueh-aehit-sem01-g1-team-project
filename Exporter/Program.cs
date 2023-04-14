using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Exporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exporter runs!");

            var config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .Build();

            Console.WriteLine("Reading database configuration ...");

            var siteConnectionString = config.GetConnectionString("siteDb");
            var modelConnectionString = config.GetConnectionString("modelDb");

            Console.WriteLine("Connecting to databases ...");

            using(var siteConnection = new SqlConnection(siteConnectionString))
            {
                using (var modelConnection = new SqlConnection(modelConnectionString))
                {
                    var insertRatingSql = "INSERT Ratings SELECT @Id, @UserId, @ItemId, @Rating";

                    Console.WriteLine("Getting last exported rating");

                    var lastExportedRating = modelConnection.Query<RatingData>("SELECT TOP 1 * FROM Ratings ORDER BY Id DESC").FirstOrDefault();
                    var startId = lastExportedRating?.Id;

                    Console.WriteLine("Last exported rating id: {0}", startId == null?"not found":startId.ToString());

                    var readerSql = "SELECT * FROM Ratings";
                    if (startId != null) readerSql = readerSql + " WHERE Id > @startId";
                    readerSql = readerSql + " ORDER BY Id";

                    using (var reader = siteConnection.ExecuteReader(readerSql, new { startId }))
                    {
                        var parser = reader.GetRowParser<RatingData>(typeof(RatingData));

                        while (reader.Read())
                        {
                            var ratingData = parser(reader);
                            Console.WriteLine($"Inserting rating: Id = {ratingData.Id}, UserId = {ratingData.UserId}, ItemId = {ratingData.ItemId}, Rating = {ratingData.Rating}");
                            modelConnection.Execute(insertRatingSql, ratingData);
                        }

                        Console.WriteLine("Done");
                    }
                }
            }
        }
    }
}
