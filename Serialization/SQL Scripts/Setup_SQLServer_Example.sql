
IF OBJECT_ID('dbo.Customer', 'U') IS NOT NULL
	DROP TABLE dbo.Customer;
GO

CREATE TABLE dbo.Customer
			(
			CustomerID	int
			,[Object]	xml
			,CONSTRAINT PK_Customer PRIMARY KEY (CustomerID)
			);
