DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS ProductCategory;
DROP TABLE IF EXISTS Supplier;

CREATE TABLE Product
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name TEXT,
	Default_price DECIMAL(20,2),
	Image TEXT,
	Description TEXT,
	Product_category_id INT,
	Supplier_id INT
);

CREATE TABLE ProductCategory
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name TEXT,
	Department TEXT,
	Description TEXT
);

CREATE TABLE Supplier
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name TEXT,
	Description TEXT
);

ALTER TABLE Product
	ADD CONSTRAINT FK_Product_ProductCategory FOREIGN KEY (Product_category_id)
	REFERENCES ProductCategory (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

ALTER TABLE Product
	ADD CONSTRAINT FK_Product_Supplier FOREIGN KEY (Supplier_id)
	REFERENCES Supplier (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

INSERT INTO Supplier VALUES ('Amazon', 'Digital content and services');
INSERT INTO Supplier VALUES ('Greek Hand Made Company', 'Hand crafted items directly from Greece');
INSERT INTO ProductCategory VALUES ('Chakram', 'Weapon', 'Round shaped weapon that works like a frizby.');
INSERT INTO ProductCategory VALUES ('Crossbow', 'Weapon', 'A bow for people who like to hold heavy weapons with two hands.');
INSERT INTO Product VALUES ('LMBTQ Chakram', 149.99, 'chakram-lmbtq.jpg', 'Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.', 1, 1);
INSERT INTO Product VALUES ('Crossbow', 259.88, 'crossbow1.jpg', 'Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.', 2, 2);

SELECT * FROM Supplier;
SELECT * FROM Product;
SELECT * FROM ProductCategory;
