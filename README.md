The goal is to maintain data integrity and enable safe incremntals without risking data. 

To do that I have chosen non-destructive approach for my schema changes. 
In V2 Renaming Grade to Final Grade the migrationBuilder.RenameCloumn changes the metadata. It maps to sp_rename in SQL server which I used for State-Based. 
The data is retained in place.
SQL server supports rename natively so no need to adding a new column and dropping the old one. (Destructive pattern) 

For modifying Course Credits data type. EF uses AlterColumn which is ALTER TABLE in SQL. 
For modifying Int to Decimal there is no lose of data because integer maps exactly to a decimal value. So changing it decimal doesn't result in data. It always keeps foreign key and indexes values untouched.
A destructive variant would be drop column and add a new one. But that require temporaily removing constraints and recreating them. There is risk of accidental data loss if not copied properly.

