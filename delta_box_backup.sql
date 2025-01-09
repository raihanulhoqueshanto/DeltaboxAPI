CREATE DATABASE  IF NOT EXISTS `delta_box` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `delta_box`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: delta_box
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240919103844_InitialCreate','8.0.8');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ads_banner`
--

DROP TABLE IF EXISTS `ads_banner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ads_banner` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `image` varchar(200) DEFAULT NULL,
  `section` varchar(100) DEFAULT NULL,
  `type` varchar(100) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ads_banner`
--

LOCK TABLES `ads_banner` WRITE;
/*!40000 ALTER TABLE `ads_banner` DISABLE KEYS */;
INSERT INTO `ads_banner` VALUES (1,'Banner 1','/Images/Banner/BannerImage/default.png','Hero','Hero Big','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-19 17:30:25','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-19 17:31:02'),(2,'Banner 2','/Images/Banner/BannerImage/default.png','Hero','Hero Big','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-20 15:38:26',NULL,NULL),(3,'Banner 3','/Images/Banner/BannerImage/default.png','Hero','Hero Small','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-20 15:38:42',NULL,NULL),(4,'Banner 4','/Images/Banner/BannerImage/default.png','Hero','Hero Small','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-20 15:38:48',NULL,NULL),(5,'Banner 5','/Images/Banner/BannerImage/default.png','Hero','Hero Small','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-20 15:38:55',NULL,NULL),(6,'Banner 6','/Images/Banner/BannerImage/default.png','Hero','Hero Medium','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-20 15:39:08',NULL,NULL),(7,'test11','/Images/Banner/BannerImage/699ddb31d4dc47cab8edddb46d02ac6f.png','hero','hero small','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-05 14:12:09',NULL,NULL);
/*!40000 ALTER TABLE `ads_banner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `article`
--

DROP TABLE IF EXISTS `article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `writer_name` varchar(100) NOT NULL,
  `category_id` int NOT NULL,
  `image` varchar(200) DEFAULT NULL,
  `description` longtext,
  `short_description` varchar(500) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article`
--

LOCK TABLES `article` WRITE;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` VALUES (1,'First Article','Raihanul Hoque Shanto',0,'/Images/Blog/BlogImage/default.png','Yoooooooooooooooooooooooooooooooooo',NULL,'Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-22 14:14:35','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-22 14:16:02');
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('889e5c2e-bf9d-40d2-bf79-899e21974050','Admin','ADMIN',NULL),('f582b216-df8b-4895-be2c-71059289cb25','User','USER',NULL);
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES ('e8db3664-8bda-41ef-8d9e-16ae89f00e69','889e5c2e-bf9d-40d2-bf79-899e21974050'),('51991cbf-2663-47f5-903b-951cddf1d584','f582b216-df8b-4895-be2c-71059289cb25'),('ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','f582b216-df8b-4895-be2c-71059289cb25');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('51991cbf-2663-47f5-903b-951cddf1d584','Shanto','shanto','SHANTO','raihanul.haque.shanto01@gmail.com','RAIHANUL.HAQUE.SHANTO01@GMAIL.COM',0,'AQAAAAIAAYagAAAAEADwIx+IC6v5CyBsS8vZj4DTAHYYTBsP7+TPMJlNdMaSoitlC84nym4Aj9iplRpiCw==','BX3DUILQ2QMWC5NKKBLHIJ2MUXMGENJR','e9e3f7d0-e40f-47f4-9219-4a7e15ba73d9',NULL,0,0,NULL,1,0),('ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','Mashruf Ehsan','user','USER','user@gmail.com','USER@GMAIL.COM',0,'AQAAAAIAAYagAAAAEEhuiGEQFQVRVAX20w4XM7Jf5ZrmbQhU25XTWKtB5yU1gYQAaszyJT9/EgrM6UHSEQ==','42UUZXCRRYY6GIT2ARO3TWDPLPQ433WL','f499a841-2b12-4842-b76f-2dfc7dbcf491',NULL,0,0,NULL,1,0),('e8db3664-8bda-41ef-8d9e-16ae89f00e69','Raihanul Hoque Shanto','admin','ADMIN','admin@gmail.com','ADMIN@GMAIL.COM',0,'AQAAAAIAAYagAAAAEMjAFPT0haC84tIe0XbIqKSNDB0oUbZG7RDgcJxrYqnGbts1vam/rJbgqPnx7DRHcw==','ATPQYZDKSJ6XFKC5HTZ7RIG2PFRENU75','bc6497d1-4434-47c6-abf3-c798d6d2b656',NULL,0,0,NULL,1,0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `associate_brand`
--

DROP TABLE IF EXISTS `associate_brand`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `associate_brand` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `image` varchar(200) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `associate_brand`
--

LOCK TABLES `associate_brand` WRITE;
/*!40000 ALTER TABLE `associate_brand` DISABLE KEYS */;
INSERT INTO `associate_brand` VALUES (1,'Zen4Tech','/Images/Brand/BrandImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-23 14:49:03','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-23 14:52:11');
/*!40000 ALTER TABLE `associate_brand` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cart` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_id` varchar(255) DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `sku` varchar(100) DEFAULT NULL,
  `quantity` int NOT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
INSERT INTO `cart` VALUES (1,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8',1,'NETFLIX-SHARED-6M',2,'N','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-08 12:28:35','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-08 12:45:21'),(2,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8',3,'AMAZON-PRIME-SHARED-6M',3,'N','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-08 13:26:47',NULL,NULL),(3,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8',4,'HOICHOI-PERSONAL-1M',1,'N','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-08 13:28:18','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-08 13:58:36');
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `common_image`
--

DROP TABLE IF EXISTS `common_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `common_image` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `image` varchar(200) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `common_image`
--

