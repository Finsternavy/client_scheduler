
DROP TABLE IF EXISTS appointment;
DROP TABLE IF EXISTS customer;
DROP TABLE IF EXISTS address;
DROP TABLE IF EXISTS city;
DROP TABLE IF EXISTS country;
DROP TABLE IF EXISTS user;

CREATE TABLE IF NOT EXISTS country (
	countryId INT(10) AUTO_INCREMENT PRIMARY KEY,
    country VARCHAR(50) NOT NULL,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40)
);

CREATE TABLE IF NOT EXISTS city(
	cityId INT(10) AUTO_INCREMENT PRIMARY KEY,
    city VARCHAR(50) NOT NULL,
    countryId INT(10) NOT NULL,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    FOREIGN KEY (countryId) REFERENCES country(countryId)
);

CREATE TABLE IF NOT EXISTS address (
	addressId INT(10) AUTO_INCREMENT  PRIMARY KEY,
    address VARCHAR(50) NOT NULL,
    address2 VARCHAR(50),
    cityId INT(10) NOT NULL,
    postalCode VARCHAR(10),
    phone VARCHAR(20) NOT NULL,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    FOREIGN KEY (cityId) REFERENCES city(cityId)
);

CREATE TABLE IF NOT EXISTS customer(
	customerId INT(10) AUTO_INCREMENT PRIMARY KEY,
    customerName VARCHAR(45) NOT NULL,
    addressId INT(10) NOT NULL,
    active TINYINT(1),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    FOREIGN KEY (addressId) REFERENCES address(addressId)
);

CREATE TABLE IF NOT EXISTS user(
	userId INT(10) AUTO_INCREMENT PRIMARY KEY,
    userName VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    active TINYINT(1),
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40)
);

CREATE TABLE IF NOT EXISTS appointment(
	appointmentId INT(10) AUTO_INCREMENT PRIMARY KEY,
    customerId INT(10) NOT NULL,
    userId INT(10) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    location TEXT,
    contact TEXT,
    type VARCHAR(100) NOT NULL,
    url VARCHAR(255),
    start DATETIME NOT NULL,
    end DATETIME NOT NULL,
    createDate DATETIME,
    createdBy VARCHAR(40),
    lastUpdate TIMESTAMP,
    lastUpdateBy VARCHAR(40),
    FOREIGN KEY (customerId) REFERENCES customer(customerId),
    FOREIGN KEY (userId) REFERENCES user(userId),
    INDEX idx_appointment_time (userId, start, end),
    INDEX idx_appointment_date (start, end)
); 