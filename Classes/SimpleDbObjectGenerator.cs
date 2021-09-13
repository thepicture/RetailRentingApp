using RetailRentingApp.Interfaces;
using System.Data.Entity;

namespace RetailRentingApp.Classes
{
    public class SimpleDbObjectGenerator
    {
        public static void Generate(IDescriptiveGenerator tactic, int count, DbContext db)
        {
            tactic.Generate(count, db);
        }
    }
}
