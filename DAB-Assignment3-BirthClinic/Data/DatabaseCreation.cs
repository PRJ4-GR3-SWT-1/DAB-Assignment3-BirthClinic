using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicLibrary.Data;
using DAB_Assignment3_BirthClinic.Models;
using MongoDB.Driver;

namespace DAB_Assignment3_BirthClinic.Data
{
    class DatabaseCreation
    {

        //Create DB

        //Clinician collection: 
        public IMongoCollection<Person> Clinicians { get; set; }

        //Room collection:
        public  IMongoCollection<Room> Rooms { get; set; }

        //Check if collections are empty: else
        //SeedData(RoomCollection, ClinicianCollection);


    }
}
