using System;
using System.Collections.Generic;
using System.Data;
using farma_tica.DAL.Models;

namespace farma_tica.DAL.Repositories
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

        /// <summary>
        /// Creates a new medicine 
        /// </summary>
        /// <param name="medicine"></param>
        public override void Create(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
                {medicine.MedicineId.ToString(), medicine.Name, medicine.RequiresPrescription, medicine.Price, medicine.OriginOffice, medicine.House,
                 medicine.Stock, medicine.NumberSold};
                command.CommandText = @"INSERT INTO Medicamento VALUES(@medicineId, @name, @reqPresc, @price, @originOffice, @house, " +
                                                                      "@stock, @numberSold)";
                var parameterNames = new string[] {"@medicineId", "@name", "@reqPresc", "@price", "@originOffice", "@house",
                                                    "@stock", "@numberSold"};
                for (var i = 0; i < medicineProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = medicineProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets an existing medicine 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override IEnumerable<Medicine> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento WHERE ID_Medicamento = @medicineId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@medicineId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Get a list of the total most sold medicines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Medicine> GetTotalMostSold()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento ORDER BY CantidadVentas DESC";
                var newParameter = command.CreateParameter();
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Get a list of the total most sold medicines for the given company
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Medicine> GetTotalMostSoldByCompany(string company)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento WHERE CasaFarmaceutica=@casaFarma ORDER BY CantidadVentas DESC";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@casaFarma";
                newParameter.Value = company;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Get a list of the total most sold medicines for the given company
        /// </summary>
        /// <returns></returns>
        public int GetAmmountSoldByCompany(string company)
        {
            int ammount = 0;
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT SUM(CantidadVentas) AS Ventas FROM Medicamento WHERE CasaFarmaceutica=@casaFarma";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@casaFarma";
                newParameter.Value = company;
                command.Parameters.Add(newParameter);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader["Ventas"];
                        ammount = result == DBNull.Value ? 0 : (int)result;

                    }
                }
            }
            return ammount;
        }

        /// <summary>
        /// Updates an existing medicine
        /// </summary>
        /// <param name="medicine"></param>
        public override void Update(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
                 {medicine.Name, medicine.RequiresPrescription, medicine.Price, medicine.OriginOffice, medicine.House,
                 medicine.Stock, medicine.NumberSold};
                command.CommandText = @"UPDATE Medicamento SET Nombre=@name, Prescripcion=@reqPresc, Precio=@price, Sucursal_Origen=@originOffice, " +
                                       "CasaFarmaceutica=@house, CantidadDisponible=@stock, CantidadVentas=@numberSold WHERE ID_Medicamento=@medicineId";
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
                idParameter.Value = medicine.MedicineId.ToString();
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes an existing medicine
        /// </summary>
        /// <param name="id"></param>
        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Medicamento WHERE ID_Medicamento= @medicineId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@medicineId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        protected override void Map(IDataRecord record, Medicine medicine)
        {
            medicine.MedicineId = (Guid)record["ID_Medicamento"];
            medicine.Name = (string)record["Nombre"];
            medicine.RequiresPrescription = (bool)record["Prescripcion"];
            medicine.Price = (decimal)record["Precio"];
            medicine.OriginOffice = (int)record["Sucursal_Origen"];
            medicine.House = (string)record["CasaFarmaceutica"];
            medicine.Stock = (int)record["CantidadDisponible"];
            medicine.NumberSold = (int)record["CantidadVentas"];
        }
    }
}