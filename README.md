# TestCarAPI

## List of endpoints

### Car endpoints

**GET /api/Car** - Get a a full list of cars.

**GET /api/Car/{slice}** - Get a list of cars between 2 indexes. 

**GET /api/Car/{carId}/info** - Get information about a specific car by id.

**POST /api/Car** - Add a new car to the database.

**PUT /api/Car/{carId}** - Update an existing car in the databse.

**DELETE /api/Car/{carId}** - Delete car by id.

### Login endpoints

**POST /api/Login** - Endpoint for JWT authorization.
