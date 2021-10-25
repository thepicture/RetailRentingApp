using Microsoft.Win32;
using RetailRentingApp.Backend;
using RetailRentingApp.Classes;
using System;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
            CheckIfNameIsValid(errorsBuilder);
            CheckifFloorIsValid(errorsBuilder);
            CheckIfPriceIsIncorrect(errorsBuilder);
            CheckIfFloorBoxValueIsIncorrect(errorsBuilder);

            if (HasAnyErrors(errorsBuilder))
            {
                SimpleMessager.ShowError(errorsBuilder.ToString());
                return;
            }

            AddAreaToDbContext();
            TryToSaveChanges();
        }

        private void TryToSaveChanges()
        {
            try
            {
                SaveChangesAndGoBack();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private static void ShowError(Exception ex)
        {
            SimpleMessager.ShowError("При сохранении данных произошла ошибка: " +
                ex.Message + ". Пожалуйста, попробуйте ещё раз.");
        }

        private void SaveChangesAndGoBack()
        {
            _ = AppData.Context.SaveChanges();
            SimpleMessager.ShowInfo("Торговая точка успешно " +
                (_tradingArea.Id == 0 ?
                "добавлена"
                : "изменена") + "!");
            AppData.MainFrame.GoBack();
        }

        private void AddAreaToDbContext()
        {
            if (AreaIsNew())
            {
                _ = AppData.Context.TradingAreas.Add(_tradingArea);
            }
        }

        private bool AreaIsNew()
        {
            return _tradingArea.Id == 0;
        }

        private static bool HasAnyErrors(StringBuilder errorsBuilder)
        {
            return errorsBuilder.Length > 0;
        }

        private void CheckIfFloorBoxValueIsIncorrect(StringBuilder errorsBuilder)
        {
            if (IsFloorBoxValueIncorrect())
            {
                _ = errorsBuilder.AppendLine("Номер этажа - " +
                    "это положительное число не больше двух знаков");
            }
        }

        private bool IsFloorBoxValueIncorrect()
        {
            return string.IsNullOrEmpty(FloorBox.Text) ||
                           !int.TryParse(FloorBox.Text, out _) ||
                           int.Parse(FloorBox.Text) <= 0;
        }

        private void CheckIfPriceIsIncorrect(StringBuilder errorsBuilder)
        {
            if (PriceBoxValueIsIncorrect())
            {
                _ = errorsBuilder.AppendLine("Стоимость торговой точки - " +
                    "это положительное целое число");
            }
        }

        private bool PriceBoxValueIsIncorrect()
        {
            return string.IsNullOrEmpty(PriceBox.Text) ||
                            !int.TryParse(PriceBox.Text, out _) ||
                            int.Parse(PriceBox.Text) <= 0;
        }

        private void CheckifFloorIsValid(StringBuilder errorsBuilder)
        {
            if (string.IsNullOrEmpty(FloorBox.Text))
            {
                _ = errorsBuilder.AppendLine("Введите этаж торговой точки");
            }
        }

        private void CheckIfNameIsValid(StringBuilder errorsBuilder)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                _ = errorsBuilder.AppendLine("Введите наименование торговой точки");
            }
        }

        private void BtnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (IsUserSelectedFile(fileDialog))
            {
                InitImage(fileDialog);
                BitmapImage bitmapImage = GetBitmapImageOf(fileDialog);
                TradingAreaImage.Source = bitmapImage;
            }
        }

        private static BitmapImage GetBitmapImageOf(OpenFileDialog fileDialog)
        {
            BitmapImage bitmapImage = new BitmapImage();
            AssignBitmapUriSource(fileDialog, bitmapImage);
            return bitmapImage;
        }

        private static void AssignBitmapUriSource(OpenFileDialog fileDialog, BitmapImage bitmapImage)
        {
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(fileDialog.FileName);
            bitmapImage.EndInit();
        }

        private void InitImage(OpenFileDialog fileDialog)
        {
            System.Drawing.Image image = Bitmap.FromFile(fileDialog.FileName);
            _tradingArea.Image = (byte[])new ImageConverter()
                .ConvertTo(
                image,
                typeof(byte[]));
        }

        private static bool IsUserSelectedFile(OpenFileDialog fileDialog)
        {
            return (bool)fileDialog.ShowDialog();
        }
    }
}
