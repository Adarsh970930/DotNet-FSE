-- SQL Exercise 1: Creating a Non-Clustered Index

-- Step 1: Query to fetch product details before index creation
SELECT * FROM Products WHERE ProductName = 'Laptop';

-- Step 2: Create a non-clustered index on ProductName
CREATE NONCLUSTERED INDEX IX_Products_ProductName 
ON Products (ProductName);

-- Step 3: Query to fetch product details after index creation
SELECT * FROM Products WHERE ProductName = 'Laptop';


-- SQL Exercise 2: Creating a Clustered Index

-- Step 1: Query to fetch orders before index creation
SELECT * FROM Orders WHERE OrderDate = '2023-01-15';

-- Step 2: Create a clustered index on OrderDate
-- Note: SQL Server default PRIMARY KEY creates a clustered index on OrderID.
-- To create a clustered index on OrderDate, we must either drop the existing clustered index (PK)
-- or configure the PK as NONCLUSTERED and then create the clustered index.
-- Here is how to create it (assuming no existing clustered index on the table):
CREATE CLUSTERED INDEX IX_Orders_OrderDate 
ON Orders (OrderDate);

-- Step 3: Query to fetch orders after index creation
SELECT * FROM Orders WHERE OrderDate = '2023-01-15';


-- SQL Exercise 3: Creating a Composite Index

-- Step 1: Query to fetch orders before index creation
SELECT * FROM Orders WHERE CustomerID = 1 AND OrderDate = '2023-01-15';

-- Step 2: Create a composite index on CustomerID and OrderDate
CREATE NONCLUSTERED INDEX IX_Orders_CustomerID_OrderDate 
ON Orders (CustomerID, OrderDate);

-- Step 3: Query to fetch orders after index creation
SELECT * FROM Orders WHERE CustomerID = 1 AND OrderDate = '2023-01-15';