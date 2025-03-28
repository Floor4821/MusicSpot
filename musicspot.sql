-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 25, 2025 at 08:53 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `musicspot`
--
CREATE DATABASE IF NOT EXISTS `musicspot` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `musicspot`;

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `accountID` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `isadmin` tinyint(1) NOT NULL,
  `profilepic` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `artist`
--

CREATE TABLE `artist` (
  `artistID` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `artistperson`
--

CREATE TABLE `artistperson` (
  `artistpersonID` int(11) NOT NULL,
  `artistID` int(11) NOT NULL,
  `personID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `genre`
--

CREATE TABLE `genre` (
  `genreID` int(11) NOT NULL,
  `subgenre` int(11) NOT NULL,
  `genre` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `genretype`
--

CREATE TABLE `genretype` (
  `ID` int(11) NOT NULL,
  `Type` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `likedlist`
--

CREATE TABLE `likedlist` (
  `likedlistID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL,
  `releaseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `mediatype`
--

CREATE TABLE `mediatype` (
  `ID` int(11) NOT NULL,
  `Type` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `person`
--

CREATE TABLE `person` (
  `personID` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `productID` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  `stock` int(11) NOT NULL,
  `mediatype` int(11) NOT NULL,
  `releaseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `purchase`
--

CREATE TABLE `purchase` (
  `purchaseID` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `paid` decimal(10,0) NOT NULL,
  `accountID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `purchaseproduct`
--

CREATE TABLE `purchaseproduct` (
  `purchaseproductID` int(11) NOT NULL,
  `purchaseID` int(11) NOT NULL,
  `productID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `release`
--

CREATE TABLE `release` (
  `releaseID` int(11) NOT NULL,
  `cover` blob NOT NULL,
  `releasetype` int(11) NOT NULL,
  `artistID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `releasegenre`
--

CREATE TABLE `releasegenre` (
  `releasegenreID` int(11) NOT NULL,
  `releaseID` int(11) NOT NULL,
  `genreID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `releasetype`
--

CREATE TABLE `releasetype` (
  `ID` int(11) NOT NULL,
  `Type` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `song`
--

CREATE TABLE `song` (
  `songID` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `length` int(11) NOT NULL,
  `releaseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `subgenretype`
--

CREATE TABLE `subgenretype` (
  `ID` int(11) NOT NULL,
  `Type` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `wishlist`
--

CREATE TABLE `wishlist` (
  `wishlistID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL,
  `productID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`accountID`);

--
-- Indexes for table `artist`
--
ALTER TABLE `artist`
  ADD PRIMARY KEY (`artistID`);

--
-- Indexes for table `artistperson`
--
ALTER TABLE `artistperson`
  ADD PRIMARY KEY (`artistpersonID`),
  ADD KEY `artistID` (`artistID`),
  ADD KEY `personID` (`personID`);

--
-- Indexes for table `genre`
--
ALTER TABLE `genre`
  ADD PRIMARY KEY (`genreID`),
  ADD KEY `subgenre` (`subgenre`),
  ADD KEY `genre` (`genre`);

--
-- Indexes for table `genretype`
--
ALTER TABLE `genretype`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `likedlist`
--
ALTER TABLE `likedlist`
  ADD PRIMARY KEY (`likedlistID`),
  ADD KEY `accountID` (`accountID`),
  ADD KEY `releaseID` (`releaseID`);

--
-- Indexes for table `mediatype`
--
ALTER TABLE `mediatype`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `person`
--
ALTER TABLE `person`
  ADD PRIMARY KEY (`personID`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`productID`),
  ADD KEY `mediatype` (`mediatype`),
  ADD KEY `releaseID` (`releaseID`);

--
-- Indexes for table `purchase`
--
ALTER TABLE `purchase`
  ADD PRIMARY KEY (`purchaseID`),
  ADD KEY `accountID` (`accountID`);

--
-- Indexes for table `purchaseproduct`
--
ALTER TABLE `purchaseproduct`
  ADD PRIMARY KEY (`purchaseproductID`),
  ADD KEY `purchaseID` (`purchaseID`),
  ADD KEY `productID` (`productID`);

--
-- Indexes for table `release`
--
ALTER TABLE `release`
  ADD PRIMARY KEY (`releaseID`),
  ADD KEY `releasetype` (`releasetype`),
  ADD KEY `artistID` (`artistID`);

--
-- Indexes for table `releasegenre`
--
ALTER TABLE `releasegenre`
  ADD PRIMARY KEY (`releasegenreID`),
  ADD KEY `releaseID` (`releaseID`),
  ADD KEY `genreID` (`genreID`);

--
-- Indexes for table `releasetype`
--
ALTER TABLE `releasetype`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `song`
--
ALTER TABLE `song`
  ADD PRIMARY KEY (`songID`),
  ADD KEY `releaseID` (`releaseID`);

--
-- Indexes for table `subgenretype`
--
ALTER TABLE `subgenretype`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `wishlist`
--
ALTER TABLE `wishlist`
  ADD PRIMARY KEY (`wishlistID`),
  ADD KEY `accountID` (`accountID`),
  ADD KEY `productID` (`productID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `artist`
--
ALTER TABLE `artist`
  MODIFY `artistID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `artistperson`
--
ALTER TABLE `artistperson`
  MODIFY `artistpersonID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `genre`
--
ALTER TABLE `genre`
  MODIFY `genreID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `genretype`
--
ALTER TABLE `genretype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `likedlist`
--
ALTER TABLE `likedlist`
  MODIFY `likedlistID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `mediatype`
--
ALTER TABLE `mediatype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `person`
--
ALTER TABLE `person`
  MODIFY `personID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `productID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `purchase`
--
ALTER TABLE `purchase`
  MODIFY `purchaseID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `purchaseproduct`
--
ALTER TABLE `purchaseproduct`
  MODIFY `purchaseproductID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `release`
--
ALTER TABLE `release`
  MODIFY `releaseID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `releasegenre`
--
ALTER TABLE `releasegenre`
  MODIFY `releasegenreID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `releasetype`
--
ALTER TABLE `releasetype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `song`
--
ALTER TABLE `song`
  MODIFY `songID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `subgenretype`
--
ALTER TABLE `subgenretype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `wishlist`
--
ALTER TABLE `wishlist`
  MODIFY `wishlistID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `artistperson`
--
ALTER TABLE `artistperson`
  ADD CONSTRAINT `artistperson_ibfk_1` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`),
  ADD CONSTRAINT `artistperson_ibfk_2` FOREIGN KEY (`personID`) REFERENCES `person` (`personID`);

--
-- Constraints for table `genre`
--
ALTER TABLE `genre`
  ADD CONSTRAINT `genre_ibfk_1` FOREIGN KEY (`subgenre`) REFERENCES `subgenretype` (`ID`),
  ADD CONSTRAINT `genre_ibfk_2` FOREIGN KEY (`genre`) REFERENCES `genretype` (`ID`);

--
-- Constraints for table `likedlist`
--
ALTER TABLE `likedlist`
  ADD CONSTRAINT `likedlist_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`),
  ADD CONSTRAINT `likedlist_ibfk_2` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`);

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `product_ibfk_1` FOREIGN KEY (`mediatype`) REFERENCES `mediatype` (`ID`),
  ADD CONSTRAINT `product_ibfk_2` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`);

--
-- Constraints for table `purchase`
--
ALTER TABLE `purchase`
  ADD CONSTRAINT `purchase_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`);

--
-- Constraints for table `purchaseproduct`
--
ALTER TABLE `purchaseproduct`
  ADD CONSTRAINT `purchaseproduct_ibfk_1` FOREIGN KEY (`purchaseID`) REFERENCES `purchase` (`purchaseID`),
  ADD CONSTRAINT `purchaseproduct_ibfk_2` FOREIGN KEY (`productID`) REFERENCES `product` (`productID`);

--
-- Constraints for table `release`
--
ALTER TABLE `release`
  ADD CONSTRAINT `release_ibfk_1` FOREIGN KEY (`releasetype`) REFERENCES `releasetype` (`ID`),
  ADD CONSTRAINT `release_ibfk_2` FOREIGN KEY (`artistID`) REFERENCES `artist` (`artistID`);

--
-- Constraints for table `releasegenre`
--
ALTER TABLE `releasegenre`
  ADD CONSTRAINT `releasegenre_ibfk_1` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`),
  ADD CONSTRAINT `releasegenre_ibfk_2` FOREIGN KEY (`genreID`) REFERENCES `genre` (`genreID`);

--
-- Constraints for table `song`
--
ALTER TABLE `song`
  ADD CONSTRAINT `song_ibfk_1` FOREIGN KEY (`releaseID`) REFERENCES `release` (`releaseID`);

--
-- Constraints for table `wishlist`
--
ALTER TABLE `wishlist`
  ADD CONSTRAINT `wishlist_ibfk_1` FOREIGN KEY (`accountID`) REFERENCES `account` (`accountID`),
  ADD CONSTRAINT `wishlist_ibfk_2` FOREIGN KEY (`productID`) REFERENCES `product` (`productID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
