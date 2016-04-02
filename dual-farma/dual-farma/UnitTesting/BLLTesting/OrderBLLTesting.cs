using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dual_farma.BLL;

namespace dual_farma.UnitTesting.BLLTesting
{
    public class OrderBLLTesting
    {
        public static void Test()
        {
            /*
            OrderManager orderManager = new OrderManager();
            string[] medicinesToOrderWithPresc =
            {
                "5ca6da9a-07c6-4f10-8f34-0bb2bd8f1de8","248e75af-0336-465c-a85c-1a7e4cfe0dee",
                "c0f93db8-548f-4708-87c0-2d6272cdbb3f","aeddf1eb-033d-49f0-83ed-74c239b03a00"
            };
            string[] medicinesWithPresc =
            {
                "c0f93db8-548f-4708-87c0-2d6272cdbb3f","aeddf1eb-033d-49f0-83ed-74c239b03a00"
            };
            var resultcreateWPresc = orderManager.CreateOrderWithPrescription("403341797", medicinesToOrderWithPresc,
                medicinesWithPresc, null, "ABC005", "75ed37b7-c0d4-4c73-a618-a7b118f1cb50", "22344444",
                DateTime.Today, true);

            string[] medicinestoOrder =
            {
                "248e75af-0336-465c-a85c-1a7e4cfe0dee", "1f4e144e-1c80-4a72-9ff8-1f45837b476d",
                "f81e29c4-d438-4b8d-8901-b6649c561a10", "3d41c098-3153-4cf0-8435-c7ceec4e8c98",
                "e398762d-359e-4ad7-8c3f-d8a8597f74bc", "248e75af-0336-465c-a85c-1a7e4cfe0dee",
                "f81e29c4-d438-4b8d-8901-b6649c561a10"
            };
            
            var resultcreateOrder= orderManager.CreateOrderWithoutPrescription("106720579",medicinestoOrder, "88aac380-a258-40dc-aa77-2df9324e2262","33443344",DateTime.Today, true);


            var ordersFound3 = orderManager.GetAllOrdersByBranchOffice("75ed37b7-c0d4-4c73-a618-a7b118f1cb50");
            var ordersFound1 = orderManager.GetAllOrdersByBranchOffice("88aac380-a258-40dc-aa77-2df9324e2262");
            var ordersFound0 = orderManager.GetAllOrdersByBranchOffice("030d3a63-2dad-4663-b88e-7dfdf487edff");

            int updateResult = orderManager.UpdateOrderStatus("fe910acf-15d2-4266-b63c-88337a3f65a0", Constants.PREPARED_ORDER_STATUS);
            int deleteResult = orderManager.DeleteOrder("dc0c496b-d5a3-4580-a0ff-f5551d108671");*/
        }
    }
}