﻿using SrezShend.Moduel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SrezShend.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        public PageMaterials()
        {
            InitializeComponent();

            lbMat.ItemsSource = DB.db.Material.ToList();

            cbFilter.Items.Add("Фильтрация");
            foreach (var matT in DB.db.MaterialType)
            {
                cbFilter.Items.Add(matT.Title);
            }
            cbFilter.SelectedIndex = 0;

            cbSort.Items.Add("Сортировка");
            cbSort.Items.Add("По Возрастанию");
            cbSort.Items.Add("По Убыванию");
            cbSort.SelectedIndex = 0;
        }

        public void FindMat()
        {
            var mats = DB.db.Material.Where(x => x.Title.StartsWith(tbFind.Text)).ToList();

            switch (cbSort.SelectedIndex)
            {
                case 0:; break;
                case 1: mats = mats.OrderBy(x => x.Title).ToList(); break;
                case 2: mats = mats.OrderByDescending(x => x.Title).ToList(); break;
            }

            if (cbFilter.SelectedIndex > 0)
            {
                string matType = cbFilter.SelectedItem.ToString();
                mats = mats.Where(x => x.MaterialType.Title == matType).ToList();
            }
            lbMat.ItemsSource = mats;
        }


        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindMat();
        }

        private void tbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindMat();
        }


        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindMat();
        }

        private void AddMat_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new PageAddMat(new Material()));
        }

        private void DelMat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var matSelect = lbMat.SelectedItem;
                if (MessageBox.Show("Удалить объект?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (matSelect != null)
                    {
                        DB.db.Material.Remove((Material)matSelect);
                        DB.db.SaveChanges();
                        lbMat.ItemsSource = DB.db.Material.ToList();
                        MessageBox.Show("Объект удален");
                    }
                    else
                    {
                        MessageBox.Show("У вас не выбран объект удаления", "Предупреждение");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void EditMat_Click(object sender, RoutedEventArgs e)
        {
            var matSelect = lbMat.SelectedItem;
            if (matSelect != null)
            {
                FrameObj.frameMain.Navigate(new PageAddMat((Material)matSelect));
            }
            else
            {
                MessageBox.Show("Не выбран объект для изменения", "Предупреждение");
            }
        }
    }
}
