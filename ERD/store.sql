
CREATE TABLE Customer
(
  created_at timestamp,
  updated_at timestamp,
  CustomerID varchar   NOT NULL,
  Name       varchar   NOT NULL,
  Surname    varchar   NOT NULL,
  Email      varchar   NOT NULL,
  Password   varchar   NOT NULL,
  Phone      varchar  ,
  Status     boolean   NOT NULL,
  PRIMARY KEY (CustomerID)
);

COMMENT ON TABLE Customer IS 'Customer Table';

CREATE TABLE CustomerProduct
(
  created_at timestamp,
  updated_at timestamp,
  CPID       varchar   NOT NULL,
  CustomerID varchar   NOT NULL,
  ProductID  varchar   NOT NULL,
  PRIMARY KEY (CPID)
);

COMMENT ON TABLE CustomerProduct IS 'Products Selected By Customer';

CREATE TABLE Order
(
  updated_at timestamp,
  OrderDate  date      NOT NULL,
  CustomerID varchar   NOT NULL,
  OrderID    varchar   NOT NULL,
  Status     boolean   NOT NULL,
  CPID       varchar   NOT NULL,
  PRIMARY KEY (OrderID)
);

COMMENT ON TABLE Order IS 'Order Table';

CREATE TABLE Product
(
  created_at timestamp,
  updated_at timestamp,
  ProductID  varchar   NOT NULL,
  Name       varchar   NOT NULL,
  Desc       varchar  ,
  Price      decimal   NOT NULL,
  Status     boolean   NOT NULL,
  PDTypeID   varchar   NOT NULL,
  PRIMARY KEY (ProductID)
);

COMMENT ON TABLE Product IS 'Product Table';

CREATE TABLE ProductType
(
  created_at timestamp,
  updated_at timestamp,
  PDTypeID   varchar   NOT NULL,
  Category   varghar   NOT NULL,
  PRIMARY KEY (PDTypeID)
);

COMMENT ON TABLE ProductType IS 'Product Type Table';

ALTER TABLE Product
  ADD CONSTRAINT FK_ProductType_TO_Product
    FOREIGN KEY (PDTypeID)
    REFERENCES ProductType (PDTypeID);

ALTER TABLE CustomerProduct
  ADD CONSTRAINT FK_Customer_TO_CustomerProduct
    FOREIGN KEY (CustomerID)
    REFERENCES Customer (CustomerID);

ALTER TABLE Order
  ADD CONSTRAINT FK_CustomerProduct_TO_Order
    FOREIGN KEY (CPID)
    REFERENCES CustomerProduct (CPID);

ALTER TABLE CustomerProduct
  ADD CONSTRAINT FK_Product_TO_CustomerProduct
    FOREIGN KEY (ProductID)
    REFERENCES Product (ProductID);

CREATE UNIQUE INDEX Product
  ON Product (ProductID ASC);

CREATE UNIQUE INDEX Customer
  ON Customer (CustomerID ASC);

CREATE UNIQUE INDEX Order
  ON Order (OrderID ASC);
