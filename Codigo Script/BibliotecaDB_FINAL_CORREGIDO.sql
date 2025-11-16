USE master
GO

-- Eliminar base de datos si existe
IF EXISTS(SELECT * FROM sys.databases WHERE name='BibliotecaDB')
BEGIN
    ALTER DATABASE BibliotecaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE BibliotecaDB
END
GO

-- Crear la base de datos
CREATE DATABASE BibliotecaDB
GO

USE BibliotecaDB
GO

-- =============================================
-- TABLAS PRINCIPALES
-- =============================================

-- Tabla de Roles
CREATE TABLE Roles (
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    NombreRol NVARCHAR(50) UNIQUE NOT NULL,
    Descripcion NVARCHAR(200)
)
GO

-- Tabla de Usuarios del Sistema (Login)
CREATE TABLE UsuariosSistema (
    IdUsuarioSistema INT PRIMARY KEY IDENTITY(1,1),
    Usuario NVARCHAR(50) UNIQUE NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL,
    IdRol INT FOREIGN KEY REFERENCES Roles(IdRol),
    NombreCompleto NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
)
GO

-- Tabla de Usuarios/Lectores (usuarios de biblioteca)
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    IdUsuarioSistema INT NULL FOREIGN KEY REFERENCES UsuariosSistema(IdUsuarioSistema),
    NombreCompleto NVARCHAR(100) NOT NULL,
    DNI NVARCHAR(20) UNIQUE NOT NULL,
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    Direccion NVARCHAR(200),
    TipoUsuario NVARCHAR(20) DEFAULT 'Externo', -- Externo o ConCuenta
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
)
GO

-- Tabla de Libros
CREATE TABLE Libros (
    IdLibro INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(200) NOT NULL,
    Autor NVARCHAR(150) NOT NULL,
    Editorial NVARCHAR(100),
    ISBN NVARCHAR(50) UNIQUE,
    AnioPublicacion INT,
    Categoria NVARCHAR(50),
    CantidadTotal INT DEFAULT 1,
    CantidadDisponible INT DEFAULT 1,
    Ubicacion NVARCHAR(50),
    Estado BIT DEFAULT 1
)
GO

-- Tabla de Préstamos
CREATE TABLE Prestamos (
    IdPrestamo INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    IdLibro INT FOREIGN KEY REFERENCES Libros(IdLibro),
    IdBibliotecario INT NULL FOREIGN KEY REFERENCES UsuariosSistema(IdUsuarioSistema),
    FechaPrestamo DATETIME DEFAULT GETDATE(),
    FechaDevolucionEstimada DATETIME NOT NULL,
    FechaDevolucionReal DATETIME NULL,
    Estado NVARCHAR(20) DEFAULT 'Prestado', -- Prestado, Devuelto, Retrasado
    MultaDias INT DEFAULT 0,
    MontoMulta DECIMAL(10,2) DEFAULT 0,
    Observaciones NVARCHAR(500)
)
GO

-- Tabla de Reservas (para usuarios lectores)
CREATE TABLE Reservas (
    IdReserva INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    IdLibro INT FOREIGN KEY REFERENCES Libros(IdLibro),
    FechaReserva DATETIME DEFAULT GETDATE(),
    FechaExpiracion DATETIME NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'Pendiente', -- Pendiente, Aprobada, Rechazada, Caducada, Completada
    Observaciones NVARCHAR(500)
)
GO

-- Tabla de Solicitudes de Préstamo
CREATE TABLE SolicitudesPrestamo (
    IdSolicitud INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    IdLibro INT FOREIGN KEY REFERENCES Libros(IdLibro),
    FechaSolicitud DATETIME DEFAULT GETDATE(),
    DiasRequeridos INT DEFAULT 15,
    Estado NVARCHAR(20) DEFAULT 'Pendiente', -- Pendiente, Aprobada, Rechazada
    IdBibliotecarioRevisa INT NULL FOREIGN KEY REFERENCES UsuariosSistema(IdUsuarioSistema),
    FechaRevision DATETIME NULL,
    Observaciones NVARCHAR(500)
)
GO

-- =============================================
-- INSERTAR ROLES
-- =============================================

INSERT INTO Roles (NombreRol, Descripcion) VALUES 
('Administrador', 'Control total del sistema. Supervisa y gestiona todos los módulos.'),
('Bibliotecario', 'Operación diaria de la biblioteca, gestiona préstamos y devoluciones.'),
('Usuario Lector', 'Usuario final del sistema que consulta catálogo y realiza reservas.')
GO

-- =============================================
-- INSERTAR USUARIOS DEL SISTEMA CON NOMBRES PERSONALIZADOS
-- =============================================

