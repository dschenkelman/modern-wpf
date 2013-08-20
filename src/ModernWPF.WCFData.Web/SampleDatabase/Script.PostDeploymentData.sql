/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE Employee

INSERT INTO Employee(Name, Email, AddressLine1, AddressLine2, IsActive) VALUES ('Person 1', 'person1@contoso.com', 'Address 1', 'Address 2', 1);
INSERT INTO Employee(Name, Email, AddressLine1, AddressLine2, IsActive) VALUES ('Person 2', 'person2@contoso.com', 'Address 1', 'Address 2', 1);
INSERT INTO Employee(Name, Email, AddressLine1, AddressLine2, IsActive) VALUES ('Person 3', 'person3@contoso.com', 'Address 1', 'Address 2', 1);
INSERT INTO Employee(Name, Email, AddressLine1, AddressLine2, IsActive) VALUES ('Person 4', 'person4@contoso.com', 'Address 1', 'Address 2', 0);