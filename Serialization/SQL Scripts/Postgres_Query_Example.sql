
-- Select the customer JSON and use inbuilt functions to extract some fields

SELECT	customer_id
	,object		
	,json_extract_path_text(object, 'Name')	AS customer_name
	,object->'Addresses'			AS customer_address_list
FROM 	public.customer
WHERE	customer_id = 111;


-- Expand the address array using json_array_elements

SELECT	customer_id
	,json_array_elements(object->'Addresses')
FROM 	public.customer
WHERE	customer_id = 111;
