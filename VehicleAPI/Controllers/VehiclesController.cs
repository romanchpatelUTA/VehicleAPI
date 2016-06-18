using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VehicleAPI.Models;

namespace VehicleAPI.Controllers
{
    public class VehiclesController : ApiController
    {

        public static List<Vehicle> list = new List<Vehicle>()
        {
            new Vehicle(){ Id=1, Make="Toyota", Model="CX500", Year=1999 },
            new Vehicle(){ Id=2, Make="Honda", Model="MX50", Year=2008 },
            new Vehicle(){ Id=3, Make="VolksWagon", Model="SW500", Year=2011 },
            new Vehicle(){ Id=4, Make="Nissan", Model="Eltima", Year=2012 },
            new Vehicle(){ Id=5, Make="Honda", Model="MX60", Year=2004 }
        };

        /// <summary>
        /// Get the list containing given id.
        /// </summary>
        /// <param name="id">Id of the Vehicle</param>
        /// <returns></returns>
        [HttpGet]
        public Vehicle Get(int id)
        {
            return list.Find(x => x.Id == id);

        }

        /// <summary>
        /// Filter list based on given attributes.
        /// </summary>
        /// <param name="vehicle">search attribute in vehicle object</param>
        /// <returns>list of filter data</returns>
        [HttpGet]
        public List<Vehicle> Get([FromUri]Vehicle vehicle)
        {
            var filterList = list;
            if (vehicle != null)
            {
                if (vehicle.Id != 0)
                {
                    filterList = filterList.FindAll(v => v.Id == vehicle.Id);
                }
                if (!String.IsNullOrEmpty(vehicle.Make))
                {
                    filterList = filterList.FindAll(v => v.Make.ToLower().Trim() == vehicle.Make.ToLower().Trim());
                }
                if (!String.IsNullOrEmpty(vehicle.Model))
                {
                    filterList = filterList.FindAll(v => v.Model.ToLower().Trim() == vehicle.Model.ToLower().Trim());
                }
                if (vehicle.Year != 0)
                {
                    filterList = filterList.FindAll(v => v.Year == vehicle.Year);
                }
            }

            return filterList;
        }

        /// <summary>
        /// Add the vehicle in list.
        /// </summary>
        /// <param name="vehicle">vehicle object</param>
        /// <returns>Ok response if vehicle is added, error otherwise.</returns>
        [HttpPost]
        public HttpResponseMessage Post(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                int nextID = list.Count + 1;
                if (vehicle != null)
                {
                    vehicle.Id = nextID;
                    list.Add(vehicle);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        /// Update the vehicle based on its ID.
        /// </summary>
        /// <param name="vehicle">Updated Vehicle object</param>
        [HttpPut]
        public HttpResponseMessage Put(Vehicle vehicle)
        {
            var vehichleToUpdate = list.Find(x => x.Id == vehicle.Id);
            if (vehichleToUpdate != null)
            {
                vehichleToUpdate.Make = vehicle.Make;
                vehichleToUpdate.Model = vehicle.Model;
                vehichleToUpdate.Year = vehicle.Year;
                return Request.CreateResponse(HttpStatusCode.OK, vehichleToUpdate);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "object not found");
            }

        }

        /// <summary>
        /// Delete the vehicle object based on id.
        /// </summary>
        /// <param name="id">Id of the vehicle</param>
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Vehicle vehicleToDelete = list.Find(x => x.Id == id);
            if(vehicleToDelete!=null)
            {
                list.Remove(vehicleToDelete);
                return Request.CreateResponse(HttpStatusCode.OK, vehicleToDelete);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Object not found");
            }
        }
    }
}