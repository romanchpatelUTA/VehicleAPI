using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VehicleAPI.Models
{
    public class Vehicle
    {

        /// <summary>
        /// Gets or sets Id of Vehicle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets year of the vehicle.
        /// </summary>
        [Range(1950, 2050)]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets make of the vehicle.
        /// </summary>
        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public string Make { get; set; }

        /// <summary>
        /// Gets or sets model of the vehicle.
        /// </summary>
        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public string Model { get; set; }

    }
}