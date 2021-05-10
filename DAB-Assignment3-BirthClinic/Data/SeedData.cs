using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using DAB_Assignment3_BirthClinic.Models;
using MongoDB.Driver;

namespace BirthClinicLibrary.Data
{
    public class SeedData
    {

        public SeedData(IMongoCollection<Room> roomCollection, IMongoCollection<Person> personCollection)
        {
            SeedRooms(roomCollection);
            SeedClinicians(personCollection);
        }

        private static void SeedRooms(IMongoCollection<Room> collection)
        {

            List<Room> rooms = new List<Room>();

            #region Resting Room

            Room r1 = new RestingRoom("R1");
            Room r2 = new RestingRoom("R2");
            Room r3 = new RestingRoom("R3");
            Room r4 = new RestingRoom("R4");
            Room r5 = new RestingRoom("R5");

            rooms.Add(r1);
            rooms.Add(r2);
            rooms.Add(r3);
            rooms.Add(r4);
            rooms.Add(r5);

            #endregion

            #region BirthRooms
            Room b1 = new BirthRoom("B1");
            Room b2 = new BirthRoom("B2");
            Room b3 = new BirthRoom("B3");
            Room b4 = new BirthRoom("B4");
            Room b5 = new BirthRoom("B5");
            Room b6 = new BirthRoom("B6");
            Room b7 = new BirthRoom("B7");
            Room b8 = new BirthRoom("B8");
            Room b9 = new BirthRoom("B9");
            Room b10 = new BirthRoom("B10");
            Room b11 = new BirthRoom("B11");
            Room b12 = new BirthRoom("B12");
            Room b13 = new BirthRoom("B13");
            Room b14 = new BirthRoom("B14");
            Room b15 = new BirthRoom("B15");

            rooms.Add(b1);
            rooms.Add(b2);
            rooms.Add(b3);
            rooms.Add(b4);
            rooms.Add(b5);
            rooms.Add(b6);
            rooms.Add(b7);
            rooms.Add(b8);
            rooms.Add(b9);
            rooms.Add(b10);
            rooms.Add(b11);
            rooms.Add(b12);
            rooms.Add(b13);
            rooms.Add(b14);
            rooms.Add(b15);


            #endregion

            #region MaternityRooms

            Room m1 = new MaternityRoom("M1");
            Room m2 = new MaternityRoom("M2");
            Room m3 = new MaternityRoom("M3");
            Room m4 = new MaternityRoom("M4");
            Room m5 = new MaternityRoom("M5");
            Room m6 = new MaternityRoom("M6");
            Room m7 = new MaternityRoom("M7");
            Room m8 = new MaternityRoom("M8");
            Room m9 = new MaternityRoom("M9");
            Room m10 = new MaternityRoom("M10");
            Room m11 = new MaternityRoom("M11");
            Room m12 = new MaternityRoom("M12");
            Room m13 = new MaternityRoom("M13");
            Room m14 = new MaternityRoom("M14");
            Room m15 = new MaternityRoom("M15");
            Room m16 = new MaternityRoom("M16");
            Room m17 = new MaternityRoom("M17");
            Room m18 = new MaternityRoom("M18");
            Room m19 = new MaternityRoom("M19");
            Room m20 = new MaternityRoom("M20");
            Room m21 = new MaternityRoom("M21");
            Room m22 = new MaternityRoom("M22");

            rooms.Add(m1);
            rooms.Add(m2);
            rooms.Add(m3);
            rooms.Add(m4);
            rooms.Add(m5);
            rooms.Add(m6);
            rooms.Add(m7);
            rooms.Add(m8);
            rooms.Add(m9);
            rooms.Add(m10);
            rooms.Add(m11);
            rooms.Add(m12);
            rooms.Add(m13);
            rooms.Add(m14);
            rooms.Add(m15);
            rooms.Add(m16);
            rooms.Add(m17);
            rooms.Add(m18);
            rooms.Add(m19);
            rooms.Add(m20);
            rooms.Add(m21);
            rooms.Add(m22);

            #endregion
            
            collection.InsertMany(rooms);

            Console.WriteLine("Rooms added.");

        }