-- USUARIOS ADMINISTRADORES
INSERT INTO UsuariosSistema (Usuario, Contrasena, IdRol, NombreCompleto, Email)
VALUES 
('admin', '12345678', 1, 'Administrador Principal', 'admin@biblioteca.com'),
('llerena', '12345678', 1, 'Llerena Administrador', 'llerena@biblioteca.com'),
-- PUEDES AGREGAR MÁS USUARIOS ADMINISTRADORES AQUÍ
('director', 'director123', 1, 'Director de Biblioteca', 'director@biblioteca.com')
GO

-- USUARIOS BIBLIOTECARIOS
INSERT INTO UsuariosSistema (Usuario, Contrasena, IdRol, NombreCompleto, Email)
VALUES 
('bibliotecario', '12345678', 2, 'María López Sánchez', 'maria.lopez@biblioteca.com'),
('biblioteca1', 'password', 2, 'Carlos Ramírez Torres', 'carlos.ramirez@biblioteca.com'),
('biblio', '123456', 2, 'Ana Martínez García', 'ana.martinez@biblioteca.com'),
-- PUEDES AGREGAR MÁS BIBLIOTECARIOS AQUÍ
('asistente', 'asistente123', 2, 'José Pérez Asistente', 'jose.perez@biblioteca.com')
GO

-- USUARIOS LECTORES CON CUENTA
INSERT INTO UsuariosSistema (Usuario, Contrasena, IdRol, NombreCompleto, Email)
VALUES 
('usuario', '12345678', 3, 'Juan Pérez García', 'juan.perez@email.com'),
('lector', 'password', 3, 'Ana Torres Vega', 'ana.torres@email.com'),
('estudiante', '123456', 3, 'Pedro Sánchez López', 'pedro.sanchez@email.com'),
('miguel', 'miguel123', 3, 'Miguel Rodríguez Silva', 'miguel.rodriguez@email.com'),
('sofia', 'sofia456', 3, 'Sofía González Ruiz', 'sofia.gonzalez@email.com'),
-- PUEDES AGREGAR MÁS LECTORES AQUÍ
('carlos', 'carlos789', 3, 'Carlos Mendoza', 'carlos.mendoza@email.com'),
('patricia', 'patri321', 3, 'Patricia Flores', 'patricia.flores@email.com')
GO

-- =============================================
-- INSERTAR USUARIOS/LECTORES EN TABLA USUARIOS
-- =============================================

-- Usuarios con cuenta en el sistema (vinculados)
INSERT INTO Usuarios (IdUsuarioSistema, NombreCompleto, DNI, Telefono, Email, Direccion, TipoUsuario)
SELECT 
    IdUsuarioSistema,
    NombreCompleto,
    '1000000' + CAST(IdUsuarioSistema AS NVARCHAR(10)) AS DNI,
    '98765' + RIGHT('0000' + CAST(IdUsuarioSistema AS NVARCHAR(10)), 4) AS Telefono,
    Email,
    'Dirección ' + CAST(IdUsuarioSistema AS NVARCHAR(10)) + ', Lima' AS Direccion,
    'ConCuenta' AS TipoUsuario
FROM UsuariosSistema
WHERE IdRol = 3 -- Solo los usuarios lectores
GO

-- Usuarios externos (sin cuenta en sistema)
INSERT INTO Usuarios (NombreCompleto, DNI, Telefono, Email, Direccion, TipoUsuario)
VALUES 
('María González López', '87654321', '912345678', 'maria.gonzalez@email.com', 'Jr. Los Olivos 456, Lima', 'Externo'),
('Carlos Rodríguez Silva', '11223344', '965432178', 'carlos.rodriguez.externo@email.com', 'Calle Lima 789, Callao', 'Externo'),
('Patricia Morales Díaz', '33445566', '977889900', 'patricia.morales@email.com', 'Av. Arequipa 567, Lima', 'Externo'),
('Roberto Fernández Paz', '44556677', '988776655', 'roberto.fernandez@email.com', 'Jr. Miraflores 234, Lima', 'Externo'),
('Lucía Vargas Torres', '55667788', '999887766', 'lucia.vargas@email.com', 'Av. Brasil 890, Lima', 'Externo')
GO

-- =============================================
-- INSERTAR LIBROS
-- =============================================

