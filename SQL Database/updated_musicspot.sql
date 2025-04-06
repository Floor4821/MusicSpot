-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: musicspot
-- ------------------------------------------------------
-- Server version	8.0.39

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
-- Table structure for table `account`
--

DROP TABLE IF EXISTS `account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `account` (
  `accountID` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `isadmin` tinyint(1) NOT NULL,
  `profilepic` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`accountID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account`
--

LOCK TABLES `account` WRITE;
/*!40000 ALTER TABLE `account` DISABLE KEYS */;
INSERT INTO `account` VALUES (4,'Johnny Bravo','johnny.bravo@gmail.com','Damn_I_Look_Good',0,'C:\\Users\\jamey\\OneDrive\\Pictures\\Bureaubladachtergrond.bmp'),(5,'Giorenno Giovanna','giorenno.giovanna@gmail.com','golden',0,'NULL'),(6,'Cederic Verlinden','cederic.verlinden@gmail.com','hopelijkindebrico',0,'NULL'),(7,'Cornelius Chesterfield','cornelius.chesterfield@gmail.com','loyaltothearmy',0,'C:\\Users\\jamey\\OneDrive\\Pictures\\Screenshots\\Muziek.png'),(8,'Yorbe','yorbe.???@gmail.com','ok',0,'C:\\Users\\jamey\\OneDrive\\Pictures\\Screenshots\\Muziek.png');
/*!40000 ALTER TABLE `account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genre` (
  `genreID` int NOT NULL AUTO_INCREMENT,
  `subgenre` int NOT NULL,
  `genre` int NOT NULL,
  PRIMARY KEY (`genreID`),
  KEY `subgenre` (`subgenre`),
  KEY `genre` (`genre`),
  CONSTRAINT `genre_ibfk_1` FOREIGN KEY (`subgenre`) REFERENCES `subgenretype` (`ID`),
  CONSTRAINT `genre_ibfk_2` FOREIGN KEY (`genre`) REFERENCES `genretype` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genretype`
--

DROP TABLE IF EXISTS `genretype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genretype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genretype`
--