        private static void SeedClinicians(IMongoCollection<Person> collection)
        {
            List<Person> clinicians = new List<Person>();

            #region MidWives

            Person M1 = new MidWife("Mary");
            Person M2 = new MidWife("Malfred");
            Person M3 = new MidWife("Marius");
            Person M4 = new MidWife("Marianne");
            Person M5 = new MidWife("Morten");
            Person M6 = new MidWife("Marie");
            Person M7 = new MidWife("Molly");
            Person M8 = new MidWife("Mingming");
            Person M9 = new MidWife("Mulle");
            Person M10 = new MidWife("Mads");
            clinicians.Add(M1);
            clinicians.Add(M2);
            clinicians.Add(M3);
            clinicians.Add(M4);
            clinicians.Add(M5);
            clinicians.Add(M6);
            clinicians.Add(M7);
            clinicians.Add(M8);
            clinicians.Add(M9);
            clinicians.Add(M10);
            #endregion

            #region Doctors
            Person D1 = new Doctor("Dorthe");
            Person D2 = new Doctor("Dennis");
            Person D3 = new Doctor("Dina");
            Person D4 = new Doctor("Daniel");
            Person D5 = new Doctor("Daniella");

            clinicians.Add(D1);
            clinicians.Add(D2);
            clinicians.Add(D3);
            clinicians.Add(D4);
            clinicians.Add(D5);
            #endregion

            #region Nurses
            Person N1 = new Nurse("Nete");
            Person N2 = new Nurse("Nathan");
            Person N3 = new Nurse("Natalie");
            Person N4 = new Nurse("Noel");
            Person N5 = new Nurse("Nadja");
            Person N6 = new Nurse("Nessa");
            Person N7 = new Nurse("Naja");
            Person N8 = new Nurse("Nikoline");
            Person N9 = new Nurse("Nik");
            Person N10 = new Nurse("Nikolaj");
            Person N11 = new Nurse("Niklas");
            Person N12 = new Nurse("Nor");
            Person N13 = new Nurse("Nazarat");
            Person N14 = new Nurse("Neo");
            Person N15 = new Nurse("Nasir");
            Person N16 = new Nurse("Niller");
            Person N17 = new Nurse("Niko");
            Person N18 = new Nurse("Niels");
            Person N19 = new Nurse("Niels-Erik");
            Person N20 = new Nurse("Niels-Ove");


            clinicians.Add(N1);
            clinicians.Add(N2);
            clinicians.Add(N3);
            clinicians.Add(N4);
            clinicians.Add(N5);
            clinicians.Add(N6);
            clinicians.Add(N7);
            clinicians.Add(N8);
            clinicians.Add(N9);
            clinicians.Add(N10);
            clinicians.Add(N11);
            clinicians.Add(N12);
            clinicians.Add(N13);
            clinicians.Add(N14);
            clinicians.Add(N15);
            clinicians.Add(N16);
            clinicians.Add(N17);
            clinicians.Add(N18);
            clinicians.Add(N19);
            clinicians.Add(N20);
            #endregion

            #region SHAssistans
            Person SHA1 = new SocialHealthAssistant("Harry");
            Person SHA2 = new SocialHealthAssistant("Harper");
            Person SHA3 = new SocialHealthAssistant("Hans");
            Person SHA4 = new SocialHealthAssistant("Hope");
            Person SHA5 = new SocialHealthAssistant("Harriet");
            Person SHA6 = new SocialHealthAssistant("Hal");
            Person SHA7 = new SocialHealthAssistant("Hamlet");
            Person SHA8 = new SocialHealthAssistant("Hubert");
            Person SHA9 = new SocialHealthAssistant("Holger");
            Person SHA10 = new SocialHealthAssistant("Holmer");
            Person SHA11 = new SocialHealthAssistant("Hansi");
            Person SHA12 = new SocialHealthAssistant("Hylle");
            Person SHA13 = new SocialHealthAssistant("Henrik");
            Person SHA14 = new SocialHealthAssistant("Hermione");
            Person SHA15 = new SocialHealthAssistant("Heidi");
            Person SHA16 = new SocialHealthAssistant("Helene");
            Person SHA17 = new SocialHealthAssistant("Helena");
            Person SHA18 = new SocialHealthAssistant("Hailey");
            Person SHA19 = new SocialHealthAssistant("Henriette");
            Person SHA20 = new SocialHealthAssistant("Hanne");

            clinicians.Add(SHA1);
            clinicians.Add(SHA2);
            clinicians.Add(SHA3);
            clinicians.Add(SHA4);
            clinicians.Add(SHA5);
            clinicians.Add(SHA6);
            clinicians.Add(SHA7);
            clinicians.Add(SHA8);
            clinicians.Add(SHA9);
            clinicians.Add(SHA10);
            clinicians.Add(SHA11);
            clinicians.Add(SHA12);
            clinicians.Add(SHA13);
            clinicians.Add(SHA14);
            clinicians.Add(SHA15);
            clinicians.Add(SHA16);
            clinicians.Add(SHA17);
            clinicians.Add(SHA18);
            clinicians.Add(SHA19);
            clinicians.Add(SHA20);
            #endregion

            #region Secretary
            Person s1 = new Secretary("Susan");
            Person s2 = new Secretary("Simon");
            Person s3 = new Secretary("Sam");
            Person s4 = new Secretary("Susanne");

            clinicians.Add(s1);
            clinicians.Add(s2);
            clinicians.Add(s3);
            clinicians.Add(s4);

            #endregion

            collection.InsertMany(clinicians);
            Console.WriteLine("Clinicians added.");
        }

    }
}

