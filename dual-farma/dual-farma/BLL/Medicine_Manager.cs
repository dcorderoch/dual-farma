using System;
using System.Collections.Generic;
using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Models;
using FarmaticaCore.DAL.Repositories;

namespace dual_farma.BLL
{
    public class Medicine_Manager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;
        private DbContext context;

        public Medicine_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }

        /// <summary>
        /// Creates new medicine.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="requiresPrescription"></param>
        /// <param name="price"></param>
        /// <param name="originOffice"></param>
        /// <param name="house"></param>
        /// <param name="stock"></param>
        /// <param name="numberSold"></param>
        /// <returns>Integer indicating whether the creation was successful.</returns>
        public int CreateMedicine(string name, string requiresPrescription, string price, string originOffice,string house, string stock,
            string numberSold)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                Medicine newMedicine = new Medicine();
                try
                {
                    newMedicine.MedicineId = Guid.NewGuid();
                    newMedicine.Name = name;
                    newMedicine.RequiresPrescription = Convert.ToBoolean(requiresPrescription);
                    newMedicine.Price = Convert.ToInt32(price);
                    newMedicine.OriginOffice = Convert.ToInt32(originOffice);
                    newMedicine.House = house;
                    newMedicine.Stock = Convert.ToInt32(stock);
                    newMedicine.NumberSold = Convert.ToInt32(numberSold);
                    medicineRepo.Create(newMedicine);
                    uow.SaveChanges();
                    response = Constants.MEDICINE_CREATED;
                }
                catch (Exception)
                {
                    response = Constants.ALREADY_EXISTS;
                }
            }
            return response;
        }
        /// <summary>
        /// Obtains a specific medicine given by the parameter.
        /// </summary>
        /// <param name="medicineId"></param>
        /// <returns>String array containing the medicine's attributes. Null value if medicine does not exist in the database.</returns>
        public string[] GetMedicineById(string medicineId)
        {
            List<Medicine> medicineList = new List<Medicine>();
            string[] result = {};
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>)medicineRepo.GetById(medicineId);
                    Medicine medicine = medicineList[0];
                    result[0] = medicineId;
                    result[1] = medicine.Name;
                    result[2] = medicine.RequiresPrescription.ToString();
                    result[3] = medicine.Price.ToString();
                    result[4] = medicine.OriginOffice.ToString();
                    result[5] = medicine.House;
                    result[6] = medicine.Stock.ToString();
                    result[7] = medicine.NumberSold.ToString();
                }
                catch (Exception)
                {
                    result = null;       
                }
            }
            return result;
        }


        /// <summary>
        /// Obtains all medicines from a specific company.
        /// </summary>
        /// <param></param>
        /// <returns>List<Medicine> that contains all the medicines of the specified company</returns>
        public List<Medicine> GetAllMedicines()
        {
            List<Medicine> medicineList = new List<Medicine>();
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>) medicineRepo.GetAll();
                }
                catch (Exception)
                {
                    medicineList = null;
                }
            }
            return medicineList;
        }

        /// <summary>
        /// Updates given medicine if possible. 
        /// </summary>
        /// <param name="medicineId"></param>
        /// <param name="name"></param>
        /// <param name="requiresPrescription"></param>
        /// <param name="price"></param>
        /// <param name="originOffice"></param>
        /// <param name="house"></param>
        /// <param name="stock"></param>
        /// <param name="numberSold"></param>
        /// <returns>Integer with the result of the update.</returns>
        public int UpdateMedicine(string medicineId, string name, string requiresPrescription, string price, string originOffice,
            string house, string stock, string numberSold)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                Medicine newMedicine = new Medicine();
                try
                {
                    newMedicine.MedicineId = new Guid(medicineId);
                    newMedicine.Name = name;
                    newMedicine.RequiresPrescription = Convert.ToBoolean(requiresPrescription);
                    newMedicine.Price = Convert.ToInt32(price);
                    newMedicine.OriginOffice = Convert.ToInt32(originOffice);
                    newMedicine.House = house;
                    newMedicine.Stock = Convert.ToInt32(stock);
                    newMedicine.NumberSold= Convert.ToInt32(numberSold);
                    medicineRepo.Update(newMedicine);
                    uow.SaveChanges();
                    response = Constants.MEDICINE_UPDATED;
                }
                catch (Exception)
                {
                    response = Constants.MEDICINE_NOT_UPDATED;
                }
            }
            return response;
        }

        /// <summary>
        /// Deletes the given medicine.
        /// </summary>
        /// <param name="medicineId"></param>
        /// <returns>Integer indicating whether the deletion completed successfully.</returns>
        public int DeleteMedicine(string medicineId)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineRepo.DeleteById(medicineId);
                    uow.SaveChanges();
                    response = Constants.MEDICINE_DELETED;
                }
                catch (Exception)
                {
                    response = Constants.MEDICINE_NOT_DELETED;
                }
            }
            return response;
        }
    }
}