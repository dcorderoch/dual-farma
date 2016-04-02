using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.BLL
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
            catch (Exception e)
            {
                return null;
            }
            return branchOfficeList;
        }
    }
}