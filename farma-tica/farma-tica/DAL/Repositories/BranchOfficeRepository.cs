using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using farma_tica.DAL.Models;

namespace farma_tica.DAL.Repositories
{
    public class BranchOfficeRepository : Repository<BranchOffice>
    {
        public BranchOfficeRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<BranchOffice> GetAll()
        {
            using (var command = Context.CreateDbCommand())
            {
                command.CommandText = @"SELECT * FROM Sucursal";
                var result = ToList(command);
                return result;
            }
        }

        public override void Create(BranchOffice entity)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<BranchOffice> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BranchOffice entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteById(object id)
        {
            throw new NotImplementedException();
        }

        protected override void Map(IDataRecord record, BranchOffice entity)
        {
            entity.BranchOfficeId = (Guid) record["ID_Sucursal"];
            entity.Name = (string) record["Nombre"];
            entity.Phone = (string) record["Telefono"];
            entity.Location = (string) record["Ubicacion"];
            entity.Company = (string) record["Compañia"];
        }
    }
}