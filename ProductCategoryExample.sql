CREATE TABLE Product
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NAME NCHAR(50) NOT NULL
);

CREATE TABLE Category
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	NAME NCHAR(50) NOT NULL
);

CREATE TABLE ProductToCategory
(
	ProductId INT NOT NULL,
	CategoryId INT NOT NULL,
	FOREIGN KEY(ProductId) REFERENCES Product(Id),
	FOREIGN KEY(CategoryId) REFERENCES Category(Id),
	PRIMARY KEY CLUSTERED (ProductId, CategoryId)
);

INSERT INTO Product(NAME)
VALUES (N'Potato'),(N'Wrench'),(N'Bees');

INSERT INTO Category(NAME)
VALUES (N'Vegetables'),(N'Tools')

DECLARE @PotatoId  AS NCHAR(50),
		@WrenchId AS NCHAR(50),
		@VegetablesId  AS NCHAR(50),
		@ToolsId AS NCHAR(50);


SELECT @PotatoId = ID 
FROM Product
WHERE NAME LIKE N'%Potato%';


SELECT @WrenchID = ID 
FROM Product
WHERE NAME LIKE N'%Wrench%';

SELECT @VegetablesId = ID 
FROM Category
WHERE NAME LIKE N'%Vegetables%';

SELECT @ToolsId = ID 
FROM Category
WHERE NAME LIKE N'%Tools%';

INSERT INTO ProductToCategory(ProductId,CategoryId)
VALUES (@PotatoId,@VegetablesId),(@WrenchId,@ToolsId);

SELECT
	P.NAME AS 'Product name',
	C.NAME AS 'Category name'
FROM Product AS P
LEFT OUTER JOIN ProductToCategory AS PC
	ON P.ID = PC.ProductId
LEFT OUTER JOIN Category AS C
	ON PC.CategoryId = C.ID;
