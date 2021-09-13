using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailRentingApp.Interfaces
{
    public interface IDescriptiveGenerator
    {
        /// <summary>
        /// The generator method, which MUST insert new objects right in the given database context. The inserted object count is exactly as given in the parameter.
        /// </summary>
        /// <param name="count">How many objects need to be generated.</param>
        /// <param name="db">The database context.</param>
        void Generate(int count, DbContext db);
    }
}
