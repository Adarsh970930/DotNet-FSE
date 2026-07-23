-- Exercise 1: Create a Cursor
-- Create a cursor to iterate over all employees and print their details.

-- 1. Declare local variables for holding fetched data
DECLARE @EmpID INT;
DECLARE @FName VARCHAR(50);
DECLARE @LName VARCHAR(50);
DECLARE @Sal DECIMAL(10,2);
DECLARE @JoinDt DATE;

-- 2. Declare the cursor
DECLARE emp_cursor CURSOR FOR
SELECT EmployeeID, FirstName, LastName, Salary, JoinDate
FROM Employees;

-- 3. Open the cursor
OPEN emp_cursor;

-- 4. Fetch the first row
FETCH NEXT FROM emp_cursor 
INTO @EmpID, @FName, @LName, @Sal, @JoinDt;

-- 5. Loop through the cursor
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Print the details of each employee
    PRINT 'Employee ID: ' + CAST(@EmpID AS VARCHAR(10)) + 
          ', Name: ' + @FName + ' ' + @LName + 
          ', Salary: $' + CAST(@Sal AS VARCHAR(20)) + 
          ', Joined: ' + CAST(@JoinDt AS VARCHAR(10));
          
    -- Fetch the next row
    FETCH NEXT FROM emp_cursor 
    INTO @EmpID, @FName, @LName, @Sal, @JoinDt;
END;

-- 6. Close and deallocate the cursor
CLOSE emp_cursor;
DEALLOCATE emp_cursor;
GO


-- Exercise 2: Types of Cursors
-- Demonstrating the declaration syntax for various cursor types in SQL Server.

-- 1. Static Cursor
-- A static cursor populates a temporary table in tempdb. It is read-only, 
-- and changes made by other transactions after opening are not visible.
DECLARE static_emp_cursor CURSOR STATIC FOR
SELECT EmployeeID, FirstName, LastName, Salary
FROM Employees;

-- 2. Dynamic Cursor
-- A dynamic cursor reflects all modifications made to the underlying rows as 
-- you scroll through the cursor.
DECLARE dynamic_emp_cursor CURSOR DYNAMIC FOR
SELECT EmployeeID, FirstName, LastName, Salary
FROM Employees;

-- 3. Forward-Only Cursor
-- A forward-only cursor can only fetch rows sequentially from start to end.
DECLARE forward_emp_cursor CURSOR FORWARD_ONLY FOR
SELECT EmployeeID, FirstName, LastName, Salary
FROM Employees;

-- 4. Keyset-Driven Cursor
-- A keyset-driven cursor builds a keyset in tempdb when opened. Membership 
-- and order of rows are fixed, but value changes made by others are visible.
DECLARE keyset_emp_cursor CURSOR KEYSET FOR
SELECT EmployeeID, FirstName, LastName, Salary
FROM Employees;

