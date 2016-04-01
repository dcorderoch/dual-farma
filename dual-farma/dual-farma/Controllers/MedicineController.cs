using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dual_farma.BLL;
using dual_farma.DAL.Models;

namespace dual_farma.Controllers
{
    public class MedicineController : ApiController
    {
        // POST api/medicine/create
        public string Create([FromBody]string name, [FromBody]string requiresPrescription, [FromBody]string price, [FromBody]string originOffice, [FromBody]string house, [FromBody]string stock,
            [FromBody]string numberSold)
        {
            var MM = new Medicine_Manager();
            if (MM.CreateMedicine(name,requiresPrescription,price,originOffice,house,stock,numberSold) == Constants.MEDICINE_CREATED)
            {
                return "{medicine:created}";
            }
            return "{medicine:still-not-created}";
        }

        // GET api/medicine/medicine
        public string[] Medicine([FromBody]string mID)
        {
            var MM = new Medicine_Manager();
            return MM.GetMedicineById(mID);
        }

        // GET api/medicine/allmeds
        public List<Medicine> AllMeds()
        {
            var MM = new Medicine_Manager();
            return MM.GetAllMedicines();
        }

        // PUT api/medicine/update
        public string Update([FromBody] string name, [FromBody] string requiresPrescription, [FromBody] string price,
            [FromBody] string originOffice, [FromBody] string house, [FromBody] string stock,
            [FromBody] string numberSold)
        {
            var MM = new Medicine_Manager();
            if (MM.CreateMedicine(name, requiresPrescription, price, originOffice, house, stock, numberSold) == Constants.MEDICINE_UPDATED)
            {
                return "{medicine:updated}";
            }
            return "{medicine:still-not-updated}";
        }

        public string Delete([FromBody] string mID)
        {
            var MM = new Medicine_Manager();
            if (MM.DeleteMedicine(mID) == Constants.MEDICINE_DELETED)
            {
                return "{medicine:deleted}";
            }
            return "{medicine:still-not-deleted}";
        }
    }
}
