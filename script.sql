CREATE DATABASE PROJETOLOJA;

USE PROJETOLOJA;

CREATE TABLE Usuarios(
 Id INT PRIMARY KEY auto_increment,
 Nome Varchar(40) NOT NULL,
 Email Varchar(40) NOT NULL,
 Senha Varchar(40) NOT NULL,
 Nivel Varchar(40) NOT NULL
);

INSERT INTO Usuarios(Nome,Email,Senha,Nivel)
VALUES('Administrador','admin@email.com','123456','admin');

INSERT INTO Usuarios(Nome,Email,Senha,Nivel)
VALUES('Usuario','usuario@email.com','123456','operador');

SELECT * FROM Usuarios;