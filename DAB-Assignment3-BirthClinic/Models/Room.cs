using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment3_BirthClinic.Models
{
   public class Room
    {
        public Room(string roomName, string type)
        {
            RoomName = roomName;
            Reservations = new List<Reservation>();
            RoomId = GlobalNumbers.Instance.getRoomId();
            Type = type;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public int RoomId { get; set; }
        public string Type;
        public string RoomName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }

   public class MaternityRoom : Room
   {
       public MaternityRoom(string RoomName) : base(RoomName, "Maternity Room")
       {

       }
   }
   public class RestingRoom : Room
   {
       public RestingRoom(string RoomName) : base(RoomName, "Resting Room")
       {

       }

   }
   public class BirthRoom : Room
   {
       public BirthRoom(string RoomName) : base(RoomName, "BirthRoom")
       {

       }
   }
}
