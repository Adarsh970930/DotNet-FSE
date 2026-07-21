-- Question 1: Basic TRY...CATCH for Error Logging
-- Create AuditLog table if not exists (already in schema)
-- CREATE TABLE AuditLog ( 
--     LogID INT IDENTITY(1,1) PRIMARY KEY, 
--     Action VARCHAR(100), 
--     ErrorMessage VARCHAR(4000), 
--     ActionDate DATETIME DEFAULT GETDATE() 
-- );

CREATE PROCEDURE AddEmployee_Q1
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('INSERT EMPLOYEE', ERROR_MESSAGE());
    END CATCH
END;
GO


-- Question 2: Using THROW to Re-raise Errors
-- Stored procedure AddEmployee that logs the error and re-raises it.
CREATE PROCEDURE AddEmployee_Q2
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('INSERT EMPLOYEE', ERROR_MESSAGE());
        -- Re-raise the error to propagate back to calling application
        THROW;
    END CATCH
END;
GO


-- Question 3: Custom Error with RAISERROR
-- Validate that salary must be greater than 0 using RAISERROR.
CREATE PROCEDURE AddEmployee_Q3
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        -- Check business rule
        IF @Salary <= 0
        BEGIN
            RAISERROR('Salary must be greater than zero.', 16, 1);
            RETURN;
        END

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('INSERT EMPLOYEE VALIDATION', ERROR_MESSAGE());
        THROW;
    END CATCH
END;
GO


-- Question 4: Nested TRY...CATCH with RAISERROR
-- Stored procedure TransferEmployee that updates department and implements nested try-catch.
CREATE PROCEDURE TransferEmployee
    @EmployeeID INT,
    @NewDepartmentID INT
AS
BEGIN
    BEGIN TRY
        -- Outer check for department existence
        IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @NewDepartmentID)
        BEGIN
            RAISERROR('Department ID does not exist.', 16, 1);
        END

        BEGIN TRY
            -- Inner try to perform update
            UPDATE Employees
            SET DepartmentID = @NewDepartmentID
            WHERE EmployeeID = @EmployeeID;
        END TRY
        BEGIN CATCH
            -- Log inner error
            INSERT INTO AuditLog (Action, ErrorMessage)
            VALUES ('TRANSFER EMPLOYEE UPDATE', ERROR_MESSAGE());
            THROW;
        END CATCH
        
    END TRY
    BEGIN CATCH
        -- Log outer error
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('TRANSFER EMPLOYEE OUTER', ERROR_MESSAGE());
        THROW;
    END CATCH
END;
GO


-- Question 5: Logging All Errors in a Transaction
-- Stored procedure BatchInsertEmployees implementing transaction rollback on failure.
CREATE PROCEDURE BatchInsertEmployees
    -- Accepts values for simulating batch insert of 2 employees
    @EmpID1 INT, @FName1 VARCHAR(50), @LName1 VARCHAR(50), @Email1 VARCHAR(100), @Sal1 DECIMAL(10,2), @DeptID1 INT,
    @EmpID2 INT, @FName2 VARCHAR(50), @LName2 VARCHAR(50), @Email2 VARCHAR(100), @Sal2 DECIMAL(10,2), @DeptID2 INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmpID1, @FName1, @LName1, @Email1, @Sal1, @DeptID1);

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmpID2, @FName2, @LName2, @Email2, @Sal2, @DeptID2);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('BATCH INSERT EMPLOYEE TRANSACTION', ERROR_MESSAGE());
        THROW;
    END CATCH
END;
GO


-- Question 6: Dynamic RAISERROR with Severity and State
-- Stored procedure AddEmployee with dynamic RAISERROR severity.
CREATE PROCEDURE AddEmployee_Q6
    @EmployeeID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Salary DECIMAL(10,2),
    @DepartmentID INT
AS
BEGIN
    BEGIN TRY
        -- Salary negative check
        IF @Salary < 0
        BEGIN
            RAISERROR('Salary cannot be negative.', 16, 1);
            RETURN;
        END
        
        -- Salary too low warning check
        IF @Salary < 1000
        BEGIN
            -- Severity 10 behaves as an informational warning (does not jump to CATCH block automatically)
            RAISERROR('Warning: Salary is below the standard minimum of 1000.', 10, 1);
        END

        INSERT INTO Employees (EmployeeID, FirstName, LastName, Email, Salary, DepartmentID)
        VALUES (@EmployeeID, @FirstName, @LastName, @Email, @Salary, @DepartmentID);
    END TRY
    BEGIN CATCH
        INSERT INTO AuditLog (Action, ErrorMessage)
        VALUES ('INSERT EMPLOYEE DYNAMIC SEVERITY', ERROR_MESSAGE());
        THROW;
    END CATCH
END;
GO