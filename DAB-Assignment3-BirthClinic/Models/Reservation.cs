using System;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class Reservation
    {
        public int ReservationNumber { get; set; }
        public Room ReservedRoom { get; set; }
        public Mother User { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
    }
}