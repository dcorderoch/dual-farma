using System;
using System.Collections.Generic;
using System.Data;
using FarmaticaCore.DAL.Models;

namespace FarmaticaCore.DAL.Repositories
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
        /// insert given orden object into repository
        /// </summary>
        /// <param name="newOrder"></param>
        public override void Create(Order newOrder)
        {
            using (var command = Context.CreateDbCommand())
            {
                var orderProps = new object[]
                {newOrder.OrderId, newOrder.ClientId, newOrder.PrescriptionId, newOrder.MedicationId, newOrder.PickUpOffice, newOrder.InvoiceCode,
                 newOrder.HasPrescription, newOrder.State, newOrder.Priority, newOrder.PrefPhoneNum, newOrder.PickUpdDate};
                command.CommandText = @"INSERT INTO Pedido VALUES(@orderId, @clientId, @prescriptionId, @medicationId, @pickUpOffice, @invoiceCode, "+
                                                                 "@hasPrescription, @state, @priority, @prefPhoneNum, @pickUpDate)";
                var parameterNames = new string[] { "@orderId", "@clientId", "@prescriptionId", "@medicationId", "@pickUpOffice", "@invoiceCode",
                                                    "@hasPrescription", "@state", "@priority", "@prefPhoneNum", "@pickUpDate"};
                for (var i = 0; i < orderProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    //Null handler
                    if (orderProps[i] == null)
                    {
                        newParameter.Value = DBNull.Value;
                    }
                    else
                    {
                        newParameter.Value = orderProps[i];
                    }
                    
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
                newParameter.Value = id;
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        public override void Update(Order order)
        {
            using (var command = Context.CreateDbCommand())
            {
                var orderProps = new object[]
               { order.ClientId, order.PrescriptionId, order.MedicationId, order.PickUpOffice, order.InvoiceCode,
                 order.HasPrescription, order.State, order.Priority, order.PrefPhoneNum, order.PickUpdDate};
                command.CommandText = @"UPDATE  Pedido SET  ID_Cliente=@clientId, ID_Receta=@prescriptionId, ID_Medicamento=@medicationId,"+ 
                                       "Sucursal_Recojo=@pickUpOffice,  CodigoFactura=@invoiceCode, Prescripcion=@hasPrescription, Estado=@state, " +
                                       "Prioridad=@priority, TelefonoPreferido=@prefPhoneNum,FechaRecojo= @pickUpDate)";
                var parameterNames = new string[] { "@ordenId", "@clientId", "@prescriptionId", "@medicationId", "@pickUpOffice", "@invoiceCode",
                                                    "@hasPrescription", "@state", "@priority", "@prefPhoneNum", "@pickUpDate"};
                for (var i = 0; i < orderProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = orderProps[i];
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@userId";
                idParameter.Value = order.OrderId;
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
                newParameter.Value = id;
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
            order.OrderId = (Guid) record["NumeroPedido"];
            order.ClientId = (string) record["ID_Cliente"];
            order.PrescriptionId = (int) record["ID_Medicamento"];
            order.MedicationId = (Guid) record["ID_Medication"];
            order.PickUpOffice = (int)record["Sucursal_Recojo"];
            order.InvoiceCode = (Guid)record["CodigoFactura"];
            order.HasPrescription = (bool)record["Prescripcion"];
            order.State = (int)record["Estado"];
            order.Priority = (string)record["Prioridad"];
            order.PrefPhoneNum = (string)record["TelefonoPreferido"];
            order.PickUpdDate = (DateTime)record["FechaRecojo"];
        }
    }
}