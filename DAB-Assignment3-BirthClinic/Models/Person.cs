using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment3_BirthClinic.Models
{
    public abstract class Person
    {
        protected Person(string name, string type)
        {
            FullName = name;
            PersonId = GlobalNumbers.Instance.getPersonId();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }

    }

    public class Child : Person
    {
        public Child(string FullName) : base(FullName,"Child")
        {

        }
        public int MotherId { get; set; }
        public List<int> FamilyMembersId { get; set; }
        public DateTime Birthday { get; set; }

    }

    public class Clinician:Person
    {

        public Clinician(string FullName, string type) : base(FullName,type)
        {
              AssociatedBirthsId = new List<int>();
        }
        public List<int> AssociatedBirthsId { get; set; }
    }
    public class Secretary : Clinician
    {
        public Secretary(string FullName) : base(FullName, "Secretary")
        {

        }
        
    }
    public class Mother : Person
    {
        public Mother(string FullName) :base(FullName,"Mother")
        {
            ReservationsIds = new List<int>();
            Children = new List<int>();
        }
        public List<int> Children { get; set; }
        public ICollection<int> ReservationsIds { get; set; }
    }

    public class FamilyMember : Person
    {
        public FamilyMember(string FullName, string relation) : base(FullName,"FamilyMember")
        {
            Relation = relation;
        }
        public string Relation { get; set; }
    }



    public class MidWife : Clinician
    {
        public MidWife(string FullName) : base(FullName, "MidWife")
        {

        }

    }

    public class Doctor : Clinician
    {
        public Doctor(string FullName) : base(FullName, "Doctor")
        {

        }
    }

    public class Nurse : Clinician
    {
        public Nurse(string FullName) : base(FullName, "Nurse")
        {
        }
    }

    public class SocialHealthAssistant : Clinician
    {
        public SocialHealthAssistant(string FullName) : base(FullName, "SocialAndHealthAssistant")
        {
        }
    }
}