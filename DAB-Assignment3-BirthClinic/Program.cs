using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAB_Assignment3_BirthClinic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new MongoClient(
                "mongodb://localhost:27017"
            );
            var database = client.GetDatabase("BirthClinic");
            var collection = database.GetCollection<BsonDocument>("Clinicians");

            var doc = new BsonDocument()
            {
                {"Name", "Hugo"}
            };
            collection.InsertOne(doc);

            var dblist = client.ListDatabases().ToList();
            foreach (var db in dblist)
            {
                Console.WriteLine(db);
            }
            Console.WriteLine("\n\n");
        }
    }
}
