using System;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class Reservation
    {
        public Reservation()
        {
            ReservationId = GlobalNumbers.Instance.getReservationId();
        }
        public int ReservationId { get; set; }
        public int ReservedRoomId { get; set; }
        public int UserId { get; set; }
        //Help found on http://www.binaryintellect.net/articles/6c715186-97b1-427a-9ccc-deb3ece7b839.aspx 
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}