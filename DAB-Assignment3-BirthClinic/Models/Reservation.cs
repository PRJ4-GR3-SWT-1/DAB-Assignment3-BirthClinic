using System;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class Reservation
    {
        Reservation()
        {
            ReservationId = GlobalNumbers.Instance.getReservationId();
        }
        public int ReservationId { get; set; }
        public Room ReservedRoom { get; set; }
        public Mother User { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}