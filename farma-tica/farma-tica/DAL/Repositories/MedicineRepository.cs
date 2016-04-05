using System;
using System.Collections.Generic;
using System.Data;
using farma_tica.DAL.Models;

namespace farma_tica.DAL.Repositories
{
    /// <summary>
<<<<<<< HEAD
    ///     Medicine Repository
=======
    /// Medicine Repository
>>>>>>> Manolo-Work
    /// </summary>
    public class MedicineRepository : Repository<Medicine>
    {
        public MedicineRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
<<<<<<< HEAD
        ///     Get all existing medicines
=======
        /// Get all existing medicines
>>>>>>> Manolo-Work
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


<<<<<<< HEAD
        /// <summary>
        ///     Creates a new medicine
=======

        /// <summary>
        /// Creates a new medicine 
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="medicine"></param>
        public override void Create(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
                {medicine.MedicineId.ToString(), medicine.Name, medicine.RequiresPrescription};
                command.CommandText = @"INSERT INTO Medicamento VALUES(@medicineId, @name, @reqPresc)";
<<<<<<< HEAD
                var parameterNames = new[] {"@medicineId", "@name", "@reqPresc"};
=======
                var parameterNames = new string[] {"@medicineId", "@name", "@reqPresc"};
>>>>>>> Manolo-Work
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
<<<<<<< HEAD
        ///     Creates a new medicine in the given branch office
=======
        /// Creates a new medicine in the given branch office
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="medicine"></param>
        /// <param name="branchOfficeId"></param>
        public void CreateMedicineInBranchOffice(Medicine medicine, Guid branchOfficeId)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
<<<<<<< HEAD
                {
                    branchOfficeId.ToString(), medicine.MedicineId.ToString(), medicine.Stock, medicine.AmmountSold,
                    medicine.Price
                };
                command.CommandText =
                    @"INSERT INTO Medicamentos_Por_Sucursal VALUES(@branchOfficeId,@medicineId, @stock, @ammountSold, @price)";
                var parameterNames = new[] {"@branchOfficeId", "@medicineId", "@stock", "@ammountSold", "@price"};
=======
                {branchOfficeId.ToString(),medicine.MedicineId.ToString(),medicine.Stock,medicine.AmmountSold,medicine.Price};
                command.CommandText = @"INSERT INTO Medicamentos_Por_Sucursal VALUES(@branchOfficeId,@medicineId, @stock, @ammountSold, @price)";
                var parameterNames = new string[] { "@branchOfficeId", "@medicineId", "@stock", "@ammountSold", "@price" };
>>>>>>> Manolo-Work
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
<<<<<<< HEAD
        ///     Updates an existing medicine in the given branch office
=======
        /// Updates an existing medicine in the given branch office
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="medicine"></param>
        /// <param name="branchOfficeId"></param>
        public void UpdateMedicineInBranchOffice(Medicine medicine, Guid branchOfficeId)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
<<<<<<< HEAD
                {
                    medicine.Stock, medicine.AmmountSold, medicine.Price, medicine.MedicineId.ToString(),
                    branchOfficeId.ToString()
                };
                command.CommandText =
                    @"UPDATE Medicamentos_Por_Sucursal SET  CantidadDisponible=@stock, CantidadVentas= @ammountSold, Precio= @price WHERE ID_Medicamento=@medicineId AND ID_Sucursal=@branchOfficeId";
                var parameterNames = new[] {"@stock", "@ammountSold", "@price", "@medicineId", "@branchOfficeId"};
=======
                {medicine.Stock,medicine.AmmountSold,medicine.Price,medicine.MedicineId.ToString(),branchOfficeId.ToString()};
                command.CommandText = @"UPDATE Medicamentos_Por_Sucursal SET  CantidadDisponible=@stock, CantidadVentas= @ammountSold, Precio= @price WHERE ID_Medicamento=@medicineId AND ID_Sucursal=@branchOfficeId";
                var parameterNames = new string[] {  "@stock", "@ammountSold", "@price","@medicineId","@branchOfficeId" };
>>>>>>> Manolo-Work
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
<<<<<<< HEAD
        ///     Deletes an existing medicine in the given branch office
=======
        /// Deletes an existing medicine in the given branch office
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="medicineId"></param>
        /// <param name="branchOfficeId"></param>
        public void DeleteMedicineFromBranchOffice(Guid medicineId, Guid branchOfficeId)
        {
            using (var command = Context.CreateDbCommand())
            {
<<<<<<< HEAD
                command.CommandText =
                    @"DELETE FROM Medicamentos_Por_Sucursal WHERE ID_Medicamento= @medicineId AND ID_Sucursal=@branchOfficeId";
=======
                command.CommandText = @"DELETE FROM Medicamentos_Por_Sucursal WHERE ID_Medicamento= @medicineId AND ID_Sucursal=@branchOfficeId";
>>>>>>> Manolo-Work
                var newParameter1 = command.CreateParameter();
                newParameter1.ParameterName = "@medicineId";
                newParameter1.Value = medicineId.ToString();
                command.Parameters.Add(newParameter1);
                var newParameter2 = command.CreateParameter();
                newParameter2.ParameterName = "@branchOfficeId";
                newParameter2.Value = branchOfficeId.ToString();
                command.Parameters.Add(newParameter2);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
<<<<<<< HEAD
        ///     Gets an existing medicine
=======
        /// Gets an existing medicine 
>>>>>>> Manolo-Work
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
<<<<<<< HEAD
        ///     Gets all medicines for the given branch office
=======
        /// Get Medicine List for the given name (should be only 1 item)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Medicine> GetByName(String name)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamento WHERE Nombre = @name";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@name";
                newParameter.Value = name;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Gets all medicines for the given branch office
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="branchOfficeId"></param>
        /// <returns></returns>
        public IEnumerable<Medicine> GetAllByBranchOffice(Guid branchOfficeId)
        {
            using (var command = Context.CreateDbCommand())
            {
<<<<<<< HEAD
                command.CommandText =
                    @"SELECT M.ID_Medicamento,M.Nombre,M.Prescripcion,MPS.CantidadDisponible,MPS.CantidadVentas,MPS.Precio FROM " +
                    "Medicamento AS M JOIN Medicamentos_Por_Sucursal AS MPS ON M.ID_Medicamento= MPS.ID_Medicamento WHERE ID_Sucursal = @branchOfficeId";
=======
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,M.Prescripcion,MPS.CantidadDisponible,MPS.CantidadVentas,MPS.Precio FROM "+
                                       "Medicamento AS M JOIN Medicamentos_Por_Sucursal AS MPS ON M.ID_Medicamento= MPS.ID_Medicamento WHERE ID_Sucursal = @branchOfficeId";
>>>>>>> Manolo-Work
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@branchOfficeId";
                newParameter.Value = branchOfficeId.ToString();
                command.Parameters.Add(newParameter);
                var result = ToCustomMedicineList(command);
                return result;
            }
        }

        /// <summary>
<<<<<<< HEAD
        ///     Get a list of the total most sold medicines by company
=======
        /// Get a list of the total most sold medicines by company
>>>>>>> Manolo-Work
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Medicine> GetTotalMostSoldByCompany(string company)
        {
            using (var command = Context.CreateDbCommand())
            {
<<<<<<< HEAD
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,SUM(MPS.CantidadVentas) AS CantidadVentas " +
                                      "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal " +
                                      "WHERE S.Compañia = @company GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC";
=======
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,SUM(MPS.CantidadVentas) AS CantidadVentas "+
                                       "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal "+
                                       "WHERE S.Compañia = @company GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC";
>>>>>>> Manolo-Work
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@company";
                newParameter.Value = company;
                command.Parameters.Add(newParameter);
                var result = ToStatisticMedicineList(command);
                return result;
            }
        }

        /// <summary>
<<<<<<< HEAD
        ///     Gets total most sold by both companies
=======
        /// Gets total most sold by both companies
>>>>>>> Manolo-Work
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Medicine> GetTotalMostSold()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,SUM(MPS.CantidadVentas) AS CantidadVentas " +
<<<<<<< HEAD
                                      "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal " +
                                      "GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC";
=======
                                       "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal " +
                                       "GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC";
>>>>>>> Manolo-Work
                var result = ToStatisticMedicineList(command);
                return result;
            }
        }

        /// <summary>
<<<<<<< HEAD
        ///     Get a list of the online most sold medicines for the given company
=======
        /// Get a list of the online most sold medicines for the given company
>>>>>>> Manolo-Work
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Medicine> GetOnlineMostSoldByCompany(string company)
        {
            using (var command = Context.CreateDbCommand())
            {
<<<<<<< HEAD
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,SUM(MPS.CantidadVentas) AS CantidadVentas " +
                                      "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal " +
                                      "JOIN Medicamentos_Por_Pedido MPP ON M.ID_Medicamento=MPP.ID_Medicamento JOIN Pedido P ON P.NumeroPedido=MPP.NumeroPedido " +
                                      "WHERE p.Tipo_Pedido=1 AND S.Compañia=@company" +
                                      "GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC;";
=======
                command.CommandText = @"SELECT M.ID_Medicamento,M.Nombre,SUM(MPS.CantidadVentas) AS CantidadVentas "+
                                       "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal "+
                                       "JOIN Medicamentos_Por_Pedido MPP ON M.ID_Medicamento=MPP.ID_Medicamento JOIN Pedido P ON P.NumeroPedido=MPP.NumeroPedido "+
                                       "WHERE p.Tipo_Pedido=1 AND S.Compañia=@company" +
                                       "GROUP BY M.ID_Medicamento,M.Nombre ORDER BY CantidadVentas DESC;";
>>>>>>> Manolo-Work
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@company";
                newParameter.Value = company;
                command.Parameters.Add(newParameter);
                var result = ToStatisticMedicineList(command);
                return result;
            }
        }

        /// <summary>
<<<<<<< HEAD
        ///     Get a list of the total most sold medicines for the given company
=======
        /// Get a list of the total most sold medicines for the given company
>>>>>>> Manolo-Work
        /// </summary>
        /// <returns></returns>
        public int GetAmmountSoldByCompany(string company)
        {
<<<<<<< HEAD
            var ammount = 0;
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT SUM(MPS.CantidadVentas) AS Ventas " +
                                      "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento " +
                                      "JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal WHERE S.Compañia=@company";
=======
            int ammount = 0;
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT SUM(MPS.CantidadVentas) AS Ventas "+
                                       "FROM Medicamento M  JOIN Medicamentos_Por_Sucursal MPS ON M.ID_Medicamento= MPS.ID_Medicamento "+
                                       "JOIN Sucursal S ON MPS.ID_Sucursal=S.ID_Sucursal WHERE S.Compañia=@company";
>>>>>>> Manolo-Work
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@company";
                newParameter.Value = company;
                command.Parameters.Add(newParameter);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = reader["Ventas"];
<<<<<<< HEAD
                        ammount = result == DBNull.Value ? 0 : (int) result;
=======
                        ammount = result == DBNull.Value ? 0 : (int)result;
>>>>>>> Manolo-Work
                    }
                }
            }
            return ammount;
        }

        /// <summary>
