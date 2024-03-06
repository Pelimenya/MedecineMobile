using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedecinMobile.Models;
using Xamarin.Forms;

namespace MedecinMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            SortBox();
        }

        async void SortBox()
        {
            try
            {
                await Operations.GetMedicines(tableMedicin);
                var data = Operations.MedicinesList.ToList();
                if (warehouseSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderBy(x => x.warehouseId).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderByDescending(x => x.warehouseId).ToList();
                }

                tableMedicin.ItemsSource = data.ToList();

                
                if (nameSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderBy(x => x.name).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderByDescending(x => x.name).ToList();
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ошибка!", ex.Message, "OK");
            }
        }

        private void NameSortPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SortBox();
        }

   
    }
}