INSERT INTO Libros (Titulo, Autor, Editorial, ISBN, AnioPublicacion, Categoria, CantidadTotal, CantidadDisponible, Ubicacion)
VALUES 
('Cien Años de Soledad', 'Gabriel García Márquez', 'Sudamericana', '978-0307474728', 1967, 'Ficción', 3, 3, 'A-01'),
('El Principito', 'Antoine de Saint-Exupéry', 'Salamandra', '978-8498381498', 1943, 'Infantil', 5, 5, 'B-15'),
('Don Quijote de la Mancha', 'Miguel de Cervantes', 'Alfaguara', '978-8420412146', 1605, 'Clásicos', 2, 2, 'C-23'),
('1984', 'George Orwell', 'Debolsillo', '978-8499890944', 1949, 'Distopía', 4, 4, 'A-12'),
('Harry Potter y la Piedra Filosofal', 'J.K. Rowling', 'Salamandra', '978-8478884452', 1997, 'Fantasía', 6, 6, 'D-08'),
('Rayuela', 'Julio Cortázar', 'Alfaguara', '978-8420471891', 1963, 'Ficción', 2, 2, 'A-15'),
('El Amor en los Tiempos del Cólera', 'Gabriel García Márquez', 'Oveja Negra', '978-0307389732', 1985, 'Romance', 3, 3, 'A-02'),
('Crónica de una Muerte Anunciada', 'Gabriel García Márquez', 'Diana', '978-0307475282', 1981, 'Ficción', 2, 2, 'A-03'),
('La Casa de los Espíritus', 'Isabel Allende', 'Plaza & Janés', '978-0525433446', 1982, 'Ficción', 3, 3, 'B-10'),
('El Túnel', 'Ernesto Sábato', 'Seix Barral', '978-8432217326', 1948, 'Ficción', 2, 2, 'C-05'),
('Orgullo y Prejuicio', 'Jane Austen', 'Penguin', '978-0141439518', 1813, 'Romance', 3, 3, 'B-20'),
('El Hobbit', 'J.R.R. Tolkien', 'Minotauro', '978-8445000656', 1937, 'Fantasía', 4, 4, 'D-10'),
('Crimen y Castigo', 'Fiódor Dostoyevski', 'Cátedra', '978-8437622569', 1866, 'Clásicos', 2, 2, 'C-15'),
('El Gran Gatsby', 'F. Scott Fitzgerald', 'Alianza', '978-8420676', 1925, 'Ficción', 3, 3, 'A-18'),
('Los Miserables', 'Victor Hugo', 'Planeta', '978-8408089803', 1862, 'Clásicos', 2, 2, 'C-30'),
('La Odisea', 'Homero', 'Gredos', '978-8424935740', -800, 'Clásicos', 2, 2, 'C-01'),
('Hamlet', 'William Shakespeare', 'Cátedra', '978-8437604190', 1603, 'Teatro', 3, 3, 'E-05'),
('La Metamorfosis', 'Franz Kafka', 'Alianza', '978-8420633459', 1915, 'Ficción', 4, 4, 'A-20'),
('El Código Da Vinci', 'Dan Brown', 'Planeta', '978-8408076759', 2003, 'Thriller', 5, 5, 'F-01'),
('Sapiens', 'Yuval Noah Harari', 'Debate', '978-8499926223', 2011, 'Historia', 3, 3, 'G-10')
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS PRINCIPALES
-- =============================================

-- SP: Crear Nuevo Usuario (PROCEDIMIENTO PARA CREAR USUARIOS PERSONALIZADOS)
CREATE PROCEDURE sp_CrearNuevoUsuario
    @Usuario NVARCHAR(50),
    @Contrasena NVARCHAR(100),
    @Rol NVARCHAR(50), -- 'Administrador', 'Bibliotecario', o 'Usuario Lector'
    @NombreCompleto NVARCHAR(100),
    @Email NVARCHAR(100),
    @CrearUsuarioBiblioteca BIT = 0, -- Si es 1, también crea usuario en tabla Usuarios
    @DNI NVARCHAR(20) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Direccion NVARCHAR(200) = NULL
AS
BEGIN
    BEGIN TRANSACTION
    
    -- Verificar si el usuario ya existe
    IF EXISTS (SELECT 1 FROM UsuariosSistema WHERE Usuario = @Usuario)
    BEGIN
        ROLLBACK TRANSACTION
        SELECT 0 AS Resultado, 'Error: El nombre de usuario ya existe' AS Mensaje
        RETURN
    END
    
    DECLARE @IdRol INT
    SELECT @IdRol = IdRol FROM Roles WHERE NombreRol = @Rol
    
    IF @IdRol IS NULL
    BEGIN
        ROLLBACK TRANSACTION
        SELECT 0 AS Resultado, 'Error: Rol no válido. Use: Administrador, Bibliotecario o Usuario Lector' AS Mensaje
        RETURN
    END
    
    -- Insertar en UsuariosSistema
    INSERT INTO UsuariosSistema (Usuario, Contrasena, IdRol, NombreCompleto, Email)
    VALUES (@Usuario, @Contrasena, @IdRol, @NombreCompleto, @Email)
    
    DECLARE @IdUsuarioSistema INT = SCOPE_IDENTITY()
    
    -- Si es Usuario Lector y se solicita crear en tabla Usuarios
    IF @IdRol = 3 AND @CrearUsuarioBiblioteca = 1
    BEGIN
        -- Generar DNI si no se proporciona
        IF @DNI IS NULL
            SET @DNI = '999' + CAST(@IdUsuarioSistema AS NVARCHAR(10)) + RIGHT(CAST(DATEPART(MILLISECOND, GETDATE()) AS NVARCHAR(10)), 3)
        
        -- Verificar que el DNI no exista
        IF EXISTS (SELECT 1 FROM Usuarios WHERE DNI = @DNI)
        BEGIN
            ROLLBACK TRANSACTION
            SELECT 0 AS Resultado, 'Error: El DNI ya existe' AS Mensaje
            RETURN
        END
        
        INSERT INTO Usuarios (IdUsuarioSistema, NombreCompleto, DNI, Telefono, Email, Direccion, TipoUsuario)
        VALUES (@IdUsuarioSistema, @NombreCompleto, @DNI, @Telefono, @Email, @Direccion, 'ConCuenta')
    END
    
    COMMIT TRANSACTION
    SELECT 1 AS Resultado, 'Usuario creado exitosamente' AS Mensaje, @IdUsuarioSistema AS IdUsuario
