BEGIN TRANSACTION;
CREATE TABLE "Utilisateurs" (
	`idUtilisateur`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`nomUtilisateur`	TEXT,
	`prenomUtilisateur`	TEXT,
	`login`	TEXT UNIQUE,
	`password`	TEXT,
	`fonction`	TEXT,
	`mail`	TEXT UNIQUE
);
INSERT INTO `Utilisateurs` VALUES (1,'testNom','testPrenom','login','password','testFct','test@test.fr');
INSERT INTO `Utilisateurs` VALUES (2,'Bertho','Matthieu','mberth','matthieu','Admin','matthieu.bertho@orange.fr');
INSERT INTO `Utilisateurs` VALUES (3,'Bouilly','Didier','dbouil','didier','Ingenieur',NULL);
CREATE TABLE SystemePhaseEssai(  
  idSysteme     INTEGER,   
  idPhaseEssai   INTEGER,  
  FOREIGN KEY(idSysteme) REFERENCES Systeme(idSysteme),
   FOREIGN KEY(idPhaseEssai) REFERENCES PhaseEssai(idPhaseEssai)
  
);
CREATE TABLE "Systeme" (
	`idSysteme`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`nomSysteme`	TEXT,
	`idPID`	TEXT,
	`idUtilisateur`	INTEGER,
	`site`	TEXT,
	FOREIGN KEY(`idPID`) REFERENCES `PID`(`idPID`),
	FOREIGN KEY(`idUtilisateur`) REFERENCES `Utilisateur`(`idUtilisateur`)
);
INSERT INTO `Systeme` VALUES (1,'gzrvvb','testProc1',NULL,'Projet1');
INSERT INTO `Systeme` VALUES (2,'Test1','pidTest',NULL,'Projet3');
INSERT INTO `Systeme` VALUES (3,'Test3','pidTest',NULL,'Projet3');
INSERT INTO `Systeme` VALUES (4,'test4','pidTest',NULL,'Projet2');
CREATE TABLE "RefFT" (
	`idRefFT`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`codeEquipement`	TEXT,
	`date`	TEXT,
	`puissance`	TEXT,
	`vitesse`	TEXT,
	`visa`	TEXT,
	`observations`	TEXT,
	`numFicheType`	TEXT,
	`indiceNumFicheType`	TEXT,
	`SystemeElementaire`	TEXT,
	`Site`	TEXT,
	`TrancheUnite`	TEXT,
	`PEE`	TEXT,
	`IndicePEE`	TEXT,
	`REE`	TEXT,
	`IndiceREE`	TEXT,
	`nomUtilisateur`	TEXT,
	`constructeur`	TEXT,
	`PVEssaisUsine`	TEXT,
	`numFabrication`	TEXT,
	`nomProcedure`	TEXT,
	`systeme`	TEXT,
	FOREIGN KEY(`codeEquipement`) REFERENCES `Equipement`(`idEquipement`)
);
CREATE TABLE "RefDoc" (
	`idRef`	INTEGER,
	`typeDoc`	TEXT,
	`nomProcedure`	TEXT,
	`nomDoc`	TEXT,
	`systeme`	TEXT,
	PRIMARY KEY(`idRef`)
);
CREATE TABLE "ProcedureBypass" (
	`idProcedureBypass`	INTEGER,
	`nomProcedure`	INTEGER,
	`systeme`	INTEGER,
	`user`	INTEGER,
	`commentary`	TEXT,
	PRIMARY KEY(`idProcedureBypass`)
);
CREATE TABLE "Procedure" (
	`indexProcedure`	INTEGER,
	`nomProcedure`	TEXT NOT NULL,
	`systeme`	TEXT NOT NULL,
	`numProcedure`	INTEGER,
	`posX`	INTEGER DEFAULT 0,
	`posY`	INTEGER DEFAULT 0,
	`signed`	INTEGER DEFAULT 0,
	`numProcedurePrec`	INTEGER,
	`typeEquipement`	TEXT,
	`avancement`	INTEGER DEFAULT 0,
	`bypass`	INTEGER DEFAULT 0,
	`user`	INTEGER DEFAULT 0,
	`date`	TEXT DEFAULT ' ',
	PRIMARY KEY(`indexProcedure`)
);
CREATE TABLE "ProcEnsemble" (
	`test1`	TEXT,
	`test2`	TEXT,
	`test3`	INTEGER
);
CREATE TABLE `PhaseEssai` (
	`idPhaseEssai`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`libellePhaseEssai`	TEXT
);
CREATE TABLE `PID` (
	`idPID`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`numPID`	TEXT UNIQUE
);
CREATE TABLE "MoteurMT" (
	`idFTPMoteur`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`codeEquipement`	TEXT,
	`PresencePlaqueSignalitique`	TEXT,
	`VerifBonMontage`	TEXT,
	`VerifPointDur`	TEXT,
	`VerifCircuitRefrigeration`	TEXT,
	`tetesCables`	TEXT,
	`CableSerageBornes`	TEXT,
	`CableConformiteCheminement`	TEXT,
	`continuiteElectriqueCable`	TEXT,
	`CableControleMiseTerre`	TEXT,
	`reperageCables`	TEXT,
	`conformiteProtectionDepartP`	TEXT,
	`miseTerreCarcasseMoteur`	TEXT,
	`miseTerreArmatureCable`	TEXT,
	`miseTerreBoiteResistance`	TEXT,
	`resistanceIsolementMoteurTerre`	TEXT,
	`resistanceIsolementPhase1Terre`	TEXT,
	`resistanceIsolementPhase2Terre`	TEXT,
	`resistanceIsolementPhase3Terre`	TEXT,
	`resistanceIsolementResistChaufTerre`	TEXT,
	`resistanceIsolementPhase12`	TEXT,
	`resistanceIsolementPhase23`	TEXT,
	`resistanceIsolementPhase31`	TEXT,
	`resistanceIsolementResistChaufTerrePhases`	TEXT,
	`ProtectionFusible`	TEXT,
	`ProtectionMagnetoThermique`	TEXT,
	`observations`	TEXT,
	`nomProcedure`	TEXT,
	`systeme`	TEXT,
	FOREIGN KEY(`codeEquipement`) REFERENCES `Equipement`(`idEquipement`)
);
CREATE TABLE `Installation` (
	`idInstallation`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`nomInstallation`	TEXT UNIQUE
);
INSERT INTO `Installation` VALUES (1,'Projet1
');
INSERT INTO `Installation` VALUES (2,'Projet2');
INSERT INTO `Installation` VALUES (3,'Projet3');
CREATE TABLE FluideSysteme(  
  idFluide     INTEGER,   
  idSysteme   INTEGER,  
  FOREIGN KEY(idFluide) REFERENCES Fluide(idFluide),
   FOREIGN KEY(idSysteme) REFERENCES Systeme(idSysteme)
  
);
CREATE TABLE `Fluide` (
	`idFluide`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`libelleFluide`	TEXT
);
CREATE TABLE "FNC" (
	`idFNC`	INTEGER,
	`nomProcedure`	TEXT,
	`systeme`	TEXT,
	`nomFNC`	TEXT,
	`commentary`	TEXT,
	PRIMARY KEY(`idFNC`)
);
CREATE TABLE `Equipement` (
	`idEquipement`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`nomEquipement`	TEXT,
	`typeEquipement`	TEXT,
	`codeEquipement`	TEXT
, idSysteme INTEGER 
REFERENCES Systeme(idSysteme));
INSERT INTO `Equipement` VALUES (1,'MoteurTest
','moteurP',NULL,3);
CREATE TABLE "EMoteurMT" (
	`idEMoteurMT`	INTEGER,
	`nomProcedure`	TEXT,
	`systeme`	TEXT,
	`VerifSensRotation`	TEXT,
	`IntensiteAVidePh1`	TEXT,
	`IntensiteAVidePh3`	TEXT,
	`Tension`	TEXT,
	`VibrationsDesaccouple`	TEXT,
	`VitesseRotAVide`	TEXT,
	`Centrage magnetique`	TEXT,
	`DebitEauRefStator`	TEXT,
	`IntensiteStart`	TEXT,
	`IntensiteAbsorbee`	TEXT,
	`TensionNominale`	TEXT,
	`PuissanceNominale`	TEXT,
	`PuissanceNomCorrigee`	TEXT,
	`TempAmbiante`	TEXT,
	`TempFluideRefrig`	TEXT,
	`TempPalierAvant`	TEXT,
	`TempPalierArriere`	TEXT,
	`TempButee`	TEXT,
	`TempStator`	TEXT,
	`EchauftPalierAvt`	TEXT,
	`EchauftPalierArri`	TEXT,
	`EchauftButee`	TEXT,
	`EchauftStator`	TEXT,
	`VibrationsNominales`	TEXT,
	`VitesseRotNom`	TEXT,
	`DebitEauRefrig`	TEXT,
	PRIMARY KEY(`idEMoteurMT`)
);
CREATE TABLE `DocInstallation` (
	`idDocInstallation`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`titreDocInstallation`	TEXT,
	`urlDocInstallation`	TEXT
);
CREATE TABLE "DocEquipement" (
	`idDocEquipement`	INTEGER PRIMARY KEY AUTOINCREMENT,
	`titreDocEquipement`	TEXT,
	`urlDocEquipement`	TEXT,
	`idEquipement`	INTEGER,
	FOREIGN KEY(`idEquipement`) REFERENCES `Equipement`(`idEquipement`)
);
INSERT INTO `DocEquipement` VALUES (1,'Cellules potent','ISO3MM3PPPPNOX3366_GT02 cellules potent & tab HTA.doc','');
INSERT INTO `DocEquipement` VALUES (2,'Tab contacteur','ISO3MM3PPPPNOX3367_GT03 Tab contacteur HTA.doc',NULL);
INSERT INTO `DocEquipement` VALUES (3,'Tab transfo 400V','ISO3MM3PPPPNOX3368_GT05 tab 400V transfo HT_BT.doc',NULL);
INSERT INTO `DocEquipement` VALUES (4,'Contacteurs BT','ISO3MM3PPPPNOX3369 GT06 contacteurs BT.doc',NULL);
INSERT INTO `DocEquipement` VALUES (5,'Redresseurs chargeurs','ISO3MM3PPPPNOX3370_GT09 redresseurs chargeurs .doc',NULL);
INSERT INTO `DocEquipement` VALUES (6,'Moteur MT','ISO3MM3PPPPNOX3371_GT11 moteur MT.doc',NULL);
INSERT INTO `DocEquipement` VALUES (7,'Moteur BT','ISO3MM3PPPPNOX3372_GT12 moteur BT.doc',NULL);
INSERT INTO `DocEquipement` VALUES (8,'Pompe','ISO3MM3PPPPNOX3373_GT13 pompe.doc',NULL);
INSERT INTO `DocEquipement` VALUES (9,'Ventilation','ISO3MM3PPPPNOX3374_GT14 ventilation.doc',NULL);
INSERT INTO `DocEquipement` VALUES (10,'Robinetterie','ISO3MM3PPPPNOX3375_GT15 robinetterie.doc',NULL);
INSERT INTO `DocEquipement` VALUES (11,'Engin manut levage','ISO3MM3PPPPNOX3376_GT18 engin manut levage.doc',NULL);
INSERT INTO `DocEquipement` VALUES (12,'Instrumentation','ISO3MM3PPPPNOX3377_GT19 instrumentation.doc',NULL);
INSERT INTO `DocEquipement` VALUES (13,'Mesure de bruit','ISO3MM3PPPPNOX3378_GT24 mesure de bruit.doc',NULL);
COMMIT;
