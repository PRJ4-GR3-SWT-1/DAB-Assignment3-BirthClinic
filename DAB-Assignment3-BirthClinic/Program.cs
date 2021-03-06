using System;
using System.Collections.Generic;
using BirthClinicLibrary.Data;
using DAB_Assignment3_BirthClinic.Models;
using MongoDB.Driver;
using Mother = DAB_Assignment3_BirthClinic.Models.Mother;

namespace DAB_Assignment3_BirthClinic
{
    class Program
    {
        private static IMongoCollection<Birth> collectionBirths;
        private static IMongoCollection<Clinician> collectionClinicians;
        private static IMongoCollection<Person> collectionOtherPersons;
        private static IMongoCollection<Room> collectionRooms;
        //private static IMongoCollection<Reservation> collectionReservations;
        private static bool _running;


        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("Welcome to the document based birth clinic program :)");
            var client = new MongoClient(
                "mongodb://localhost:27017"
            );
            var database = client.GetDatabase("BirthClinic");
            collectionBirths = database.GetCollection<Birth>("Births");
            collectionClinicians = database.GetCollection<Clinician>("Clinicians");
            collectionOtherPersons = database.GetCollection<Person>("OtherPersons");
            collectionRooms = database.GetCollection<Room>("Rooms");
            //collectionReservations = database.GetCollection<Reservation>("Reservations");
            _running = true;
            
            Console.WriteLine("Current Time: " + DateTime.Now);
            while (_running)
            {
                
                Console.WriteLine("\n\n---------------\nMuligheder: ");
                Console.WriteLine("1: Vis planlagte fødsler de næste 3 dage: ");
                Console.WriteLine("3: Aktuelt igangværende fødsler ");
                Console.WriteLine("5: Vis reserverede rum og associeret personale til specifik fødsel");
                Console.WriteLine("B: Lav en reservation til en fødsel");
                Console.WriteLine("S: Seed data til databasen ");
                Console.WriteLine("x: Luk \n---------------");
                var key = Console.ReadKey();

                Console.Clear();

                HandleKey(key);
            }


            // MUST BE AS THE LAST LINE
            // This Updates the GlobalNumbers document, so the ids are correct
            GlobalNumbers.Instance.Dispose();
        }

       

        
        private static void HandleKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    ShowPlannedBirthsNext3Days();
                    break;
               
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    ShowOngoingBirths();
                    break;
                
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    ShowRoomsAndCliniciansWbirth();
                    break;
                case ConsoleKey.X:
                    _running = false;
                    break;
                
                case ConsoleKey.B:
                    AddBirth();
                    break;
                
