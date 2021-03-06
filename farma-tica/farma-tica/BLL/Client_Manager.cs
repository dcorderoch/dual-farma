﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;
using Microsoft.SqlServer.Server;

namespace farma_tica.BLL
{

    /// <summary>
    /// Client_Manager is intended to validate most of the business rules related to the Clients. 
    /// </summary>
    public class Client_Manager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;
        private DbContext context;

        public Client_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }
        /// <summary>
        /// Method called from REST API in order to create a new Client.
        /// </summary>
        /// <param name="cedula"></param>
        /// <param name="name"></param>
        /// <param name="lastName1"></param>
        /// <param name="lastName2"></param>
        /// <param name="fines"></param>
        /// <param name="home"></param>
        /// <param name="history"></param>
        /// <param name="birthDate"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns>1, if successful operation, 0, otherwise.</returns>
        public int CreateClient(string cedula, string name, string lastName1, string lastName2, int fines, string home, string history,
            string birthDate, string phoneNumber, string password)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var clientRepo = new ClientRepository(context);
                Client newClient = new Client();
                try
                {
                    newClient.NumCed = cedula;
                    newClient.Password = password;
                    newClient.Name = name;
                    newClient.LastName1 = lastName1;
                    newClient.LastName2 = lastName2;
                    newClient.PenaltiesNumber = fines;
                    newClient.PlaceResidence = home;
                    newClient.MedicalHistory = history;
                    newClient.BornDate = String.Format("{0:dd/MM/yyyy}", birthDate);
                    newClient.PhoneMum = phoneNumber;
                    clientRepo.Create(newClient);
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
        /// Method that returns the list of existing clients.
        /// </summary>
        /// <returns>A list containing all the clients in the database.</returns>
        public List<Client> GetAllClients()
        {
            List<Client> clientList = new List<Client>();
            using (var uow = context.CreateUnitOfWork())
            {
                var clientRepo = new ClientRepository(context);
                try
                {
                    clientList = (List<Client>)clientRepo.GetAll();
                }
                catch (Exception)
                {
                    clientList = null;
                }
            }
            return clientList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientPass"></param>
        /// <returns></returns>
        public List<string> AuthClientLogin(string clientId, string clientPass)
        {
            List<string> response = new List<string>();
            using (var uow = context.CreateUnitOfWork())
            {
                var clientRepo = new ClientRepository(context);
                try
                {
                    List<Client> clientList = (List<Client>)clientRepo.GetById(clientId);
                    
                    if (clientList.Any())
                    {
                        var client = clientList.First();
                        if (client.Password == clientPass)
                        {
                            response.Add(Constants.SUCCESSFUL_LOGIN.ToString());
                            //response[0] = Constants.SUCCESSFUL_LOGIN.ToString();
                            response.Add(client.NumCed);
                            response.Add(client.Name);
                            response.Add(client.LastName1);
                            response.Add(client.LastName2);
                            response.Add(client.PenaltiesNumber.ToString());
                            response.Add(client.PlaceResidence);
                            response.Add(client.MedicalHistory);
                            response.Add(String.Format("{0:dd/MM/yyyy}", client.BornDate));
                            response.Add(client.PhoneMum);
                        }
                        else
                        {
                            response.Add(Constants.INVALID_PASSWORD.ToString());
                        }
                    }
                    else
                    {
                        response.Add(Constants.INVALID_USER.ToString());
                    }
                }
                catch (Exception)
                {
                    response.Add(Constants.ERROR.ToString());
                }
            }
            return response;
        }

        /// <summary>
        /// Method that updates a client given the attributes to modify.
        /// </summary>
        /// <param name="cedula"></param>
        /// <param name="name"></param>
        /// <param name="lastName1"></param>
        /// <param name="lastName2"></param>
        /// <param name="fines"></param>
        /// <param name="home"></param>
        /// <param name="history"></param>
        /// <param name="birthDate"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="password"></param>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public int UpdateClient(string cedula, string name, string lastName1, string lastName2, int fines, string home, string history,
            string birthDate, string phoneNumber, string password){
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var clientRepo = new ClientRepository(context);
                Client newClient = new Client();
                try
                {
                    newClient.NumCed = cedula;
                    newClient.Password = password;
                    newClient.Name = name;
                    newClient.LastName1 = lastName1;
                    newClient.LastName2 = lastName2;
                    newClient.PenaltiesNumber = fines;
                    newClient.PlaceResidence = home;
                    newClient.MedicalHistory = history;
                    newClient.BornDate = String.Format("{0:dd/MM/yyyy}", newClient.BornDate);
                    newClient.PhoneMum = phoneNumber;
                    clientRepo.Update(newClient);
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
        /// Method that eliminates the given client.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>1 or 0 depending whether the operation completed or not.</returns>
        public int DeleteClient(string clientId)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var clientRepo = new ClientRepository(context);
                try
                {
                    clientRepo.DeleteById(clientId);
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