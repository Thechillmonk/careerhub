--1.Provide a SQL script that initializes the database for the Job Board scenario “CareerHub”
create database careerhub

--2 & 3.Create tables for Companies, Jobs, Applicants and Applications,Define appropriate primary keys, foreign keys, and constraints.
--create table companies
create table companies(
 companyid int primary key,
 companyname varchar(30),
 [location] varchar(40))

 --create table jobs
 create table jobs(
 jobid int primary key,
 companyid int foreign key references companies(companyid) not null,
 jobtitle varchar(40),
 jobdescription text,
 joblocation varchar(200),
 salary decimal,
 jobtype varchar(50),
 postdate datetime)

 

 --create table applicat

 create table applicant(
 applicantid int primary key,
 firstname varchar(30),
 lastname varchar(40),
 email varchar(100) unique not null,
 phone varchar(20),
 [resume] text)

 --create table applications
 create table applications(
 applicationid int primary key,
 jobid int foreign key references jobs(jobid),
 applicantid int foreign key references applicant(applicantid),
 applicationdate datetime,
 coverletter text)

 --inert value into companies

 insert into companies( companyid,companyname,[location])
 values
      (1,'techcorp','chennai'),
      (2,'hexaware','mumbai'),
      (3,'mahindra' ,'bangalore'),
      (4,'edutech' ,'chennai'),
      (5,'bharattech' ,'delhi')

      select * from companies

      --insert values into jobs
      insert into jobs(jobid,companyid,jobtitle,jobdescription,joblocation,salary,jobtype,postdate)
      values
      (1,1,'software dev','developer with software skills and framework','chennai',75000.00,'full-time','2025-06-15 10:00:00'),
      (2,1,'senior engineer','lead development team and architect solution','chennai',95000.00,'full-time','2025-05-12 12:00:00'),
      (3,2,'data engineer','build and maintain data pipelines','mumbai',95000.00,'full-time','2025-01-14 9:15:00'),
      (4,3,'frontend developer', 'create responsive ui using framworks','bangalore',70000.00,'full-time','2024-05-17 11:45:00'),
      (5,4,'junior dev', 'entry level dev position','delhi',72000.00,'contract','2025-05-15 12:45:00')

    --insert value applicants

    insert into applicant(applicantid,firstname, LastName, Email, Phone, [Resume]) 
    values
(1,'sarath', 'shukla', 'sarathsarath@email.com', '9876543210', 'Experienced software developer with 5 years experience'),
(2,'mohammed', 'avdul', 'avdul123@email.com', '9876543211', 'Frontend specialist with React expertise' ),
(3,'Mike', 'tyson', 'ironmike@email.com', '9876543212', 'Full-stack developer with cloud experience'),
(4,'joseph', 'joestar', 'jojo@email.com', '9876543213', 'Data engineer with Python and SQL skills'),
(5,'David', 'miller', 'captianmiller@email.com', '9876543214', 'DevOps engineer with AWS certification')
 
 --insert value applications

 insert into applications (applicationid,jobid,applicantid,applicationdate,coverletter)
 values
(1,1, 1, '2025-01-16 10:30:00', 'i am excited to apply for the software developer position...'),
(2,1, 1, '2025-01-17 14:20:00', 'my frontend skills make me perfect for this role...'),
(3,3, 2, '2025-01-21 09:45:00', 'i would love to take on a senior role...'),
(4,2, 4, '2025-01-26 11:15:00', 'my data engineering experience aligns perfectly...'),
(5,5, 3, '2025-02-02 15:30:00', 'i have extensive devops experience...')

--tasks

--5.Write an SQL query to count the number of applications received for each job listing in the
--"Jobs" table. Display the job title and the corresponding application count. Ensure that it lists all
--jobs, even if they have no applications
select j.jobtitle,count(a.applicationid) as applicationcount
from jobs j
left join
applications a 
on j.jobid = a.jobid
group by j.jobid , j.jobtitle
order by applicationcount, j.jobtitle

--6.Develop an SQL query that retrieves job listings from the "Jobs" table within a specified salary
--range. Allow parameters for the minimum and maximum salary values. Display the job title,
--company name, location, and salary for each matching job.
  
  select j.jobtitle,c.companyname,j.joblocation,j.salary
  from jobs j
  inner join
  companies c
  on j.companyid = c.companyid
  where j.salary between 50000 and 75000
  order by j.salary 

  --7.Write an SQL query that retrieves the job application history for a specific applicant. Allow a
  --parameter for the ApplicantID, and return a result set with the job titles, company names, and
  --application dates for all the jobs the applicant has applied.

  select j.jobtitle,c.companyname,a.applicationdate
  from applications a
  inner join jobs j 
  on a.jobid = j.jobid
  inner join companies c
  on j.companyid = c.companyid
  where a.applicantid = 1
  order by a.applicationdate 


  --8.Create an SQL query that calculates and displays the average salary offered by all companies for
  --job listings in the "Jobs" table. Ensure that the query filters out jobs with a salary of zero.

  select round(avg(salary),2) as avgsalary
  from jobs
  where salary <> 0

  --11.Retrieve a list of distinct job titles with salaries between $60,000 and $80,000.

  select jobtitle,salary
  from jobs
  where salary between 60000 and 80000
  order by jobtitle,salary 

  --12.Find the jobs that have not received any applications.
  select j.jobid,j.jobtitle,c.companyname,j.salary
  from jobs j
  join companies c
  on j.companyid = c.companyid
  left join applications a
  on j.jobid = a.applicationid
  where a.applicationid is null

  --14Retrieve a list of companies along with the count of jobs they have posted, even if they have not
  --received any applications.
   select c.companyname,count(j.jobid) as jobsposted
   from companies c
   left join jobs j
   on c.companyid = j.companyid
   group by c.companyid, c.companyname
   order by jobsposted 

   --16 Find companies that have posted jobs with a salary higher than the average salary of all job

   select distinct c.companyname,c.[location],j.salary
   from companies c
   inner join jobs j 
   on c.companyid = j.companyid
   where j.salary > (
   select avg(salary)
   from jobs
   where salary > 0)
   order by salary

   --18.Retrieve a list of jobs with titles containing either 'Developer' or 'Engineer'.

   select jobid,jobtitle,joblocation,salary,jobtype
   from jobs
   where jobtitle like '%developer%' or jobtitle like '%engineer%'
   order by jobtitle

   --9.Write an SQL query to identify the company that has posted the most job listings. Display the
   --company name along with the count of job listings they have posted. Handle ties if multiple
   --companies have the same maximum count

   select c.companyname,count(j.jobid) as jobcount
   from companies c
   inner join jobs j 
   on c.companyid = j.companyid
   group by c.companyid, c.companyname
   having count(j.jobid) = 
   ( select max(job_count)
    from 
    ( select count(jobid) as job_count
      from jobs
      group by companyid) as max_counts)





   


