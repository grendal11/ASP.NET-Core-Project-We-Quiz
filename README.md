# ASP.NET-Core-Project-We-Quiz

**WeQuiz** is school quiz management system with question bank. The app is with Bulgarian interface.

**There are several types of roles:**
-	**Administrator** – adds and approves schools, approves and denies school admins, approves main categories for the questions, gets statistics for users
-	**School administrator** – manages school users – approves and denies teachers and users, gets statistics
-	**Teacher** – generates, configures tests, can add and remove question categories to himself
-	**Student** – can have tests, offer subcategories
-	**User without school** – can offer subcategories
-	all users except Admin can check and edit their own profile 


**Next steps** (will be implemented soon):
-	**Teacher** – can add questions and approve suggested questions, can get statistics for tests
-	**Student** – can get statistics for own tests and suggested questions, can suggest questions, can access list with own suggested questions with status(approved/denied), can generate and take instant tests
-	**User without school** – can offer subcategories, suggest questions, can generate and take tests with free for all questions


**Test accounts**:
- student@weqiz.bg => student123
- teacher@weqiz.bg => teacher123
- sadmin@weqiz.bg => sadmin123
- admin@weqiz.bg => admin123


**Technology stack:**
-	ASP.NET Core 5.0
-	Entity Framework Core 5.0
-	SQL Server
-	JavaScript
-	jQuery
-	Bootstrap


**DataBase Diagram**:

![WeQuizDbDiagram](https://user-images.githubusercontent.com/40993628/129940049-56b29fb6-18bc-4a06-8320-200284526933.png)

