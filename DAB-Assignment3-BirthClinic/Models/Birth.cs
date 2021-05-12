using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class Birth
    {
        public Birth()
        {
            BirthId = GlobalNumbers.Instance.getBirthId();
            CliniciansId = new List<int>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int BirthId { get; set; }
        public Child Child { get; set; }

       
        
       // public List<Clinician> Clinicians { get; set; }
       public List<int> CliniciansId { get; set; }
       [BsonElement]
       [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime PlannedStartTime { get; set; }
    }
}