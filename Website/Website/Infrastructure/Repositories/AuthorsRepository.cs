using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Website.Models.DbDto;

namespace Website.Infrastructure.Repositories
{
    public class AuthorsRepository : BaseRepository, IAuthorsRepository
    {
        protected List<string> filtersList;
        protected Dictionary<string, int> filterIndexes;

        public static List<string> GetFiltersList()
        {
            var result = new List<string>();
            result.Add("0-9");

            var startCode = (int)'A';
            for (var i = 0; i < 26; i++)
            {
                result.Add(((char)(startCode + i)).ToString());
            }

            return result;
        }
        public AuthorsRepository(string connectionString) : base(connectionString)
        {
            filtersList = GetFiltersList();
            filterIndexes = new Dictionary<string, int>();

            for (var i = 0; i < filtersList.Count; i++)
            {
                filterIndexes[filtersList[i]] = i;
            }
        }
        public List<AuthorInfo> GetAuthors(string filterString)
        {
            var normFilterString = filterString.ToUpper();
            if (!filterIndexes.ContainsKey(normFilterString)) return new List<AuthorInfo>();

            var index = filterIndexes[normFilterString];

            var baseSql = "SELECT a.[Id], a.[Name], a.[Description], BooksCount = isnull(bc.BooksCount, 0) " +
            "FROM [Authors] a LEFT JOIN (SELECT AuthorId, BooksCount = COUNT(1) FROM [Books] GROUP BY AuthorId) bc " +
            "ON a.Id = bc.AuthorId ";

            var whereString = $"WHERE a.[Name] >= '{normFilterString}'";
            if (index < filtersList.Count - 1) whereString = whereString + $" AND a.[Name] < '{filtersList[index + 1]}'";

            var sql = baseSql + whereString;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<AuthorInfo>(sql).ToList();
            }
        }
    }
}
