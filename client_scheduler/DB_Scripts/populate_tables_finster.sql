-- insert data into tables
-- usa id = 1
-- england = 2

INSERT INTO country (
	country,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES 
	("USA","2025-06-02", "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ("England", "2025-06-02", "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster")
;

INSERT INTO city (
	city,
    countryId,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES 
	("Phoenix, Arizona", 1, "2025-06-02", "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ("New York, New York", 1, "2025-06-02", "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ("London", 2, "2025-06-02", "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster")
;

INSERT INTO address (
	address,
    address2,
    cityId,
    postalCode,
    phone,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES 
	(
		"101 Main St",
        "Apartment 4",
		1,
        "85001",
        "555-555-5555",
        "2025-06-02", 
        "Chris Finster", 
        CURRENT_TIMESTAMP, 
        "Chris Finster"
	),
    (
		"562 Main St",
        "Suit 4",
		1,
        "85001",
        "555-555-4444",
        "2025-06-02", 
        "Chris Finster", 
        CURRENT_TIMESTAMP, 
        "Chris Finster"
	),
    (
		"101 State St",
        "Apartment B2",
		2,
        "07008",
        "555-555-3333",
        "2025-06-02", 
        "Chris Finster", 
        CURRENT_TIMESTAMP, 
        "Chris Finster"
	),
    (
		"444 Queen Ave",
        null,
		3,
        "E1 7DB",
        "020-7777-7777",
        "2025-06-02", 
        "Chris Finster", 
        CURRENT_TIMESTAMP, 
        "Chris Finster"
	)
;

INSERT INTO customer (
	customerName,
    addressId,
    active,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES
	( "John Doe", 1, 0, CURRENT_DATE, "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ( "Jane Doe", 2, 1, CURRENT_DATE, "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ( "Joe Schmo", 3, 0, CURRENT_DATE, "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster"),
    ( "Lucy Who", 4, 0, CURRENT_DATE, "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster")
;

INSERT INTO user (
	userName,
    password,
    active,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES
	("test", "test", 0, CURRENT_DATE, "Chris Finster", CURRENT_TIMESTAMP, "Chris Finster")
;

INSERT INTO appointment (
	customerId,
    userId,
    title,
    description,
    location,
    contact,
    type,
    url,
    start,
    end,
    createDate,
    createdBy,
    lastUpdate,
    lastUpdateBy
)
VALUES
	(
		1, 
		1, 
        "Test appointment One", 
        "A test of the appointment system", 
        "Phoenix, Arizona", 
        "555-555-5555",
        "Consultation",
        "https://www.google.com",
        "2025:09:13 12:00:00",
        DATE_ADD("2025:09:13 12:00:00", INTERVAL 15 MINUTE),
        CURRENT_DATE,
        "Chris Finster",
        CURRENT_TIMESTAMP,
        "Chris Finster"
	)
;

-- SELECT * from country;
-- SELECT * from city;
-- SELECT * from address;