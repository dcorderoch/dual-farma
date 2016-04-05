using System;
using System.Collections.Generic;
using System.Linq;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;


namespace farma_tica.BLL
{
    /// <summary>
    /// Medicine_Manager is intended to validate most of the business rules related to the medicines. 
    /// </summary>
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
        /// <param name="stock"></param>
        /// <returns>Integer indicating whether the creation was successful.</returns>
        public int CreateMedicine(string name, string requiresPrescription, string price, string originOffice, string stock)
        {
            List<Medicine> medicineList = new List<Medicine>();
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                 try
                {
                    medicineList = (List<Medicine>)medicineRepo.GetByName(name);
                    if (!medicineList.Any())
                    {
                        Medicine newMedicine = new Medicine();
                        newMedicine.MedicineId = Guid.NewGuid();
                        newMedicine.Name = name;
                        newMedicine.RequiresPrescription = Convert.ToBoolean(requiresPrescription);

                        newMedicine.Price = Convert.ToDecimal(price);
                        newMedicine.Stock = Convert.ToInt32(stock);
                        newMedicine.AmmountSold = 0;
                        medicineRepo.Create(newMedicine);
                        medicineRepo.CreateMedicineInBranchOffice(newMedicine, new Guid(originOffice));

                    }
                    else
                    {
                        var medicine = medicineList.First();
                        medicine.Price = Convert.ToDecimal(price);
                        medicine.Stock = Convert.ToInt32(stock);
                        medicine.AmmountSold = 0;
                        medicineRepo.CreateMedicineInBranchOffice(medicine, Guid.Parse(originOffice));
                    }
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
        /// Obtains all medicines from a specific company.
        /// </summary>
        /// <param></param>
        /// <returns>List<Medicine> that contains all the medicines of the specified company</returns>
        public List<Medicine> GetAllMedicines(string branchOffice)
        {
            List<Medicine> medicineList = new List<Medicine>();
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>) medicineRepo.GetAllByBranchOffice(new Guid(branchOffice));
                }
                catch (Exception)
                {
                    medicineList = null;
                }
            }
            return medicineList;
        }

        /// <summary>
        /// Gets most sold medicines by the company given.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>List of medicines ordered descendently by most sales.</returns>
        public List<Medicine> GetMostSoldMedicinesByCompany(string company)
        {
            List<Medicine> medicineList = new List<Medicine>();
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>)medicineRepo.GetTotalMostSoldByCompany(company);
                }
                catch (Exception)
                {
                    medicineList = null;
                }
            }
            return medicineList;

        }

        /// <summary>
        /// Gets table with medicines most sold by using the new software.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>List of most sold medicines by using new software.</returns>
        public List<Medicine> GetMostSoldByNewSoftware(string company)
        {
            List<Medicine> medicineList = new List<Medicine>();
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>)medicineRepo.GetOnlineMostSoldByCompany(company);
                }
                catch (Exception)
                {
                    medicineList = null;
                }
            }
            return medicineList;
        }

        /// <summary>
        /// Gives the total amount of sales accomplished by given company.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>Integer value that indicates the total sales accomplished by the company.</returns>
        public int TotalSalesByCompany(string company)
        {
            var sales = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    sales = medicineRepo.GetAmmountSoldByCompany(company);
                }
                catch (Exception)
                {
                    sales = 0;
                }
            }
            return sales;
        }

        /// <summary>
        /// Gets the most sold medicines globally.
        /// </summary>
        /// <returns>List of medicines most sold by both companies.</returns>
        public List<Medicine> GlobalMostSoldMedicines()
        {
            List<Medicine> medicineList = new List<Medicine>();
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineList = (List<Medicine>)medicineRepo.GetTotalMostSold();
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
        /// <param name="price"></param>
        /// <param name="branchOffice"></param>
        /// <param name="stock"></param>
        /// <param name="numberSold"></param>
        /// <returns>Integer with the result of the update.</returns>
        public int UpdateMedicine(string medicineId, string price, string branchOffice, string stock, string numberSold)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                Medicine modifiedMedicine = new Medicine();
                try
                {
                    modifiedMedicine.MedicineId = Guid.Parse(medicineId);
                    modifiedMedicine.Price = Convert.ToDecimal(price);
                    modifiedMedicine.Stock = Convert.ToInt32(stock);
                    modifiedMedicine.AmmountSold = Convert.ToInt32(numberSold);
                    medicineRepo.UpdateMedicineInBranchOffice(modifiedMedicine,Guid.Parse(branchOffice));
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
        /// <param name="branchOffice"></param>
        /// <returns>Integer indicating whether the deletion completed successfully.</returns>
        public int DeleteMedicine(string medicineId, string branchOffice)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                try
                {
                    medicineRepo.DeleteMedicineFromBranchOffice(Guid.Parse(medicineId), Guid.Parse(branchOffice));
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