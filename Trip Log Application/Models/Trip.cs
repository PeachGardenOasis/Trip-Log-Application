using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trip_Log_Application.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        [Required(ErrorMessage="Please enter a destination")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Please enter a valid start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please enter a valid end date")]
        public DateTime EndDate { get; set; }

        public string Accomodation { get; set; }

        public string AccomodationPhone { get; set; }

        public string AccomodationEmail { get; set; }

        public string ThingToDo1 { get; set; }

        public string ThingToDo2 { get; set; }

        public string ThingToDo3 { get; set; }
    }
}
