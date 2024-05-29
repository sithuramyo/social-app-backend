# Using tech stack
- dotnet 8
- EF core 8
- mysql
- smtp
- google drive api


# Step One
- run docker-compose.yml file using cmd 'docker-compose up -d'
  
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
  For Tbl_OtpLog
```sql
CREATE TABLE `Tbl_OtpLog` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Otp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OtpExpires` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```
 For Tbl_Friends
```sql
CREATE TABLE `Tbl_Friends` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserIdOne` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserIdTwo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedDate` datetime(6) NOT NULL,
  `IsUnfriend` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```
For Tbl_FriendShips
```sql
CREATE TABLE `Tbl_FriendShips` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `SenderUserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReceiverUserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FriendShipStatus` int NOT NULL,
  `SendDate` datetime(6) NOT NULL,
  `ApprovedDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
```

# Step Five
## API Documentation
- [Database Health Check](#health-check)
- [Users Register](#users-register)
- [Login](#login)
- [GetOtp](#get-otp)
- [ValidateOtp](#validate-otp)
- [ChangeForgetPassword](#forget-password)
- [SearchFriendList](#search-friend-list)
- [GetFriendList](#get-friend-list)
- [GetFriendSentRequestList](#get-friend-sent-request-list)
- [AddFriendSentRequest](#add-friend-sent-request)
- [ApproveFriendSentRequest](#approve-friend-sent-request)
- [UnFriend](#un-friend)


 ### Health Check
 #### Health means database connection can connect with server

 ##### For authentication database health check
 ```js
 GET http://localhost:8080/authapi/health
 ```

 ##### For social media database health check
 ```js
 GET http://localhost:8080/socialmediaapi/health
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
 POST http://localhost:8080/authapi/users/users-register
 ```
 #### Request
 ```json
  {
   "name" : "sithuramyo",
   "email" : "sithuramyo@gmail.com",
   "password" : "sithuramyo",
   "dateOfBirth" : "2024-05-24",
   "profileImage" : ""
}
 ```
 #### Response
```json
 {
    "accessTokenType": "Bearer",
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI5OTFkMGY5Yi0xMTRlLTQwYTUtODUyNC0xN2UxMTg4ZjYzOWQiLCJleHAiOjE3MTcyMzQzMjR9.j-HLGNe0-YedY_A14-0b0GURr5ISNK5N7jRcEw2897x2u7jp8FTz7YC7dEyK7Z1Ks86bKWPU7tAQ2DYwbK4TZQ",
    "accessTokenExpires": 168,
    "refreshToken": "U5+0UdeXkumO/vNApOkuueM+HaXCKOWy2z+b3I9u2Q1EDiSOnrx1lmBALl7vFo2PkoTWmF6mU5S6hio2EWjvDA==",
    "refreshTokenExpires": 240,
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
}
```

 ### Login
 ```js
 POST http://localhost:8080/authapi/login/users-login
 ```
 #### Request
 ```json
  {
    "email": "sithuramyo@gmail.com",
    "password": "sithuramyo"
  }
 ```
 #### Response
 ```json
 {
    "accessTokenType": "Bearer",
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI5OTFkMGY5Yi0xMTRlLTQwYTUtODUyNC0xN2UxMTg4ZjYzOWQiLCJleHAiOjE3MTcyMzQ0ODN9.m69l9YAX3xYN_4ufYZkfcTLciNJxbn2_GLvuLZRimT6jZJX6p09_a-nRE3suFWdoI_eFgqsuStmDPCAf7FTjhw",
    "accessTokenExpires": 168,
    "refreshToken": "NN3yNjzj+xqRefXQBThA3bfBmbuM71R6XkQPX8FMptHNTdE4R1ccZ6CVg75omjlaMuW7ggAfOkEObW9e/nGEoQ==",
    "refreshTokenExpires": 240,
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
  }
 ```

 ### Get Otp
 ```js
 POST http://localhost:8080/authapi/otp/get-otp
 ```
 #### Request
 ```json
 {
  "email": "sithuramyo@gmail.com"
 }
 ```
 #### Response
 ```json
 {
    "otpExpires": 5,
    "expireType": "Minute",
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
  }
 ```

 ### Validate Otp
 ```js
 POST http://localhost:8080/authapi/otp/validate-otp
 ```
 #### Request
 ```json
 {
  "otpCode": "7253"
 }
 ```
 #### Response
 ```json
 {
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Change Forget Password
 ```js
 POST http://localhost:8080/authapi/users/forget-password
 ```
 ### Request
 ```json
 {
  "email": "sithuramyo@gmail.com",
  "password": "string"
 }
 ```
 ### Response
 ```json
 {
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Search Friend List
 ```js
 POST http://localhost:8080/socialmediaapi/friendships/search-friend-list
 ```
 ### Request
 ```json
 {
    "name" : "kya"
 }
 ```
 ### Response
 ```json
 {
    "friendShipsList": [
        {
            "friendId": "35a114c1-cd01-45b6-ae7d-67559fdf4a4c",
            "name": "kyawkyaw",
            "friendPhotoPath": ""
        },
        {
            "friendId": "1dd57e00-e3b6-467f-abf4-33efb0c5d911",
            "name": "kyawlay",
            "friendPhotoPath": "https://drive.google.com/file/d/1J-VxICZVZZVJ89ZjHTG7XviDgyG7kPym"
        },
        {
            "friendId": "67db8896-d093-4668-81db-064172fcf4e0",
            "name": "kyawkyawlay",
            "friendPhotoPath": "https://drive.google.com/file/d/1fXck6whns1MwEiNexYrmnErdJnvn7OrP"
        },
        {
            "friendId": "77e0ae3b-b111-4aab-be31-d090e87c5c8a",
            "name": "kyawlay",
            "friendPhotoPath": ""
        }
    ],
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Get Friend List
 ```js
 GET http://localhost:8080/socialmediaapi/friendships/get-friend-list
 ```
 ### Request 
 #### No request

 ### Response
 ```json
 {
    "friendShipsList": [],
    "friendCount": 0,
    "response": {
        "responseCode": "W0002",
        "responseDescription": "Data not found",
        "responseType": 3,
        "isError": true
    }
 }
 ```
 ### Get Friend Sent Request List
 ```js
 GET http://localhost:8080/socialmediaapi/friendships/get-friend-sent-request-list
 ```
 ### Request
 #### No request

 ### Response
 ```json
 {
    "addFriendRequestList": [
        {
            "friendId": "1dd57e00-e3b6-467f-abf4-33efb0c5d911",
            "name": "kyawlay",
            "friendPhotoPath": "https://drive.google.com/file/d/1J-VxICZVZZVJ89ZjHTG7XviDgyG7kPym"
        }
    ],
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Add Friend Sent Request
 ```js
 POST http://localhost:8080/socialmediaapi/friendships/add-friend-request-sent
 ```
 ### Request
 ```json
 {
    "friendId": "1dd57e00-e3b6-467f-abf4-33efb0c5d911"
 }
 ```

 ### Response 
 ```json
 {
    "addFriendSentRequestStatus": "Pending",
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Approve Friend Sent Request
 ```js
 POST http://localhost:8080/socialmediaapi/friendships/approve-friend-sent-request
 ```
 ### Request
 ```json
 {
  "friendId": "991d0f9b-114e-40a5-8524-17e1188f639d",
  "isAccept": true
 }
 ```
 ### Response
 ```json
 {
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```

 ### Un Friend
 ```js
 POST http://localhost:8080/socialmediaapi/friendships/un-friend
 ```
 ### Request
 ```json
 {
  "friendId": "b15f953b-b711-4f87-bad7-561c4ed8b82f"
 }
 ```
 ### Response
 ```json
 {
    "response": {
        "responseCode": "S0000",
        "responseDescription": "Success",
        "responseType": 1,
        "isError": false
    }
 }
 ```
