using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using dual_farma.DAL.Models;

namespace dual_farma.DAL.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>
    {
        public PrescriptionRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Returns all existing prescriptions
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Prescription> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Receta";
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// creates a new prescription
        /// </summary>
        /// <param name="prescription"></param>
        public override void Create(Prescription prescription)
        {
            using (var command = Context.CreateDbCommand())
            {
                var prescriotionProps = new object[]
                {prescription.PrescriptionID.ToString(), prescription.Doctor,ConvertImageToByteArray(prescription.Image)};
                command.CommandText = @"INSERT INTO Receta VALUES(@prescriptionId, @doctor, @image)";
                var parameterNames = new string[] { "@prescriptionId", "@doctor", "@image" };
                for (var i = 0; i < prescriotionProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = prescriotionProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds a medicine into prescription
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <param name="medicineId"></param>
        public void AddMedicineIntoPrescription(Guid prescriptionId, Guid medicineId)
        {
            using (var command = Context.CreateDbCommand())
            {
                var medPresProps = new object[]
                {prescriptionId,medicineId};
                command.CommandText = @"INSERT INTO Medicamentos_Por_Receta VALUES(@prescriptionId, @medicineId)";
                var parameterNames = new string[] { "@prescriptionId", "@medicineId" };
                for (var i = 0; i < medPresProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = medPresProps[i];
                    command.Parameters.Add(newParameter);
                }
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieve an existing prescription for the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override IEnumerable<Prescription> GetById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Receta WHERE NumeroReceta = @prescriptionId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@prescriptionId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                var result = ToList(command);
                return result;
            }
        }

        /// <summary>
        /// Updates an existing prescription
        /// </summary>
        /// <param name="prescription"></param>
        public override void Update(Prescription prescription)
        {
            using (var command = Context.CreateDbCommand())
            {
                var prescriptionProps = new object[]
               {prescription.Doctor, ConvertImageToByteArray(prescription.Image)};
                command.CommandText = @"UPDATE  Receta SET  Doctor=@doctor, Imagen=@image";
                var parameterNames = new string[] { "@doctor", "@image" };
                for (var i = 0; i < prescriptionProps.Length; i++)
                {
                    var newParameter = command.CreateParameter();
                    newParameter.ParameterName = parameterNames[i];
                    newParameter.Value = prescriptionProps[i];
                    command.Parameters.Add(newParameter);
                }
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@prescriptionId";
                idParameter.Value = prescription.PrescriptionID.ToString();
                command.Parameters.Add(idParameter);
                command.ExecuteNonQuery();
            }
        }

        public override void DeleteById(object id)
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"DELETE FROM Receta WHERE NumeroReceta= @prescriptionId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@prescriptionId";
                newParameter.Value = id.ToString();
                command.Parameters.Add(newParameter);
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Gets a list of medicines for the given prescription
        /// </summary>
        /// <param name="pres"></param>
        /// <returns></returns>
        public IEnumerable<Medicine> GetPrescriptionMedicines(Prescription pres)
        {
            using (var command = Context.CreateDbCommand())
            {
                var prescriptId = pres.PrescriptionID;
                command.CommandText = @"SELECT *  FROM Receta JOIN Medicamentos_Por_Receta WHERE NumeroReceta = @prescriptionId";
                var newParameter = command.CreateParameter();
                newParameter.ParameterName = "@prescriptionId";
                newParameter.Value = prescriptId.ToString();
                command.Parameters.Add(newParameter);
                var result = ToListMedicines(command);
                return result;
            }
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

        protected override void Map(IDataRecord record, Prescription entity)
        {
            entity.PrescriptionID = (Guid)record["NumeroReceta"];
            entity.Doctor = (string)record["Doctor"];
            entity.Image = ConvertByteArrayToImage((byte[])record["Imagen"]);
        }

        /// <summary>
        /// Converts a byte array into an Image type
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        protected Image ConvertByteArrayToImage(byte[] array)
        {
            var ms = new MemoryStream(array);
            return Image.FromStream(ms);
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