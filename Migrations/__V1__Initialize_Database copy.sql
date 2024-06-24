-- Create database
CREATE DATABASE CustomFormsDB;

-- Connect to the database
\c  CustomFormsDB;

-- Create CustomForms table
CREATE TABLE CustomForms (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL UNIQUE,
    Description TEXT,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DateUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Create CustomFields table
CREATE TABLE CustomFields (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL UNIQUE,
    Value INT,
    CustomFormId INT NOT NULL,
    FOREIGN KEY (CustomFormId) REFERENCES CustomForms(Id) ON DELETE CASCADE
);

-- Insert sample data
INSERT INTO CustomForms (Title, Description, DateCreated, DateUpdated)
VALUES ('Sample Form', 'This is a sample form.', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- Assuming the ID of the inserted CustomForms row is 1
INSERT INTO CustomFields (Name, Value, CustomFormId)
VALUES ('Sample Field', '123', 1);
