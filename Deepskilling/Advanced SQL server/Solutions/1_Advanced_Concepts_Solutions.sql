-- SQL Exercise 1: Ranking and Window Functions

-- 1. ROW_NUMBER() to assign unique rank
SELECT 
    ProductID,
    ProductName,
    Category,
    Price,
    ROW_NUMBER() OVER(PARTITION BY Category ORDER BY Price DESC) AS RowNumRank
FROM Products;

-- 2. RANK() and DENSE_RANK() to compare tie handling
SELECT 
    ProductID,
    ProductName,
    Category,
    Price,
    ROW_NUMBER() OVER(PARTITION BY Category ORDER BY Price DESC) AS RowNumRank,
    RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS RankVal,
    DENSE_RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS DenseRankVal
FROM Products;

-- 3. Top 3 most expensive products in each category using DENSE_RANK()
WITH RankedProducts AS (
    SELECT 
        ProductID,
        ProductName,
        Category,
        Price,
        DENSE_RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS PriceRank
    FROM Products
)
SELECT ProductID, ProductName, Category, Price, PriceRank
FROM RankedProducts
WHERE PriceRank <= 3;


-- SQL Exercise 2: Aggregation with GROUPING SETS, CUBE, and ROLLUP

-- 1 & 2. GROUPING SETS to get totals by Region, Category, and both
SELECT 
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantitySold
FROM OrderDetails od
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Customers c ON o.CustomerID = c.CustomerID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY GROUPING SETS (
    (c.Region, p.Category),
    (c.Region),
    (p.Category),
    ()
);

-- 3. ROLLUP to get subtotals and grand totals
SELECT 
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantitySold
FROM OrderDetails od
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Customers c ON o.CustomerID = c.CustomerID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY ROLLUP (c.Region, p.Category);

-- 4. CUBE to get all combinations of Region and Category
SELECT 
    c.Region,
    p.Category,
    SUM(od.Quantity) AS TotalQuantitySold
FROM OrderDetails od
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Customers c ON o.CustomerID = c.CustomerID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY CUBE (c.Region, p.Category);


-- SQL Exercise 3: CTEs and MERGE

-- a) Recursive CTE to generate a calendar table from '2025-01-01' to '2025-01-31'
WITH CalendarCTE AS (
    SELECT CAST('2025-01-01' AS DATE) AS CalendarDate
    UNION ALL
    SELECT DATEADD(day, 1, CalendarDate)
    FROM CalendarCTE
    WHERE CalendarDate < '2025-01-31'
)
SELECT CalendarDate
FROM CalendarCTE
OPTION (MAXRECURSION 31);

-- b) MERGE statement to update or insert product prices from a staging table
-- Assumes Products table and StagingProducts table exist
-- Create Staging Table for demo:
-- CREATE TABLE StagingProducts (ProductID INT, ProductName VARCHAR(100), Category VARCHAR(50), Price DECIMAL(10,2));
MERGE Products AS target
USING StagingProducts AS source
ON (target.ProductID = source.ProductID)
WHEN MATCHED THEN
    UPDATE SET 
        target.ProductName = source.ProductName,
        target.Category = source.Category,
        target.Price = source.Price
WHEN NOT MATCHED BY TARGET THEN
    INSERT (ProductID, ProductName, Category, Price)
    VALUES (source.ProductID, source.ProductName, source.Category, source.Price);


-- SQL Exercise 4: PIVOT and UNPIVOT

-- 1. Base Aggregation of sales by Product and Month
-- (Assuming we extract MONTH name or number from OrderDate)
WITH MonthlySales AS (
    SELECT 
        p.ProductName,
        DATENAME(month, o.OrderDate) AS OrderMonth,
        SUM(od.Quantity) AS TotalQty
    FROM OrderDetails od
    JOIN Orders o ON od.OrderID = o.OrderID
    JOIN Products p ON od.ProductID = p.ProductID
    GROUP BY p.ProductName, DATENAME(month, o.OrderDate)
)
-- 2. PIVOT to convert rows into columns (Months)
SELECT ProductName, [January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December]
INTO #PivotedSales
FROM MonthlySales
PIVOT (
    SUM(TotalQty)
    FOR OrderMonth IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
) AS PivotTable;

SELECT * FROM #PivotedSales;

-- 3. UNPIVOT to convert pivoted data back to row format
SELECT ProductName, OrderMonth, TotalQty
FROM #PivotedSales
UNPIVOT (
    TotalQty FOR OrderMonth IN ([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
) AS UnpivotTable;

DROP TABLE #PivotedSales;


-- SQL Exercise 5: Using CTE to Simplify a Query
WITH CustomerOrderCounts AS (
    SELECT 
        o.CustomerID,
        COUNT(o.OrderID) AS OrderCount
    FROM Orders o
    GROUP BY o.CustomerID
)
SELECT 
    c.CustomerID,
    c.Name,
    coc.OrderCount
FROM CustomerOrderCounts coc
JOIN Customers c ON c.CustomerID = coc.CustomerID
WHERE coc.OrderCount > 3;