<<<<<<< HEAD
        ///     Updates an existing medicine
=======
        /// Updates an existing medicine
>>>>>>> Manolo-Work
        /// </summary>
        /// <param name="medicine"></param>
        public override void Update(Medicine medicine)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medicineProps = new object[]
<<<<<<< HEAD
                {medicine.Name, medicine.RequiresPrescription};
                command.CommandText =
                    @"UPDATE Medicamento SET Nombre=@name, Prescripcion=@reqPresc WHERE ID_Medicamento=@medicineId";
                var parameterNames = new[] {"@name", "@reqPresc"};
=======
                 {medicine.Name, medicine.RequiresPrescription};
                command.CommandText = @"UPDATE Medicamento SET Nombre=@name, Prescripcion=@reqPresc WHERE ID_Medicamento=@medicineId";
                var parameterNames = new string[] { "@name", "@reqPresc"};
>>>>>>> Manolo-Work
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
<<<<<<< HEAD
        ///     Deletes an existing medicine
=======
        /// Deletes an existing medicine
>>>>>>> Manolo-Work
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

        protected IEnumerable<Medicine> ToCustomMedicineList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
<<<<<<< HEAD
                var itemList = new List<Medicine>();
=======
                List<Medicine> itemList = new List<Medicine>();
>>>>>>> Manolo-Work
                while (reader.Read())
                {
                    var item = new Medicine();
                    MapCustomMedicine(reader, item);
                    itemList.Add(item);
                }
                return itemList;
            }
        }

        protected IEnumerable<Medicine> ToStatisticMedicineList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
