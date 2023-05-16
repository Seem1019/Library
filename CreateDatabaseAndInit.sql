CREATE DATABASE TravelLibrary;
GO

USE TravelLibrary;
GO

CREATE TABLE autores (
    id INT IDENTITY PRIMARY KEY,
    apellidos NVARCHAR(50),
    nombre NVARCHAR(50)
);

CREATE TABLE autores_has_libros (
    autores_id INT,
    libros_ISBN INT,
    PRIMARY KEY (autores_id, libros_ISBN),
    FOREIGN KEY (autores_id) REFERENCES autores(id),
    FOREIGN KEY (libros_ISBN) REFERENCES libros(ISBN)
);

CREATE TABLE libros (
    ISBN INT IDENTITY PRIMARY KEY,
    editoriales_id INT,
    n_paginas NVARCHAR(10),
    sinopsis TEXT,
    title NVARCHAR(50),
    FOREIGN KEY (editoriales_id) REFERENCES editoriales(id)
);

CREATE TABLE editoriales (
    id INT IDENTITY PRIMARY KEY,
    nombre NVARCHAR(45),
    sede NVARCHAR(45)
);

CREATE TABLE seguridad (
    id INT IDENTITY PRIMARY KEY,
    usuario NVARCHAR(50) NOT NULL,
    nombre_de_usuario NVARCHAR(50) NOT NULL,
    contraseña NVARCHAR(255) NOT NULL,
    rol NVARCHAR(15) NOT NULL
);
