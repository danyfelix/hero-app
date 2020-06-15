README
---------------------------------------------------
1) All the decisions that I made along the project.
-----------------------------------------------------------------------------------------------------
	I decided to:
	a) Implement the application with Visual Studio Enterprise 2017 because I already had it installed.
	b) Implement the DB with SQL Server Management Studio v17.7 because I also already had it installed and working.
	c) Start with the DB as per the Entity Framework Data Base First implementation technique.
		- Here I created the two tables: 1) Hero 2) RatingHistorial (one to many relationship).
		- Later on I created the table: 3) ExceptionLog (to troubleshoot problems while deploying to Azure Cloud).
	d) Then continued with the code, create a new project then run the EF commands to create the Model etc.
	e) Started modifying the already existing MVC template (C#,Razor,HTML,CSS,Bootstrap,etc).
	f) All was done in the HomeController.cs and Index.cshtml at first.
		- I decided to add the functionality of checking the DB if there were any existing heroes, otherwise
		  needed to Generate all the Hero data objects with their information and with their respective
		  Ratings, with randomly generated numbers, and then save all of this to the SQL tables.
	g) Then I created a new Action called ListHeroRatings with its respective View and started developing it.
		- When listing the heroes I saw that the requirements asked to order the heroes in descendingly order
		  of it's median score, and the rating of the hero asked to be shown in the list didn't say anywhere
		  that his needed to be the average/median, so I had deducted this was just the "current" or "last" rating,
		  but then it didn't make any sense to order them if there was not going to be any way of noticing
		  why a certain hero was at the top or why one was lower than the other, so I decided by deduction
		  that the column "rating of hero" should be the average/median score so that one could notice the reason
		  for this order. In a case of a serious office scenario where I would have requirments which are not
		  clear or specific like these ones, I would need to ask the person who wrote it, to see if there
		  was a mistake or if they indeed wanted to show a current rating or something else. Right now for the time
		  sake and being that this is just an excercise I wil leave the columns as the average.
	h) Downloaded from Google the hero images to be used for each hero: 
		1. Snake
		2. Maximus
		3. Hercules
		4. Hades
		5. Zeus
		6. Marcus Aurelius
		7. Cyclops
		8. Link
		9. Wolverine
		10.Colossus
	i) Save the images as byte arrays in the "Picture" field of Hero DB Table (data type Image)
	   For this I needed to create helper methods to convert the image resources to byte array and then save them
	   along with the respective hero and when retrieving the data from the DB another method for parsing the 
	   byte array data into an encoded 64 string to be able to display it in the browser as Content. This approach
	   Worked at first but then I decided that it was "too much" and I decided to just save the image relative path
	   as a string in the DB and then just use the path to retrieve the image as a content resource from the solution
	   when displaying it in the view.
	j) Everything worked fine locally but when deploying it to Azure and clicking the button it was redirecting me to the 
	   Error page with no info displayed, thus I decided to create the ExceptionLog table and started implementing
	   the log mechanism in C# code to catch and save the error, to be able to know what was happening. This let
	   me know that is was a the parameter MultipleActiveSetResults in the connection string config that needed to be set
	   to "True" to work fine.
	k) Then "Clean up" and refactor the code and arrange everything into separate methods/classes/files
	   added comments, documentation, etc.
	l) Then I remembered that I must also have taken the TDD approach. But as I had limited amount of time I decided
	   to just start directly coding the main functionality. So I just decided to start creating this Readme files
	   with instructions and descriptions about the app, and if I got any time left I would then create some Unit Tests
	   for the main parts of the functionality requirements.
	m) Then after all was done, last but not least, version the code and upload to my GitHub account repository.
	
	   
2) Walkthrought the project structure.
----------------------------------------------------------------------------------
	The app structure is very simple, it consists of:
	a) The EFModel.edmx which contains the entity mappings of the DB tables.
		1. Hero Table Entity
		2. RatingHistorial Table Entity
		3. ExceptionLog Table Entity
	b) Withing the Resources folder of the Solution I have my 10 Hero JPG images:
		1. Snake
		2. Maximus
		3. Hercules
		4. Hades
		5. Zeus
		6. Marcus Aurelius
		7. Cyclops
		8. Link
		9. Wolverine
		10.Colossus
	c) Withing Controllers folder I have only the HomeController which contains:
		1. The properties and private members section.
		2. The actions: Index and ListHeroRatings
		3. The controller action helpers, the methods which do the dirty but specific job.
		4. The LogException area whose job is to catch, handle and log exceptions for the controller.
	d) In the Models section we got the:
		1. HeroViewModel.cs (To map the Hero entity and display its info in the view)
		2. RatingViewModel.cs (To map the RatingHistorial entity info and map it as a list within each of the HeroViewModels and display it inside the view as the past ratings historial).
	e) As the Views I'm only using:
		1. Index.cshtml (To display the centered button)
		2. ListHeroRatings.cshtml (To list the heroes with their pictures and ratings in order)
	f) Withing the Helpers and Loggers and Filters folders I have also the rest of the methods which I refactored out
	   from the HomeController to help me do the dirty work of calculations and filtering errors and logging etc.
	g) Also if I finish this on time, I might be including the TestProject with the respective unit tests
	   to make sure everything is working as expected, as a kind of a contract to manage and verify for further modifications
	   or enhancements to this app.
	   
3) Steps to deploy the app.
---------------------------------------------------------------
	a) Create the SQL Server in Azure.
	b) Create the SQL DataBase inside that server.
	c) Generate the SQL Schema script to run in the cloud DB using the query editor to generate the DB tables.
	d) Run the script in the SQL Database.
	e) Create the web app service for publishing the app inside the same resource where the DB was created.
	f) Download the publish profile settings file to just import it in Visual Studio while publishing.
	g) Deploy the HeroApp by right clicking the project and clicking "Publish"
	h) Use the downloaded publish profile file to load the data of where the app will be published in Azure.
	i) Test the conectivity with the given functionality.
	j) Go to the Azure account and inside the SQL DB Server settings copy the given Connection Strings.
	k) Use the copied connections strings to past it in the Settings section of the publishing process, you needed to use
	   your userId/password here, which you used in the creation of the database/server etc.
	l) Modify the connection strings accordingly, in case you need to do any other modifications.
	m) Finally click Save and then click Publish, Visual Studio will take care of everything else.
	n) A browser window will open in the URL where your application is published and your app will be displayed.
	
That was all.
Thanks!!!
	
	