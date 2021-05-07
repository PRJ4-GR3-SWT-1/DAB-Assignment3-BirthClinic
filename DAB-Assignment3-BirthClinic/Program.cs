using System;
using System.Linq;
using System.Text.Json;
using DAB_Assignment3_BirthClinic.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
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
            //var collection = database.GetCollection<BsonDocument>("Clinicians");
            var collection = database.GetCollection<BsonDocument>("Births");
            var globalNumbers = database.GetCollection<BsonDocument>("GlobalNumbers");

            //// Single Use
            //GlobalNumbers GN = new GlobalNumbers();
            //GN.BirthId = 0;
            //GN.PersonId = 0;
            //GN.ReservationId = 0;

            //BsonDocument globalNumbersDocument = BsonDocument.Parse(JsonSerializer.Serialize(GN));

            //globalNumbers.InsertOne(globalNumbersDocument);
            //// Single Use

            var Hugo = globalNumbers.Find(new BsonDocument()).FirstOrDefault();
            GlobalNumbers GN = BsonSerializer.Deserialize<GlobalNumbers>(Hugo);
            Console.WriteLine(Hugo);
            Console.WriteLine(GN.BirthId);
            Console.WriteLine(GN.ReservationId);
            Console.WriteLine(GN.PersonId);

            //var doc = new BsonDocument()
            //{
            //    {"Name", "Hugo"}
            //};
            //collection.InsertOne(doc);

            //var dblist = client.ListDatabases().ToList();
            //foreach (var db in dblist)
            //{
            //    Console.WriteLine(db);
            //}
            //Console.WriteLine("\n\n");

            Birth testBirth = new Birth();
            Child testChild = new Child("TheChild");
            Clinician testClinician = new Doctor("TheDoctor");
            Clinician testClinician2 = new MidWife("TheMidwife");
            DateTime testTime = new DateTime(2021, 06, 05);

            testBirth.Child = testChild;
            testBirth.Clinicians.Add(testClinician);
            testBirth.Clinicians.Add(testClinician2);
            testBirth.PlannedStartTime = testTime;

            BsonDocument output = BsonDocument.Parse(JsonSerializer.Serialize(testBirth));

            collection.InsertOne(output);
            Console.WriteLine(output);


        }

        // Opdel databasen i Person, Birth og reservation. 
        // Person kan indeholde et array af details, som bruges til at fortælle om personen er en clinician, barn, familiemedlem m.m.
        //

        // 2. View
        //Show clinicians, birth rooms maternity rooms and rest rooms available at the clinic for
        //the next five days


        // 3. View
        //Show the at current time ongoing births with information about the birth, parents,
        //    clinicians associated and the birth room

        // 5. View
        //Given a birth can planned
        //    a) Show the rooms reserved the birth
        //b) Show the clinicians assigned the birth



        //HELP
        // Hvordan skal vores documenter få deres id?, Igennem _id eller skal vi lave et globalt dokument med Id'er? Eller begge?
        // Ved fx birth vil vi ikke have nye objekter af fx clinicians, men i stedet have et array af deres Id'er. Derved kan der nemt søges på dem.
                // Men hvordan laves dette id nemmest, og mest korrekt.
    }
}
