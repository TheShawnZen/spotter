Use spotter;
INSERT INTO membre (email,username,motdepasse)
VALUES	('theo@email.com','Kespawss','123'),
		('shawn@email.com','Kpop','123'),
        ('radu@email.com','threeR','123'),
        ('wassup@email.com','pouloulou','hollaatme'),
        ('onfrappe@email.com','wigglewiggle','desjardins');

INSERT INTO categorie (nom)
VALUES	('Jeux vidéo'),('Danse'),('Musique'),('Cuisine'),('Sport'), 
		('Sport d\'hiver'), ('Sport Aquatique'),('Lecture'),('Voyage'),
        ('Film'),('Course'),('Fête'),('Randonnée');
        
INSERT INTO activite (membre_id,nom,categorie_id,dateheure,lieu,dsc,motdepasse)
VALUES ('1','Hockey sur glace','6','2021-01-20 14:00','Parc Pierre-Bédard','Une partie de hockey amicale',NULL),
	('2','Rainbow 6','1','2021-01-26 14:00','Cafe Gaming','On fait un tournoi de LAN au cafe Gaming',NULL),
    ('3','Karaoké','3','2020-12-20 15:00','6200 rue Sherbrooke Est','Karaoke en gang',NULL),
    ('1','Natation','7','2020-12-20 19:00','Stade olympique','Cours de natation gratuit',NULL),
    ('4','Club de Lecture','8','2020-12-29 15:00','Bibliothèque Nationale','Fan de lecture vous êtes le bienvenue',NULL),
    ('5','Tournage de film','10','2021-3-20 9:00','Métro Henri-Bourassa','Besoin de figurants',NULL),
    ('1','Jogging 20km','11','2020-04-21 10:00','Parc Maisonneuve','Course à pied de 20km niveau EXPERT ',NULL),
    ('2','Trip à Québec','9','2021-02-10 9:00','Métro Radisson','Nous cherchons des partants pour une fin de semaine à Québec',NULL),
    ('5','Hiking Mont St-Hilaire','13','2020-12-20 12:00','Mont St-Hilaire','Petite randonnée facile',NULL),
    ('2','Chorégraphie Hip-Hop','2','2020-12-19 18:00','Studio de danse Fuzzy','Nous cherchons 3 danseurs',NULL),
    ('4','Grosse brosse','12','2020-12-31 22:00','Appartement 200','La tournée des bars',NULL),
    ('3','Match de Foot','5','2021-05-20 15:00','Terrain de Louis-Riel','Petit match amical, venez en grand nombre',NULL),
    ('1','Soirée sushi','3','2020-12-18 15:00','3263 rue Robert','Vous êtes invités à la soirée sushi','CX5F8S');
		
INSERT INTO participant (membre_id,activite_id)
VALUES 
('2','1'),
('1','2'),
('1','5'),
('2','3'),
('3','6'),
('3','2'),
('4','3');