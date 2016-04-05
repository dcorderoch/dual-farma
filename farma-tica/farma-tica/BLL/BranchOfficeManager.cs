using System;
using System.Collections.Generic;
using farma_tica.DAL;
using farma_tica.DAL.Models;
using farma_tica.DAL.Repositories;

namespace farma_tica.BLL
{
    public class BranchOfficeManager
    {
        private readonly DbContext context;

        /// <summary>
        ///     Class members which allow connecting to the Database.
        /// </summary>
        private readonly DbConnectionFactory factory;

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