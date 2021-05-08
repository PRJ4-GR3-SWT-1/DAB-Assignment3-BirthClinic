using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class GlobalNumbers : IDisposable
    {
        [BsonIgnore]
        private static GlobalNumbers instance = null;
        [BsonIgnore]
        public static GlobalNumbers Instance
        {
            get
            {
                if (instance == null) instance = new GlobalNumbers();
                return instance;
            }
        }
        public GlobalNumbers()
        {
            var client = new MongoClient(
                "mongodb://localhost:27017"
            );
            var database = client.GetDatabase("BirthClinic");
            var globalNumbers = database.GetCollection<BsonDocument>("GlobalNumbers");

            var Hugo = globalNumbers.Find(new BsonDocument()).FirstOrDefault();
            if (Hugo == null)
            {
                GlobalNumbersSetup(database);
                Hugo = globalNumbers.Find(new BsonDocument()).FirstOrDefault();
            }
            //
            //
            GlobalNumberDto GN = BsonSerializer.Deserialize<GlobalNumberDto>(Hugo);
            PersonId = GN.PersonId;
            BirthId = GN.BirthId;
            ReservationId = GN.ReservationId;
            RoomId = GN.RoomId;
        }

        public string _id { get; set; }
        private int PersonId { get; set; }

        public int getPersonId()
        {
            int temp = PersonId;
            PersonId++;
            return temp;
        }
        private int BirthId { get; set; }
        public int getBirthId()
        {
            int temp = BirthId;
            BirthId++;
            return temp;
        }
        private int ReservationId { get; set; }
        public int getReservationId()
        {
            int temp = ReservationId;
            ReservationId++;
            return temp;
        }
        private int RoomId { get; set; }
        public int getRoomId()
        {
            int temp = RoomId;
            RoomId++;
            return temp;
        }

        public void Dispose()
        {
            var client = new MongoClient(
                "mongodb://localhost:27017"
            );
            var database = client.GetDatabase("BirthClinic");
            var globalNumbers = database.GetCollection<BsonDocument>("GlobalNumbers");
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = globalNumbers.DeleteMany(filter);
            //
            //
            GlobalNumberDto GN = new GlobalNumberDto();
            GN.BirthId = BirthId;
            GN.PersonId = PersonId;
            GN.RoomId = RoomId;
            GN.ReservationId = ReservationId;

           globalNumbers.InsertOne(GN.ToBsonDocument());
        }
        public static void GlobalNumbersSetup(IMongoDatabase database)
        {
            var globalNumbers = database.GetCollection<BsonDocument>("GlobalNumbers");
            // Single Use
            GlobalNumberDto GN = new GlobalNumberDto();
            GN.BirthId = 0;
            GN.PersonId = 0;
            GN.ReservationId = 0;
            GN.RoomId = 0;

            BsonDocument globalNumbersDocument = BsonDocument.Parse(JsonSerializer.Serialize(GN));

            globalNumbers.InsertOne(globalNumbersDocument);
            // Single Use
        }
    }
}
