IF OBJECT_ID('dbo.Enrollments','U') IS NOT NULL DROP TABLE dbo.Enrollments;
IF OBJECT_ID('dbo.Courses','U') IS NOT NULL DROP TABLE dbo.Courses;
IF OBJECT_ID('dbo.Students','U') IS NOT NULL DROP TABLE dbo.Students;

CREATE TABLE dbo.Students
(
    Id INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName  NVARCHAR(50) NOT NULL,
    Email     NVARCHAR(255) NOT NULL,
    EnrollmentDate DATETIME2 NOT NULL
);
CREATE UNIQUE INDEX UX_Students_Email ON dbo.Students(Email);

CREATE TABLE dbo.Courses
(
    Id INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL
);

CREATE TABLE dbo.Enrollments
(
    Id INT IDENTITY PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId  INT NOT NULL,
    Grade INT NOT NULL,
    CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentId) REFERENCES dbo.Students(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseId) REFERENCES dbo.Courses(Id) ON DELETE CASCADE
);

IF COL_LENGTH('dbo.Students','MiddleName') IS NULL
ALTER TABLE dbo.Students ADD MiddleName NVARCHAR(50) NULL;