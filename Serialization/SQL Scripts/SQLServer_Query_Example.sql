

-- Raw fields
SELECT	*
FROM	dbo.Customer
WHERE	CustomerID = 111;


-- Use inbuilt XML functions to drill into individual elements
SELECT	CustomerID
		,[Object]
		,[Object].value('(Customer/Name)[1]', 'varchar(100)')				AS CustomerName
		,[Object].query('Customer/Addresses')								AS CustomerAddressList
		,[Object].value('(Customer/Addresses/string)[1]', 'varchar(200)')	AS CustomerFirstAddress
		,[Object].value('(Customer/Addresses/string)[2]', 'varchar(200)')	AS CustomerSecondAddress
FROM	dbo.Customer
WHERE	CustomerID = 111;


-- Return address array as rows
SELECT	c.CustomerID
		,t.AddressEntry.value('.[1]', 'varchar(200)')
FROM	dbo.Customer AS c
		CROSS APPLY [Object].nodes('/Customer/Addresses/string') AS t(AddressEntry);