END
GO

-- SP: Login de Usuario
CREATE PROCEDURE sp_LoginUsuario
    @Usuario NVARCHAR(50),
    @Contrasena NVARCHAR(100)
AS
BEGIN
    SELECT 
        US.IdUsuarioSistema,
        US.Usuario,
        US.NombreCompleto,
        US.Email,
        R.IdRol,
        R.NombreRol,
        US.Estado,
        CASE 
            WHEN US.Estado = 1 THEN 'Login exitoso'
            ELSE 'Usuario desactivado'
        END AS Mensaje
    FROM UsuariosSistema US
    INNER JOIN Roles R ON US.IdRol = R.IdRol
    WHERE US.Usuario = @Usuario AND US.Contrasena = @Contrasena
    
    IF @@ROWCOUNT = 0
    BEGIN
        SELECT 'Usuario o contraseña incorrecta' AS Mensaje
    END
END
GO

-- SP: Cambiar Contraseña
CREATE PROCEDURE sp_CambiarContrasena
    @Usuario NVARCHAR(50),
    @ContrasenaActual NVARCHAR(100),
    @ContrasenaNueva NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM UsuariosSistema WHERE Usuario = @Usuario AND Contrasena = @ContrasenaActual)
    BEGIN
        UPDATE UsuariosSistema
        SET Contrasena = @ContrasenaNueva
        WHERE Usuario = @Usuario
        
        SELECT 1 AS Resultado, 'Contraseña actualizada exitosamente' AS Mensaje
    END
    ELSE
    BEGIN
        SELECT 0 AS Resultado, 'Usuario o contraseña actual incorrecta' AS Mensaje
    END
END
GO

-- SP: Listar Todos los Usuarios del Sistema
CREATE PROCEDURE sp_ListarUsuariosSistema
AS
BEGIN
    SELECT 
        US.IdUsuarioSistema,
        US.Usuario,
        US.NombreCompleto,
        US.Email,
        R.NombreRol AS Rol,
        US.FechaCreacion,
        CASE WHEN US.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
    FROM UsuariosSistema US
    INNER JOIN Roles R ON US.IdRol = R.IdRol
    ORDER BY R.IdRol, US.NombreCompleto
END
GO

-- SP: Buscar Libros
CREATE PROCEDURE sp_BuscarLibros
    @Criterio NVARCHAR(200)
AS
BEGIN
    SELECT * FROM Libros 
    WHERE Estado = 1 AND
    (Titulo LIKE '%' + @Criterio + '%' OR 
     Autor LIKE '%' + @Criterio + '%' OR
     ISBN LIKE '%' + @Criterio + '%' OR
     Categoria LIKE '%' + @Criterio + '%')
END
GO

-- SP: Registrar Préstamo
CREATE PROCEDURE sp_RegistrarPrestamo
    @IdUsuario INT,
    @IdLibro INT,
    @DiasPrestamo INT,
    @IdBibliotecario INT = NULL
AS
BEGIN
    BEGIN TRANSACTION
    
    DECLARE @CantDisp INT
    SELECT @CantDisp = CantidadDisponible FROM Libros WHERE IdLibro = @IdLibro
    
    IF @CantDisp > 0
    BEGIN
        INSERT INTO Prestamos (IdUsuario, IdLibro, IdBibliotecario, FechaDevolucionEstimada)
        VALUES (@IdUsuario, @IdLibro, @IdBibliotecario, DATEADD(DAY, @DiasPrestamo, GETDATE()))
        
        UPDATE Libros SET CantidadDisponible = CantidadDisponible - 1
        WHERE IdLibro = @IdLibro
        
        COMMIT TRANSACTION
        SELECT 1 AS Resultado, 'Préstamo registrado exitosamente' AS Mensaje
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION
        SELECT 0 AS Resultado, 'No hay libros disponibles' AS Mensaje
    END
END
GO

-- SP: Devolver Libro
CREATE PROCEDURE sp_DevolverLibro
    @IdPrestamo INT