LOCK TABLES `common_image` WRITE;
/*!40000 ALTER TABLE `common_image` DISABLE KEYS */;
/*!40000 ALTER TABLE `common_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `faqs_setup`
--

DROP TABLE IF EXISTS `faqs_setup`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `faqs_setup` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  `description` varchar(300) DEFAULT NULL,
  `is_active` varchar(1) NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) NOT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faqs_setup`
--

LOCK TABLES `faqs_setup` WRITE;
/*!40000 ALTER TABLE `faqs_setup` DISABLE KEYS */;
INSERT INTO `faqs_setup` VALUES (1,'FAQs','Live Faqs','Y',NULL,'e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-09 17:36:24','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-09 17:37:31');
/*!40000 ALTER TABLE `faqs_setup` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `general_question`
--

DROP TABLE IF EXISTS `general_question`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `general_question` (
  `id` int NOT NULL AUTO_INCREMENT,
  `question` varchar(300) DEFAULT NULL,
  `answer` varchar(500) DEFAULT NULL,
  `is_active` varchar(1) NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) NOT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `general_question`
--

LOCK TABLES `general_question` WRITE;
/*!40000 ALTER TABLE `general_question` DISABLE KEYS */;
INSERT INTO `general_question` VALUES (1,'What is DeltaBox IT?','DeltaBox IT, accessible via deltaboxit.com, is your go-to hub for a variety of digital subscriptions and exclusive offers. Learn more about us here.','Y',NULL,'e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-10 11:06:44','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-10 11:10:49');
/*!40000 ALTER TABLE `general_question` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_details`
--

DROP TABLE IF EXISTS `order_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_details` (
  `id` int NOT NULL AUTO_INCREMENT,
  `order_profile_id` int NOT NULL,
  `item_invoice_no` varchar(50) NOT NULL,
  `invoice_no` varchar(50) NOT NULL,
  `product_id` int NOT NULL,
  `product_name` varchar(255) DEFAULT NULL,
  `thumbnail_image` varchar(200) DEFAULT NULL,
  `product_variant_id` int NOT NULL,
  `product_variant_name` varchar(255) DEFAULT NULL,
  `sku` varchar(100) NOT NULL,
  `unit_price` decimal(10,2) DEFAULT NULL,
  `final_price` decimal(10,2) DEFAULT NULL,
  `quantity` int DEFAULT NULL,
  `sub_total` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  `order_item_status` varchar(50) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `item_invoice_no_UNIQUE` (`item_invoice_no`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_details`
--

LOCK TABLES `order_details` WRITE;
/*!40000 ALTER TABLE `order_details` DISABLE KEYS */;
INSERT INTO `order_details` VALUES (1,1,'ITM202412213D925E','DIT202412210001',1,'Netflix','/Images/Product/ProductProfile/ThumbnailImage/default.png',2,'Netflix Shared Plan - 6 Months','NETFLIX-SHARED-6M',65.99,55.99,2,131.98,111.98,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30',NULL,NULL),(2,1,'ITM2024122154019D','DIT202412210001',3,'Amazon Prime','/Images/Product/ProductProfile/ThumbnailImage/default.png',9,'Amazon Prime Shared Plan - 6 Months','AMAZON-PRIME-SHARED-6M',49.99,45.99,3,149.97,137.97,'Delivered','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30',NULL,NULL),(3,1,'ITM20241221AE6228','DIT202412210001',4,'Hoichoi','/Images/Product/ProductProfile/ThumbnailImage/default.png',14,'Hoichoi Personal Plan - 1 Month','HOICHOI-PERSONAL-1M',3.99,2.99,1,3.99,2.99,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30',NULL,NULL),(4,2,'ITM2024122194460E','DIT202412210002',1,'Netflix','/Images/Product/ProductProfile/ThumbnailImage/default.png',2,'Netflix Shared Plan - 6 Months','NETFLIX-SHARED-6M',65.99,55.99,2,131.98,111.98,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:44:02',NULL,NULL),(5,2,'ITM202412214A565A','DIT202412210002',3,'Amazon Prime','/Images/Product/ProductProfile/ThumbnailImage/default.png',9,'Amazon Prime Shared Plan - 6 Months','AMAZON-PRIME-SHARED-6M',49.99,45.99,3,149.97,137.97,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:44:02',NULL,NULL),(6,2,'ITM20241221455FD7','DIT202412210002',4,'Hoichoi','/Images/Product/ProductProfile/ThumbnailImage/default.png',14,'Hoichoi Personal Plan - 1 Month','HOICHOI-PERSONAL-1M',3.99,2.99,1,3.99,2.99,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:44:02',NULL,NULL),(7,3,'ITM202412210FDFEF','DIT202412210003',1,'Netflix','/Images/Product/ProductProfile/ThumbnailImage/default.png',2,'Netflix Shared Plan - 6 Months','NETFLIX-SHARED-6M',65.99,55.99,2,131.98,111.98,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53',NULL,NULL),(8,3,'ITM2024122182288E','DIT202412210003',3,'Amazon Prime','/Images/Product/ProductProfile/ThumbnailImage/default.png',9,'Amazon Prime Shared Plan - 6 Months','AMAZON-PRIME-SHARED-6M',49.99,45.99,3,149.97,137.97,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53',NULL,NULL),(9,3,'ITM20241221D40D74','DIT202412210003',4,'Hoichoi','/Images/Product/ProductProfile/ThumbnailImage/default.png',14,'Hoichoi Personal Plan - 1 Month','HOICHOI-PERSONAL-1M',3.99,2.99,1,3.99,2.99,'Order Placed','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53',NULL,NULL);
/*!40000 ALTER TABLE `order_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_profile`
--

DROP TABLE IF EXISTS `order_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order_profile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `invoice_no` varchar(50) NOT NULL,
  `customer_id` varchar(255) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `email` varchar(256) DEFAULT NULL,
  `phone_number` varchar(50) DEFAULT NULL,
  `net_amount` decimal(10,2) DEFAULT NULL,
  `sub_total` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  `coin_redeemed` decimal(10,2) DEFAULT NULL,
  `promotion_code` varchar(255) DEFAULT NULL,
  `promotion_code_amount` decimal(10,2) DEFAULT NULL,
  `no_of_use` int DEFAULT NULL,
  `order_status` varchar(50) DEFAULT NULL,
  `order_note` varchar(255) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `invoice_no_UNIQUE` (`invoice_no`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_profile`
--

LOCK TABLES `order_profile` WRITE;
/*!40000 ALTER TABLE `order_profile` DISABLE KEYS */;
INSERT INTO `order_profile` VALUES (1,'DIT202412210001','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','Raihanul Hoque','Shanto','01975241213',285.94,252.94,202.94,0.00,'DELTASTAR',50.00,1,'Order Placed','Early','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30.472748'),(2,'DIT202412210002','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','Raihanul Hoque','Shanto','01975241213',285.94,252.94,242.94,10.00,'DELTASTAR',0.00,2,'Order Placed','Early','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:44:02','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:44:01.700450'),(3,'DIT202412210003','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','Raihanul Hoque','Shanto','01975241213',285.94,252.94,232.94,20.00,'string',0.00,1,'Order Placed','Early','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53.071916');
/*!40000 ALTER TABLE `order_profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `password_reset_otp`
--

DROP TABLE IF EXISTS `password_reset_otp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `password_reset_otp` (
  `id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(100) DEFAULT NULL,
  `otp` varchar(50) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `expires_at` datetime DEFAULT NULL,
  `is_used` tinyint DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `password_reset_otp`
--

LOCK TABLES `password_reset_otp` WRITE;
/*!40000 ALTER TABLE `password_reset_otp` DISABLE KEYS */;
INSERT INTO `password_reset_otp` VALUES (1,'user@gmail.com','108682','2024-11-26 11:37:52','2024-11-26 11:47:52',0),(2,'raihanul.haque.shanto01@gmail.com','702205','2024-11-26 11:40:01','2024-11-26 11:50:01',1);
/*!40000 ALTER TABLE `password_reset_otp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_information`
--

DROP TABLE IF EXISTS `payment_information`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment_information` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_id` varchar(255) NOT NULL,
  `invoice_no` varchar(50) NOT NULL,
  `invoice_id` varchar(100) NOT NULL,
  `payment_method` varchar(50) NOT NULL,
  `sender_number` varchar(50) NOT NULL,
  `transaction_id` varchar(50) NOT NULL,
  `date` datetime NOT NULL,
  `status` varchar(50) NOT NULL,
  `amount` varchar(50) NOT NULL,
  `fee` varchar(50) NOT NULL,
  `charged_amount` varchar(50) NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `invoice_no_UNIQUE` (`invoice_no`),
  UNIQUE KEY `invoice_id_UNIQUE` (`invoice_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_information`
--

LOCK TABLES `payment_information` WRITE;
/*!40000 ALTER TABLE `payment_information` DISABLE KEYS */;
INSERT INTO `payment_information` VALUES (1,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','DIT202412180001','Twk23ce41P4cZZF4sm1c','nagad','01975241213','FGJJFHJH','2024-12-18 15:15:21','COMPLETED','14.00','0.00','14.00','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-18 15:15:37',NULL,NULL);
/*!40000 ALTER TABLE `payment_information` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_proof`
--

DROP TABLE IF EXISTS `payment_proof`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payment_proof` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `image` varchar(200) DEFAULT NULL,
  `is_active` varchar(1) NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) NOT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_proof`
--

LOCK TABLES `payment_proof` WRITE;
/*!40000 ALTER TABLE `payment_proof` DISABLE KEYS */;
INSERT INTO `payment_proof` VALUES (1,'Image 1','/Images/Payment/PaymentProof/e5b8adff205b4f619fc80ca49350d66a.png','Y',NULL,'e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-10 14:08:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-10-12 13:16:28');
/*!40000 ALTER TABLE `payment_proof` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_attribute`
--

DROP TABLE IF EXISTS `product_attribute`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_attribute` (
  `id` int NOT NULL AUTO_INCREMENT,
  `variant_id` int NOT NULL,
  `attribute_name` varchar(255) NOT NULL,
  `attribute_value` varchar(255) NOT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_attribute`
--

LOCK TABLES `product_attribute` WRITE;
/*!40000 ALTER TABLE `product_attribute` DISABLE KEYS */;
INSERT INTO `product_attribute` VALUES (1,1,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(2,1,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(3,2,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(4,2,'Duration','6 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(5,3,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(6,3,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(7,4,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(8,4,'Duration','12 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(9,5,'Color','Green','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(10,5,'Size','S','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(11,6,'Color','Green','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(12,6,'Size','M','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(13,7,'Color','Black','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(14,7,'Size','L','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(15,8,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(16,8,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(17,9,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(18,9,'Duration','6 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(19,10,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(20,10,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(21,11,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(22,11,'Duration','12 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(23,12,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03'),(24,12,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03'),(25,13,'Color','Shared','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:05'),(26,13,'Duration','12 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:05'),(27,14,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:01'),(28,14,'Duration','1 Month','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:01'),(29,15,'Color','Personal','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03'),(30,15,'Duration','3 Months','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03');
/*!40000 ALTER TABLE `product_attribute` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_category`
--

DROP TABLE IF EXISTS `product_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_category` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `image` varchar(200) DEFAULT NULL,
  `is_popular` varchar(1) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_category`
--

LOCK TABLES `product_category` WRITE;
/*!40000 ALTER TABLE `product_category` DISABLE KEYS */;
INSERT INTO `product_category` VALUES (1,'Netflix Entertainment','/Images/Product/ProductCategory/default.png','Y','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 13:04:22',NULL,NULL),(2,'Prime Entertainment','/Images/Product/ProductCategory/default.png','N','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 13:04:48',NULL,NULL),(3,'Hoichoi Entertainment','/Images/Product/ProductCategory/default.png','Y','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:02:55',NULL,NULL);
/*!40000 ALTER TABLE `product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_faq`
--

DROP TABLE IF EXISTS `product_faq`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_faq` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `customer_name` varchar(255) DEFAULT NULL,
  `customer_email` varchar(255) DEFAULT NULL,
  `question` varchar(500) DEFAULT NULL,
  `answer` varchar(1000) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_faq`
--

LOCK TABLES `product_faq` WRITE;
/*!40000 ALTER TABLE `product_faq` DISABLE KEYS */;
INSERT INTO `product_faq` VALUES (1,1,'Shanto','shanto@gmail.com','Is a 3-month subscription to Netflix available?','Yes,Sir.','Y','127.0.0.1',NULL,'2024-11-24 17:31:26','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-24 17:42:46'),(2,2,'Mishu','mishu98@gmail.com','Is a 1-Year subscription to Prime available?','Yes, available.','N','127.0.0.1',NULL,'2024-11-25 11:39:00','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-25 11:44:48');
/*!40000 ALTER TABLE `product_faq` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_image`
--

DROP TABLE IF EXISTS `product_image`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_image` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `color_name` varchar(100) NOT NULL,
  `image` varchar(200) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_image`
--

LOCK TABLES `product_image` WRITE;
/*!40000 ALTER TABLE `product_image` DISABLE KEYS */;
INSERT INTO `product_image` VALUES (3,1,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(4,1,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(7,1,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(8,1,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(12,2,'Green','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(13,2,'Green','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(14,2,'Green','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(15,2,'Black','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(16,2,'Black','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(19,3,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(20,3,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(23,3,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(24,3,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(27,4,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40',NULL,NULL),(28,4,'Shared','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40',NULL,NULL),(31,4,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40',NULL,NULL),(32,4,'Personal','/Images/Product/ProductImage/default.png','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40',NULL,NULL);
/*!40000 ALTER TABLE `product_image` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_profile`
--

DROP TABLE IF EXISTS `product_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_profile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `category_id` int NOT NULL,
  `short_description` text,
  `description` text,
  `thumbnail_image` varchar(200) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `latest_offer` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_profile`
--

LOCK TABLES `product_profile` WRITE;
/*!40000 ALTER TABLE `product_profile` DISABLE KEYS */;
INSERT INTO `product_profile` VALUES (1,'Netflix',1,NULL,'Stream your favorite shows and movies on Netflix.','/Images/Product/ProductProfile/ThumbnailImage/default.png','Y','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:27',NULL,NULL),(2,'Shirt',1,NULL,'A trendy shirt with multiple color options.','/Images/Product/ProductProfile/ThumbnailImage/default.png','Y',NULL,'127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(3,'Amazon Prime',2,NULL,'Enjoy movies, shows, and exclusive benefits with Amazon Prime.','/Images/Product/ProductProfile/ThumbnailImage/default.png','Y','N','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(4,'Hoichoi',3,'Shared & Personal available.','Stream Bengali movies, web series, and more with Hoichoi.','/Images/Product/ProductProfile/ThumbnailImage/default.png','Y','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:14:57');
/*!40000 ALTER TABLE `product_profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_review`
--

DROP TABLE IF EXISTS `product_review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_review` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `customer_name` varchar(255) DEFAULT NULL,
  `customer_email` varchar(255) DEFAULT NULL,
  `review` varchar(500) DEFAULT NULL,
  `rating` decimal(10,2) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_review`
--

LOCK TABLES `product_review` WRITE;
/*!40000 ALTER TABLE `product_review` DISABLE KEYS */;
INSERT INTO `product_review` VALUES (1,1,'Mishu','mishu@gmail.com','Best Product!',5.00,'Y','127.0.0.1',NULL,'2024-11-26 13:23:09',NULL,NULL),(2,2,'Shanto','shanto@gmail.com','Not bad at all!',3.00,'N','127.0.0.1',NULL,'2024-11-26 13:25:37','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-26 13:27:43');
/*!40000 ALTER TABLE `product_review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_variant`
--

DROP TABLE IF EXISTS `product_variant`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product_variant` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `product_id` int NOT NULL,
  `category_id` int NOT NULL,
  `sku` varchar(100) DEFAULT NULL,
  `dp_price` decimal(10,2) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `stock_quantity` int DEFAULT '0',
  `discount_amount` decimal(10,2) DEFAULT NULL,
  `discount_start_date` datetime DEFAULT NULL,
  `discount_end_date` datetime DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `sku_UNIQUE` (`sku`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_variant`
--

LOCK TABLES `product_variant` WRITE;
/*!40000 ALTER TABLE `product_variant` DISABLE KEYS */;
INSERT INTO `product_variant` VALUES (1,'Netflix Shared Plan - 1 Month',1,1,'NETFLIX-SHARED',10.99,12.99,90,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-18 16:04:06'),(2,'Netflix Shared Plan - 6 Months',1,1,'NETFLIX-SHARED-6M',55.99,65.99,88,10.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53'),(3,'Netflix Personal Plan - 1 Month',1,1,'NETFLIX-PERSONAL',7.99,9.99,100,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28',NULL,NULL),(4,'Netflix Personal Plan - 12 Months',1,1,'NETFLIX-PERSONAL-12M',75.99,89.99,88,14.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:28','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-17 17:44:26'),(5,'Shirt Green Small',2,1,'SHIRT-GREEN-S',15.99,19.99,20,5.00,'2024-10-01 00:00:00','2024-10-31 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(6,'Shirt Green Medium',2,1,'TSHIRT-GREEN-M',16.99,20.99,10,3.00,'2024-10-01 00:00:00','2024-10-25 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(7,'Shirt Black Large',2,1,'TSHIRT-BLACK-L',17.99,21.99,20,4.00,'2024-10-01 00:00:00','2024-10-31 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:00:55',NULL,NULL),(8,'Amazon Prime Shared Plan - 1 Month',3,2,'AMAZON-PRIME-SHARED',8.99,10.99,100,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(9,'Amazon Prime Shared Plan - 6 Months',3,2,'AMAZON-PRIME-SHARED-6M',45.99,49.99,82,4.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53'),(10,'Amazon Prime Personal Plan - 1 Month',3,2,'AMAZON-PRIME-PERSONAL',6.99,8.99,98,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-18 12:04:05'),(11,'Amazon Prime Personal Plan - 12 Months',3,2,'AMAZON-PRIME-PERSONAL-12M',65.99,79.99,100,14.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-11-21 16:01:44',NULL,NULL),(12,'Hoichoi Shared Plan - 1 Month',4,3,'HOICHOI-SHARED-1M',3.99,4.99,100,1.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03'),(13,'Hoichoi Shared Plan - 12 Months',4,3,'HOICHOI-SHARED-12M',10.99,12.99,100,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:15:03'),(14,'Hoichoi Personal Plan - 1 Month',4,3,'HOICHOI-PERSONAL-1M',2.99,3.99,94,1.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53'),(15,'Hoichoi Personal Plan - 3 Months',4,3,'HOICHOI-PERSONAL-3M',8.99,10.99,82,2.00,'2024-11-21 00:00:00','2024-12-21 23:59:59','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-04 16:03:40','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-17 17:44:26');
/*!40000 ALTER TABLE `product_variant` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promotion_code`
--

DROP TABLE IF EXISTS `promotion_code`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promotion_code` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `code` varchar(255) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `promotion_start_date` datetime DEFAULT NULL,
  `promotion_end_date` datetime DEFAULT NULL,
  `one_time` varchar(1) DEFAULT NULL,
  `is_active` varchar(1) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `code_UNIQUE` (`code`),
  UNIQUE KEY `amount_UNIQUE` (`amount`),
  UNIQUE KEY `one_time_UNIQUE` (`one_time`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promotion_code`
--

LOCK TABLES `promotion_code` WRITE;
/*!40000 ALTER TABLE `promotion_code` DISABLE KEYS */;
INSERT INTO `promotion_code` VALUES (1,'Best Customer Offer','DELTASTAR',50.00,'2024-12-09 00:00:00','2024-12-31 00:00:00','Y','Y','127.0.0.1','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-09 13:18:05','e8db3664-8bda-41ef-8d9e-16ae89f00e69','2024-12-09 13:18:58');
/*!40000 ALTER TABLE `promotion_code` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reward_point`
--

DROP TABLE IF EXISTS `reward_point`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reward_point` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_id` varchar(255) NOT NULL,
  `point` decimal(10,2) DEFAULT '0.00',
  `redeemed_point` decimal(10,2) DEFAULT '0.00',
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reward_point`
--

LOCK TABLES `reward_point` WRITE;
/*!40000 ALTER TABLE `reward_point` DISABLE KEYS */;
INSERT INTO `reward_point` VALUES (1,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8',67.00,30.00,'127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:43:30','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-21 16:46:53');
/*!40000 ALTER TABLE `reward_point` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tokeninfos`
--

DROP TABLE IF EXISTS `tokeninfos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tokeninfos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshTokenExpiry` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tokeninfos`
--

LOCK TABLES `tokeninfos` WRITE;
/*!40000 ALTER TABLE `tokeninfos` DISABLE KEYS */;
INSERT INTO `tokeninfos` VALUES (1,'user','dk3m7coAwUlG/wkgK9PHD+cFupByduhJtNm/RPzYu0Q=','2024-12-22 16:22:11.672015'),(2,'admin','iO/mQ8/4Cvr5DoS1s7oTtzLg5RQjhEx3yQaMFoCsRdE=','2024-12-23 14:13:30.922380');
/*!40000 ALTER TABLE `tokeninfos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wishlist`
--

DROP TABLE IF EXISTS `wishlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wishlist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `customer_id` varchar(255) NOT NULL,
  `product_id` int NOT NULL,
  `sku` varchar(100) NOT NULL,
  `is_active` varchar(1) NOT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `created_by` varchar(255) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(255) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wishlist`
--

LOCK TABLES `wishlist` WRITE;
/*!40000 ALTER TABLE `wishlist` DISABLE KEYS */;
INSERT INTO `wishlist` VALUES (1,'ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8',1,'NETFLIX-SHARED-6M','Y','127.0.0.1','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-07 15:20:42','ac1c5be5-ba32-4f13-b511-bd1e8f2ba2f8','2024-12-07 15:26:04');
/*!40000 ALTER TABLE `wishlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'delta_box'
--

--
-- Dumping routines for database 'delta_box'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-25 11:38:33
