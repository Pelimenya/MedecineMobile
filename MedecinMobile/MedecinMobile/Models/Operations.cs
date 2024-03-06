using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MedecinMobile.Models
{
    public class Operations
    {
        private const string ClientPath = "http://192.168.1.2:5055/api/Medicines";
        private const string WarehousePath = "http://192.168.1.2:5055/api/Warehouses";
        public static async Task GetMedicines(ListView tableMedicin)
        {
            WebClient client = new WebClient();
            var result = await client.DownloadStringTaskAsync(ClientPath);
            await GetWareHouse();
            var medicines =  JsonConvert.DeserializeObject<List<Medicine>>(result);
            tableMedicin.ItemsSource = medicines.ToList();
            MedicinesList = medicines.ToList();
        }

        public static List<Medicine> MedicinesList;
        public static List<Warehouse> WarehousesList;
        public static async Task GetWareHouse()
        {
            WebClient client = new WebClient();
            var result = await client.DownloadStringTaskAsync(WarehousePath);
            WarehousesList = JsonConvert.DeserializeObject<List<Warehouse>>(result);
            
        }



    }
}