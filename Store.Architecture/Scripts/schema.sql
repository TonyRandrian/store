CREATE DATABASE store;

CREATE TABLE categories
(
	id SERIAL			PRIMARY KEY,
	name				VARCHAR(100),
	parent_category_id	INT REFERENCES categories(id)
);

CREATE TABLE products
(
	id			SERIAL PRIMARY KEY,
	name		VARCHAR(255),
	category_id	INT REFERENCES categories(id),
	price		DECIMAL(10, 2)
);

CREATE TABLE suppliers
(
	id		SERIAL PRIMARY KEY,
	name	VARCHAR(255)
);

CREATE TABLE customers
(
	id		SERIAL PRIMARY KEY,
	name	VARCHAR(255)
);

CREATE TABLE products_supplies
(
	id			SERIAL PRIMARY KEY,
	supplier_id	INT REFERENCES suppliers(id),
	product_id	INT REFERENCES products(id)
);

CREATE TABLE invoices
(
	id			SERIAL PRIMARY KEY,
	reference	VARCHAR(50),
	customer_id INT REFERENCES customers(id),
	total		DECIMAL(10, 2)
);