AS
BEGIN
    BEGIN TRANSACTION
    
    DECLARE @IdLibro INT
    DECLARE @FechaEstimada DATETIME
    DECLARE @DiasRetraso INT
    
    SELECT @IdLibro = IdLibro, @FechaEstimada = FechaDevolucionEstimada
    FROM Prestamos WHERE IdPrestamo = @IdPrestamo
    
    SET @DiasRetraso = DATEDIFF(DAY, @FechaEstimada, GETDATE())
    IF @DiasRetraso < 0 SET @DiasRetraso = 0
    
    UPDATE Prestamos 
    SET FechaDevolucionReal = GETDATE(),
        Estado = CASE WHEN @DiasRetraso > 0 THEN 'Retrasado' ELSE 'Devuelto' END,
        MultaDias = @DiasRetraso,
        MontoMulta = @DiasRetraso * 2.00
    WHERE IdPrestamo = @IdPrestamo
    
    UPDATE Libros SET CantidadDisponible = CantidadDisponible + 1
    WHERE IdLibro = @IdLibro
    
    COMMIT TRANSACTION
    SELECT @DiasRetraso AS DiasRetraso, (@DiasRetraso * 2.00) AS Multa
END
GO

-- SP: Libros Más Populares
CREATE PROCEDURE sp_LibrosMasPopulares
AS
BEGIN
    SELECT TOP 10 
        L.Titulo, 
        L.Autor,
        COUNT(P.IdPrestamo) AS TotalPrestamos
    FROM Libros L
    LEFT JOIN Prestamos P ON L.IdLibro = P.IdLibro
    GROUP BY L.IdLibro, L.Titulo, L.Autor
    ORDER BY TotalPrestamos DESC
END
GO

-- SP: Reporte de Multas
CREATE PROCEDURE sp_ReporteMultas
AS
BEGIN
    SELECT 
        U.NombreCompleto,
        U.DNI,
        L.Titulo,
        P.FechaPrestamo,
        P.FechaDevolucionEstimada,
        P.FechaDevolucionReal,
        P.MultaDias,
        P.MontoMulta,
        P.Estado
    FROM Prestamos P
    INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
    INNER JOIN Libros L ON P.IdLibro = L.IdLibro
    WHERE P.MultaDias > 0
    ORDER BY P.MontoMulta DESC
END
GO

-- SP: Crear Solicitud de Préstamo
CREATE PROCEDURE sp_CrearSolicitudPrestamo
    @IdUsuario INT,
    @IdLibro INT,
    @DiasRequeridos INT,
    @Observaciones NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO SolicitudesPrestamo (IdUsuario, IdLibro, DiasRequeridos, Observaciones)
    VALUES (@IdUsuario, @IdLibro, @DiasRequeridos, @Observaciones)
    
    SELECT SCOPE_IDENTITY() AS IdSolicitud, 'Solicitud creada exitosamente' AS Mensaje
END
GO

-- SP: Aprobar Solicitud de Préstamo
CREATE PROCEDURE sp_AprobarSolicitudPrestamo
    @IdSolicitud INT,
    @IdBibliotecario INT
AS
BEGIN
    BEGIN TRANSACTION
    
    DECLARE @IdUsuario INT, @IdLibro INT, @DiasRequeridos INT
    DECLARE @CantDisp INT
    
    SELECT @IdUsuario = IdUsuario, @IdLibro = IdLibro, @DiasRequeridos = DiasRequeridos
    FROM SolicitudesPrestamo WHERE IdSolicitud = @IdSolicitud
    
    SELECT @CantDisp = CantidadDisponible FROM Libros WHERE IdLibro = @IdLibro
    
    IF @CantDisp > 0
    BEGIN
        UPDATE SolicitudesPrestamo 
        SET Estado = 'Aprobada', 
            IdBibliotecarioRevisa = @IdBibliotecario,
            FechaRevision = GETDATE()
        WHERE IdSolicitud = @IdSolicitud
        
        INSERT INTO Prestamos (IdUsuario, IdLibro, IdBibliotecario, FechaDevolucionEstimada)
        VALUES (@IdUsuario, @IdLibro, @IdBibliotecario, DATEADD(DAY, @DiasRequeridos, GETDATE()))
        
        UPDATE Libros SET CantidadDisponible = CantidadDisponible - 1
        WHERE IdLibro = @IdLibro
        
        COMMIT TRANSACTION
        SELECT 1 AS Resultado, 'Solicitud aprobada y préstamo creado' AS Mensaje
    END
    ELSE
    BEGIN
        UPDATE SolicitudesPrestamo 
        SET Estado = 'Rechazada', 
            IdBibliotecarioRevisa = @IdBibliotecario,
            FechaRevision = GETDATE(),
            Observaciones = ISNULL(Observaciones + ' | ', '') + 'Rechazada: No hay libros disponibles'
        WHERE IdSolicitud = @IdSolicitud
        
        COMMIT TRANSACTION
        SELECT 0 AS Resultado, 'Solicitud rechazada: No hay libros disponibles' AS Mensaje
    END
