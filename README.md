# Scottish-Parliament-CRM
This project was to practice the idea of someone handing me a SQL Database to build an application around. I decided to the use the data to build a scaled down Client Relationship Manager. 

*Build Database*
1. First create a database called ScottishParliament in VS Studio
2. Run Members.edmx.sql in VS Studio
3. Select Members.edmx, right click members and select update model from database
	- if this fails, then delete the model and select create model from database, follow the wizard to select the members table and finish. 
4. The solution should now build as normal. 

*Application*
You will have access to all CRUD operations using the console applciation. Note the delete is a soft delete, meaning that the data never gets removed from the database. Currently there is no validation on the Add or Update functionality. This will be added later. As of now if you fail to insert the feilds properly then the application will throw an exception. I have done my best to point out how the fields should be filled out to prevent this issue. 
