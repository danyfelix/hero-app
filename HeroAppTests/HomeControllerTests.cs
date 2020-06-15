using HeroApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;
using HeroApp;
using System.Collections.Generic;
using HeroApp.Models;
using System;

namespace HeroAppTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _homeController;
        public HomeControllerTests()
        {
            _homeController = new HomeController();
        }

        [TestMethod]
        public void GenerateHeroes_CreatesAndSaves10Heroes()
        {
            // Arrange
            int heroesCount = 10;
            // Act
            _homeController.GenerateHeroes();
            // Assert
            _homeController.Context.Hero.Count().ShouldBe(heroesCount);
        }

        [TestMethod]
        public void Hero_MustHaveValid_NameAgeCityPictureRating()
        {
            // Arrange
            var heroes = _homeController.Context.Hero.ToList();
            // Act
            foreach (var hero in heroes)
            {
                // Asserts
                hero.Name.ShouldNotBeNullOrWhiteSpace();
                hero.Age.ShouldNotBeNull();
                hero.City.ShouldNotBeNullOrWhiteSpace();
                hero.Picture.ShouldNotBeNullOrWhiteSpace();
                hero.Rating.ShouldNotBeNull();
            }
        }

        [TestMethod]
        public void GenerateRatingHistorials_CreatesFrom1to5Ratings_PerHero()
        {
            // Arrange
            var heros = _homeController.Context.Hero.ToList();
            // Act
            _homeController.GenerateRatingHistorials();
            // Assert
            var historials = _homeController.Context.RatingHistorial.ToList();
            foreach (var hero in heros)
            {
                var heroRatings = historials.Where(h => h.HeroId == hero.HeroId).ToList();
                heroRatings.Count().ShouldBeGreaterThan(0);
                heroRatings.Count().ShouldBeLessThanOrEqualTo(5);
            }
        }


        [TestMethod]
        public void RatingHistorials_ShouldContainRatingsFrom0To5_AndRaterName()
        {
            List<RatingHistorial> historials = null;
            // Arrange
            if (_homeController.Context.RatingHistorial.Any())
            {
                historials = _homeController.Context.RatingHistorial.ToList();
            }
            else // Act
            {
                _homeController.GenerateRatingHistorials();
            }
            // Assert
            foreach (var ratingHistorial in historials)
            {
                ratingHistorial.Rating.Value.ShouldBeGreaterThan(-1);
                ratingHistorial.Rating.Value.ShouldBeLessThanOrEqualTo(5);
                ratingHistorial.RaterName.ShouldNotBeNullOrWhiteSpace();
            }
        }

        [TestMethod]
        public void MapHeroes_ShouldReturn_HeroViewModels_WithMappedValues()
        {
            //Arrange
            var modelList = new List<HeroViewModel>();
            var heros = _homeController.Context.Hero.ToList();
            //Act
            modelList = _homeController.MapHeroes();
            //Asserts
            foreach (var hero in heros)
            {
                var heroModel = modelList.Where(h => h.HeroId.Equals(hero.HeroId)).FirstOrDefault();
                heroModel.HeroName.ShouldBe(hero.Name);
                heroModel.Age.ShouldBe(hero.Age);
                heroModel.City.ShouldBe(hero.City);
                heroModel.Rating.ShouldBe(hero.Rating);
                heroModel.Image.ShouldBe(hero.Picture);
                var ratingAvg = GetRatesAverage(hero);
                heroModel.RateAverage.ShouldBe(ratingAvg);
            }
        }

        private decimal GetRatesAverage(Hero hero)
        {
            return Convert.ToDecimal(hero.RatingHistorial.ToList().Sum(r => r.Rating)) / Convert.ToDecimal(hero.RatingHistorial.Count());
        }

        [TestMethod]
        public void GetRatingHistorialsByHero_ReturnsRatingHistorialList()
        {
            //Arrange
            var hero = _homeController.Context.Hero.FirstOrDefault();
            //Act
            var ratingHistorials = _homeController.GetRatingHistorialsByHero(hero.HeroId);
            //Asserts
            ratingHistorials.ShouldNotBeNull();
            ratingHistorials.Count().ShouldBeInRange(1, 5);
        }

        [TestMethod]
        public void GetRatingViewModels_Returns_RatingViewModels_WithMappedValues()
        {
            //Arrange
            var modelList = new List<RatingViewModel>();
            var hero = _homeController.Context.Hero.FirstOrDefault();
            var ratingHistorials = _homeController.GetRatingHistorialsByHero(hero.HeroId);
            //Act
            modelList = _homeController.GetRatingViewModels(ratingHistorials);
            //Asserts
            foreach (var model in modelList)
            {
                var ratingHistorial = ratingHistorials.Where(rh => rh.RatingHistorialId == model.RatingHistorialId).FirstOrDefault();
                model.Rating.ShouldBe(ratingHistorial.Rating);
                model.RaterName.ShouldBe(ratingHistorial.RaterName);
            }
        }

        [TestMethod]
        public void OrderByRateAverage_ShouldOrderViewModelDescending_ByRateAverage()
        {
            //Arrange
            var modelList = _homeController.MapHeroes();
            //Act
            var orderedModelList = _homeController.OrderByRateAverage(modelList);
            //Assert
            for (int i=1; i<=modelList.Count-1; i++)
            {
                decimal previousRating = orderedModelList[i - 1].RateAverage;
                orderedModelList[i].RateAverage.ShouldBeLessThanOrEqualTo(previousRating);
            }
        }
    }
}
