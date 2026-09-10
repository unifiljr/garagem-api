using System;
using System.Collections.Generic;
using System.Linq;
using GaragemAPI.Controllers;
using GaragemAPI.Database;
using GaragemAPI.Models;
using GaragemAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GaragemAPI.Tests
{
    public class CarrosControllerTests
    {
        // Cria um DbContext novo e isolado (banco em memória) para cada teste,
        // evitando que um teste interfira no outro.
        private AppDbContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated(); // aplica os HasData (Status) definidos no OnModelCreating
            return context;
        }

        [Fact]
        public void Get_DeveRetornarListaVazia_QuandoNaoHaCarrosCadastrados()
        {
            // Arrange
            using var context = CriarContextoEmMemoria();
            var controller = new CarrosController(context);

            // Act
            var resultado = controller.Get() as OkObjectResult;
            var lista = resultado?.Value as List<CarroListaDto>;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);
            Assert.NotNull(lista);
            Assert.Empty(lista!);
        }

        [Fact]
        public void Get_DeveRetornarCarroComDescricaoDoStatus_QuandoExistirCarroCadastrado()
        {
            // Arrange
            using var context = CriarContextoEmMemoria();
            context.Carros.Add(new Carro
            {
                Marca = "Fiat",
                Modelo = "Uno",
                Ano = 2015,
                Preco = 25000,
                StatusId = 1 // "Disponivel", semeado via HasData
            });
            context.SaveChanges();

            var controller = new CarrosController(context);

            // Act
            var resultado = controller.Get() as OkObjectResult;
            var lista = resultado?.Value as List<CarroListaDto>;

            // Assert
            Assert.NotNull(lista);
            Assert.Single(lista!);
            Assert.Equal("Fiat", lista![0].Marca);
            Assert.Equal("Disponivel", lista[0].StatusDescricao);
        }

        [Fact]
        public void Post_DeveAdicionarCarro_QuandoDadosValidos()
        {
            // Arrange
            using var context = CriarContextoEmMemoria();
            var controller = new CarrosController(context);

            var novoCarro = new Carro
            {
                Marca = "Volkswagen",
                Modelo = "Gol",
                Ano = 2020,
                Preco = 45000,
                StatusId = 1
            };

            // Act
            var resultado = controller.Post(novoCarro) as OkObjectResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);
            Assert.Equal(1, context.Carros.Count());
            Assert.Equal("Volkswagen", context.Carros.First().Marca);
        }

        [Fact]
        public void Delete_DeveRemoverCarro_QuandoIdExistir()
        {
            // Arrange
            using var context = CriarContextoEmMemoria();
            var carro = new Carro
            {
                Marca = "Chevrolet",
                Modelo = "Onix",
                Ano = 2019,
                Preco = 38000,
                StatusId = 1
            };
            context.Carros.Add(carro);
            context.SaveChanges();

            var controller = new CarrosController(context);

            // Act
            var resultado = controller.Delete(carro.Id) as OkResult;

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(200, resultado.StatusCode);
            Assert.Empty(context.Carros);
        }

        [Fact]
        public void Delete_DeveRetornarNotFound_QuandoIdNaoExistir()
        {
            // Arrange
            using var context = CriarContextoEmMemoria();
            var controller = new CarrosController(context);

            // Act
            var resultado = controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
