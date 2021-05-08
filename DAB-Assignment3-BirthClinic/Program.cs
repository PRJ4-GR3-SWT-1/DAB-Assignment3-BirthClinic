﻿using System;
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
            var collectionClinicians = database.GetCollection<BsonDocument>("Clinicians");
            var collectionOtherPersons = database.GetCollection<BsonDocument>("OtherPersons");
            var globalNumbers = database.GetCollection<BsonDocument>("GlobalNumbers");



           



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
                Mother testMother = new Mother("TheMother");
                FamilyMember testFather = new FamilyMember("TheFather", "Father");
                testChild.FamilyMembersId.Add(testFather.id);
                testChild.MotherId = testMother.id;
                // Vi kunne også gøre children til Id'er. IDK
                testMother.Children.Add(testChild);

                // Her kan vi instedet bruge insert many
                collectionOtherPersons.InsertOne(BsonDocument.Parse(JsonSerializer.Serialize(testChild)));
                collectionOtherPersons.InsertOne(BsonDocument.Parse(JsonSerializer.Serialize(testFather)));
                collectionOtherPersons.InsertOne(BsonDocument.Parse(JsonSerializer.Serialize(testMother)));
            // God ide at kalde dispose efter hver insert. Ellers hvis der sker fejl undervejs, så vil Id'er ikke være helt korrekte.
            GlobalNumbers.Instance.Dispose();

                Clinician testClinician = new Doctor("TheDoctor");
                Clinician testClinician2 = new MidWife("TheMidwife");

            collectionClinicians.InsertOne(BsonDocument.Parse(JsonSerializer.Serialize(testClinician)));
            collectionClinicians.InsertOne(BsonDocument.Parse(JsonSerializer.Serialize(testClinician2)));
            GlobalNumbers.Instance.Dispose();

            DateTime testTime = new DateTime(2021, 06, 05);
                
                // Child er ikke et Id pt, det kan vi altid gøre så det bliver.
                testBirth.Child = testChild;
                testBirth.CliniciansId.Add(testClinician.id);
                testBirth.CliniciansId.Add(testClinician2.id);
                testBirth.PlannedStartTime = testTime;

                BsonDocument output = BsonDocument.Parse(JsonSerializer.Serialize(testBirth));

                collection.InsertOne(output);
                GlobalNumbers.Instance.Dispose();
                Console.WriteLine(output);

            

                // MUST BE AS THE LAST LINE
                GlobalNumbers.Instance.Dispose();
        }

        // Opdel databasen i Person, Birth og reservation og rooms. 
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
        //    b) Show the clinicians assigned the birth



        //HELP
        // Hvordan skal vores documenter få deres id?, Igennem _id eller skal vi lave et globalt dokument med Id'er? Eller begge?
        // Ved fx birth vil vi ikke have nye objekter af fx clinicians, men i stedet have et array af deres Id'er. Derved kan der nemt søges på dem.
        // Men hvordan laves dette id nemmest, og mest korrekt.
        // MÅSKE SKAL VI HAVE ET _id I ALLE KLASSER SOM SÅ BRUGER GLOBALNUMBERS, DERVED SIGER VI SCREW YOU TIL MONGODBS ID'ER SOM IKKE GØR ANDET END AT IRRITERE.
        // Enten skal vi have en collection for hvert slags rum/person osv. ellers så skal vi have en collection som fx hedder persons, som så har arrays af Clinicians, et array af mothers osv.
    }
}
