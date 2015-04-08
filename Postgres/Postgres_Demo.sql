DROP TABLE IF EXISTS public.VIPCustomer;
DROP TABLE IF EXISTS public.Customer;

-- Create a basic customer data table
CREATE TABLE public.Customer
		(
		CustomerID	int
		,Name		varchar(100)
		,Addresses	json
		);

-- Create a table which inherits from customer and adds a "Discount" field
CREATE TABLE public.VIPCustomer
		(
		VIPDiscountPercentage	smallint	-- Can we do anything useful with this as a custom type?
		)
		INHERITS (public.Customer);


-- See the columns in each table
SELECT * FROM public.Customer;
SELECT * FROM public.VIPCustomer;


-- Try an insert to VIPCustomer - it appears in both tables
INSERT INTO public.VIPCustomer (CustomerID, Name, VIPDiscountPercentage)
	VALUES (111, 'John Smith', 20);	-- Will add some JSON addresses later :-)

SELECT * FROM public.VIPCustomer;
SELECT * FROM public.Customer;


-- Insert to the base customer table - it DOESN'T appear in the inherited table
INSERT INTO public.Customer (CustomerID, Name)
	VALUES (112, 'Bob Jones');	-- Will add some JSON addresses later :-)


SELECT * FROM public.VIPCustomer;	-- Does not have the basic customer
SELECT * FROM public.Customer;		-- Has both customers
SELECT * FROM ONLY public.Customer;	-- ONLY keyword means we DON'T get VIP records