END
GO

-- SP: Obtener Solicitudes Pendientes Ordenadas
CREATE PROCEDURE sp_ObtenerSolicitudesPendientes
AS
BEGIN
    SELECT 
        S.IdSolicitud,
        U.NombreCompleto AS Usuario,
        U.DNI,
        L.Titulo AS Libro,
        L.Autor,
        S.FechaSolicitud,
        S.DiasRequeridos,
        S.Observaciones
    FROM SolicitudesPrestamo S
    INNER JOIN Usuarios U ON S.IdUsuario = U.IdUsuario
    INNER JOIN Libros L ON S.IdLibro = L.IdLibro
    WHERE S.Estado = 'Pendiente'
    ORDER BY S.FechaSolicitud
END
GO

-- SP: Obtener Estadísticas del Sistema
CREATE PROCEDURE sp_EstadisticasSistema
AS
BEGIN
    SELECT 
        (SELECT COUNT(*) FROM Libros WHERE Estado = 1) AS TotalLibros,
        (SELECT SUM(CantidadDisponible) FROM Libros WHERE Estado = 1) AS LibrosDisponibles,
        (SELECT COUNT(*) FROM Usuarios WHERE Estado = 1) AS TotalUsuarios,
        (SELECT COUNT(*) FROM Prestamos WHERE Estado = 'Prestado') AS PrestamosActivos,
        (SELECT COUNT(*) FROM Prestamos WHERE Estado = 'Retrasado' OR (FechaDevolucionEstimada < GETDATE() AND FechaDevolucionReal IS NULL)) AS PrestamosRetrasados,
        (SELECT COUNT(*) FROM SolicitudesPrestamo WHERE Estado = 'Pendiente') AS SolicitudesPendientes,
        (SELECT COUNT(*) FROM Reservas WHERE Estado = 'Pendiente' AND FechaExpiracion >= GETDATE()) AS ReservasActivas,
        (SELECT COUNT(*) FROM UsuariosSistema WHERE Estado = 1) AS UsuariosSistemaActivos
END
GO

-- =============================================
-- VISTAS PARA REPORTES (SIN ORDER BY)
-- =============================================

-- Vista de Préstamos Activos
CREATE VIEW vw_PrestamosActivos
AS
SELECT 
    P.IdPrestamo,
    U.NombreCompleto AS Usuario,
    U.DNI,
    L.Titulo AS Libro,
    L.Autor,
    P.FechaPrestamo,
    P.FechaDevolucionEstimada,
    DATEDIFF(DAY, GETDATE(), P.FechaDevolucionEstimada) AS DiasRestantes,
    B.NombreCompleto AS Bibliotecario
FROM Prestamos P
INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
INNER JOIN Libros L ON P.IdLibro = L.IdLibro
LEFT JOIN UsuariosSistema B ON P.IdBibliotecario = B.IdUsuarioSistema
WHERE P.Estado = 'Prestado'
GO

-- Vista de Inventario de Libros
CREATE VIEW vw_InventarioLibros
AS
SELECT 
    IdLibro,
    Titulo,
    Autor,
    Editorial,
    ISBN,
    Categoria,
    CantidadTotal,
    CantidadDisponible,
    (CantidadTotal - CantidadDisponible) AS CantidadPrestada,
    Ubicacion,
    CASE 
        WHEN CantidadDisponible = 0 THEN 'Sin Stock'
        WHEN CantidadDisponible < (CantidadTotal * 0.3) THEN 'Stock Bajo'
        ELSE 'Disponible'
    END AS EstadoStock
FROM Libros
WHERE Estado = 1
GO

-- Vista de Solicitudes Pendientes (CORREGIDA - SIN ORDER BY)
CREATE VIEW vw_SolicitudesPendientes
AS
SELECT 
    S.IdSolicitud,
    U.NombreCompleto AS Usuario,
    U.DNI,
    L.Titulo AS Libro,
    L.Autor,
    S.FechaSolicitud,
    S.DiasRequeridos,
    S.Observaciones
FROM SolicitudesPrestamo S
INNER JOIN Usuarios U ON S.IdUsuario = U.IdUsuario
INNER JOIN Libros L ON S.IdLibro = L.IdLibro
WHERE S.Estado = 'Pendiente'
GO

-- Vista de Historial de Préstamos
CREATE VIEW vw_HistorialPrestamos
AS
SELECT 
    P.IdPrestamo,
    U.NombreCompleto AS Usuario,
    U.DNI,
    L.Titulo AS Libro,
    L.Autor,
    P.FechaPrestamo,
    P.FechaDevolucionEstimada,
    P.FechaDevolucionReal,
    P.Estado,
    P.MultaDias,
    P.MontoMulta,
    B.NombreCompleto AS Bibliotecario
