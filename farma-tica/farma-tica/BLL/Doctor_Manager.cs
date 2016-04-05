using System;
using System.Collections.Generic;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;

namespace farma_tica.BLL
{
    /// <summary>
    ///     Doctor_Manager is intended to validate most of the business rules related to the Doctors.
    /// </summary>
    public class Doctor_Manager
    {
        private readonly DbContext context;

        /// <summary>
        ///     Class members which allow connecting to the Database.
        /// </summary>
        private readonly DbConnectionFactory factory;

        public Doctor_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }

        /// <summary>
        ///     Creates doctor with given values.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="numeroCedula"></param>
        /// <param name="name"></param>
        /// <param name="lastName1"></param>
        /// <param name="lastName2"></param>
        /// <param name="home"></param>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public int CreateDoctor(string doctorId, string numeroCedula, string name, string lastName1, string lastName2,
            string home)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var doctorRepo = new DoctorRepository(context);
                var newDoctor = new Doctor();
                try
                {
                    newDoctor.DoctorId = doctorId;
                    newDoctor.IdNumber = numeroCedula;
                    newDoctor.Name = name;
                    newDoctor.LastName1 = lastName1;
                    newDoctor.LastName2 = lastName2;
                    newDoctor.PlaceResidence = home;
                    doctorRepo.Create(newDoctor);
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
        ///     Returns the list of doctors in the DB.
        /// </summary>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public List<Doctor> GetAllDoctors()
        {
            var doctorList = new List<Doctor>();
            using (var uow = context.CreateUnitOfWork())
            {
                var doctorRepo = new DoctorRepository(context);
                try
                {
                    doctorList = (List<Doctor>) doctorRepo.GetAll();
                }
                catch (Exception)
                {
                    doctorList = null;
                }
            }
            return doctorList;
        }

        /// <summary>
        ///     Updates doctor with given values.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="numeroCedula"></param>
        /// <param name="name"></param>
        /// <param name="lastName1"></param>
        /// <param name="lastName2"></param>
        /// <param name="home"></param>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public int UpdateDoctor(string doctorId, string numeroCedula, string name, string lastName1, string lastName2,
            string home)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var doctorRepo = new DoctorRepository(context);
                var newDoctor = new Doctor();
                try
                {
                    newDoctor.DoctorId = doctorId;
                    newDoctor.IdNumber = numeroCedula;
                    newDoctor.Name = name;
                    newDoctor.LastName1 = lastName1;
                    newDoctor.LastName2 = lastName2;
                    newDoctor.PlaceResidence = home;
                    doctorRepo.Update(newDoctor);
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
        ///     Deletes the given doctor.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public int DeleteDoctor(string doctorId)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var doctorRepo = new DoctorRepository(context);
                try
                {
                    doctorRepo.DeleteById(doctorId);
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