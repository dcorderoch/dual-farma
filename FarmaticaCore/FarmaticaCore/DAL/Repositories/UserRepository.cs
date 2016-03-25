﻿using System.Data;
using System.Runtime.InteropServices;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.DAL.Repositories
{
    /// <summary>
    /// User repository to perform CRUDs operations
    /// </summary>
    public class UserRepository : Repository<User>
    {
        /// <summary>
        /// Creates an user repository for the given context
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// CRUD method to insert a new user
        /// </summary>
        /// <param name="user">user to insert</param>
        public void Create(User user)
        {
            using (var command = Context.CreateDbCommand())
            {
                var userProps = new object[]
                {user.IdUsuario, user.Password, user.Name, user.LastName1, user.LastName2, user.Email, user.Role};
                command.CommandText = @"INSERT INTO Usuario VALUES(@userId, @pass, @name, @lastName1, @lastName2, @email, @role)";
                var parameterNames = new string[] {"@userId", "@pass", "@name", "@lastName1", "@lastName2", "@email", "@role" };
                for (var i = 0; i < userProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = userProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }
        protected override void Map(IDataRecord record, User entity)
        {
            entity.IdUsuario = (string) record["ID_Usuario"];
            entity.Password = (string) record["Pass"];
            entity.Name = (string) record["Nombre"];
            entity.LastName1 = (string) record["PrimerApellido"];
            entity.LastName2 = (string) record["SegundoApellido"];
            entity.Email = (string) record["Email"];
            entity.Role = (int) record["Rol_Usuario"];
        }
    }
}