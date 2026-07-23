-- Exercise 1: Create a Scalar Function
-- Define a scalar function named fn_CalculateAnnualSalary.
CREATE FUNCTION fn_CalculateAnnualSalary (
    @Salary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @Salary * 12;
END;
GO


-- Exercise 2: Create a Table-Valued Function
-- Define a table-valued function named fn_GetEmployeesByDepartment.
CREATE FUNCTION fn_GetEmployeesByDepartment (
    @DepartmentID INT
)
RETURNS TABLE
AS
RETURN (
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID
);
GO


-- Exercise 3: Create a User-Defined Function
-- Define a user-defined function named fn_CalculateBonus.
CREATE FUNCTION fn_CalculateBonus (
    @Salary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @Salary * 0.10;
END;
GO


-- Exercise 4: Modify a User-Defined Function
-- Alter the fn_CalculateBonus function to return Salary * 0.15.
ALTER FUNCTION fn_CalculateBonus (
    @Salary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @Salary * 0.15;
END;
GO


-- Exercise 5: Delete a User-Defined Function
-- Drop the fn_CalculateBonus function.
-- DROP FUNCTION fn_CalculateBonus;
-- GO


-- Exercise 6: Execute a User-Defined Function
-- Use the fn_CalculateAnnualSalary function to calculate the annual salary for each employee.
SELECT 
    EmployeeID, 
    FirstName, 
    LastName, 
    Salary, 
    dbo.fn_CalculateAnnualSalary(Salary) AS AnnualSalary
FROM Employees;
GO


-- Exercise 7: Return Data from a Scalar Function
-- Execute the fn_CalculateAnnualSalary function for an employee with EmployeeID = 1.
SELECT dbo.fn_CalculateAnnualSalary(Salary) AS Employee1AnnualSalary
FROM Employees
WHERE EmployeeID = 1;
GO


-- Exercise 8: Return Data from a Table-Valued Function
-- Execute the fn_GetEmployeesByDepartment function for DepartmentID = 3 (Finance).
SELECT * 
FROM dbo.fn_GetEmployeesByDepartment(3);
GO


-- Exercise 9: Create a Nested User-Defined Function
-- Define a user-defined function named fn_CalculateTotalCompensation.
-- This function uses fn_CalculateAnnualSalary and fn_CalculateBonus.
-- Note: Ensure fn_CalculateBonus is present before compiling.
CREATE FUNCTION fn_CalculateTotalCompensation (
    @Salary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @AnnualSalary DECIMAL(10, 2);
    DECLARE @Bonus DECIMAL(10, 2);
    
    SET @AnnualSalary = dbo.fn_CalculateAnnualSalary(@Salary);
    SET @Bonus = dbo.fn_CalculateBonus(@Salary);
    
    RETURN @AnnualSalary + @Bonus;
END;
GO

-- Test nested function:
SELECT 
    EmployeeID,
    FirstName,
    LastName,
    Salary,
    dbo.fn_CalculateTotalCompensation(Salary) AS TotalCompensation
FROM Employees;
GO


-- Exercise 10: Modify a Nested User-Defined Function
-- Alter fn_CalculateTotalCompensation to include updated logic or verify compilation 
-- with modified fn_CalculateBonus.
ALTER FUNCTION fn_CalculateTotalCompensation (
    @Salary DECIMAL(10, 2)
)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    -- Uses modified fn_CalculateBonus (which is now Salary * 0.15)
    RETURN dbo.fn_CalculateAnnualSalary(@Salary) + dbo.fn_CalculateBonus(@Salary);
END;
GO