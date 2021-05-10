using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BirthClinicLibrary.Data;
using DAB_Assignment3_BirthClinic.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DAB_Assignment3_BirthClinic
{
    class Program
    {
        private static IMongoCollection<Birth> collectionBirths;
        private static IMongoCollection<Clinician> collectionClinicians;
        private static IMongoCollection<Person> collectionOtherPersons;
        private static IMongoCollection<Room> collectionRooms;
        private static IMongoCollection<Reservation> collectionReservations;
        private static bool _running;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new MongoClient(
                "mongodb://localhost:27017"
            );
            var database = client.GetDatabase("BirthClinic");
            collectionBirths = database.GetCollection<Birth>("Births");
            collectionClinicians = database.GetCollection<Clinician>("Clinicians");
            collectionOtherPersons = database.GetCollection<Person>("OtherPersons");
            collectionRooms = database.GetCollection<Room>("Rooms");
            collectionReservations = database.GetCollection<Reservation>("Reservations");
            _running = true;
            AddBirth();
            while (_running)
            {
                Console.WriteLine("Muligheder: ");
                Console.WriteLine("1: Vis planlagte fødsler: ");
                Console.WriteLine("2: Ledige rum og klinikarbejdere ");
                Console.WriteLine("3: Aktuelt igangværende fødsler ");
                Console.WriteLine("4: Værelser i brug lige nu (ikke fødselsrum)");
                Console.WriteLine("5: Vis reserverede rum og associeret personale til specifik fødsel");
                Console.WriteLine("F: Færdiggør reservation af rum ");
                Console.WriteLine("B: Lav en reservation til en fødsel");
                Console.WriteLine("A: Annuller reservation af rum ");
                Console.WriteLine("S: Seed data til databasen ");
                Console.WriteLine("x: Luk ");
                var key = Console.ReadKey();
                HandleKey(key);
            }







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
                testChild.FamilyMembersId.Add(testFather.PersonId);
                testChild.MotherId = testMother.PersonId;
                // Vi kunne også gøre children til Id'er. IDK
                testMother.Children.Add(testChild.PersonId);

                // Her kan vi instedet bruge insert many
                collectionOtherPersons.InsertOne(testChild);
                collectionOtherPersons.InsertOne(testFather);
                collectionOtherPersons.InsertOne(testMother);
            // God ide at kalde dispose efter hver insert. Ellers hvis der sker fejl undervejs, så vil Id'er ikke være helt korrekte.
            GlobalNumbers.Instance.Dispose();

                Clinician testClinician = new Doctor("TheDoctor");
                Clinician testClinician2 = new MidWife("TheMidwife");

            collectionClinicians.InsertOne(testClinician);
            collectionClinicians.InsertOne(testClinician2);
            GlobalNumbers.Instance.Dispose();

            DateTime testTime = new DateTime(2021, 06, 05);
                
                // Child er ikke et Id pt, det kan vi altid gøre så det bliver.
                testBirth.Child = testChild;
                testBirth.CliniciansId.Add(testClinician.PersonId);
                testBirth.CliniciansId.Add(testClinician2.PersonId);
                testBirth.PlannedStartTime = testTime;
                

                BsonDocument output = BsonDocument.Parse(JsonSerializer.Serialize(testBirth));

                collectionBirths.InsertOne(testBirth);
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

        private static void HandleKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    //ShowPlannedBirths(context);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    //ShowAvailableRoomsAndClinicians(context);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    //ShowOngoingBirths(context);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    //ShowMaternityRoomsAndRestingRoomsInUse(context);
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    ShowRoomsAndCliniciansWbirth();
                    break;
                case ConsoleKey.X:
                    _running = false;
                    break;
                case ConsoleKey.F:
                    //FinnishRoomReservation(context);
                    break;
                case ConsoleKey.B:
                    //AddBirth(context);
                    break;
                case ConsoleKey.A:
                    //CancelRoomReservation(context);
                    break;
                case ConsoleKey.S:
                    new SeedData(collectionRooms,collectionClinicians);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg");
                    break;
            }
        }

        public static void AddBirth()
        {
            // Til når brugeren skal vælge doctor og midwife.
            //var doctorFilter = Builders<Clinician>.Filter.All("Type", "Doctor");
            List<Clinician> doctors = collectionClinicians.Find(r=>r.Type=="Doctor").ToList();
            List<Clinician> midWives = collectionClinicians.Find(r => r.Type == "MidWife").ToList();

            foreach (var doc in doctors)
            {
                Console.WriteLine(doc.FullName);
            }

            foreach (var doc in midWives)
            {
                Console.WriteLine(doc.FullName);
            }

            return;

            Console.WriteLine("Velkommen til reservation af fødsel");
            Console.WriteLine("-----------------------------------");

            Console.WriteLine("Hvad er navnet på barnet");
            string childName = Console.ReadLine();

            Console.WriteLine("Hvad er navnet på moderen til barnet");
            string motherName = Console.ReadLine();

            Console.WriteLine("Hvad er navnet på faderen til barnet");
            string fatherName = Console.ReadLine();

            Console.WriteLine("Hvilken dato vil du have reservationen til. Skriv på formen DD/MM/ÅÅÅÅ");
            string dato = Console.ReadLine();
            string[] datoOpsplittet = dato.Split("/");
            int dag = int.Parse(datoOpsplittet[0]);
            int måned = int.Parse(datoOpsplittet[1]);
            int år = int.Parse(datoOpsplittet[2]);

            Console.WriteLine("Hvilken tid vil du have reservationen. Skriv på formen TT.MM");
            string tid = Console.ReadLine();
            string[] tidOpsplittet = tid.Split(".");
            int time = int.Parse(tidOpsplittet[0]);
            int minut = int.Parse(tidOpsplittet[1]);

            ////Console.WriteLine("Hvilken jordmor vil du gerne have? Indtast tallet ud fra personen");
            ////foreach (MidWife mW in midWives)
            ////{
            ////    Console.WriteLine(mW.PersonId + ". " + mW.FullName);
            ////}

            int valgtMidwife = int.Parse(Console.ReadLine());

            Console.WriteLine("Hvilken doktor vil du gerne have? Indtast tallet ud fra personen");
            ////foreach (Doctor dc in doctors)
            ////{
            ////    Console.WriteLine(dc.PersonId + ". " + dc.FullName);
            ////}

            int valgtDoctor = int.Parse(Console.ReadLine());

            ////Console.WriteLine("Du skal også have et fødselsrum reserveret, Vi finder ledige rum for dagen. \n Indtast tallet ud fra rummet");
            ////ShowAvailableRooms(context, new DateTime(år, måned, dag, time, minut, 00), "birthroom");
            ////int valgtRumId = int.Parse(Console.ReadLine());
            ////Room chosenBirthRoom = context.Room.SingleOrDefault(r => r.RoomId == valgtRumId);

            ////Console.WriteLine("Vil du også reservere et Maternityroom y/n");
            ////Room chosenMaternityRoom = null;
            ////if (Console.ReadLine().ToLower() == "y")
            ////{
            ////    Console.WriteLine("MaternityRoom reservation \n Indtast tallet ud fra rummet");
            ////    ShowAvailableRooms(context, new DateTime(år, måned, dag, time, minut, 00), "maternityroom");
            ////    valgtRumId = int.Parse(Console.ReadLine());
            ////    chosenMaternityRoom = context.Room.SingleOrDefault(r => r.RoomId == valgtRumId);
            ////}
            ////Console.WriteLine("Vil du også reservere et restingroom y/n");
            ////Room chosenRestingRoom = null;
            ////if (Console.ReadLine().ToLower() == "y")
            ////{
            ////    Console.WriteLine("RestingRoom reservation \n Indtast tallet ud fra rummet");
            ////    ShowAvailableRooms(context, new DateTime(år, måned, dag, time, minut, 00), "restingroom");
            ////    valgtRumId = int.Parse(Console.ReadLine());
            ////    chosenRestingRoom = context.Room.SingleOrDefault(r => r.RoomId == valgtRumId);
            ////}

            Console.WriteLine("Tak for dit info, vores super database vil nu oprette reservationen for dig");

            // Følgende kode opsætter de rigtige referencer m.m. Så dataen kan gemmes på databasen
            // Her oprettes de forskellige klasser
            Child child1 = new Child(childName);
            Birth birth1 = new Birth();
            Mother mother1 = new Mother(motherName);
            FamilyMember father1 = new FamilyMember(fatherName, "Father");
            // Savechanges skal kaldes allerede nu, da child skal laves før birth ellers så kan EF core ikke finde ud af hvilken den skal lave først.
            /// INSERT MANGLER MÅSKE HER

            // Her sættes referencer
            birth1.Child = child1;
            DateTime PST = new DateTime(år, måned, dag, time, minut, 00);
            birth1.PlannedStartTime = PST;
            //child1.Birth = birth1;
            child1.MotherId = mother1.PersonId;
            child1.FamilyMembersId = new List<int>();
            child1.FamilyMembersId.Add(father1.PersonId);
            mother1.Children = new List<int>();
            mother1.Children.Add(child1.PersonId);

            // DOCTOR OG MIDWIFE MANGLER

            ////doctors[valgtDoctor].AssociatedBirths = new List<ClinicianBirth>();
            ////doctors[valgtDoctor].AssociatedBirths.Add(CB1);
            ////midWives[valgtMidwife].AssociatedBirths = new List<ClinicianBirth>();
            ////midWives[valgtMidwife].AssociatedBirths.Add(CB2);
            ////// Her sættes clinicians på selve birth
            ////birth1.CliniciansId = new List<int>();
            ////birth1.CliniciansId.Add(doctors[valgtDoctor].PersonId);
            ////birth1.CliniciansId.Add(midWives[valgtMidwife].PersonId);
            //////


            ////// Her sættes reservationerne for alle rum.
            ////Reservation res1 = new Reservation();
            ////res1.ReservationStart = new DateTime(år, måned, dag, time, minut, 00);
            ////res1.ReservationEnd = res1.ReservationStart + TimeSpan.FromHours(5);
            ////res1.UserId = mother1.PersonId;
            ////res1.ReservedRoomId = chosenBirthRoom.RoomId;
            ////mother1.ReservationsIds = new List<int>();
            ////chosenBirthRoom.ReservationsIds = new List<int>();
            ////mother1.ReservationsIds.Add(res1.ReservationId);
            ////chosenBirthRoom.ReservationsIds.Add(res1.ReservationId);

            ////Reservation res2 = new Reservation();
            ////if (chosenMaternityRoom != null)
            ////{
            ////    res2.ReservationStart = new DateTime(år, måned, dag, time, minut, 00);
            ////    res2.ReservationEnd = res2.ReservationStart + TimeSpan.FromDays(5);
            ////    res2.User = mother1;
            ////    res2.ReservedRoom = chosenMaternityRoom;
            ////    chosenMaternityRoom.Reservations = new List<Reservation>();
            ////    mother1.Reservations.Add(res2);
            ////    chosenMaternityRoom.Reservations.Add(res2);
            ////}

            ////Reservation res3 = new Reservation();
            ////if (chosenRestingRoom != null)
            ////{
            ////    res3.ReservationStart = new DateTime(år, måned, dag, time, minut, 00) + TimeSpan.FromHours(5);
            ////    res3.ReservationEnd = res3.ReservationStart + TimeSpan.FromHours(4);
            ////    res3.User = mother1;
            ////    res3.ReservedRoom = chosenRestingRoom;
            ////    chosenRestingRoom.Reservations = new List<Reservation>();
            ////    mother1.Reservations.Add(res3);
            ////    chosenRestingRoom.Reservations.Add(res3);
            ////}
            ////// Alt gemmes og reservationen er gemmenført
            ////context.SaveChanges();

            ////Console.WriteLine($"Reservation til den {dato}, med personerne {childName}, {motherName} og {fatherName} er gennemført og gemt");




        }
        // 5. View
        //Given a birth can planned
        //    a) Show the rooms reserved the birth
        //    b) Show the clinicians assigned the birth
        public static void ShowRoomsAndCliniciansWbirth()
        {
            Console.WriteLine("Type Birth ID:");
            int id = int.Parse(Console.ReadLine());
            var filter = Builders<Birth>.Filter.Where(b => b.BirthId == id);
            Birth birth =collectionBirths.Find(filter).Single();
            var mother=collectionOtherPersons.Find(Builders<Person>.Filter.Where(p => p.PersonId == birth.Child.MotherId));
            Console.WriteLine(birth.Child.FullName);

            //Find rooms:

            //Find clinicians:
        }
    }

}
