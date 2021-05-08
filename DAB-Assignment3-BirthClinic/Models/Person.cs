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
        protected Person(string name)
        {
            id = GlobalNumbers.Instance.getPersonId();
            FullName = name;
        }

        public int id { get; set; }
        public string FullName { get; set; }
        
    }

    public class Child : Person
    {
        public Child(string FullName) : base(FullName)
        {
            FamilyMembersId = new List<int>();
        }

        public int MotherId { get; set; }
       // public Mother Mother { get; set; }
        //public List<FamilyMember> FamilyMembers { get; set; }
        public List<int> FamilyMembersId { get; set; }
        public DateTime Birthday { get; set; }

    }

    public class Clinician:Person
    {
        public Clinician(string FullName) : base(FullName)
        {
          //  AssociatedBirths = new List<ClinicianBirth>();
        }
        public List<ClinicianBirth> AssociatedBirths { get; set; }
    }
    public class Secretary : Person
    {
        public Secretary(string FullName) : base(FullName)
        {

        }
        
    }
    public class Mother : Person
    {
        public Mother(string FullName) :base(FullName)
        {
            Reservations = new List<Reservation>();
            Children = new List<Child>();
        }
        public List<Child> Children { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }

    public class FamilyMember : Person
    {
        public FamilyMember(string FullName, string relation) : base(FullName)
        {
            Relation = relation;
        }
        public string Relation { get; set; }
    }



    public class MidWife : Clinician
    {
        public MidWife(string FullName) : base(FullName)
        {

        }

    }

    public class Doctor : Clinician
    {
        public Doctor(string FullName) : base(FullName)
        {

        }
    }

    public class Nurse : Clinician
    {
        public Nurse(string FullName) : base(FullName)
        {
        }
    }

    public class SocialHealthAssistant : Clinician
    {
        public SocialHealthAssistant(string FullName) : base(FullName)
        {
        }
    }
}