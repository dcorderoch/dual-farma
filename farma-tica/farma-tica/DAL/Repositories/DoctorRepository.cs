using System.Collections.Generic;
using System.Data;
using farma_tica.DAL.Models;

namespace farma_tica.DAL.Repositories
{
    public class DoctorRepository : Repository<Doctor>
    {
        public DoctorRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        ///     Get all doctors
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Doctor> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Doctor";
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        ///     Creates a new doctor
        /// </summary>
        /// <param name="doctor"></param>
        public override void Create(Doctor doctor)
        {
            using (var command = Context.CreateDbCommand())
            {
                var doctorProps = new object[]
                {
                    doctor.DoctorId, doctor.IdNumber, doctor.Name, doctor.LastName1, doctor.LastName2,
                    doctor.PlaceResidence
                };
                command.CommandText =
                    @"INSERT INTO Doctor VALUES(@doctorId, @idNumber, @name, @lastName1, @lastName2, @placeResidence)";
                var parameterNames = new[]
                {"@doctorId", "@idNumber", "@name", "@lastName1", "@lastName2", "@placeResidence"};
                for (var i = 0; i < doctorProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = doctorProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Gets a doctor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override IEnumerable<Doctor> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Doctor WHERE ID_Doctor = @doctorId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@doctorId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        ///     Updates an existing doctor
        /// </summary>
        /// <param name="doctor"></param>
        public override void Update(Doctor doctor)
        {
            using (var command = Context.CreateDbCommand())
            {
                var doctorProps = new object[]
                {doctor.Name, doctor.LastName1, doctor.LastName2, doctor.PlaceResidence, doctor.DoctorId};
                command.CommandText =
                    @"UPDATE Doctor SET NumeroCedula= @idNumber, Nombre= @name, PrimerApellido= @lastName1, SegundoApellido= @lastName2, LugarResidencia= @placeResidence WHERE ID_Doctor= @doctorId";
                var parameterNames = new[]
                {"@idNumber", "@name", "@lastName1", "@lastName2", "@placeResidence", "@doctorId"};
                for (var i = 0; i < doctorProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = doctorProps[i];
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@doctorId";
                idParameter.Value = doctor.DoctorId;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Delete doctor by its id
        /// </summary>
        /// <param name="id"></param>
        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Doctor WHERE ID_Doctor= @doctorId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@doctorId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     map result from query to object doctor
        /// </summary>
        /// <param name="record"></param>
        /// <param name="doctor"></param>
        protected override void Map(IDataRecord record, Doctor doctor)
        {
            doctor.DoctorId = (string) record["ID_Doctor"];
            doctor.IdNumber = (string) record["NumeroCedula"];
            doctor.Name = (string) record["Nombre"];
            doctor.LastName1 = (string) record["PrimerApellido"];
            doctor.LastName2 = (string) record["SegundoApellido"];
            doctor.PlaceResidence = (string) record["LugarResidencia"];
        }
    }
}