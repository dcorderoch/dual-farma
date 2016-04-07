using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;

namespace farma_tica.BLL
{
    public class Prescription_Manager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;

        private DbContext context;

        public Prescription_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }

        /// <summary>
        /// Creates a prescription given a doctor Id and a prescription image.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="prescriptionImage"></param>
        /// <returns></returns>
        public int CreatePrescription(string doctorId, string prescriptionImage)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var prescriptionRepo = new PrescriptionRepository(context);
                try
                {
                    Prescription newPrescription = new Prescription();
                    newPrescription.PrescriptionID = new Guid();
                    newPrescription.Doctor = doctorId;
                    newPrescription.Image = Convert.FromBase64String(prescriptionImage);
                    prescriptionRepo.Create(newPrescription);
                    uow.SaveChanges();
                    response = Constants.SUCCESS;
                }
                catch (Exception)
                {
                    response = Constants.FAIL;
                }
            }
            return response;
        }

        /// <summary>
        /// Obtains all existing prescriptions from the database.
        /// </summary>
        /// <returns></returns>
        public List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> prescriptionList = new List<Prescription>();
            using (var uow = context.CreateUnitOfWork())
            {
                var prescriptionRepo = new PrescriptionRepository(context);
                try
                {
                    prescriptionList = (List<Prescription>)prescriptionRepo.GetAll();
                }
                catch (Exception)
                {
                    prescriptionList = null;
                }
            }
            return prescriptionList;
        }

/// <summary>
/// Method in charge of deleting a prescription given its ID.
/// </summary>
/// <param name="prescriptionID"></param>
/// <returns></returns>
        public int DeletePrescription(string prescriptionID)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var prescriptionRepo = new PrescriptionRepository(context);
                try
                {
                    prescriptionRepo.DeleteById(prescriptionID);
                    uow.SaveChanges();
                    response = Constants.SUCCESS;
                }
                catch (Exception)
                {
                    response = Constants.FAIL;
                }
            }
            return response;
        }
    }
    }
