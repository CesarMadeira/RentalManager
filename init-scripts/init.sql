
/*
CREATE TABLE motorcycle (
    identifier VARCHAR(100),
    licence_plate VARCHAR(100),
    model VARCHAR(100),
    year int
);
CREATE TABLE delivery_person (
    id VARCHAR(100),
    name VARCHAR(200),
    cnpj VARCHAR(200),
    date_of_birth date,
    document_number VARCHAR(100),
    document_type VARCHAR(2),
    document_image VARCHAR(100)
);
CREATE TABLE rent (
    id VARCHAR(100),
    delivery_person_id VARCHAR(100),
    motorcycle_id VARCHAR(100),
    start date,
	finish date,
	end_forecast date,
    plan int
);
*/


-- delete from delivery_person
select * from delivery_person;

--delete from motorcycle
select * from motorcycle;

-- delete from rent
select * from rent;



