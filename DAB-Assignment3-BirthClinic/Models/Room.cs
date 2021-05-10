using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment3_BirthClinic.Models
{
   public class Room
    {
        public Room(string roomName, string type)
        {
            RoomName = roomName;
            ReservationsIds = new List<int>();
            RoomId = GlobalNumbers.Instance.getRoomId();
            Type = type;
        }
        public int RoomId { get; set; }
        public string Type;
        public string RoomName { get; set; }
        public ICollection<int> ReservationsIds { get; set; }

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
