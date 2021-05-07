using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DAB_Assignment3_BirthClinic.Models
{
    public class GlobalNumbers
    {
        public ObjectId _id { get; set; }
        public int PersonId { get; set; }
        public int BirthId { get; set; }
        public int ReservationId { get; set; }
    }
}
