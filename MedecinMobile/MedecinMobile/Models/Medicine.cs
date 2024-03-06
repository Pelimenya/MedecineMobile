using System.Collections.Generic;
using System.Linq;

namespace MedecinMobile.Models
{
    public class Medicine
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tradeName { get; set; }
        public string manufacturer { get; set; }
        public string image { get; set; }
        public int price { get; set; }
        public int stockQuantity { get; set; }
        public int warehouseId { get; set; }

        public string WareHouseName
        {
            get
            {
                
                List<Warehouse> warehouseNames = Operations.WarehousesList;
                return warehouseNames.FirstOrDefault(x => x.id == warehouseId)?.name;
            }
        }
    }
}