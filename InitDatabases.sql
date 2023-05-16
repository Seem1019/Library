USE TravelLibrary;
GO

-- Inicializar tabla de autores
INSERT INTO autores (apellidos, nombre) 
VALUES ('García Márquez', 'Gabriel'), 
       ('Allende', 'Isabel'),
       ('Borges', 'Jorge Luis'),
       ('Cortázar', 'Julio'),
       ('Paz', 'Octavio');

-- Inicializar tabla de editoriales
INSERT INTO editoriales (nombre, sede) 
VALUES ('Editorial Planeta', 'Barcelona'), 
       ('Editorial Sudamericana', 'Buenos Aires');

-- Inicializar tabla de libros
INSERT INTO libros (editoriales_id, n_paginas, sinopsis, title) 
VALUES (1, '148', 'Un relato de amor y tragedia en Macondo', 'Cien años de soledad'),
       (1, '200', 'Una historia de amor y magia', 'La casa de los espíritus'),
       (2, '95', 'Una colección de cuentos fantásticos', 'Ficciones'),
       (2, '150', 'Una exploración del amor y el tiempo', 'Rayuela'),
       (1, '120', 'Reflexiones sobre la vida y la poesía', 'El laberinto de la soledad');

-- Inicializar tabla de relaciones autor-libro
INSERT INTO autores_has_libros (autores_id, libros_ISBN) 
VALUES (1, 1), 
       (2, 2),
       (3, 3),
       (4, 4),
       (5, 5);

-- Inicializar tabla de seguridad
INSERT INTO seguridad (usuario, nombre_de_usuario, contraseña, rol)
VALUES ('admin', 'Administrador', 'admin', 'Administrador'), 
       ('consumer', 'Usuario', 'consumer', 'Consumer');
