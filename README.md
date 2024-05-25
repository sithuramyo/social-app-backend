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

