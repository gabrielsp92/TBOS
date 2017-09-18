CREATE DATABASE  IF NOT EXISTS `zoologico` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `zoologico`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: zoologico
-- ------------------------------------------------------
-- Server version	5.7.14-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `animal`
--

DROP TABLE IF EXISTS `animal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `animal` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `especie` varchar(45) NOT NULL,
  `origem` varchar(45) DEFAULT NULL,
  `nascimento` date DEFAULT NULL,
  `peso` int(11) DEFAULT NULL,
  `tratador_id` int(11) DEFAULT NULL,
  `rotina_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_animal_rotina_idx` (`rotina_id`),
  KEY `fk_animal_tratador_idx` (`tratador_id`),
  CONSTRAINT `fk_animal_rotina` FOREIGN KEY (`rotina_id`) REFERENCES `rotina` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_animal_tratador` FOREIGN KEY (`tratador_id`) REFERENCES `tratador` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `animal`
--

LOCK TABLES `animal` WRITE;
/*!40000 ALTER TABLE `animal` DISABLE KEYS */;
INSERT INTO `animal` VALUES (1,'Brioco','Pombo','Malasia','1957-10-12',1000,1,1),(2,'Bibiro','Cobra','Amazonia','2017-05-10',500,2,2);
/*!40000 ALTER TABLE `animal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `animal_tratador`
--

DROP TABLE IF EXISTS `animal_tratador`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `animal_tratador` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `animal_id` int(11) DEFAULT NULL,
  `tratador_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_tratador_idx` (`tratador_id`),
  KEY `fk_animal_idx` (`animal_id`),
  CONSTRAINT `fk_animal` FOREIGN KEY (`animal_id`) REFERENCES `animal` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tratador` FOREIGN KEY (`tratador_id`) REFERENCES `tratador` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `animal_tratador`
--

LOCK TABLES `animal_tratador` WRITE;
/*!40000 ALTER TABLE `animal_tratador` DISABLE KEYS */;
INSERT INTO `animal_tratador` VALUES (1,1,2),(2,1,3),(3,2,4),(4,2,5);
/*!40000 ALTER TABLE `animal_tratador` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `boletim`
--

DROP TABLE IF EXISTS `boletim`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `boletim` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tratador_id` int(11) NOT NULL,
  `animal_id` int(11) NOT NULL,
  `parecer` varchar(255) DEFAULT NULL,
  `data` date DEFAULT NULL,
  `observacao` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_tratador_03_idx` (`tratador_id`),
  KEY `fk_animal_03_idx` (`animal_id`),
  CONSTRAINT `fk_animal_03` FOREIGN KEY (`animal_id`) REFERENCES `animal` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tratador_03` FOREIGN KEY (`tratador_id`) REFERENCES `tratador` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `boletim`
--

LOCK TABLES `boletim` WRITE;
/*!40000 ALTER TABLE `boletim` DISABLE KEYS */;
INSERT INTO `boletim` VALUES (1,1,1,'Animal bem dahora, fez a alegria da moçada','2017-06-09',NULL),(2,4,2,'Rastejante e peçonhento, viu espiritos','2017-06-05','');
/*!40000 ALTER TABLE `boletim` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `consulta`
--

DROP TABLE IF EXISTS `consulta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `consulta` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `data_agendada` date DEFAULT NULL,
  `data_realizada` date DEFAULT NULL,
  `animal_id` int(11) NOT NULL,
  `veterinario_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_animal_04_idx` (`animal_id`),
  KEY `fk_veterinario_04_idx` (`veterinario_id`),
  CONSTRAINT `fk_animal_04` FOREIGN KEY (`animal_id`) REFERENCES `animal` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_veterinario_04` FOREIGN KEY (`veterinario_id`) REFERENCES `veterinario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consulta`
--

LOCK TABLES `consulta` WRITE;
/*!40000 ALTER TABLE `consulta` DISABLE KEYS */;
INSERT INTO `consulta` VALUES (1,'2017-06-04','2017-06-04',1,1),(2,'2017-06-09','2017-06-07',1,2),(3,'2017-06-12',NULL,2,3);
/*!40000 ALTER TABLE `consulta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medicamento`
--

DROP TABLE IF EXISTS `medicamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `medicamento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicamento`
--

LOCK TABLES `medicamento` WRITE;
/*!40000 ALTER TABLE `medicamento` DISABLE KEYS */;
INSERT INTO `medicamento` VALUES (1,'Flumexil'),(2,'Baytril'),(3,'Naquilene'),(4,'Marbocyl'),(5,'Flumisol'),(6,'FLUBACTIN'),(7,'ROXACIN'),(8,'ENROGAL'),(9,'Branzil'),(10,'Enrobioflox');
/*!40000 ALTER TABLE `medicamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receita`
--

DROP TABLE IF EXISTS `receita`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `receita` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `medicamento_id` int(11) DEFAULT NULL,
  `data` date DEFAULT NULL,
  `observacoes` varchar(255) DEFAULT NULL,
  `veterinario_id` int(11) DEFAULT NULL,
  `dose` int(11) DEFAULT NULL,
  `frequencia` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_medicamento_01_idx` (`medicamento_id`),
  KEY `fk_veterinario_01_idx` (`veterinario_id`),
  CONSTRAINT `fk_medicamento_01` FOREIGN KEY (`medicamento_id`) REFERENCES `medicamento` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_veterinario_01` FOREIGN KEY (`veterinario_id`) REFERENCES `veterinario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receita`
--

LOCK TABLES `receita` WRITE;
/*!40000 ALTER TABLE `receita` DISABLE KEYS */;
INSERT INTO `receita` VALUES (1,1,'2017-06-09',NULL,1,3,12);
/*!40000 ALTER TABLE `receita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registro`
--

DROP TABLE IF EXISTS `registro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `registro` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `consulta_id` int(11) NOT NULL,
  `emergencia` int(11) DEFAULT '0',
  `diagnostico` varchar(255) DEFAULT NULL,
  `observacoes` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_consulta_02_idx` (`consulta_id`),
  CONSTRAINT `fk_consulta_02` FOREIGN KEY (`consulta_id`) REFERENCES `consulta` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registro`
--

LOCK TABLES `registro` WRITE;
/*!40000 ALTER TABLE `registro` DISABLE KEYS */;
INSERT INTO `registro` VALUES (1,1,0,'Aids Pesada','Passa bem'),(2,2,1,'Gonorreia Erosiva 5ª DAN','Com o tempo cura');
/*!40000 ALTER TABLE `registro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rotina`
--

DROP TABLE IF EXISTS `rotina`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rotina` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `validade` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rotina`
--

LOCK TABLES `rotina` WRITE;
/*!40000 ALTER TABLE `rotina` DISABLE KEYS */;
INSERT INTO `rotina` VALUES (1,'2017-12-24'),(2,'2017-12-01');
/*!40000 ALTER TABLE `rotina` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rotina_receita`
--

DROP TABLE IF EXISTS `rotina_receita`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rotina_receita` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rotina_id` int(11) NOT NULL,
  `receita_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_rotina_03_idx` (`rotina_id`),
  KEY `fk_receita_03_idx` (`receita_id`),
  CONSTRAINT `fk_receita_03` FOREIGN KEY (`receita_id`) REFERENCES `receita` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_rotina_03` FOREIGN KEY (`rotina_id`) REFERENCES `rotina` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rotina_receita`
--

LOCK TABLES `rotina_receita` WRITE;
/*!40000 ALTER TABLE `rotina_receita` DISABLE KEYS */;
INSERT INTO `rotina_receita` VALUES (1,1,1);
/*!40000 ALTER TABLE `rotina_receita` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rotina_tarefa`
--

DROP TABLE IF EXISTS `rotina_tarefa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rotina_tarefa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `rotina_id` int(11) NOT NULL,
  `tarefa_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_rotina01_idx` (`rotina_id`),
  KEY `fk_tarefa01_idx` (`tarefa_id`),
  CONSTRAINT `fk_rotina01` FOREIGN KEY (`rotina_id`) REFERENCES `rotina` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_tarefa01` FOREIGN KEY (`tarefa_id`) REFERENCES `tarefa` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rotina_tarefa`
--

LOCK TABLES `rotina_tarefa` WRITE;
/*!40000 ALTER TABLE `rotina_tarefa` DISABLE KEYS */;
INSERT INTO `rotina_tarefa` VALUES (1,1,1),(2,1,2),(3,1,4),(4,2,1),(5,2,5),(6,2,6);
/*!40000 ALTER TABLE `rotina_tarefa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tarefa`
--

DROP TABLE IF EXISTS `tarefa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tarefa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarefa`
--

LOCK TABLES `tarefa` WRITE;
/*!40000 ALTER TABLE `tarefa` DISABLE KEYS */;
INSERT INTO `tarefa` VALUES (1,'Comer'),(2,'Passear'),(3,'Brincar'),(4,'Nadar'),(5,'Correr'),(6,'Cantar');
/*!40000 ALTER TABLE `tarefa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tratador`
--

DROP TABLE IF EXISTS `tratador`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tratador` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `endereco` varchar(255) DEFAULT NULL,
  `telefone` varchar(45) DEFAULT NULL,
  `senha` varchar(45) NOT NULL,
  `login` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tratador`
--

LOCK TABLES `tratador` WRITE;
/*!40000 ALTER TABLE `tratador` DISABLE KEYS */;
INSERT INTO `tratador` VALUES (1,'Joao Alberto','Rua Dr. Vigilante','3232323232','123','joao'),(2,'Carlos Almeida','Rua Amazonas 777','32988655897','123','carlos'),(3,'Robert De niro','Rua Antonio de Paula Cunha 332','3265988965','123','robert'),(4,'Jeferson Braga','Rua Pimentel Alvim','3296588965','123','jeferson'),(5,'George Rei da Floresta','Rua Selva Selvagem','3298568569','123','george');
/*!40000 ALTER TABLE `tratador` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `veterinario`
--

DROP TABLE IF EXISTS `veterinario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `veterinario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) NOT NULL,
  `endereco` varchar(255) DEFAULT NULL,
  `telefone` varchar(255) DEFAULT NULL,
  `registro` varchar(45) DEFAULT NULL,
  `login` varchar(45) NOT NULL,
  `senha` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `veterinario`
--

LOCK TABLES `veterinario` WRITE;
/*!40000 ALTER TABLE `veterinario` DISABLE KEYS */;
INSERT INTO `veterinario` VALUES (1,'Gabriel Soares','Rua Antonio de Paula Mendes','32988264217','123456','gabriel','123'),(2,'Felix Felino','Rua Presidente Inelegível','3296855698','333333','felix','123'),(3,'Tiger Woods','Rua Ace in a hole','553265985678','666666','tiger','123');
/*!40000 ALTER TABLE `veterinario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-06-09 11:09:54