LOCK TABLES `genretype` WRITE;
/*!40000 ALTER TABLE `genretype` DISABLE KEYS */;
INSERT INTO `genretype` VALUES (1,'Ambient'),(2,'Electronic'),(3,'Experimental'),(4,'Folk'),(5,'HipHop'),(6,'Jazz'),(7,'Metal'),(8,'Pop'),(9,'Punk'),(10,'Rock'),(11,'Soul');
/*!40000 ALTER TABLE `genretype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `likedlist`
--

DROP TABLE IF EXISTS `likedlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `likedlist` (
  `likedlistID` int NOT NULL AUTO_INCREMENT,
  `accountID` int NOT NULL,
  `releaseID` int NOT NULL,
  PRIMARY KEY (`likedlistID`),
  KEY `accountID` (`accountID`),
  KEY `releaseID` (`releaseID`),
  CONSTRAINT `likedlist_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`),
  CONSTRAINT `likedlist_ibfk_2` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `likedlist`
--

LOCK TABLES `likedlist` WRITE;
/*!40000 ALTER TABLE `likedlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `likedlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mediatype`
--

DROP TABLE IF EXISTS `mediatype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mediatype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mediatype`
--

LOCK TABLES `mediatype` WRITE;
/*!40000 ALTER TABLE `mediatype` DISABLE KEYS */;
INSERT INTO `mediatype` VALUES (1,'Vinyl'),(2,'CD'),(3,'Cassette');
/*!40000 ALTER TABLE `mediatype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `productID` int NOT NULL AUTO_INCREMENT,
  `price` int NOT NULL,
  `stock` int NOT NULL,
  `mediatype` int NOT NULL,
  `releaseID` int NOT NULL,
  PRIMARY KEY (`productID`),
  KEY `mediatype` (`mediatype`),
  KEY `releaseID` (`releaseID`),
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`mediatype`) REFERENCES `mediatype` (`ID`),
  CONSTRAINT `product_ibfk_2` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase`
--

DROP TABLE IF EXISTS `purchase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `purchase` (
  `purchaseID` int NOT NULL AUTO_INCREMENT,
  `date` datetime NOT NULL,
  `paid` decimal(10,0) NOT NULL,
  `accountID` int NOT NULL,
  PRIMARY KEY (`purchaseID`),
  KEY `accountID` (`accountID`),
  CONSTRAINT `purchase_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase`
--

LOCK TABLES `purchase` WRITE;
/*!40000 ALTER TABLE `purchase` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchaseproduct`
--

DROP TABLE IF EXISTS `purchaseproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `purchaseproduct` (
  `purchaseproductID` int NOT NULL AUTO_INCREMENT,
  `purchaseID` int NOT NULL,
  `productID` int NOT NULL,
  PRIMARY KEY (`purchaseproductID`),
  KEY `purchaseID` (`purchaseID`),
  KEY `productID` (`productID`),
  CONSTRAINT `purchaseproduct_ibfk_1` FOREIGN KEY (`purchaseID`) REFERENCES `purchase` (`purchaseID`),
  CONSTRAINT `purchaseproduct_ibfk_2` FOREIGN KEY (`productID`) REFERENCES `product` (`productID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchaseproduct`
--

LOCK TABLES `purchaseproduct` WRITE;
/*!40000 ALTER TABLE `purchaseproduct` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchaseproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `release`
--

DROP TABLE IF EXISTS `release`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `release` (
  `releaseID` int NOT NULL AUTO_INCREMENT,
  `cover` blob NOT NULL,
  `releasetype` int NOT NULL,
  `artist` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `releasedate` datetime DEFAULT NULL,
  PRIMARY KEY (`releaseID`),
  KEY `releasetype` (`releasetype`),
  KEY `artistID` (`artist`),
  CONSTRAINT `release_ibfk_1` FOREIGN KEY (`releasetype`) REFERENCES `releasetype` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `release`
--

LOCK TABLES `release` WRITE;
/*!40000 ALTER TABLE `release` DISABLE KEYS */;
/*!40000 ALTER TABLE `release` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `releasegenre`
--

DROP TABLE IF EXISTS `releasegenre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `releasegenre` (
  `releasegenreID` int NOT NULL AUTO_INCREMENT,
  `releaseID` int NOT NULL,
  `genreID` int NOT NULL,
  PRIMARY KEY (`releasegenreID`),
  KEY `releaseID` (`releaseID`),
  KEY `genreID` (`genreID`),
  CONSTRAINT `releasegenre_ibfk_1` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`),
  CONSTRAINT `releasegenre_ibfk_2` FOREIGN KEY (`genreID`) REFERENCES `genre` (`genreID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `releasegenre`
--

LOCK TABLES `releasegenre` WRITE;
/*!40000 ALTER TABLE `releasegenre` DISABLE KEYS */;
/*!40000 ALTER TABLE `releasegenre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `releasetype`
--

DROP TABLE IF EXISTS `releasetype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `releasetype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `releasetype`
--

LOCK TABLES `releasetype` WRITE;
/*!40000 ALTER TABLE `releasetype` DISABLE KEYS */;
INSERT INTO `releasetype` VALUES (1,'Album'),(2,'EP'),(3,'Single'),(4,'Other');
/*!40000 ALTER TABLE `releasetype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `song`
--

DROP TABLE IF EXISTS `song`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `song` (
  `songID` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `length` int NOT NULL,
  `releaseID` int NOT NULL,
  PRIMARY KEY (`songID`),
  KEY `releaseID` (`releaseID`),
  CONSTRAINT `song_ibfk_1` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `song`
--

LOCK TABLES `song` WRITE;
/*!40000 ALTER TABLE `song` DISABLE KEYS */;
/*!40000 ALTER TABLE `song` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subgenretype`
--

DROP TABLE IF EXISTS `subgenretype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subgenretype` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=88 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subgenretype`
--

LOCK TABLES `subgenretype` WRITE;
/*!40000 ALTER TABLE `subgenretype` DISABLE KEYS */;
INSERT INTO `subgenretype` VALUES (1,'Ambient'),(2,'DarkAmbient'),(3,'ElectronicAmbient'),(4,'BitMusic'),(5,'Breakbeat'),(6,'BubblegumBass'),(7,'Chillout'),(8,'DrumAndBass'),(9,'Dubstep'),(10,'ElectroIndustrial'),(11,'Electronic'),(12,'GlitchHop'),(13,'Hardcore'),(14,'House'),(15,'IDM'),(16,'Indietronica'),(17,'Techno'),(18,'TripHop'),(19,'Drone'),(20,'Experimental'),(21,'Noise'),(22,'Plunderphonics'),(23,'AvantFolk'),(24,'ChamberFolk'),(25,'FolkRock'),(26,'IndieFolk'),(27,'AbstractHipHop'),(28,'ConsciousHipHop'),(29,'ExperimentalHipHop'),(30,'HardcoreHipHop'),(31,'JazzRap'),(32,'PopRap'),(33,'Trap'),(34,'AvantGardeJazz'),(35,'CoolJazz'),(36,'HardBop'),(37,'JazzFusion'),(38,'ModalJazz'),(39,'SmoothJazz'),(40,'AlternativeMetal'),(41,'AvantGardeMetal'),(42,'BlackMetal'),(43,'DeathMetal'),(44,'HeavyMetal'),(45,'ProgressiveMetal'),(46,'StonerMetal'),(47,'ThrashMetal'),(48,'ArtPop'),(49,'BaroquePop'),(50,'DancePop'),(51,'Electropop'),(52,'GlitchPop'),(53,'IndiePop'),(54,'JanglePop'),(55,'ProgressivePop'),(56,'PsychedelicPop'),(57,'Synthpop'),(58,'ArtPunk'),(59,'Emo'),(60,'GothicRock'),(61,'HardcorePunk'),(62,'PopPunk'),(63,'PostHardcore'),(64,'PostPunk'),(65,'PunkRock'),(66,'AlternativeRock'),(67,'ArtRock'),(68,'DreamPop'),(69,'ExperimentalRock'),(70,'GarageRock'),(71,'Grunge'),(72,'HardRock'),(73,'IndieRock'),(74,'IndustrialRock'),(75,'MathRock'),(76,'NewWave'),(77,'NoiseRock'),(78,'PopRock'),(79,'PostRock'),(80,'ProgressiveRock'),(81,'PsychedelicRock'),(82,'Shoegaze'),(83,'SlackerRock'),(84,'Slowcore'),(85,'NeoSoul'),(86,'ProgressiveSoul'),(87,'Soul');
/*!40000 ALTER TABLE `subgenretype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wishlist`
--

DROP TABLE IF EXISTS `wishlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wishlist` (
  `wishlistID` int NOT NULL AUTO_INCREMENT,
  `accountID` int NOT NULL,
  `productID` int NOT NULL,
  PRIMARY KEY (`wishlistID`),
  KEY `accountID` (`accountID`),
  KEY `productID` (`productID`),
  CONSTRAINT `wishlist_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`),
  CONSTRAINT `wishlist_ibfk_2` FOREIGN KEY (`productID`) REFERENCES `product` (`productID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wishlist`
--

LOCK TABLES `wishlist` WRITE;
/*!40000 ALTER TABLE `wishlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `wishlist` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-06 16:34:08
