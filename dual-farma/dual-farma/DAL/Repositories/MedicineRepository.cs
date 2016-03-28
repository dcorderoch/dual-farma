using System;
using System.Collections.Generic;
using System.Data;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.DAL.Repositories
{
    /// <summary>
    /// Medicine Repository
    /// </summary>
    public class MedicineRepository : Repository<Medicine>
    {
        public MedicineRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all existing medicines
        /// </summary>
        /// <returns>List of medicines</returns>
        public override IEnumerable<Medicine> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento";
                var result = ToList(command);
                return result;
            }
        }

        public override void Create(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
                {medicine.MedicineId, medicine.Name, medicine.RequiresPrescription, medicine.Price, medicine.OriginOffice, medicine.House,
                 medicine.Stock, medicine.NumberSold};
                command.CommandText = @"INSERT INTO Medicamento VALUES(@medicineId, @name, @reqPresc, @price, @originOffice, @house, " +
                                                                      "@stock, @numberSold)";
                var parameterNames = new string[] { "@medicineId", "@name", "@reqPresc", "@price", "@originOffice", "@house",
                                                    "@stock", "@numberSold"};
                for (var i = 0; i < medicineProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    //Null handler
                    if (medicineProps[i] == null)
                    {
                        newParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        newParameter.Value = medicineProps[i];
                    }

                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        public override IEnumerable<Medicine> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento WHERE ID_Medicamento = @medicineId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@medicineId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        public override void Update(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
                 {medicine.Name, medicine.RequiresPrescription, medicine.Price, medicine.OriginOffice, medicine.House,
                 medicine.Stock, medicine.NumberSold};
                command.CommandText = @"UPDATE Medicamento SET Nombre=@name, Prescripcion=@reqPresc, Precio=@price, Sucursal_Origen=@originOffice, "+
                                       "CasaFarmaceutica=@house, CantidadDisponible=@stock, CantidadVentas=@numberSold)";
                var parameterNames = new string[] { "@name", "@reqPresc", "@price", "@originOffice", "@house",
                                                    "@stock", "@numberSold"};
                for (var i = 0; i < medicineProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = medicineProps[i];
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@medicineId";
                idParameter.Value = medicine.MedicineId;
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Medicamento WHERE ID_Medicamento= @medicineId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@medicineId";
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        protected override void Map(IDataRecord record, Medicine medicine)
        {
            medicine.MedicineId = (Guid) record["ID_Medicamento"];
            medicine.Name = (string) record["Nombre"];
            medicine.RequiresPrescription = (bool) record["Prescripcion"];
            medicine.Price = (int) record["Precio"];
            medicine.OriginOffice = (int) record["Sucursal_Origen"];
            medicine.House = (string) record["CasaFarmaceutica"];
            medicine.Stock = (int) record["CantidadDisponible"];
            medicine.NumberSold = (int) record["CantidadVentas"];
        }
    }
}