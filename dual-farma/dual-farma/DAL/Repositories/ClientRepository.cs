using System;
using System.Collections.Generic;
using System.Data;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.DAL.Repositories
{
    /// <summary>
    /// Client repository
    /// </summary>
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all the exiting clients
        /// </summary>
        /// <returns>A list of clients</returns>
        public override IEnumerable<Client> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Cliente";
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Insert new client into repository
        /// </summary>
        /// <param name="newClient"></param>
        public override void Create(Client newClient)
        {
            using (var command = Context.CreateDbCommand())
            {
                var clientProps = new object[]
                {newClient.Id, newClient.Name, newClient.LastName1, newClient.LastName2,newClient.PenaltiesNumber,newClient.PlaceResidence,newClient.MedicalHistory,newClient.BornDate,newClient.PhoneMum};
                command.CommandText = @"INSERT INTO Cliente VALUES(@id, @name, @lastName1, @lastName2, @penaltiesNumber, @placeResidence, @medicalHistory, @bornDate, @phoneNum)";
                var parameterNames = new string[] { "@id", "@name", "@lastName1", "@lastName2", "@penaltiesNumber", "@placeResidence", "@medicalHistory", "@bornDate", "@phoneNum"};
                for (var i = 0; i < clientProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = clientProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets client by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override IEnumerable<Client> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Cliente WHERE NumeroCedula = @clientId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@clientId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Updates an existing client
        /// </summary>
        /// <param name="newClient"></param>
        public override void Update(Client newClient)
        {
            using (var command = Context.CreateDbCommand())
            {
                var clientProps = new object[]
                { newClient.Name, newClient.LastName1, newClient.LastName2,newClient.PenaltiesNumber,newClient.PlaceResidence,newClient.MedicalHistory,newClient.BornDate,newClient.PhoneMum};
                command.CommandText = @"UPDATE Cliente SET Nombre = @name, PrimerApellido=@lastName1, SegundoApellido=@lastName2, CantidadMultas=@penaltiesNum, LugarResidencia=@placeResidence, 
                                                           Historial= @medicalHistory, FechaNacimiento= @bornDate, NumeroTelefono=@phoneNum WHERE NumeroCedula =@clientId";
                var parameterNames = new string[] { "@name", "@lastName1", "@lastName2", "@penaltiesNum", "@placeResidence", "@medicalHistory", "@bornDate", "@phoneNum" };
                for (var i = 0; i < clientProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = clientProps[i];
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@clientId";
                idParameter.Value = newClient.Id;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes object by its id
        /// </summary>
        /// <param name="id"></param>
        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Cliente WHERE NumeroCedula= @clientId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@clientId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Map result into entity
        /// </summary>
        /// <param name="record">query result</param>
        /// <param name="client">entity to map values into</param>
        protected override void Map(IDataRecord record, Client client)
        {
            client.Id = (string) record["NumeroCedula"];
            client.Name = (string) record["Nombre"];
            client.LastName1=(string) record["PrimerApellido"];
            client.LastName2 = (string) record["SegundoApellido"];
            client.PenaltiesNumber = (int) record["CantidadMultas"];
            client.PlaceResidence = (string) record["LugarResidencia"];
            client.MedicalHistory = (string) record["Historial"];
            client.BornDate = (DateTime) record["FechaNacimiento"];
            client.PhoneMum = (string) record["NumeroTelefono"];
        }
    }
}