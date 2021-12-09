using Microsoft.Win32;
using SrezShend.Moduel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SrezShend.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddMat.xaml
    /// </summary>
    public partial class PageAddMat : Page
    {
        Material mat;
        public PageAddMat(Material mat)
        {
            InitializeComponent();

            this.mat = mat;

            cbType.ItemsSource = DB.db.MaterialType.ToList();

            if (mat != null)
            {
                CheckMat();
            }
            else
            {
                MessageBox.Show("Выберите Данные");
            }
        }
        public void CheckMat()
        {
            tbTitle.Text = mat.Title;
            tbCountInPack.Text = mat.CountInPack.ToString();
            tbUnit.Text = mat.Unit;
            tbCountInStock.Text = mat.CountInStock.ToString();
            tbMinCount.Text = mat.MinCount.ToString();
            tbCost.Text = mat.Cost.ToString();

            if (mat.MaterialType == null)
            {
                cbType.SelectedIndex = 0;
            }
            else
            {
                cbType.SelectedItem = mat.MaterialType;
            }

            tbImage.Text = mat.Image;
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберите изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                tbImage.Text = op.FileName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text) || string.IsNullOrWhiteSpace(tbCountInPack.Text) ||
                string.IsNullOrWhiteSpace(tbCountInStock.Text) || string.IsNullOrWhiteSpace(tbMinCount.Text) ||
                string.IsNullOrWhiteSpace(tbCost.Text) ||
                cbType.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {

                mat.Title = tbTitle.Text;
                mat.CountInPack = int.Parse(tbCountInPack.Text);
                mat.Unit = tbUnit.Text;
                mat.CountInStock = int.Parse(tbCountInStock.Text);
                mat.MinCount = int.Parse(tbMinCount.Text);
                mat.Cost = int.Parse(tbCost.Text, System.Globalization.NumberStyles.Any);
                mat.MaterialType = (MaterialType)cbType.SelectedItem;
                mat.Image = tbImage.Text;
                if (mat.ID == 0)
                {
                    DB.db.Material.Add(mat);
                }
                DB.db.SaveChanges();

                FrameObj.frameMain.Navigate(new PageMaterials());
            }

        }

        private void DelMat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удалить объект?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DB.db.Material.Remove(mat);
                    DB.db.SaveChanges();
                    
                    MessageBox.Show("Объект удален");

                    FrameObj.frameMain.Navigate(new PageMaterials());
                }
            }
            catch
            {

            }
        }
    }
}