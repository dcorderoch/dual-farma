using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class MedicineController : Controller
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
        // PUT api/medicine/delete
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
