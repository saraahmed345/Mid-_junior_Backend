# Mid-_junior_Backend
In this exercise i designed an API for a vending machine, allowing users
with a “seller” role to add, update or remove products, while users with a “buyer” role
can deposit coins into the machine and make purchases. Your vending machine
should only accept 5, 10, 20, 50 and 100 cent coins
Tasks
● REST API  implemented consuming and producing
“application/json”
● product model with amountAvailable, cost, productName and
sellerId fields Implemented
● user model with username, password, deposit and role fields Implemented
● CRUD for users (POST shouldn’t require authentication) Implemented
● CRUD for a product model (GET can be called by anyone, while
POST, PUT and DELETE can be called only by the seller user who created the
product) Implemented

● deposit endpoint so users with a “buyer” role can deposit 5, 10,
20, 50 and 100 cent coins into their vending machine account implemented
● buy endpoint (accepts productId, amount of products) so users
with a “buyer” role can buy products with the money they’ve deposited. API
should return total they’ve spent, products they’ve purchased and their
change if there’s any (in 5, 10, 20, 50 and 100 cent coins) implemented
● reset endpoint so users with a “buyer” role can reset their
deposit implemented


















Running in Visual Studio:

1. Build the Project:

Right-click the project in Solution Explorer and select "Build".
2. Start Debugging:

Press F5 or click the "Start Debugging" button in the toolbar.
The API will start running, typically on https://localhost:5001 (or a specified port).
3. Test Endpoints:

Use a REST client (e.g., Postman, curl) to make requests to the API endpoints.
Provide authentication headers for protected endpoints.
Observe responses and logs to verify functionality.
Additional Considerations:

Consider using a database for persistent data storage.
Explore advanced features like input validation, filtering, and pagination.
Implement security measures like input sanitization and rate limiting.
Deploy the API to a hosting environment for production use.
