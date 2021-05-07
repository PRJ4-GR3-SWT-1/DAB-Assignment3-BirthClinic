using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment3_BirthClinic.Models
{
   public class Room
    {
        public Room(string roomName)
        {
            RoomName = roomName;
            Reservations = new List<Reservation>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }

   public class MaternityRoom : Room
   {
       public MaternityRoom(string RoomName) : base(RoomName)
       {

       }
   }
   public class RestingRoom : Room
   {
       public RestingRoom(string RoomName) : base(RoomName)
       {

       }

   }
   public class BirthRoom : Room
   {
       public BirthRoom(string RoomName) : base(RoomName)
       {

       }
   }
}
