namespace Dao
{
    public interface IJobBoardServiceImpl
    {
        void InsertJobListing(model.JobListing job);
        List<model.JobListing> GetJobListings();
        List<model.JobListing> GetJobListingsBySalaryRange(decimal minSalary, decimal maxSalary);

        void InsertCompany(model.Company company);
        List<model.Company> GetCompanies();
        void InsertApplicant(model.Applicant applicant);
        List<model.Applicant> GetApplicants();
        void InsertJobApplication(model.JobApplication application);
        List<model.JobApplication> GetApplicationsForJob(int jobID);
        List<model.Applicant> GetApplicantsForJob(int jobID);

        void InitializeDatabase();
        bool ApplyForJob(int applicantID, int jobID, string coverLetter);
        bool PostJob(int companyID, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType);
    }
}
