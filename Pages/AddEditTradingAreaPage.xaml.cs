using Microsoft.Win32;
using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetailRentingApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditTradingAreaPage.xaml
    /// </summary>
    public partial class AddEditTradingAreaPage : Page
    {
        public TradingArea _tradingArea = new TradingArea();
        public AddEditTradingAreaPage(TradingArea tradingArea)
        {
            InitializeComponent();
            SetDataContext(tradingArea);
        }

        private void SetDataContext(TradingArea tradingArea)
        {
            InitTradingArea(tradingArea);
            DataContext = _tradingArea;
        }

        private void InitTradingArea(TradingArea tradingArea)
        {
            if (tradingArea != null)
            {
                _tradingArea = tradingArea;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (SimpleMessager.ShowQuestion($"Точно отменить редактирование " +
                $"торговой точки" +
                $"{(string.IsNullOrEmpty(_tradingArea.Name) ? "" : " " + _tradingArea.Name)}?"))
            {
                AppData.MainFrame.GoBack();
            }
        }

        private void BtnAddOrEditTradingArea_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorsBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(NameBox.Text))
            {
                _ = errorsBuilder.AppendLine("Введите наименование торговой точки");
            }

            if (string.IsNullOrEmpty(FloorBox.Text))
            {
                _ = errorsBuilder.AppendLine("Введите этаж торговой точки");
            }

            if (string.IsNullOrEmpty(PriceBox.Text) ||
                !int.TryParse(PriceBox.Text, out _) ||
                int.Parse(PriceBox.Text) <= 0
                )
            {
                _ = errorsBuilder.AppendLine("Стоимость торговой точки - " +
                    "это положительное целое число");
            }

            if (string.IsNullOrEmpty(FloorBox.Text) ||
               !int.TryParse(FloorBox.Text, out _) ||
               int.Parse(FloorBox.Text) <= 0
               )
            {
                _ = errorsBuilder.AppendLine("Номер этажа - " +
                    "это положительное число не больше двух знаков");
            }

            if (errorsBuilder.Length > 0)
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return;
            }

            if (_tradingArea.Id == 0)
            {
                _ = AppData.Context.TradingAreas.Add(_tradingArea);
            }

            try
            {
                _ = AppData.Context.SaveChanges();
                SimpleMessager.ShowInfo("Торговая точка успешно " +
                    (_tradingArea.Id == 0 ?
                    "добавлена"
                    : "изменена") + "!");
                AppData.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError("При сохранении данных произошла ошибка: " +
                    ex.Message + ". Пожалуйста, попробуйте ещё раз.");
            }
        }

        private void BtnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if ((bool)fileDialog.ShowDialog())
            {
                System.Drawing.Image image = Bitmap.FromFile(fileDialog.FileName);
                _tradingArea.Image = (byte[])new ImageConverter()
                    .ConvertTo(
                    image,
                    typeof(byte[]));

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fileDialog.FileName);
                bitmapImage.EndInit();
                TradingAreaImage.Source = bitmapImage;
            }
        }
    }
}
