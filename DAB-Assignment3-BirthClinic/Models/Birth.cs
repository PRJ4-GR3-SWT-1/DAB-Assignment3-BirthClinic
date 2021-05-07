using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class Birth
    {
        public Birth()
        {
            Clinicians = new List<Clinician>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int BirthId { get; set; }
        public Child Child { get; set; }

       
        
        public List<Clinician> Clinicians { get; set; }
        public DateTime PlannedStartTime { get; set; }
    }
}