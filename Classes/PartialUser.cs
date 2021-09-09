namespace RetailRentingApp.Backend
{
    /// <summary>
    /// Public partial class for User.
    /// </summary>
    public partial class User
    {
        public override string ToString()
        {
            return LastName + " " + MiddleName + " " + FirstName;
        }
    }
}
