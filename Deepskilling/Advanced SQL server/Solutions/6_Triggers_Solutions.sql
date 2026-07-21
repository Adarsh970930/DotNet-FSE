-- Exercise 1: Create an After Trigger
-- 1. Create a new table named EmployeeChanges to store change logs.
CREATE TABLE EmployeeChanges (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT,
    OldSalary DECIMAL(10,2),
    NewSalary DECIMAL(10,2),
    ChangeDate DATETIME DEFAULT GETDATE()
);
GO

-- 2. Create an AFTER trigger on the Employees table to insert a log on salary update.
CREATE TRIGGER trg_AfterEmployeeSalaryUpdate
ON Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    -- Only log if the Salary column was modified
    IF UPDATE(Salary)
    BEGIN
        INSERT INTO EmployeeChanges (EmployeeID, OldSalary, NewSalary)
        SELECT 
            i.EmployeeID,
            d.Salary AS OldSalary,
            i.Salary AS NewSalary
        FROM inserted i
        JOIN deleted d ON i.EmployeeID = d.EmployeeID;
    END
END;
GO


-- Exercise 2: Create an Instead of Trigger
-- Create an INSTEAD OF DELETE trigger on the Employees table to prevent deletions.
CREATE TRIGGER trg_InsteadOfDeleteEmployee
ON Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    RAISERROR('Deletions from the Employees table are not allowed.', 16, 1);
END;
GO


-- Exercise 3: Create a Logon Trigger
-- Create a LOGON trigger to restrict access to the database during maintenance hours (2 AM to 3 AM).
-- Note: LOGON triggers are server-scoped.
CREATE TRIGGER trg_LogonMaintenanceRestricted
ON ALL SERVER
FOR LOGON
AS
BEGIN
    DECLARE @CurrentHour INT = DATEPART(hour, GETDATE());
    -- Prevent login between 2 AM and 3 AM
    IF @CurrentHour = 2
    BEGIN
        -- Rollback the logon attempt
        ROLLBACK;
    END
END;
GO


-- Exercise 4: Modify a Trigger using SSMS / ALTER TRIGGER
-- Example modifying trg_InsteadOfDeleteEmployee to permit deletions only for admin users:
ALTER TRIGGER trg_InsteadOfDeleteEmployee
ON Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF IS_MEMBER('db_owner') = 1 OR ORIGINAL_LOGIN() = 'sa'
    BEGIN
        DELETE e
        FROM Employees e
        JOIN deleted d ON e.EmployeeID = d.EmployeeID;
    END
    ELSE
    BEGIN
        RAISERROR('Only database administrators can delete employee records.', 16, 1);
    END
END;
GO




-- 2. Create a trigger to update the AnnualSalary column whenever the Salary column is updated.
CREATE TRIGGER trg_UpdateAnnualSalary
ON Employees
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(Salary)
    BEGIN
        UPDATE e
        SET e.AnnualSalary = i.Salary * 12
        FROM Employees e
        JOIN inserted i ON e.EmployeeID = i.EmployeeID;
    END
END;
GO