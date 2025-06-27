using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace Dao
{
    public class JobBoardServiceImpl : IJobBoardServiceImpl
    {
        private string connectionString;

        public JobBoardServiceImpl()
        {
            connectionString = Utility.DBPropertyUtility.GetConnectionString("database.properties");
        }

        public void InitializeDatabase()
        {
            Console.WriteLine("Database initialized successfully.");
        }

        public void InsertJobListing(model.JobListing job)
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO jobs (jobid, companyid, jobtitle, jobdescription, joblocation, salary, jobtype, postdate) VALUES (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", job.JobID);
                        command.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                        command.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                        command.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                        command.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                        command.Parameters.AddWithValue("@Salary", job.Salary);
                        command.Parameters.AddWithValue("@JobType", job.JobType);
                        command.Parameters.AddWithValue("@PostedDate", job.PostedDate);

                        command.ExecuteNonQuery();
                    }
                    connection.Close(); 
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error inserting job listing: {ex.Message}", ex);
            }
        }

        public List<model.JobListing> GetJobListings()
        {
            List<model.JobListing> jobListings = new List<model.JobListing>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM jobs";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.JobListing job = new model.JobListing
                                {
                                    JobID = Convert.ToInt32(reader["jobid"]),
                                    CompanyID = Convert.ToInt32(reader["companyid"]),
                                    JobTitle = reader["jobtitle"].ToString(),
                                    JobDescription = reader["jobdescription"].ToString(),
                                    JobLocation = reader["joblocation"].ToString(),
                                    Salary = Convert.ToDecimal(reader["salary"]),
                                    JobType = reader["jobtype"].ToString(),
                                    PostedDate = Convert.ToDateTime(reader["postdate"])
                                };
                                jobListings.Add(job);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving job listings: {ex.Message}", ex);
            }

            return jobListings;
        }

        public List<model.JobListing> GetJobListingsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            List<model.JobListing> jobListings = new List<model.JobListing>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM jobs WHERE salary BETWEEN @MinSalary AND @MaxSalary";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MinSalary", minSalary);
                        command.Parameters.AddWithValue("@MaxSalary", maxSalary);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.JobListing job = new model.JobListing
                                {
                                    JobID = Convert.ToInt32(reader["jobid"]),
                                    CompanyID = Convert.ToInt32(reader["companyid"]),
                                    JobTitle = reader["jobtitle"].ToString(),
                                    JobDescription = reader["jobdescription"].ToString(),
                                    JobLocation = reader["joblocation"].ToString(),
                                    Salary = Convert.ToDecimal(reader["salary"]),
                                    JobType = reader["jobtype"].ToString(),
                                    PostedDate = Convert.ToDateTime(reader["postdate"])
                                };
                                jobListings.Add(job);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving job listings by salary range: {ex.Message}", ex);
            }

            return jobListings;
        }

        public void InsertCompany(model.Company company)
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO companies (companyid, companyname, location) VALUES (@CompanyID, @CompanyName, @Location)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                        command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                        command.Parameters.AddWithValue("@Location", company.Location);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error inserting company: {ex.Message}", ex);
            }
        }

        public List<model.Company> GetCompanies()
        {
            List<model.Company> companies = new List<model.Company>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM companies";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.Company company = new model.Company
                                {
                                    CompanyID = Convert.ToInt32(reader["companyid"]),
                                    CompanyName = reader["companyname"].ToString(),
                                    Location = reader["location"].ToString()
                                };
                                companies.Add(company);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving companies: {ex.Message}", ex);
            }

            return companies;
        }

        public void InsertApplicant(model.Applicant applicant)
        {
            try
            {
                ValidateEmail(applicant.Email);

                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO applicant (applicantid, firstname, lastname, email, phone, resume) VALUES (@ApplicantID, @FirstName, @LastName, @Email, @Phone, @Resume)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                        command.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                        command.Parameters.AddWithValue("@LastName", applicant.LastName);
                        command.Parameters.AddWithValue("@Email", applicant.Email);
                        command.Parameters.AddWithValue("@Phone", applicant.Phone);
                        command.Parameters.AddWithValue("@Resume", applicant.Resume);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception.InvalidEmailFormatException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error inserting applicant: {ex.Message}", ex);
            }
        }

        public List<model.Applicant> GetApplicants()
        {
            List<model.Applicant> applicants = new List<model.Applicant>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM applicant";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.Applicant applicant = new model.Applicant
                                {
                                    ApplicantID = Convert.ToInt32(reader["applicantid"]),
                                    FirstName = reader["firstname"].ToString(),
                                    LastName = reader["lastname"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    Resume = reader["resume"].ToString()
                                };
                                applicants.Add(applicant);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving applicants: {ex.Message}", ex);
            }

            return applicants;
        }

        public void InsertJobApplication(model.JobApplication application)
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO applications (applicationid, jobid, applicantid, applicationdate, coverletter) VALUES (@ApplicationID, @JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                        command.Parameters.AddWithValue("@JobID", application.JobID);
                        command.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                        command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                        command.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error inserting job application: {ex.Message}", ex);
            }
        }

        public List<model.JobApplication> GetApplicationsForJob(int jobID)
        {
            List<model.JobApplication> applications = new List<model.JobApplication>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM applications WHERE jobid = @JobID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", jobID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.JobApplication application = new model.JobApplication
                                {
                                    ApplicationID = Convert.ToInt32(reader["applicationid"]),
                                    JobID = Convert.ToInt32(reader["jobid"]),
                                    ApplicantID = Convert.ToInt32(reader["applicantid"]),
                                    ApplicationDate = Convert.ToDateTime(reader["applicationdate"]),
                                    CoverLetter = reader["coverletter"].ToString()
                                };
                                applications.Add(application);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving applications for job: {ex.Message}", ex);
            }

            return applications;
        }

        public List<model.Applicant> GetApplicantsForJob(int jobID)
        {
            List<model.Applicant> applicants = new List<model.Applicant>();

            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT a.* FROM applicant a 
                                   INNER JOIN applications app ON a.applicantid = app.applicantid 
                                   WHERE app.jobid = @JobID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@JobID", jobID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.Applicant applicant = new model.Applicant
                                {
                                    ApplicantID = Convert.ToInt32(reader["applicantid"]),
                                    FirstName = reader["firstname"].ToString(),
                                    LastName = reader["lastname"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    Resume = reader["resume"].ToString()
                                };
                                applicants.Add(applicant);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error retrieving applicants for job: {ex.Message}", ex);
            }

            return applicants;
        }

        public bool ApplyForJob(int applicantID, int jobID, string coverLetter)
        {
            try
            {
                int nextApplicationID = GetNextApplicationID();

                model.JobApplication application = new model.JobApplication
                {
                    ApplicationID = nextApplicationID,
                    JobID = jobID,
                    ApplicantID = applicantID,
                    ApplicationDate = DateTime.Now,
                    CoverLetter = coverLetter
                };

                InsertJobApplication(application);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PostJob(int companyID, string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            try
            {
                if (salary < 0)
                {
                    throw new Exception.InvalidSalaryException("Salary cannot be negative");
                }

                int nextJobID = GetNextJobID();

                model.JobListing job = new model.JobListing
                {
                    JobID = nextJobID,
                    CompanyID = companyID,
                    JobTitle = jobTitle,
                    JobDescription = jobDescription,
                    JobLocation = jobLocation,
                    Salary = salary,
                    JobType = jobType,
                    PostedDate = DateTime.Now
                };

                InsertJobListing(job);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new Exception.InvalidEmailFormatException("Invalid email format. Email must contain @ and a valid domain.");
            }
        }

        private int GetNextJobID()
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(jobid), 0) + 1 FROM jobs";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
            }
            catch
            {
                return 1; 
            }
        }

        private int GetNextApplicationID()
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(applicationid), 0) + 1 FROM applications";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
            }
            catch
            {
                return 1; 
            }
        }

        private int GetNextApplicantID()
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(applicantid), 0) + 1 FROM applicant";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close(); 
                }
            }
            catch
            {
                return 1;
            }
        }

        private int GetNextCompanyID()
        {
            try
            {
                using (SqlConnection connection = Utility.DBConnUtility.GetConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(companyid), 0) + 1 FROM companies";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
            }
            catch
            {
                return 1; 
            }
        }

        public decimal CalculateAverageSalary()
        {
            try
            {
                List<model.JobListing> jobs = GetJobListings();
                if (jobs.Count == 0) return 0;

                decimal totalSalary = 0;
                int validJobs = 0;

                foreach (var job in jobs)
                {
                    if (job.Salary < 0)
                    {
                        throw new Exception.InvalidSalaryException($"Job ID {job.JobID} has negative salary: {job.Salary}");
                    }
                    totalSalary += job.Salary;
                    validJobs++;
                }

                return validJobs > 0 ? totalSalary / validJobs : 0;
            }
            catch (Exception.InvalidSalaryException)
            {
                throw;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error calculating average salary: {ex.Message}", ex);
            }
        }
    }
}

