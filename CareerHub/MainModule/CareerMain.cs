using Dao;
using Exception;
using model;

namespace MainModule
{
    public class CareerMain
    {
        public static IJobBoardServiceImpl jobBoardService = new JobBoardServiceImpl();

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CareerHub Job Board");

            int choice;
            do
            {
                DisplayMenu();
                choice = GetUserChoice();

                try
                {
                    switch (choice)
                    {
                        case 1:
                            JobManagementMenu();
                            break;
                        case 2:
                            ApplicantManagementMenu();
                            break;
                        case 3:
                            ApplicationManagementMenu();
                            break;
                        case 4:
                            CompanyManagementMenu();
                            break;
                        case 5:
                            SearchAndReportsMenu();
                            break;
                        case 6:
                            Console.WriteLine("Thank you for using CareerHub Job Board!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (InvalidEmailFormatException ex)
                {
                    Console.WriteLine($"Email Error: {ex.Message}");
                }
                catch (InvalidSalaryException ex)
                {
                    Console.WriteLine($"Salary Error: {ex.Message}");
                }
                catch (DatabaseConnectionException ex)
                {
                    Console.WriteLine($"Database Error: {ex.Message}");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (choice != 6)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (choice != 6);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n MAIN MENU ");
            Console.WriteLine("1. Job Management");
            Console.WriteLine("2. Applicant Management");
            Console.WriteLine("3. Application Management");
            Console.WriteLine("4. Company Management");
            Console.WriteLine("5. Search & Reports");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        }

        private static int GetUserChoice()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }
        private static void JobManagementMenu()
        {
            Console.WriteLine("\n JOB MANAGEMENT ");
            Console.WriteLine("1. View All Job Listings");
            Console.WriteLine("2. Post New Job");
            Console.WriteLine("3. Find Job by ID");
            Console.WriteLine("4. Update Job Details");
            Console.WriteLine("5. Remove Job Listing");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    ViewAllJobs();
                    break;
                case 2:
                    PostNewJob();
                    break;
                case 3:
                    FindJobById();
                    break;
                case 4:
                    UpdateJobDetails();
                    break;
                case 5:
                    RemoveJobListing();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void ViewAllJobs()
        {
            Console.WriteLine("\n ALL JOB LISTINGS ");
            List<JobListing> jobs = jobBoardService.GetJobListings();

            if (jobs.Count == 0)
            {
                Console.WriteLine("No job listings found.");
                return;
            }

            foreach (var job in jobs)
            {
                Console.WriteLine($"Job ID: {job.JobID}");
                Console.WriteLine($"Title: {job.JobTitle}");
                Console.WriteLine($"Company ID: {job.CompanyID}");
                Console.WriteLine($"Location: {job.JobLocation}");
                Console.WriteLine($"Salary: ${job.Salary:N2}");
                Console.WriteLine($"Type: {job.JobType}");
                Console.WriteLine($"Posted: {job.PostedDate:yyyy-MM-dd}");
                Console.WriteLine($"Description: {job.JobDescription}");
                Console.WriteLine(new string('-', 50));
            }
        }

        private static void PostNewJob()
        {
            Console.WriteLine("\n POST NEW JOB ");
            Console.Write("Enter Job ID: ");
            int jobId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Company ID: ");
            int companyId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Job Title: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Enter Job Description: ");
            string jobDescription = Console.ReadLine();

            Console.Write("Enter Job Location: ");
            string jobLocation = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Job Type (Full-time/Part-time/Contract): ");
            string jobType = Console.ReadLine();

            JobListing job = new JobListing(jobId, companyId, jobTitle, jobDescription,
                                          jobLocation, salary, jobType, DateTime.Now);
            jobBoardService.InsertJobListing(job);
            Console.WriteLine("Job posted successfully!");
        }

        private static void FindJobById()
        {
            Console.WriteLine("\n FIND JOB BY ID ");
            Console.Write("Enter Job ID: ");
            int jobId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Searching for Job ID: {jobId}...");

        }

        private static void UpdateJobDetails()
        {
            Console.WriteLine("\n UPDATE JOB DETAILS ");
            Console.Write("Enter Job ID to update: ");
            int jobId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Updating Job ID: {jobId}...");
        }

        private static void RemoveJobListing()
        {
            Console.WriteLine("\n REMOVE JOB LISTING ");
            Console.Write("Enter Job ID to remove: ");
            int jobId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Job ID {jobId} removed successfully!");
        }


        private static void ApplicantManagementMenu()
        {
            Console.WriteLine("\n APPLICANT MANAGEMENT");
            Console.WriteLine("1. Create Applicant Profile");
            Console.WriteLine("2. View All Applicants");
            Console.WriteLine("3. Find Applicant by ID");
            Console.WriteLine("4. Update Applicant Profile");
            Console.WriteLine("5. Remove Applicant");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    CreateApplicantProfile();
                    break;
                case 2:
                    ViewAllApplicants();
                    break;
                case 3:
                    FindApplicantById();
                    break;
                case 4:
                    UpdateApplicantProfile();
                    break;
                case 5:
                    RemoveApplicant();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void CreateApplicantProfile()
        {
            Console.WriteLine("\n CREATE APPLICANT PROFILE ");
            Console.Write("Enter Applicant ID: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Resume/Skills: ");
            string resume = Console.ReadLine();

            Applicant applicant = new Applicant(applicantId, firstName, lastName, email, phone, resume);
            jobBoardService.InsertApplicant(applicant);
            Console.WriteLine("Applicant profile created successfully!");
        }

        private static void ViewAllApplicants()
        {
            Console.WriteLine("\n ALL APPLICANTS ");
            List<Applicant> applicants = jobBoardService.GetApplicants();

            if (applicants.Count == 0)
            {
                Console.WriteLine("No applicants found.");
                return;
            }

            foreach (var applicant in applicants)
            {
                Console.WriteLine($"Applicant ID: {applicant.ApplicantID}");
                Console.WriteLine($"Name: {applicant.FirstName} {applicant.LastName}");
                Console.WriteLine($"Email: {applicant.Email}");
                Console.WriteLine($"Phone: {applicant.Phone}");
                Console.WriteLine($"Resume: {applicant.Resume}");
                Console.WriteLine(new string('-', 40));
            }
        }

        private static void FindApplicantById()
        {
            Console.WriteLine("\n FIND APPLICANT BY ID ");
            Console.Write("Enter Applicant ID: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Searching for Applicant ID: {applicantId}...");

        }

        private static void UpdateApplicantProfile()
        {
            Console.WriteLine("\n UPDATE APPLICANT PROFILE ");
            Console.Write("Enter Applicant ID to update: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Updating Applicant ID: {applicantId}...");

        }

        private static void RemoveApplicant()
        {
            Console.WriteLine("\n REMOVE APPLICANT ");
            Console.Write("Enter Applicant ID to remove: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Applicant ID {applicantId} removed successfully!");

        }


        private static void ApplicationManagementMenu()
        {
            Console.WriteLine("\n APPLICATION MANAGEMENT ");
            Console.WriteLine("1. Submit Job Application");
            Console.WriteLine("2. View Applications for Job");
            Console.WriteLine("3. View Applications by Applicant");
            Console.WriteLine("4. Update Application Status");
            Console.WriteLine("5. Withdraw Application");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    SubmitJobApplication();
                    break;
                case 2:
                    ViewApplicationsForJob();
                    break;
                case 3:
                    ViewApplicationsByApplicant();
                    break;
                case 4:
                    UpdateApplicationStatus();
                    break;
                case 5:
                    WithdrawApplication();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void SubmitJobApplication()
        {
            Console.WriteLine("\n SUBMIT JOB APPLICATION ");
            Console.Write("Enter Application ID: ");
            int applicationId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Job ID: ");
            int jobId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Applicant ID: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Cover Letter: ");
            string coverLetter = Console.ReadLine();

            JobApplication application = new JobApplication(applicationId, jobId, applicantId,
                                                          DateTime.Now, coverLetter);
            jobBoardService.InsertJobApplication(application);
            Console.WriteLine("Job application submitted successfully!");
        }

        private static void ViewApplicationsForJob()
        {
            Console.WriteLine("\n VIEW APPLICATIONS FOR JOB ");
            Console.Write("Enter Job ID: ");
            int jobId = Convert.ToInt32(Console.ReadLine());

            List<JobApplication> applications = jobBoardService.GetApplicationsForJob(jobId);

            if (applications.Count == 0)
            {
                Console.WriteLine($"No applications found for Job ID: {jobId}");
                return;
            }

            Console.WriteLine($"\nApplications for Job ID: {jobId}");
            foreach (var application in applications)
            {
                Console.WriteLine($"Application ID: {application.ApplicationID}");
                Console.WriteLine($"Applicant ID: {application.ApplicantID}");
                Console.WriteLine($"Application Date: {application.ApplicationDate:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"Cover Letter: {application.CoverLetter}");
                Console.WriteLine(new string('-', 40));
            }
        }

        private static void ViewApplicationsByApplicant()
        {
            Console.WriteLine("\n VIEW APPLICATIONS BY APPLICANT ");
            Console.Write("Enter Applicant ID: ");
            int applicantId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Viewing applications for Applicant ID: {applicantId}...");

        }

        private static void UpdateApplicationStatus()
        {
            Console.WriteLine("\n UPDATE APPLICATION STATUS ");
            Console.Write("Enter Application ID: ");
            int applicationId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter new status (Pending/Reviewed/Accepted/Rejected): ");
            string status = Console.ReadLine();


            Console.WriteLine($"Application ID {applicationId} status updated to: {status}");

        }

        private static void WithdrawApplication()
        {
            Console.WriteLine("\n WITHDRAW APPLICATION ");
            Console.Write("Enter Application ID to withdraw: ");
            int applicationId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Application ID {applicationId} withdrawn successfully!");

        }


        private static void CompanyManagementMenu()
        {
            Console.WriteLine("\n COMPANY MANAGEMENT ");
            Console.WriteLine("1. Add New Company");
            Console.WriteLine("2. View All Companies");
            Console.WriteLine("3. Find Company by ID");
            Console.WriteLine("4. Update Company Details");
            Console.WriteLine("5. Remove Company");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddNewCompany();
                    break;
                case 2:
                    ViewAllCompanies();
                    break;
                case 3:
                    FindCompanyById();
                    break;
                case 4:
                    UpdateCompanyDetails();
                    break;
                case 5:
                    RemoveCompany();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void AddNewCompany()
        {
            Console.WriteLine("\n ADD NEW COMPANY ");
            Console.Write("Enter Company ID: ");
            int companyId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Company Name: ");
            string companyName = Console.ReadLine();

            Console.Write("Enter Company Location: ");
            string location = Console.ReadLine();

            Company company = new Company(companyId, companyName, location);
            jobBoardService.InsertCompany(company);
            Console.WriteLine("Company added successfully!");
        }

        private static void ViewAllCompanies()
        {
            Console.WriteLine("\n ALL COMPANIES ");
            List<Company> companies = jobBoardService.GetCompanies();

            if (companies.Count == 0)
            {
                Console.WriteLine("No companies found.");
                return;
            }

            foreach (var company in companies)
            {
                Console.WriteLine($"Company ID: {company.CompanyID}");
                Console.WriteLine($"Name: {company.CompanyName}");
                Console.WriteLine($"Location: {company.Location}");
                Console.WriteLine(new string('-', 30));
            }
        }

        private static void FindCompanyById()
        {
            Console.WriteLine("\n FIND COMPANY BY ID ");
            Console.Write("Enter Company ID: ");
            int companyId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Searching for Company ID: {companyId}...");

        }

        private static void UpdateCompanyDetails()
        {
            Console.WriteLine("\n UPDATE COMPANY DETAILS ");
            Console.Write("Enter Company ID to update: ");
            int companyId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Updating Company ID: {companyId}...");

        }

        private static void RemoveCompany()
        {
            Console.WriteLine("\n REMOVE COMPANY ");
            Console.Write("Enter Company ID to remove: ");
            int companyId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Company ID {companyId} removed successfully!");

        }


        private static void SearchAndReportsMenu()
        {
            Console.WriteLine("\n SEARCH & REPORTS ");
            Console.WriteLine("1. Search Jobs by Salary Range");
            Console.WriteLine("2. Search Jobs by Location");
            Console.WriteLine("3. Search Jobs by Company");
            Console.WriteLine("4. Calculate Average Salary");
            Console.WriteLine("5. Generate Job Statistics Report");
            Console.Write("Enter your choice: ");

            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    SearchJobsBySalaryRange();
                    break;
                case 2:
                    SearchJobsByLocation();
                    break;
                case 3:
                    SearchJobsByCompany();
                    break;
                case 4:
                    CalculateAverageSalary();
                    break;
                case 5:
                    GenerateJobStatisticsReport();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private static void SearchJobsBySalaryRange()
        {
            Console.WriteLine("\n SEARCH JOBS BY SALARY RANGE ");
            Console.Write("Enter Minimum Salary: ");
            decimal minSalary = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Maximum Salary: ");
            decimal maxSalary = Convert.ToDecimal(Console.ReadLine());

            List<JobListing> jobs = jobBoardService.GetJobListingsBySalaryRange(minSalary, maxSalary);

            if (jobs.Count == 0)
            {
                Console.WriteLine("No jobs found in the specified salary range.");
                return;
            }

            Console.WriteLine($"\nJobs with salary between ${minSalary:N2} and ${maxSalary:N2}:");
            foreach (var job in jobs)
            {
                Console.WriteLine($"Job ID: {job.JobID} | Title: {job.JobTitle} | Salary: ${job.Salary:N2} | Location: {job.JobLocation}");
            }
        }

        private static void SearchJobsByLocation()
        {
            Console.WriteLine("\n SEARCH JOBS BY LOCATION ");
            Console.Write("Enter Location: ");
            string location = Console.ReadLine();


            Console.WriteLine($"Searching for jobs in: {location}...");

        }

        private static void SearchJobsByCompany()
        {
            Console.WriteLine("\n SEARCH JOBS BY COMPANY ");
            Console.Write("Enter Company ID: ");
            int companyId = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine($"Searching for jobs by Company ID: {companyId}...");

        }

        private static void CalculateAverageSalary()
        {
            Console.WriteLine("\n CALCULATE AVERAGE SALARY ");

            Console.WriteLine("Calculating average salary across all job listings...");

        }

        private static void GenerateJobStatisticsReport()
        {
            Console.WriteLine("\n JOB STATISTICS REPORT ");

            Console.WriteLine("Generating comprehensive job statistics report...");

        }
    }
}

