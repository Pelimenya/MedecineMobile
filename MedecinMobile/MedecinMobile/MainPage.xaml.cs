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
            InitializeGetdata();
        }

        private List<Medicine> _data;

        private async void InitializeGetdata()
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

        private async Task SortBoxWarehouse()
        {
            try
            {
                var data = _data;
                if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == -1)
                {
                    data = data.OrderBy(x => x.WareHouseName).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == -1)
                {
                    data = data.OrderByDescending(x => x.WareHouseName).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderBy(x => x.WareHouseName).ThenBy(x => x.name).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderByDescending(x => x.WareHouseName).ThenBy(x => x.name).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 0 && nameSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderBy(x => x.WareHouseName).ThenByDescending(x => x.name).ToList();
                }
                else if (warehouseSortPicker.SelectedIndex == 1 && nameSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderByDescending(x => x.WareHouseName).ThenByDescending(x => x.name).ToList();
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка!", ex.Message, "OK");
                await Operations.GetMedicines(tableMedicin);
            }
        }

        private async Task SortBoxName()
        {
            try
            {
                var data = _data.ToList();
                if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == -1)
                {
                    data = data.OrderBy(x => x.name).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == -1)
                {
                    data = data.OrderByDescending(x => x.name).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderBy(x => x.name).ThenBy(x => x.WareHouseName).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == 0)
                {
                    data = data.OrderByDescending(x => x.name).ThenBy(x => x.WareHouseName).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 0 && warehouseSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderBy(x => x.name).ThenByDescending(x => x.WareHouseName).ToList();
                }
                else if (nameSortPicker.SelectedIndex == 1 && warehouseSortPicker.SelectedIndex == 1)
                {
                    data = data.OrderByDescending(x => x.name).ThenByDescending(x => x.WareHouseName).ToList();
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка!", ex.Message, "OK");
                await Operations.GetMedicines(tableMedicin);
            }
        }

        private async Task FilterWarehouse()
        {
            try
            {
                var data = _data.ToList();
                if (warehouseFilterPicker.SelectedIndex > 0)
                {
                    data = _data.Where(x =>
                        x.WareHouseName ==
                        _data.FirstOrDefault(y => y.WareHouseName == (string)warehouseFilterPicker.SelectedItem)
                            ?.WareHouseName).ToList();
                }
                else
                {
                    data = _data;
                }

                tableMedicin.ItemsSource = data.ToList();
            }
            catch (Exception e)
            {
                await DisplayAlert("Ошибка фильтрации", e.Message, "OK");
            }
        }

        private async Task FilterName()
        {
            try
            {
                var data = _data.ToList();
                if (nameFilterPicker.SelectedIndex > 0)
                {
                    data = _data.Where(x =>
                        x.tradeName == _data.FirstOrDefault(f => f.tradeName == (string)nameFilterPicker.SelectedItem)
                            ?.tradeName).ToList();
                }
                else
                {
                    data = _data;
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
            await SortBoxWarehouse();
        }

        private async void NameSortPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await SortBoxName();
        }

        private async void NameFilterPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await FilterName();
        }

        private async void WarehouseFilterPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await FilterWarehouse();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
            InitializeGetdata();
            }
            catch (Exception exception)
            {
                DisplayAlert("Ошибка", exception.Message, "OK");
            }
        }
    }
}