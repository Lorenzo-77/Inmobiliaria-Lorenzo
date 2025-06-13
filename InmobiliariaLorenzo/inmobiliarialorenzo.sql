-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-06-2025 a las 17:41:56
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliarialorenzo`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id_Contrato` int(11) NOT NULL,
  `Fecha_Inicio` date NOT NULL,
  `Fecha_Fin` date NOT NULL,
  `Monto` decimal(10,0) NOT NULL,
  `Id_Inmueble` int(11) NOT NULL,
  `Id_Inquilino` int(11) NOT NULL,
  `Id_Usuario_Creador` int(11) NOT NULL DEFAULT 17,
  `Id_Usuario_Finalizador` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id_Contrato`, `Fecha_Inicio`, `Fecha_Fin`, `Monto`, `Id_Inmueble`, `Id_Inquilino`, `Id_Usuario_Creador`, `Id_Usuario_Finalizador`) VALUES
(1, '2024-01-01', '2024-12-31', '70000', 1, 1, 17, NULL),
(2, '2024-03-15', '2025-03-14', '120000', 2, 2, 17, NULL),
(3, '2024-05-01', '2025-04-30', '85000', 3, 3, 17, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id_Inmueble` int(11) NOT NULL,
  `Direccion` varchar(150) NOT NULL,
  `Id_Uso` int(11) NOT NULL,
  `Id_Tipo` int(11) NOT NULL,
  `Ambientes` int(11) NOT NULL,
  `Latitud` decimal(10,0) NOT NULL,
  `Longitud` decimal(10,0) NOT NULL,
  `Precio` decimal(10,0) NOT NULL,
  `Activo` tinyint(1) NOT NULL,
  `Id_Propietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id_Inmueble`, `Direccion`, `Id_Uso`, `Id_Tipo`, `Ambientes`, `Latitud`, `Longitud`, `Precio`, `Activo`, `Id_Propietario`) VALUES
(1, 'Belgrano 123', 1, 1, 3, '-33000', '-66000', '70000', 1, 1),
(2, 'San Martín 456', 2, 3, 1, '-33100', '-66100', '120000', 1, 2),
(3, 'Av. Illia 789', 1, 2, 4, '-33200', '-66200', '85000', 1, 3),
(4, 'Junín 1010', 1, 1, 2, '-33050', '-66050', '65000', 0, 1),
(12, 'Lavalle', 2, 3, 231, '123', '123213', '600', 1, 14);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id_Inquilino` int(11) NOT NULL,
  `Apellido` varchar(150) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Dni` varchar(10) NOT NULL,
  `Telefono` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id_Inquilino`, `Apellido`, `Nombre`, `Dni`, `Telefono`) VALUES
(1, 'Rodríguez', 'Juan', '32123456', '2664887777'),
(2, 'Martínez', 'Sofía', '33123456', '2664888888'),
(3, 'López', 'Diego', '34123456', '2664999999'),
(9, 'Lucero', 'David', '39796666', '02664952528');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `Id_Pago` int(11) NOT NULL,
  `Fecha` date NOT NULL,
  `Importe` decimal(10,0) NOT NULL,
  `Id_Contrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`Id_Pago`, `Fecha`, `Importe`, `Id_Contrato`) VALUES
(1, '2024-01-05', '70000', 1),
(2, '2024-02-05', '70000', 1),
(4, '2024-03-20', '120000', 2),
(5, '2024-05-05', '85000', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id_Propietario` int(11) NOT NULL,
  `Apellido` varchar(150) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Dni` varchar(10) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Clave` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id_Propietario`, `Apellido`, `Nombre`, `Dni`, `Telefono`, `Email`, `Clave`) VALUES
(1, 'Pérez', 'Carlos', '30123456', '2664001111', 'carlos.perez@mail.com', '1234'),
(2, 'Gómezz', 'Laura', '29123456', '2664002222', 'laura.gomez@mail.com', '1234'),
(3, 'Fernández', 'Ana', '28123456', '2664003333', 'ana.fernandez@mail.com', '1234'),
(14, 'Ricardo', 'Lucero', '39796666', '02664952528', 'ricardo@ejemplo.com', '123');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipos`
--

CREATE TABLE `tipos` (
  `Id_Tipo` int(11) NOT NULL,
  `Nombre` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipos`
