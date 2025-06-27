namespace model
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        public JobListing() { }

        public JobListing(int jobID, int companyID, string jobTitle, string jobDescription,string jobLocation, decimal salary, string jobType, DateTime postedDate)
        {
            JobID = jobID;
            CompanyID = companyID;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            JobLocation = jobLocation;
            Salary = salary;
            JobType = jobType;
            PostedDate = postedDate;
        }
    }
}
