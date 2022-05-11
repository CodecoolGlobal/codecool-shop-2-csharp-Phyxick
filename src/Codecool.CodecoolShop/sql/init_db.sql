DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS ProductCategory;
DROP TABLE IF EXISTS Supplier;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS BillingAddress;
DROP TABLE IF EXISTS ShippingAddress;
DROP TABLE IF EXISTS ShoppingCart;
DROP TABLE IF EXISTS OrderHistory;
DROP TABLE IF EXISTS CartHistory;

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

CREATE TABLE Users
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR(255) NOT NULL UNIQUE,
	Password VARCHAR(255) NOT NULL,
	Name TEXT,
	Email TEXT,
	Phone TEXT,
	Billing_country TEXT,
	Billing_zipcode TEXT,
	Billing_city TEXT,
	Billing_street TEXT,
	Billing_house_number TEXT,
	Shipping_country TEXT,
	Shipping_zipcode TEXT,
	Shipping_city TEXT,
	Shipping_street TEXT,
	Shipping_house_number TEXT
);

CREATE TABLE ShoppingCart
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Product_id INT,
	User_id INT
);

CREATE TABLE OrderHistory
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Order_date DateTime,
	Order_status TEXT,
	Total_price Decimal(20,0),
	User_id INT
);

CREATE TABLE CartHistory
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Product_id INT,
	Order_history_id INT
);

ALTER TABLE OrderHistory
	DROP CONSTRAINT IF EXISTS FK_OrderHistory_User_id;

ALTER TABLE CartHistory
	DROP CONSTRAINT IF EXISTS FK_CartHistory_Product_id;

ALTER TABLE CartHistory
	DROP CONSTRAINT IF EXISTS FK_CartHistory_Order_history_id;

ALTER TABLE Product
	DROP CONSTRAINT IF EXISTS FK_Product_ProductCategory;

ALTER TABLE Product
	DROP CONSTRAINT IF EXISTS FK_Product_Supplier;

ALTER TABLE ShoppingCart
	DROP CONSTRAINT IF EXISTS FK_ShoppingCart_User_id;

ALTER TABLE ShoppingCart
	DROP CONSTRAINT IF EXISTS FK_ShoppingCart_Product_id;

