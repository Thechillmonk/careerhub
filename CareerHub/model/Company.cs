namespace model
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public Company() { }
        public Company(int companyID, string companyName, string location)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Location = location;
        }
    }
}
