-- Check if the database exists
SELECT datname FROM pg_catalog.pg_database WHERE datname = 'CustomFormsDB';

-- If the database does not exist, create it
DO $$ 
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_catalog.pg_database WHERE datname = 'CustomFormsDB') THEN
        CREATE DATABASE CustomFormsDB;
    END IF;
END $$;

-- Connect to the database
\c CustomFormsDB;

-- Create CustomForms table if it does not exist
CREATE TABLE IF NOT EXISTS CustomForms (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL UNIQUE,
    Description TEXT,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DateUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Create CustomFields table if it does not exist
CREATE TABLE IF NOT EXISTS CustomFields (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL UNIQUE,
    Value INT,
    CustomFormId INT NOT NULL,
    FOREIGN KEY (CustomFormId) REFERENCES CustomForms(Id) ON DELETE CASCADE
);

-- Insert sample data if tables are created or exist
INSERT INTO CustomForms (Title, Description, DateCreated, DateUpdated)
VALUES ('Sample Form', 'This is a sample form.', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- Assuming the ID of the inserted CustomForms row is 1
INSERT INTO CustomFields (Name, Value, CustomFormId)
VALUES ('Sample Field', 123, 1);