--

INSERT INTO `tipos` (`Id_Tipo`, `Nombre`) VALUES
(1, 'Departamento'),
(2, 'Casa'),
(3, 'Local'),
(4, 'Galpón');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usos`
--

CREATE TABLE `usos` (
  `Id_Uso` int(11) NOT NULL,
  `Nombre` varchar(150) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usos`
--

INSERT INTO `usos` (`Id_Uso`, `Nombre`) VALUES
(1, 'Residencial'),
(2, 'Comercial');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id_Usuario` int(11) NOT NULL,
  `Apellido` varchar(150) NOT NULL,
  `Nombre` varchar(150) NOT NULL,
  `Mail` varchar(150) NOT NULL,
  `Password` varchar(150) NOT NULL,
  `Rol` int(11) NOT NULL,
  `Avatar` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id_Usuario`, `Apellido`, `Nombre`, `Mail`, `Password`, `Rol`, `Avatar`) VALUES
(17, 'Muñoz', 'Lorenzo', 'Lorenzo@gmail.com', 'GAKKw6Co5EiIGNiZC1OfQC6offL+e8CoEs3SX0LIrHA=', 1, '/Uploads\\avatar_7ac5cc72-7f59-4253-bdd2-8398400d6203.jpg');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id_Contrato`),
  ADD KEY `Id_Inmueble` (`Id_Inmueble`,`Id_Inquilino`),
  ADD KEY `Id_Inquilino` (`Id_Inquilino`),
  ADD KEY `FK_Creador` (`Id_Usuario_Creador`),
  ADD KEY `FK_Finalizador` (`Id_Usuario_Finalizador`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id_Inmueble`),
  ADD KEY `Id_Uso` (`Id_Uso`,`Id_Tipo`,`Id_Propietario`),
  ADD KEY `Id_Tipo` (`Id_Tipo`),
  ADD KEY `Id_Propietario` (`Id_Propietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id_Inquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`Id_Pago`),
  ADD KEY `Id_Contrato` (`Id_Contrato`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id_Propietario`);

--
-- Indices de la tabla `tipos`
--
ALTER TABLE `tipos`
  ADD PRIMARY KEY (`Id_Tipo`);

--
-- Indices de la tabla `usos`
--
ALTER TABLE `usos`
  ADD PRIMARY KEY (`Id_Uso`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id_Usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id_Contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=43;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id_Inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id_Inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `Id_Pago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id_Propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `tipos`
--
ALTER TABLE `tipos`
  MODIFY `Id_Tipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usos`
--
ALTER TABLE `usos`
  MODIFY `Id_Uso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id_Usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `CONTRATOS_ibfk_1` FOREIGN KEY (`Id_Inquilino`) REFERENCES `inquilinos` (`Id_Inquilino`) ON UPDATE CASCADE,
  ADD CONSTRAINT `CONTRATOS_ibfk_2` FOREIGN KEY (`Id_Inmueble`) REFERENCES `inmuebles` (`Id_Inmueble`) ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_Creador` FOREIGN KEY (`Id_Usuario_Creador`) REFERENCES `usuarios` (`Id_Usuario`),
  ADD CONSTRAINT `FK_Finalizador` FOREIGN KEY (`Id_Usuario_Finalizador`) REFERENCES `usuarios` (`Id_Usuario`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `INMUEBLES_ibfk_1` FOREIGN KEY (`Id_Uso`) REFERENCES `usos` (`Id_Uso`) ON UPDATE CASCADE,
  ADD CONSTRAINT `INMUEBLES_ibfk_2` FOREIGN KEY (`Id_Tipo`) REFERENCES `tipos` (`Id_Tipo`) ON UPDATE CASCADE,
  ADD CONSTRAINT `INMUEBLES_ibfk_3` FOREIGN KEY (`Id_Propietario`) REFERENCES `propietarios` (`Id_Propietario`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `PAGOS_ibfk_1` FOREIGN KEY (`Id_Contrato`) REFERENCES `contratos` (`Id_Contrato`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