ALTER TABLE OrderHistory
	ADD CONSTRAINT FK_OrderHistory_User_id FOREIGN KEY (User_id)
	REFERENCES Users (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

ALTER TABLE CartHistory
	ADD CONSTRAINT FK_CartHistory_Product_id FOREIGN KEY (Product_id)
	REFERENCES Product (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

ALTER TABLE CartHistory
	ADD CONSTRAINT FK_CartHistory_Order_history_id FOREIGN KEY (Order_history_id)
	REFERENCES OrderHistory (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

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

ALTER TABLE ShoppingCart
	ADD CONSTRAINT FK_ShoppingCart_User_id FOREIGN KEY (User_id)
	REFERENCES Users (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

ALTER TABLE ShoppingCart
	ADD CONSTRAINT FK_ShoppingCart_Product_id FOREIGN KEY (Product_id)
	REFERENCES Product (Id)
	ON DELETE CASCADE
    ON UPDATE CASCADE
;

INSERT INTO Supplier VALUES ('Amazon', 'Digital content and services');
INSERT INTO Supplier VALUES ('Greek Hand Made Company', 'Hand crafted items directly from Greece');
INSERT INTO Supplier VALUES ('Fancy Dress Central', 'Fancy dresses for all your parties');
INSERT INTO Supplier VALUES ('Ancient Times GmbH', 'Finest weapons from Alemania');

INSERT INTO ProductCategory VALUES ('Chakram', 'Weapon', 'Round shaped weapon that works like a frizby');
INSERT INTO ProductCategory VALUES ('Crossbow', 'Weapon', 'A bow for people who like to hold heavy weapons with two hands');
INSERT INTO ProductCategory VALUES ('Dagger', 'Weapon', 'A fine piece of weapon suitable for a fine fighter');
INSERT INTO ProductCategory VALUES ('Shield', 'Armour', 'Something to protect you against your foes');
INSERT INTO ProductCategory VALUES ('Sword', 'Weapon', 'A weapon for a real man');
INSERT INTO ProductCategory VALUES ('Nunchaku', 'Weapon', 'You don''t want to be hit by this');
INSERT INTO ProductCategory VALUES ('Ship', 'Vehicle', 'A first class method of transportation');
INSERT INTO ProductCategory VALUES ('Spear', 'Weapon', 'If you want to reach far, but stay protected at the same time');
INSERT INTO ProductCategory VALUES ('Helmet', 'Armour', 'For your fine head');
INSERT INTO ProductCategory VALUES ('Mace', 'Weapon', 'Weapon for a tough guy');
INSERT INTO ProductCategory VALUES ('Axe', 'Weapon', 'Weapon for a tough guy');
INSERT INTO ProductCategory VALUES ('Armour', 'Armour', 'A fine piece of body wear to protect from all your foes');
INSERT INTO ProductCategory VALUES ('Sandal', 'Footwear', 'Great footwear for even the longest fights');
INSERT INTO ProductCategory VALUES ('Various items', 'Other', 'Various items');
INSERT INTO ProductCategory VALUES ('Trident', 'Weapon', 'A weapon fit for a God');

INSERT INTO Product VALUES ('LMBTQ Chakram', 149.99, 'chakram-lmbtq.jpg', 'A chakram fit for anyone regardless of belief or sexual orientation', 1, 1);
INSERT INTO Product VALUES ('Xena Chakram Alt', 179.99, 'chakram-xena2.png', 'The perfect replica of Xena''s most precious weapon', 1, 2);
INSERT INTO Product VALUES ('Xena Chakram', 9999.99, 'chakram-xena.png', 'The original from the warrior princess herself... A once in a lifetime opportunity', 1, 2);
INSERT INTO Product VALUES ('Metallic Chakram', 169.99, 'chakram-metallic.png', 'Finished with a metallic coat to make it shinier than anything else out there', 1, 4);
INSERT INTO Product VALUES ('Crossbow', 619.99, 'crossbow1.jpg', 'Even fit for a wookie', 2, 2);
INSERT INTO Product VALUES ('Crossbow', 599.99, 'crossbow2.jpg', 'A crossbow for a hunter like you', 2, 1);
INSERT INTO Product VALUES ('Crossbow', 599.99, 'crossbow3.jpg', 'A crossbow for a hunter like you', 2, 4);
INSERT INTO Product VALUES ('Dagger', 349.99, 'dagger-sword.png', 'A dagger that is mandatory in any collector''s collection', 3, 2);
INSERT INTO Product VALUES ('Gastraphetes', 649.99, 'gastraphetes.png', 'Crossbow fit for a god', 2, 2);
INSERT INTO Product VALUES ('Gastraphetes', 799.99, 'gastraphetes2.jpg', 'The crossbow of Hermes', 2, 4);
INSERT INTO Product VALUES ('Hoplon', 249.99, 'hoplon.png', 'A beautiful red shield with a blue warrior just like you', 4, 2);
INSERT INTO Product VALUES ('Kopis', 449.99, 'kopis.png', 'A fine kopis from the times of the persian war', 5, 1);
INSERT INTO Product VALUES ('Kopis of Alexander the Great', 7999.99, 'kopis-alexander-the-great.png', 'The kopis of the Great Alexander himself', 5, 2);
INSERT INTO Product VALUES ('Leonidas'' warrior knife', 4990.00, 'leonidas-warrior-knife.jpg', 'The knife of the great warrior Leonidas', 3, 2);
INSERT INTO Product VALUES ('Nunchaku', 239.99, 'nunchaku1.png', 'Do you like to show off? This is your weapon', 6, 2);
INSERT INTO Product VALUES ('Nunchaku', 229.99, 'nunchaku2.png', 'Nunchaku from a Godess', 6, 1);
INSERT INTO Product VALUES ('Ship', 39990.00, 'ship1.png', 'The biggest ship you can find around Greece', 7, 2);
INSERT INTO Product VALUES ('Ship', 26660.66, 'ship2.png', 'You need to get anywhere? This is your Nr.1 choice', 7, 2);
INSERT INTO Product VALUES ('Persian Ship', 34990.00, 'ship-persian.png', 'Fine piece of art directly from Persia', 7, 1);
INSERT INTO Product VALUES ('Spartan Army Bowie Knife', 210.00, 'spartan-army-bowie-knife.jpg', 'Army Knife fit for a Spartan', 3, 2);
INSERT INTO Product VALUES ('Xiphos', 259.88, 'xiphos.png', 'If your wife likes shiny things, this is the perfect gift for her... and she can even protect herself with it', 3, 2);
INSERT INTO Product VALUES ('Spartan Army Spear', 399.99, 'spartan-army-spear.jpg', 'Army Spear fit for a Spartan', 8, 2);
INSERT INTO Product VALUES ('Red Hoplon', 259.99, 'spartan-hoplon.png', 'Hoplon fit for a Spartan', 4, 2);
INSERT INTO Product VALUES ('Red and Yellow Hoplon', 259.99, 'spartan-hoplon2.png', 'Hoplon fit for a Spartan', 4, 2);
INSERT INTO Product VALUES ('Scorpion Hoplon', 259.99, 'spartan-hoplon3.png', 'Hoplon with a beautiful Scorpion', 4, 2);
INSERT INTO Product VALUES ('Spartan Spear', 359.99, 'spartan-spear.png', 'Army Spear fit for a Spartan', 8, 2);
INSERT INTO Product VALUES ('Double Sided Spear', 349.99, 'spear-double-sided.png', 'Spear that will never leave you in trouble', 8, 4);
INSERT INTO Product VALUES ('Helmet', 174.99, 'greekHelmet.jpg', 'Show off your style with this helmet', 9, 1);
INSERT INTO Product VALUES ('Helmet', 199.99, 'greekHelmet2.jpg', 'You''ll pass as a Roman with this helmet', 9, 1);
INSERT INTO Product VALUES ('Helmet', 179.99, 'greekHelmet3.jpg', 'A helmet fit for a god', 9, 2);
INSERT INTO Product VALUES ('Sword', 819.99, 'sword1.png', 'A simple sword that will never fail you', 5, 2);
INSERT INTO Product VALUES ('Sword', 777.77, 'sword2.jpg', 'A sword will your choice of inscription', 5, 4);
INSERT INTO Product VALUES ('Sword', 888.88, 'sword3.jpg', 'A sword fit for a king', 5, 2);
INSERT INTO Product VALUES ('Sword', 888.88, 'sword4.jpg', 'Mercenary sword', 5, 4);
INSERT INTO Product VALUES ('Ship', 32990.00, 'ship3.jpg', 'A ship for long distance journeys', 7, 2);
INSERT INTO Product VALUES ('Ship', 24990.00, 'ship4.jpg', 'Poseidon will protect you when you sail with beast', 7, 2);
INSERT INTO Product VALUES ('Ship', 29990.00, 'ship5.jpg', 'Nothing can stand in the way of your flagship', 7, 1);
INSERT INTO Product VALUES ('Spear', 345.00, 'spear.jpg', 'A simple spear that will never fail you', 8, 1);
INSERT INTO Product VALUES ('Spear', 339.99, 'spear2.jpg', 'A simple spear that will never fail you', 8, 4);
INSERT INTO Product VALUES ('Spear', 349.99, 'spear3.jpg', 'The spear of Athena', 8, 2);
INSERT INTO Product VALUES ('Spear', 339.99, 'spear4.jpg', 'Throw this and it will always hit your target', 8, 4);
INSERT INTO Product VALUES ('Spear', 319.99, 'spear5.jpg', 'Is it an axe? Is it a spear? It''s both... It''s a spearaxe!!!', 8, 4);
INSERT INTO Product VALUES ('Spear', 319.99, 'spear6.jpg', 'A simple yet perfect addition to your collection of spears', 8, 2);
INSERT INTO Product VALUES ('Mace', 479.99, 'mace.jpg', 'The finest mace out there', 10, 1);
INSERT INTO Product VALUES ('Mace', 489.99, 'mace2.jpg', 'For a tough guy like you', 10, 4);
INSERT INTO Product VALUES ('Mace', 479.99, 'mace3.jpg', 'When you like to kill with shiny stuff', 10, 2);
INSERT INTO Product VALUES ('Axe', 579.99, 'axe.jpg', 'Find your magic', 11, 4);
INSERT INTO Product VALUES ('Axe of Hephaistos', 8999.99, 'axe-of-hephaistos.jpg', 'An Axe forged by Hephaistos himself', 11, 2);
INSERT INTO Product VALUES ('Shield', 259.99, 'shield1.jpg', 'Protect yourself with Poseidon''s shield', 4, 1);
INSERT INTO Product VALUES ('Shield', 299.99, 'shield2.jpg', 'A shield for a golden personality like you', 4, 4);
INSERT INTO Product VALUES ('Shield', 289.99, 'shield3.jpg', 'Nothing to fear when using this shield', 4, 2);
INSERT INTO Product VALUES ('Shield', 250.00, 'shield4.jpg', 'The sun god will protect you', 4, 2);
INSERT INTO Product VALUES ('Armour', 450.00, 'armour.jpg', 'Leather armour for your personal protection', 12, 1);
INSERT INTO Product VALUES ('Complete Armour', 520.00, 'complete-armour.jpg', 'A fine piece of armour directly from Hispania', 12, 4);
INSERT INTO Product VALUES ('Complete Armour', 579.99, 'complete-armour2.jpg', 'The complete set that cannot be missing from your closet', 12, 4);
INSERT INTO Product VALUES ('Complete Armour', 580.00, 'complete-armour3.jpg', 'You''ll look like a Roman Centurion in this fine piece of armour', 12, 2);
INSERT INTO Product VALUES ('Complete Armour', 559.99, 'complete-armour4.jpg', 'Dress yourself in gold all over', 12, 2);
INSERT INTO Product VALUES ('Helmet', 173.29, 'helmet.jpg', 'Stylish, but protective at the same time', 9, 2);
INSERT INTO Product VALUES ('Sandal', 198.88, 'sandal.jpg', 'Dress like the god Hermes', 13, 1);
INSERT INTO Product VALUES ('Sandal', 210.00, 'sandal2.jpg', 'Stylish everyday, but still perfect for those fights', 13, 1);
INSERT INTO Product VALUES ('Armour', 449.99, 'armour2.jpg', 'A simple looking armour, but perfect for your personal protection', 12, 4);
INSERT INTO Product VALUES ('Armour', 459.99, 'armour3.jpg', 'Don''t have big muscles? When you wear this, nobody will care', 12, 4);
INSERT INTO Product VALUES ('Armour', 459.99, 'armour4.jpg', 'A golden armour to show off your style', 12, 2);
INSERT INTO Product VALUES ('Xena Fancy Dress', 399.99, 'xena-fancy-dress.jpg', 'If you want to look like the Warrior Princess, you won''t find a better outfit than this', 14, 3);
INSERT INTO Product VALUES ('Roman Fancy Dress', 349.99, 'roman-fancy-dress.jpg', 'Dress like a real Roman to infiltrate their troops without being discovered', 14, 3);
INSERT INTO Product VALUES ('Ouzo 1l', 299.99, 'ouzo.jpg', 'For those nights after your victories', 14, 1);
INSERT INTO Product VALUES ('Vase', 339.99, 'vase.jpg', 'The perfect container for your ale or milk', 14, 2);
INSERT INTO Product VALUES ('Ancient Greek Phone', 88.88, 'ancient-greek-phone.jpg', 'It''s been around for centuries and it''s still the best out there', 14, 2);
INSERT INTO Product VALUES ('Trident', 559.49, 'trident.jpg', 'A simple trident that will serve you well in all your fights', 15, 1);
INSERT INTO Product VALUES ('Trident of Poseidon', 9999.00, 'trident2.jpg', 'The Trident of Poseidon, the god of the seas, himself', 15, 2);
INSERT INTO Product VALUES ('Trident', 649.99, 'trident3.jpg', 'A fine piece of trident for the collectors', 15, 4);

INSERT INTO Users VALUES ('admin', 'admin', '', '', '', '', '', '', '', '', '', '', '', '', '');
INSERT INTO Users VALUES ('user1', '1234', '', '', '', '', '', '', '', '', '', '', '', '', '');

INSERT INTO BillingAddress VALUES ('Hungary', '1234', 'Budapest', 'Kossuth Lajos utca', '55', 1);
INSERT INTO BillingAddress VALUES ('USA', '123456', 'New York', 'Fifth Avenue', '5555', 2);

INSERT INTO ShippingAddress VALUES ('Hungary', '1234', 'Budapest', 'Kossuth Lajos utca', '55', 1);
INSERT INTO ShippingAddress VALUES ('Greece', '10675', 'Athens', 'Agamemnon Street', '555', 2);

SELECT * FROM Supplier;
SELECT * FROM Product;
SELECT * FROM ProductCategory;
SELECT * FROM Users;
SELECT * FROM ShoppingCart;
SELECT * FROM OrderHistory;
