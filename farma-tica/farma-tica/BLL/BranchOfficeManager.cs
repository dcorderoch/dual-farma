using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;

namespace farma_tica.BLL
{
    public class BranchOfficeManager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;
        private DbContext context;

        public BranchOfficeManager()
        {
            factory = new DbConnectionFactory("local");
            context = new DbContext(factory);
        }

        public List<BranchOffice> GetAll()
        {
            List<BranchOffice> branchOfficeList;
            try
            {
                using (var uow = context.CreateUnitOfWork())
                {
                    var branchOfficeRepo = new BranchOfficeRepository(context);
                    branchOfficeList = branchOfficeRepo.GetAll() as List<BranchOffice>;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return branchOfficeList;
        }
    }
}