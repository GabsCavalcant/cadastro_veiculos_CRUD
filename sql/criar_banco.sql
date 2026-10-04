-- Script de criação do banco - Cadastro de Veículos (MySQL)

CREATE DATABASE IF NOT EXISTS CadastroVeiculos;

USE CadastroVeiculos;

CREATE TABLE IF NOT EXISTS Veiculo (
    Id        INT AUTO_INCREMENT PRIMARY KEY,  -- chave primária como dito no enunciado
    Placa     VARCHAR(10) NOT NULL UNIQUE,     -- unico
    Marca     VARCHAR(50) NOT NULL,
    Modelo    VARCHAR(50) NOT NULL,
    Cor       VARCHAR(30) NOT NULL,
    Ano       INT         NOT NULL,
    Porte     VARCHAR(20) NOT NULL,
    TipoCarga VARCHAR(30) NOT NULL,
    Chassis   VARCHAR(17) NOT NULL UNIQUE      -- unico
);
