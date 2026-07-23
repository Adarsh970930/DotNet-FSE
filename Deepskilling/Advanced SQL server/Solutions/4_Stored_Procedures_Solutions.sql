-- Exercise 1: Create a Stored Procedure
-- Create a stored procedure to retrieve employee details by department.
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, DepartmentID, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- Create a stored procedure named sp_InsertEmployee as described in the text:
CREATE PROCEDURE sp_InsertEmployee 
    @FirstName VARCHAR(50), 
    @LastName VARCHAR(50), 
    @DepartmentID INT, 
    @Salary DECIMAL(10,2), 
    @JoinDate DATE 
AS 
BEGIN 
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate) 
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate); 
END; 
GO


-- Exercise 2: Modify a Stored Procedure
-- Modify the stored procedure to include employee salary in the result.
ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- Exercise 3: Delete a Stored Procedure
-- Write the SQL command to delete the stored procedure sp_GetEmployeesByDepartment.
-- DROP PROCEDURE sp_GetEmployeesByDepartment;
-- GO


-- Exercise 4: Execute a Stored Procedure
-- Execute the stored procedure to retrieve employee details for department 1.
EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;
GO


-- Exercise 5: Return Data from a Stored Procedure
-- Create a stored procedure that returns the total number of employees in a department.
CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT COUNT(EmployeeID) AS TotalEmployees
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- Exercise 6: Use Output Parameters in a Stored Procedure
-- Create a stored procedure that returns the total salary of employees in a department 
-- using an output parameter.
CREATE PROCEDURE sp_GetTotalSalaryByDepartment
    @DepartmentID INT,
    @TotalSalary DECIMAL(10,2) OUTPUT
AS
BEGIN
    SELECT @TotalSalary = ISNULL(SUM(Salary), 0)
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- Example execution:
-- DECLARE @SalarySum DECIMAL(10,2);
-- EXEC sp_GetTotalSalaryByDepartment 2, @SalarySum OUTPUT;
-- SELECT @SalarySum AS DepartmentTotalSalary;
-- GO


-- Exercise 7: Create a Stored Procedure with Multiple Parameters
-- Create a stored procedure named sp_UpdateEmployeeSalary to update employee salary.
CREATE PROCEDURE sp_UpdateEmployeeSalary
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees
    SET Salary = @NewSalary
    WHERE EmployeeID = @EmployeeID;
END;
GO

-- Example execution:
-- EXEC sp_UpdateEmployeeSalary 1, 5500.00;
-- GO


-- Exercise 8: Create a Stored Procedure with Conditional Logic
-- Create a stored procedure named sp_GiveBonus to give a bonus to employees based on their department.
CREATE PROCEDURE sp_GiveBonus
    @DepartmentID INT,
    @BonusAmount DECIMAL(10,2)
AS
BEGIN
    -- Apply different bonuses depending on department if needed, 
    -- here we give the specified bonus to all employees in the department.
    UPDATE Employees
    SET Salary = Salary + @BonusAmount
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- Example execution:
-- EXEC sp_GiveBonus 1, 500.00;
-- GO


-- Exercise 9: Use Transactions in a Stored Procedure
-- Create a stored procedure that updates employee salaries and uses a transaction.
CREATE PROCEDURE sp_UpdateSalaryWithTransaction
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


-- Exercise 10: Use Dynamic SQL in a Stored Procedure
-- Create a stored procedure that uses dynamic SQL to retrieve employee details based on a flexible filter.
CREATE PROCEDURE sp_GetEmployeesDynamic
    @FilterColumn VARCHAR(50),
    @FilterValue VARCHAR(100)
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    -- Validate input column to prevent SQL injection
    IF @FilterColumn IN ('FirstName', 'LastName', 'DepartmentID')
    BEGIN
        SET @SQL = N'SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate 
                     FROM Employees 
                     WHERE ' + QUOTENAME(@FilterColumn) + N' = @Val';
        EXEC sp_executesql @SQL, N'@Val VARCHAR(100)', @Val = @FilterValue;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid column filter specified.', 16, 1);
    END
END;
GO


-- Exercise 11: Handle Errors in a Stored Procedure
-- Create a stored procedure that handles errors and returns a custom error message.
CREATE PROCEDURE sp_UpdateSalaryWithErrorHandling
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID)
        BEGIN
            RAISERROR('Employee ID does not exist.', 16, 1);
            RETURN;
        END

        UPDATE Employees
        SET Salary = @NewSalary
        WHERE EmployeeID = @EmployeeID;
    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_SEVERITY() AS ErrorSeverity;
    END CATCH
END;
GO