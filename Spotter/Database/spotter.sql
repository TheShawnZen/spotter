DROP DATABASE IF EXISTS spotter;

CREATE DATABASE spotter;
USE spotter;

ALTER SCHEMA `spotter` DEFAULT CHARACTER SET utf8;


DROP TABLE IF EXISTS participant;
DROP TABLE IF EXISTS activite;
DROP TABLE IF EXISTS categorie;
DROP TABLE IF EXISTS membre;

CREATE TABLE membre(
	id				INT				NOT NULL AUTO_INCREMENT,
	email			VARCHAR(255)	NOT NULL,
	username		VARCHAR(255)	NOT NULL,
	motdepasse	    VARCHAR(255)	NOT NULL,
	PRIMARY KEY (id),
	UNIQUE (username),
    UNIQUE (email)
);

CREATE TABLE categorie(
    id              INT             NOT NULL AUTO_INCREMENT,
    nom             VARCHAR(255)           NOT NULL,
    PRIMARY KEY(id)
);

CREATE TABLE activite(
    id              INT             NOT NULL AUTO_INCREMENT,
    membre_id       INT             NOT NULL,
    nom             VARCHAR(255)    NOT NULL,
    categorie_id	INT	            NOT NULL,
    dateheure		DATETIME		NOT NULL,
    lieu            VARCHAR(255)    NOT NULL,
    dsc             VARCHAR(255)        NULL,
    motdepasse      VARCHAR(255)        NULL,
    PRIMARY KEY(id, membre_id,categorie_id),
    FOREIGN KEY(membre_id) REFERENCES membre(id) ON DELETE CASCADE,
    FOREIGN KEY(categorie_id) REFERENCES categorie(id)
);

CREATE TABLE participant(
    id              INT             NOT NULL AUTO_INCREMENT,
    membre_id       INT             NOT NULL,
    activite_id     INT             NOT NULL,
    PRIMARY KEY(id,membre_id,activite_id),
    FOREIGN KEY(membre_id) REFERENCES membre(id) ON DELETE CASCADE,
    FOREIGN KEY(activite_id) REFERENCES activite(id) ON DELETE CASCADE
);

