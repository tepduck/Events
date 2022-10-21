using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EventManipulationDto
    {
        [Required(ErrorMessage = "Event's name is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Event's description is a required field")]
        [MaxLength(255, ErrorMessage = "Maximum length for the Description is 255 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Event's speeker is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Speeker is 100 characters")]
        public string Speeker { get; set; }

        [Required(ErrorMessage = "Event's date and place is a required field")]
        [MaxLength(100, ErrorMessage = "Maximum length for the EventDateAndPlace is 100 characters")]
        public string EventDateAndPlace { get; set; }
    }
}
