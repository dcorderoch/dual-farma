using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using dual_farma.DAL.Models;

namespace dual_farma.DAL.Repositories
{
    /// <summary>
    /// Order repository
    /// </summary>
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all existing orders
        /// </summary>
        /// <returns>List of orders</returns>
        public override IEnumerable<Order> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Pedido";
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Get all orders for the given branchoffice
        /// </summary>
        /// <param name="branchOfficeId"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrdersByBranchOffice(Guid branchOfficeId)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Pedido WHERE Sucursal_Recojo=@branchOfficeId ORDER BY FechaRecojo";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@branchOfficeId";
                newParameter.Value = branchOfficeId.ToString();
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// insert given orden object into repository
        /// </summary>
        /// <param name="newOrder"></param>
        public override void Create(Order newOrder)
        {
            using (var command = Context.CreateDbCommand())
            {
                var orderProps = new object[]
                {newOrder.OrderId.ToString(), newOrder.ClientId, newOrder.PrescriptionId, newOrder.PickUpOffice, ConvertImageToByteArray(newOrder.InvoiceImage),
                 newOrder.HasPrescription, newOrder.State, newOrder.Priority, newOrder.PrefPhoneNum, newOrder.PickUpdDate, newOrder.Type};
                command.CommandText = @"INSERT INTO Pedido VALUES(@orderId, @clientId, @prescriptionId, @pickUpOffice, @invoiceImage, " +
                                                                 "@hasPrescription, @state, @priority, @prefPhoneNum, @pickUpDate, @type)";
                var parameterNames = new string[] { "@orderId", "@clientId", "@prescriptionId", "@pickUpOffice", "@invoiceImage",
                                                    "@hasPrescription", "@state", "@priority", "@prefPhoneNum", "@pickUpDate", "@type"};
                for (var i = 0; i < orderProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    //Null handler
                    newParameter.Value = orderProps[i] ?? DBNull.Value;
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a new medicine into an order
        /// </summary>
        /// <param name="medicineId"></param>
        /// <param name="orderId"></param>
        public void AddMedicineinToOrder(Guid medicineId, Guid orderId)
        {
            using (var command = Context.CreateDbCommand())
            {
                var mediOrderProps = new object[] { orderId, medicineId };
                command.CommandText = @"INSERT INTO Medicamentos_Por_Pedido VALUES(@orderId, @medicineId)";
                var parameterNames = new string[] { "@orderId", "@medicineId" };
                for (var i = 0; i < mediOrderProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = mediOrderProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets order for the given id
        /// </summary>
        /// <param name="id">GUID identifier</param>
        /// <returns></returns>
        public override IEnumerable<Order> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Pedido WHERE NumeroPedido = @orderId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@orderId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        public IEnumerable<Medicine> GetOrderMedicines(Guid orderId)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Medicamentos_Por_Pedido JOIN Medicamento WHERE NumeroPedido = @orderId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@orderId";
                newParameter.Value = orderId.ToString();
                command.Parameters.Add(newParameter);
                var result = ToListMedicines(command);
                return result;
            }
        }

        /// <summary>
        /// updates an existing order
        /// </summary>
        /// <param name="order"></param>
        public override void Update(Order order)
        {
            using (var command = Context.CreateDbCommand())
            {
                var orderProps = new object[]
               { order.ClientId, order.PrescriptionId, order.PickUpOffice, ConvertImageToByteArray(order.InvoiceImage),
                 order.HasPrescription, order.State, order.Priority, order.PrefPhoneNum, order.PickUpdDate, order.Type};
                command.CommandText = @"UPDATE  Pedido SET  ID_Cliente=@clientId, ID_Receta=@prescriptionId, " +
                                       "Sucursal_Recojo=@pickUpOffice,  ImagenFactura=@invoiceImage, Prescripcion=@hasPrescription, Estado=@state, " +
                                       "Prioridad=@priority, TelefonoPreferido=@prefPhoneNum,FechaRecojo= @pickUpDate, Tipo_Pedido=@type WHERE NumeroPedido=@orderId";
                var parameterNames = new string[] { "@clientId", "@prescriptionId", "@pickUpOffice", "@invoiceImage",
                                                    "@hasPrescription", "@state", "@priority", "@prefPhoneNum", "@pickUpDate","@type"};
                for (var i = 0; i < orderProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    //Null handler
                    newParameter.Value = orderProps[i] ?? DBNull.Value;
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@orderId";
                idParameter.Value = order.OrderId.ToString();
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes an object by its id
        /// </summary>
        /// <param name="id"></param>
        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Pedido WHERE NumeroPedido= @orderId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@orderId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteOrderMedicinesByOrderId(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Medicamentos_Por_Pedido WHERE NumeroPedido= @orderId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@orderId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Maps a result into an object
        /// </summary>
        /// <param name="record"></param>
        /// <param name="order"></param>
        protected override void Map(IDataRecord record, Order order)
        {
            order.OrderId = (Guid)record["NumeroPedido"];
            order.ClientId = (string)record["ID_Cliente"];
            var myGuid = record["ID_Receta"];
            order.PrescriptionId = myGuid == DBNull.Value ? (Guid?)null : Guid.Parse((string)myGuid);
            order.PickUpOffice = (Guid)record["Sucursal_Recojo"];
            order.HasPrescription = (bool)record["Prescripcion"];
            order.State = (int)record["Estado"];
            order.Priority = (string)record["Prioridad"];
            order.PrefPhoneNum = (string)record["TelefonoPreferido"];
            order.PickUpdDate = (DateTime)record["FechaRecojo"];
            order.InvoiceImage = ConvertByteArrayToImage((byte[])record["ImagenFactura"]);
            order.Type = (bool) record["Tipo_Pedido"];
        }

        /// <summary>
        /// Method to create a list o medicines from the result of the query
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected IEnumerable<Medicine> ToListMedicines(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var itemList = new List<Medicine>();
                while (reader.Read())
                {
                    var item = new Medicine
                    {
                        MedicineId = (Guid)reader["ID_Medicamento"],
                        Name = (string)reader["Nombre"],
                        RequiresPrescription = (bool)reader["Prescripcion"]
                    };
                    itemList.Add(item);
                }
                return itemList;
            }
        }

        /// <summary>
        /// Converts a byte array into an Image type
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected Image ConvertByteArrayToImage(byte[] array)
        {
            var ms = new MemoryStream(array);
            Image res = array.Length == 0 ? null : res = Image.FromStream(ms);
            return res;
        }

        /// <summary>
        /// Converts an image to byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        protected byte[] ConvertImageToByteArray(Image img)
        {
            ImageConverter imgconv = new ImageConverter();
            byte[] xByte = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return xByte;
        }
    }
}