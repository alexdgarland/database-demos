
DROP TABLE IF EXISTS public.customer;

CREATE TABLE public.customer
(
  customer_id integer NOT NULL,
  object json,
  CONSTRAINT pk_customer PRIMARY KEY (customer_id)
);