                case ConsoleKey.S:
                    new SeedData(collectionRooms, collectionClinicians);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg");
                    break;
            }
        }

        public static void AddBirth()
        {
            // Til når brugeren skal vælge doctor og midwife.
            List<Clinician> doctors = collectionClinicians.Find(r => r.Type == "Doctor").ToList();
            List<Clinician> midWives = collectionClinicians.Find(r => r.Type == "MidWife").ToList();

            Console.WriteLine("\nVelkommen til reservation af fødsel");
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

            Console.WriteLine("Hvilken jordmor vil du gerne have? Indtast tallet ud fra personen");
            int counter = 0;
            foreach (var mW in midWives)
            {
                Console.WriteLine(counter + ". " + mW.FullName);
                counter++;
            }

            int valgtMidwife = int.Parse(Console.ReadLine());
            counter = 0;
            Console.WriteLine("Hvilken doktor vil du gerne have? Indtast tallet ud fra personen");
            foreach (var dc in doctors)
            {
                Console.WriteLine(counter + ". " + dc.FullName);
                counter++;
            }

            int valgtDoctor = int.Parse(Console.ReadLine());

            Console.WriteLine("Du skal også have et fødselsrum reserveret, Vi finder ledige rum for dagen. \n Indtast tallet ud fra rummet");
            var birthrooms = showAvailableRooms(new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc), new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc) +TimeSpan.FromHours(5), "BirthRoom");
            int valgtRumId = int.Parse(Console.ReadLine());
            Room chosenBirthRoom = birthrooms.Find(r => r.RoomId == valgtRumId);

            Console.WriteLine("Vil du også reservere et Maternityroom y/n");
            Room chosenMaternityRoom = null;
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("MaternityRoom reservation \n Indtast tallet ud fra rummet");
                var maternityRooms = showAvailableRooms(new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc), new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc) + TimeSpan.FromDays(5), "Maternity Room");
                valgtRumId = int.Parse(Console.ReadLine());
                chosenMaternityRoom = maternityRooms.Find(r => r.RoomId == valgtRumId);
            }
            Console.WriteLine("Vil du også reservere et restingroom y/n");
            Room chosenRestingRoom = null;
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("RestingRoom reservation \n Indtast tallet ud fra rummet"); 
                var restingRooms = showAvailableRooms(new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc), new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc) + TimeSpan.FromHours(4), "Resting Room");
                valgtRumId = int.Parse(Console.ReadLine());
                chosenRestingRoom = restingRooms.Find(r => r.RoomId == valgtRumId);
            }

            Console.WriteLine("Tak for dit info, vores super database vil nu oprette reservationen for dig");

            // Følgende kode opsætter de rigtige referencer m.m. Så dataen kan gemmes på databasen
            // Her oprettes de forskellige klasser
            Child child1 = new Child(childName);
            Birth birth1 = new Birth();
            Mother mother1 = new Mother(motherName);
            FamilyMember father1 = new FamilyMember(fatherName, "Father");

            // Her sættes referencer
            // Mother, child, father and birth
            birth1.Child = child1;
            DateTime PST = new DateTime(år, måned, dag, time, minut, 00,DateTimeKind.Utc);
            birth1.PlannedStartTime = PST;
            child1.Mother = mother1;
            child1.FamilyMembersId = new List<int>();
            child1.FamilyMembersId.Add(father1.PersonId);
            mother1.Children = new List<int>();
            mother1.Children.Add(child1.PersonId);

            // Clinicians references
            doctors[valgtDoctor].AssociatedBirthsId = new List<int>();
            doctors[valgtDoctor].AssociatedBirthsId.Add(birth1.BirthId);
            midWives[valgtMidwife].AssociatedBirthsId = new List<int>();
            midWives[valgtMidwife].AssociatedBirthsId.Add(birth1.BirthId);
            // Her sættes clinicians på selve birth
            birth1.CliniciansId = new List<int>();
            birth1.CliniciansId.Add(doctors[valgtDoctor].PersonId);
            birth1.CliniciansId.Add(midWives[valgtMidwife].PersonId);

            // Her sættes reservationerne for alle rum.
            Reservation res1 = new Reservation();
            res1.ReservationStart = new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc);
            res1.ReservationEnd = res1.ReservationStart + TimeSpan.FromHours(5);
            res1.UserId = mother1.PersonId;
            res1.ReservedRoomId = chosenBirthRoom.RoomId;
            mother1.Reservations = new List<Reservation>();
            chosenBirthRoom.Reservations = new List<Reservation>();
            mother1.Reservations.Add(res1);
            chosenBirthRoom.Reservations.Add(res1);

            Reservation res2 = new Reservation();
            if (chosenMaternityRoom != null)
            {

                res2.ReservationStart = new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc);
                res2.ReservationEnd = res2.ReservationStart + TimeSpan.FromDays(5);
                res2.UserId = mother1.PersonId;
                res2.ReservedRoomId = chosenMaternityRoom.RoomId;
                chosenMaternityRoom.Reservations = new List<Reservation>();
                mother1.Reservations.Add(res2);
                chosenMaternityRoom.Reservations.Add(res2);
            }

            Reservation res3 = new Reservation();
            if (chosenRestingRoom != null)
            {

                res3.ReservationStart = new DateTime(år, måned, dag, time, minut, 00, DateTimeKind.Utc) + TimeSpan.FromHours(5);
                res3.ReservationEnd = res3.ReservationStart + TimeSpan.FromHours(4);
                res3.UserId = mother1.PersonId;
                res3.ReservedRoomId = chosenRestingRoom.RoomId;
                chosenRestingRoom.Reservations = new List<Reservation>();
                mother1.Reservations.Add(res3);
                chosenRestingRoom.Reservations.Add(res3);
            }
            // Alt gemmes og reservationen er gemmenført
            // Insert
            collectionBirths.InsertOne(birth1);
            // Mother and child can be found under the specific birth
            collectionOtherPersons.InsertOne(father1);
            
            // Update Clinicians
            var doctorfilter = Builders<Clinician>.Filter.Eq("PersonId", doctors[valgtDoctor].PersonId);
            var doctorUpdate = Builders<Clinician>.Update.Push("AssociatedBirthsId", birth1.BirthId);
            var result = collectionClinicians.UpdateOne(doctorfilter, doctorUpdate);

            var midwifefilter = Builders<Clinician>.Filter.Eq("PersonId", midWives[valgtMidwife].PersonId);
            var midwifeUpdate = Builders<Clinician>.Update.Push("AssociatedBirthsId", birth1.BirthId);
            var result2 = collectionClinicians.UpdateOne(midwifefilter, midwifeUpdate);
            // Update Rooms
            var birthroomfilter = Builders<Room>.Filter.Eq("RoomId", chosenBirthRoom.RoomId);
            var birthroomupdate = Builders<Room>.Update.Push("Reservations", res1);
            var resultroom1 = collectionRooms.UpdateOne(birthroomfilter, birthroomupdate);
            if (chosenMaternityRoom != null)
            {
                var maternityroomfilter = Builders<Room>.Filter.Eq("RoomId", chosenMaternityRoom.RoomId);
                var maternityroomupdate = Builders<Room>.Update.Push("Reservations", res2);
                var resultroom2 = collectionRooms.UpdateOne(maternityroomfilter, maternityroomupdate);
            }

            if (chosenRestingRoom != null)
            {
                var restroomfilter = Builders<Room>.Filter.Eq("RoomId", chosenRestingRoom.RoomId);
                var restroomupdate = Builders<Room>.Update.Push("Reservations", res3);
                var resultroom3 = collectionRooms.UpdateOne(restroomfilter, restroomupdate);
            }
            GlobalNumbers.Instance.Dispose();

            Console.WriteLine($"Reservation til den {dato}, med personerne {childName}, {motherName} og {fatherName} er gennemført og gemt");




        }

        public static List<Room> showAvailableRooms(DateTime startTime,DateTime endTime, string roomType)
        {
            var filter = Builders<Room>.Filter.Eq(x => x.Type,roomType);
            List<Room> liRo = collectionRooms.Find(filter).ToList();

            foreach (var room in liRo)
            {
                bool roomAlreadyReserved = false;
                foreach (var res in room.Reservations)
                {
                    if (res.ReservationEnd <= startTime || res.ReservationStart >= endTime) continue;
                    else roomAlreadyReserved = true;
                }
                if (roomAlreadyReserved == false) Console.WriteLine(room.Type+": "+room.RoomId + " is available");
            }

            return liRo;

        }
        // 5. View
        //Given a birth can planned
        //    a) Show the rooms reserved the birth
        //    b) Show the clinicians assigned the birth
        public static void ShowRoomsAndCliniciansWbirth()
        {
            Console.WriteLine("\nType Birth ID:");
            int id = int.Parse(Console.ReadLine());
            var filter = Builders<Birth>.Filter.Where(b => b.BirthId == id);
            Birth birth=null;
            try
            {
            birth = collectionBirths.Find(filter).Single();

            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Ingen fødsler er tilføjet endnu. Tryk 'b' for at tilføje en fødsel");
                return;
            }
            if (birth == null)
            {
                Console.WriteLine("Fødslen kunne ikke findes :(");
                return;
            }

            Console.WriteLine("Name: " + birth.Child.FullName+ " mother: "+ birth.Child.Mother.FullName +" Planned to start " + birth.PlannedStartTime.ToUniversalTime().ToString("F"));

            //Find clinicians:
            Console.WriteLine("Associated clinicians: ");
            Clinician clinician;
            foreach (var i in birth.CliniciansId)
            {
                clinician = collectionClinicians.Find(c => c.PersonId == i).Single();
                Console.WriteLine(" " + clinician.FullName + " " + clinician.Type + " med id: " + clinician.PersonId);
            }
            //Find rooms:
            Console.WriteLine("Reserved Rooms: ");
            foreach (var res in birth.Child.Mother.Reservations)
            {
                var room = collectionRooms.Find(r => r.RoomId == res.ReservedRoomId).Single();
                Console.WriteLine(" " + room.RoomName + " ("+room.Type+ ") med id: " + room.RoomId);
            }

        }
        //1: show planned births the next 3 days
        public static void ShowPlannedBirthsNext3Days()
        {
            var births = collectionBirths.Find(b =>
                b.PlannedStartTime < DateTime.Now + TimeSpan.FromHours(2) + TimeSpan.FromDays(3)
                && b.PlannedStartTime > DateTime.Now + TimeSpan.FromHours(2)).ToList();
            Console.WriteLine("\nPlanned births the next 3 days:");
            foreach (var b in births)
            {
                Console.WriteLine("  BirthId: " + b.BirthId + ". Name: " + b.Child.FullName + ". Mother: " + b.Child.Mother.FullName+".");
            }
        }

        //3: Aktuelt igangværende fødsler 
        public static void ShowOngoingBirths()
        {
            var births = collectionBirths.Find(b =>
                (b.PlannedStartTime < DateTime.Now+TimeSpan.FromHours(2)) 
                && (b.PlannedStartTime > (DateTime.Now + TimeSpan.FromHours(2) - TimeSpan.FromHours(5)))).ToList();
            Console.WriteLine("\nOngoing Births (Births with a startTime in the last 5 hours):");
            foreach (var b in births)
            {
                Console.WriteLine("BirthId: " + b.BirthId + ". Name: " + b.Child.FullName + ". Mother: " + b.Child.Mother.FullName +".");
            }
        }
    }
}
