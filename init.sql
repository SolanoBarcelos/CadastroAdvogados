CREATE DATABASE IF NOT EXISTS db_advogados;
USE db_advogados;

CREATE TABLE IF NOT EXISTS Advogados (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(200) NOT NULL,
    Senioridade INT NOT NULL, 
    Logradouro VARCHAR(255) NOT NULL,
    Bairro VARCHAR(100) NOT NULL,
    Estado INT NOT NULL,      
    Cep VARCHAR(9) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Complemento VARCHAR(100)  
);

INSERT INTO Advogados (Nome, Senioridade, Logradouro, Bairro, Estado, Cep, Numero, Complemento) 
VALUES 
('Luiz Santos', 1, 'Rua A', 'Bairro A', 25, '00001-001', '1', 'apt 01'),
('Maria Silva', 2, 'Rua B', 'Bairro B', 19, '00002-002', '2', NULL),
('Bruno Oliveira', 3, 'Rua C', 'Bairro C', 11, '00003-003', '3', 'Bloco C, apt 03');