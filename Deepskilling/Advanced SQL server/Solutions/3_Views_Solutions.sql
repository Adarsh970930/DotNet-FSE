-- Exercise 1: Create a Simple View
-- Create a view named vw_EmployeeBasicInfo that displays EmployeeID, FirstName, 
-- LastName, and DepartmentName by joining Employees and Departments.

CREATE VIEW vw_EmployeeBasicInfo AS
SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;
GO


-- Exercise 2: Add Computed Column - Full Name
-- Create a view named vw_EmployeeFullName that includes a computed column 
-- FullName (concatenation of FirstName and LastName).

CREATE VIEW vw_EmployeeFullName AS
SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;
GO


-- Exercise 3: Add Computed Column - Annual Salary
-- Create a view named vw_EmployeeAnnualSalary that includes a computed column 
-- AnnualSalary (Salary * 12).

CREATE VIEW vw_EmployeeAnnualSalary AS
SELECT 
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    e.Salary,
    e.Salary * 12 AS AnnualSalary
FROM Employees e;
GO


-- Exercise 4: Add Multiple Computed Columns
-- Create a view named vw_EmployeeReport that includes:
-- - EmployeeID
-- - FullName
-- - DepartmentName
-- - AnnualSalary
-- - Bonus (10% of AnnualSalary)

CREATE VIEW vw_EmployeeReport AS
SELECT 
    e.EmployeeID,
    e.FirstName + ' ' + e.LastName AS FullName,
    d.DepartmentName,
    e.Salary * 12 AS AnnualSalary,
    (e.Salary * 12) * 0.10 AS Bonus
FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID;
GO