FROM Prestamos P
INNER JOIN Usuarios U ON P.IdUsuario = U.IdUsuario
INNER JOIN Libros L ON P.IdLibro = L.IdLibro
LEFT JOIN UsuariosSistema B ON P.IdBibliotecario = B.IdUsuarioSistema
GO

-- Vista de Usuarios del Sistema
CREATE VIEW vw_UsuariosSistema
AS
SELECT 
    US.IdUsuarioSistema,
    US.Usuario,
    US.NombreCompleto,
    US.Email,
    R.NombreRol AS Rol,
    US.FechaCreacion,
    CASE WHEN US.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
FROM UsuariosSistema US
INNER JOIN Roles R ON US.IdRol = R.IdRol
GO

-- =============================================
-- FUNCIONES ÚTILES
-- =============================================

-- Función para calcular multa
CREATE FUNCTION fn_CalcularMulta
(
    @FechaDevolucionEstimada DATETIME,
    @FechaDevolucionReal DATETIME
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @DiasRetraso INT
    DECLARE @Multa DECIMAL(10,2)
    
    SET @DiasRetraso = DATEDIFF(DAY, @FechaDevolucionEstimada, @FechaDevolucionReal)
    
    IF @DiasRetraso > 0
        SET @Multa = @DiasRetraso * 2.00
    ELSE
        SET @Multa = 0
    
    RETURN @Multa
END
GO

-- Función para verificar disponibilidad
CREATE FUNCTION fn_VerificarDisponibilidad
(
    @IdLibro INT
)
RETURNS BIT
AS
BEGIN
    DECLARE @Disponible BIT
    DECLARE @Cantidad INT
    
    SELECT @Cantidad = CantidadDisponible 
    FROM Libros 
    WHERE IdLibro = @IdLibro
    
    IF @Cantidad > 0
        SET @Disponible = 1
    ELSE
        SET @Disponible = 0
    
    RETURN @Disponible
END
GO

-- =============================================
-- TRIGGERS
-- =============================================

-- Trigger para actualizar estado de préstamos retrasados
CREATE TRIGGER trg_ActualizarPrestamosRetrasados
ON Prestamos
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Prestamos
    SET Estado = 'Retrasado'
    WHERE FechaDevolucionEstimada < GETDATE() 
    AND FechaDevolucionReal IS NULL
    AND Estado = 'Prestado'
END
GO

-- Trigger para validar cantidad disponible
CREATE TRIGGER trg_ValidarCantidadDisponible
ON Libros
AFTER UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE CantidadDisponible < 0)
    BEGIN
        RAISERROR ('La cantidad disponible no puede ser negativa', 16, 1)
        ROLLBACK TRANSACTION
    END
END
GO

-- =============================================
-- INSERTAR DATOS DE EJEMPLO Y PRUEBAS
-- =============================================

-- Insertar algunos préstamos de ejemplo
DECLARE @IdUsuario1 INT = (SELECT TOP 1 IdUsuario FROM Usuarios WHERE TipoUsuario = 'ConCuenta')
DECLARE @IdUsuario2 INT = (SELECT TOP 1 IdUsuario FROM Usuarios WHERE TipoUsuario = 'Externo')
DECLARE @IdBiblio INT = (SELECT TOP 1 IdUsuarioSistema FROM UsuariosSistema WHERE IdRol = 2)

IF @IdUsuario1 IS NOT NULL AND @IdBiblio IS NOT NULL
BEGIN
    INSERT INTO Prestamos (IdUsuario, IdLibro, IdBibliotecario, FechaDevolucionEstimada)
    VALUES (@IdUsuario1, 1, @IdBiblio, DATEADD(DAY, 15, GETDATE()))
    
    UPDATE Libros SET CantidadDisponible = CantidadDisponible - 1 WHERE IdLibro = 1
END
GO

-- Insertar algunas solicitudes de préstamo de ejemplo
DECLARE @IdUsuario4 INT = (SELECT IdUsuario FROM Usuarios WHERE DNI = '87654321')
DECLARE @IdUsuario5 INT = (SELECT IdUsuario FROM Usuarios WHERE DNI = '33445566')

IF @IdUsuario4 IS NOT NULL
BEGIN
    INSERT INTO SolicitudesPrestamo (IdUsuario, IdLibro, DiasRequeridos, Observaciones)
    VALUES (@IdUsuario4, 5, 20, 'Necesito el libro para investigación')
END

IF @IdUsuario5 IS NOT NULL
BEGIN
    INSERT INTO SolicitudesPrestamo (IdUsuario, IdLibro, DiasRequeridos, Observaciones)
    VALUES (@IdUsuario5, 7, 15, 'Para lectura personal')
END
GO

-- =============================================
-- EJEMPLOS DE USO DE PROCEDIMIENTOS
-- =============================================

