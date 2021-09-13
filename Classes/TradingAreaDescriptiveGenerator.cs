using RetailRentingApp.Backend;
using RetailRentingApp.Interfaces;
using System;
using System.Data.Entity;
using System.IO;

namespace RetailRentingApp.Classes
{
    public class TradingAreaDescriptiveGenerator : IDescriptiveGenerator
    {
        private readonly Random random = new Random();
        private readonly string[] namesArrayPart1 = { "Золотая", "Потрясающая", "Идеальная", "Замечательная", "Отличная" };
        private readonly string[] namesArrayPart2 = { "открытая площадка", "область торговли", "площадь", "торговая местность", "площадка" };
        public void Generate(int count, DbContext db)
        {
            string[] images;
            try
            {
                images = Directory.GetFiles(@"D:\Downloads2\dummyImages");
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError(ex.Message);

                return;
            }


            for (int i = 0; i < count; i++)
            {
                TradingArea currentArea = new TradingArea
                {
                    Name = namesArrayPart1[random.Next(0, namesArrayPart1.Length)] + " " + namesArrayPart2[random.Next(0, namesArrayPart2.Length)],
                    AreaInSquareMeters = random.Next(10, 100),
                    CostPerDay = decimal.Parse(Convert.ToString(random.Next(5000, 50000))),
                    Floor = Convert.ToByte(random.Next(1, 10 + 1)),
                    IsAirVenting = Convert.ToBoolean(random.Next(0, 1 + 1)),
                    Image = File.ReadAllBytes(images[random.Next(0, images.Length)])
                };

                ((RetailRentingBaseEntities)db).TradingAreas.Add(currentArea);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                SimpleMessager.ShowError(ex.Message);
            }
        }
    }
}