<<<<<<< HEAD
                var itemList = new List<Medicine>();
=======
                List<Medicine> itemList = new List<Medicine>();
>>>>>>> Manolo-Work
                while (reader.Read())
                {
                    var item = new Medicine();
                    MapStatisticMedicine(reader, item);
                    itemList.Add(item);
                }
                return itemList;
            }
        }

        protected override void Map(IDataRecord record, Medicine medicine)
        {
<<<<<<< HEAD
            medicine.MedicineId = (Guid) record["ID_Medicamento"];
            medicine.Name = (string) record["Nombre"];
            medicine.RequiresPrescription = (bool) record["Prescripcion"];
        }

        protected void MapCustomMedicine(IDataRecord record, Medicine medicine)
        {
            medicine.MedicineId = (Guid) record["ID_Medicamento"];
            medicine.Name = (string) record["Nombre"];
            medicine.RequiresPrescription = (bool) record["Prescripcion"];
=======
            medicine.MedicineId = (Guid)record["ID_Medicamento"];
            medicine.Name = (string)record["Nombre"];
            medicine.RequiresPrescription = (bool)record["Prescripcion"];
        }

        protected  void MapCustomMedicine(IDataRecord record, Medicine medicine)
        {
            medicine.MedicineId = (Guid)record["ID_Medicamento"];
            medicine.Name = (string)record["Nombre"];
            medicine.RequiresPrescription = (bool)record["Prescripcion"];
>>>>>>> Manolo-Work
            medicine.Price = (decimal) record["Precio"];
            medicine.AmmountSold = (int) record["CantidadVentas"];
            medicine.Stock = (int) record["CantidadDisponible"];
        }

        protected void MapStatisticMedicine(IDataRecord record, Medicine medicine)
        {
<<<<<<< HEAD
            medicine.MedicineId = (Guid) record["ID_Medicamento"];
            medicine.Name = (string) record["Nombre"];
            medicine.AmmountSold = (int) record["CantidadVentas"];
=======
            medicine.MedicineId = (Guid)record["ID_Medicamento"];
            medicine.Name = (string)record["Nombre"];
            medicine.AmmountSold = (int)record["CantidadVentas"];
>>>>>>> Manolo-Work
        }
    }
}