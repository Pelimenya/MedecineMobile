using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            Initializedata();
        }

        private List<Medicine> _data;

        private async void Initializedata()
        {
            try
            {
                await GetData();
                _data = Operations.MedicinesList.ToList();

                var itemsWarehouse = _data.Select(x => x.WareHouseName).Distinct().ToList();
                var itemsName = _data.Select(x => x.tradeName).Distinct().ToList();

                itemsWarehouse.Insert(0, "Все");
                itemsName.Insert(0, "Все");

                warehouseFilterPicker.ItemsSource = itemsWarehouse.ToList();
                nameFilterPicker.ItemsSource = itemsName.ToList();
                nameFilterPicker.SelectedIndex = 0;
                warehouseFilterPicker.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка!", e.Message, "OK");
            }
        }

        private async Task GetData()
        {
            try
            {
                await Operations.GetMedicines(tableMedicin);
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка!", e.Message, "OK");
            }
        }

        private async Task SortData(string sortBy)
        {
            try
            {
                var data = _data.ToList();

                switch (sortBy)
                {
                    case "WareHouseName":
                        if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == -1)
                            data = data.OrderBy(x => x.WareHouseName).ToList();
                        else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == -1)
                            data = data.OrderByDescending(x => x.WareHouseName).ToList();
                        else if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == 0)
                            data = data.OrderBy(x => x.WareHouseName).ThenBy(x => x.name).ToList();
                        else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == 0)
                            data = data.OrderByDescending(x => x.WareHouseName).ThenBy(x => x.name).ToList();
                        else if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == 1)
                            data = data.OrderBy(x => x.WareHouseName).ThenByDescending(x => x.name).ToList();
                        else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == 1)
                            data = data.OrderByDescending(x => x.WareHouseName).ThenByDescending(x => x.name).ToList();
                        break;
                    case "Name":
                        if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == -1)
                            data = data.OrderBy(x => x.name).ToList();
                        else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == -1)
                            data = data.OrderByDescending(x => x.name).ToList();
                        else if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == 0)
                            data = data.OrderBy(x => x.name).ThenBy(x => x.WareHouseName).ToList();
                        else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == 0)
                            data = data.OrderByDescending(x => x.name).ThenBy(x => x.WareHouseName).ToList();
                        else if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == 1)
                            data = data.OrderBy(x => x.name).ThenByDescending(x => x.WareHouseName).ToList();
                        else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == 1)
                            data = data.OrderByDescending(x => x.name).ThenByDescending(x => x.WareHouseName).ToList();
                        break;
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка!", ex.Message, "OK");
                await Operations.GetMedicines(tableMedicin);
            }
        }

        private async Task Filter()
        {
            try
            {
                var data = _data;
                if (nameFilterPicker.SelectedIndex > 0)
                {
                    data = data.Where(x => x.tradeName == (string)nameFilterPicker.SelectedItem).ToList();
                }

                if (warehouseFilterPicker.SelectedIndex > 0)
                {
                    data = data.Where(x => x.WareHouseName == (string)warehouseFilterPicker.SelectedItem).ToList();
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка фильтрации", e.Message, "OK");
            }
        }

        private async void WarehouseSortPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await SortData("WareHouseName");
        }

        private async void NameSortPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await SortData("Name");
        }

        private async void FilterPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await Filter();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Initializedata();
        }
    }
}