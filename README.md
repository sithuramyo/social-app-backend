# Step One
- run docker-compose file using cmd 'docker-compose up -d'
  
# Step Two
- Change Stage value as 1 in appsetting.json(1 defined as default,2 defined as local)
  
# Step Three
- Login to mysql database(recommend use mysql workbench)
- Host_Name => localhost
- Port => 3307
- User => socialuser
- Password => socialpassword

# Step Four
- Run table script
  
  For Tbl_Users
```sql
CREATE TABLE `Tbl_Users` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DateOfBirth` datetime(6) NOT NULL,
  `ProfileImagePath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `CreatedUser` bigint DEFAULT NULL,
  `ModifiedDate` datetime(6) DEFAULT NULL,
  `ModifiedUser` bigint DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```

  For Tbl_Login
```sql
CREATE TABLE `Tbl_Login` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AccessToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshTokenExpires` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```
# Step Five
## API Documentation
- [Database Health Check](#health-check)
- [Users Register](#users-register)
- [Login](#login)


 ### Health Check
 #### Health means database connection can connect with server
 ```js
 GET http://localhost:8081/health
 ```
 #### Request
 ##### No need

 #### Response
 ##### If database is healthy
 ```js
 {
  "status": "Healthy",
  "totalDuration": "00:00:00.6618975",
  "entries": {
    "mysql": {
      "data": {},
      "duration": "00:00:00.6553515",
      "status": "Healthy",
      "tags": []
    }
  }
 }
 ```
 ##### If database is unhealthy
 ```js
 {
  "status": "Unhealthy",
  "totalDuration": "00:00:00.1178625",
  "entries": {
    "mysql": {
       "data": {},
       "description": "Unable to connect to any of the specified MySQL hosts.",
       "duration": "00:00:00.1023617",
       "exception": "Unable to connect to any of the specified MySQL hosts.",
       "status": "Unhealthy",
       "tags": []
      }
   }
 }
 ```

 ### Users Register
 ```js
 POST http://localhost:8081/authapi/users/users-register
 ```
 #### Request
 ```json
 {
    "name" : "string",
    "email" : "string@gmail.com",
    "password" : "string",
    "dateOfBirth" : "2024-05-24",
    "profileImage" : ""
 }
 ```
 #### Response
```json

```

 ### Login
 ```js
 POST http://localhost:8081/authapi/users/users-register
 ```
 #### Reqeust
 ```json
 {
    "email" : "string",
    "password" : "string"
 }
 ```
 #### Response
 ```json

 ```
