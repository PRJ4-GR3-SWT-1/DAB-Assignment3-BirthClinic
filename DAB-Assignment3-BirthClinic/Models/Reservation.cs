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
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}