-- EJEMPLO 1: CREAR UN NUEVO USUARIO ADMINISTRADOR
PRINT '================================================='
PRINT 'EJEMPLO DE CREACIÓN DE USUARIOS:'
PRINT '================================================='
GO

EXEC sp_CrearNuevoUsuario 
    @Usuario = 'nuevo_admin',
    @Contrasena = 'admin2024',
    @Rol = 'Administrador',
    @NombreCompleto = 'Nuevo Administrador',
    @Email = 'nuevo.admin@biblioteca.com',
    @CrearUsuarioBiblioteca = 0
GO

-- EJEMPLO 2: CREAR UN NUEVO USUARIO LECTOR CON CUENTA EN BIBLIOTECA
EXEC sp_CrearNuevoUsuario 
    @Usuario = 'nuevo_lector',
    @Contrasena = 'lector2024',
    @Rol = 'Usuario Lector',
    @NombreCompleto = 'Nuevo Lector Ejemplo',
    @Email = 'nuevo.lector@email.com',
    @CrearUsuarioBiblioteca = 1,
    @DNI = '99887766',
    @Telefono = '987654321',
    @Direccion = 'Av. Ejemplo 123, Lima'
GO

PRINT ''
PRINT '================================================='
PRINT '  BASE DE DATOS BibliotecaDB CREADA CON ÉXITO'
PRINT '================================================='
PRINT ''
PRINT '📚 USUARIOS ADMINISTRADORES DISPONIBLES:'
PRINT '--------------------------------------------'
PRINT '  ✓ Usuario: admin         | Contraseña: 12345678'
PRINT '  ✓ Usuario: llerena       | Contraseña: 12345678'
PRINT '  ✓ Usuario: director      | Contraseña: director123'
PRINT '  ✓ Usuario: nuevo_admin   | Contraseña: admin2024'
PRINT ''
PRINT '📖 USUARIOS BIBLIOTECARIOS DISPONIBLES:'
PRINT '--------------------------------------------'
PRINT '  ✓ Usuario: bibliotecario | Contraseña: 12345678'
PRINT '  ✓ Usuario: biblioteca1   | Contraseña: password'
PRINT '  ✓ Usuario: biblio        | Contraseña: 123456'
PRINT '  ✓ Usuario: asistente     | Contraseña: asistente123'
PRINT ''
PRINT '👤 USUARIOS LECTORES DISPONIBLES:'
PRINT '--------------------------------------------'
PRINT '  ✓ Usuario: usuario       | Contraseña: 12345678'
PRINT '  ✓ Usuario: lector        | Contraseña: password'
PRINT '  ✓ Usuario: estudiante    | Contraseña: 123456'
PRINT '  ✓ Usuario: miguel        | Contraseña: miguel123'
PRINT '  ✓ Usuario: sofia         | Contraseña: sofia456'
PRINT '  ✓ Usuario: carlos        | Contraseña: carlos789'
PRINT '  ✓ Usuario: patricia      | Contraseña: patri321'
PRINT '  ✓ Usuario: nuevo_lector  | Contraseña: lector2024'
PRINT ''
PRINT '🔧 PROCEDIMIENTO PARA CREAR NUEVOS USUARIOS:'
PRINT '--------------------------------------------'
PRINT 'EXEC sp_CrearNuevoUsuario'
PRINT '  @Usuario = ''tu_nombre'','
PRINT '  @Contrasena = ''tu_password'','
PRINT '  @Rol = ''Administrador'', -- o Bibliotecario o Usuario Lector'
PRINT '  @NombreCompleto = ''Tu Nombre Completo'','
PRINT '  @Email = ''tu_email@biblioteca.com'''
PRINT ''
PRINT '✅ TODAS LAS FUNCIONALIDADES LISTAS:'
PRINT '--------------------------------------------'
PRINT '  ✓ Sistema completo de préstamos'
PRINT '  ✓ Gestión de multas automáticas'
PRINT '  ✓ Sistema de reservas'
PRINT '  ✓ Solicitudes con aprobación'
PRINT '  ✓ Vistas sin errores de ORDER BY'
PRINT '  ✓ Procedimiento para crear usuarios personalizados'
PRINT '  ✓ Login funcional'
PRINT '  ✓ Cambio de contraseña'
PRINT '  ✓ Estadísticas del sistema'
PRINT '  ✓ 20 libros registrados'
PRINT '  ✓ Triggers de validación'
PRINT ''
PRINT '📊 PARA VER TODOS LOS USUARIOS CREADOS:'
PRINT '--------------------------------------------'
PRINT 'EXEC sp_ListarUsuariosSistema'
PRINT ''
PRINT '================================================='
PRINT '        ¡BASE DE DATOS LISTA PARA USAR!'
PRINT '================================================='
GO

-- Ver todos los usuarios creados
EXEC sp_ListarUsuariosSistema
GO