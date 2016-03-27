using System.Collections.Generic;
using System.Data;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.DAL.Repositories
{
    /// <summary>
    /// Role Repository to perform CRUD operations
    /// </summary>
    public class RoleRepository :Repository<Role>
    {
        /// <summary>
        /// Initialize repository for the given context
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets all the roles
        /// </summary>
        /// <returns>Role list</returns>
        public override IEnumerable<Role> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Rol";
                var result = ToList(command);
                return result;
            }
        }

        public override void Create(Role role)
        {
            using (var command = Context.CreateDbCommand())
            {
                var roleProps = new object[] {role.Id, role.RoleName};
                command.CommandText = @"INSERT INTO Rol VALUES(@roleId, @roleName)";
                var parameterNames = new string[] { "@roleId", "@roleName"};
                for (var i = 0; i < roleProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = roleProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Get a role by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list with the found role></returns>
        public override IEnumerable<Role> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Rol WHERE ID_Rol = @roleId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@roleId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Updates an existing role
        /// </summary>
        /// <param name="role"></param>
        public override void Update(Role role)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"UPDATE Rol SET Rol= @roleName WHERE ID_Rol=@roleId";
                var parameterName = "@roleName";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = parameterName;
                newParameter.Value = role.RoleName;
                command.Parameters.Add(newParameter);
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@roleId";
                idParameter.Value = role.Id;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Deletes an existing role
        /// </summary>
        /// <param name="id"></param>
        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Rol WHERE ID_Rol= @roleId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@roleId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Maps the query result to an object
        /// </summary>
        /// <param name="record"></param>
        /// <param name="entity"></param>
        protected override void Map(IDataRecord record, Role entity)
        {
            entity.Id = (int) record["ID_Rol"];
            entity.RoleName = (string) record["Rol"];
        }
    }
}