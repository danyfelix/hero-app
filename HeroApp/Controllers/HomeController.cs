using HeroApp.Helpers;
using HeroApp.Logger;
using HeroApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HeroApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        #region Props and Members
        private DataBaseFirstEntities _context;
        public DataBaseFirstEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new DataBaseFirstEntities();
                return _context;
            }
        }

        private NamesHelper _nameHelper;
        internal NamesHelper NameHelper
        {
            get
            {
                if (_nameHelper is null)
                    _nameHelper = new NamesHelper();
                return _nameHelper;
            }
        }
        #endregion

        #region Controller Actions
        /// <summary>
        /// The Index action is the first action to run
        /// This action generates the data if does not exist.
        /// </summary>
        /// <returns>The Index view</returns>
        public ActionResult Index()
        {
            if (!Context.Hero.Any())
            {
                GenerateData();
            }
            return View();
        }

        /// <summary>
        /// The ListHeroRatings view loads the DB context data
        /// and creates and maps the entities to their respective view models 
        /// and gets the ratings to be displayed in the desired order.
        /// </summary>
        /// <returns>The view to show the list of heroes with ratings.</returns>
        public ActionResult ListHeroRatings()
        {
            var modelList = MapHeroes();
            // Order the list descendingly by the hero's rate average
            var orderedModelList = OrderByRateAverage(modelList);
            // Return view
            return View(orderedModelList);
        }

        #endregion

        #region Controller Action Helpers
        /// <summary>
        /// Calls the generate heroes and rating historial methods
        /// </summary>
        private void GenerateData()
        {
            GenerateHeroes();
            GenerateRatingHistorials();
        }

        /// <summary>
        /// Creates and saves the Hero objects.
        /// </summary>
        public void GenerateHeroes()
        {
            if (Context.Hero.Any() && Context.Hero.Count() == 10)
                return;
            
            var heroList = new List<Hero> {
            new Hero()
            {
                HeroId = 1,
                Name = "Snake",
                Age = 35,
                City = "New York",
                Picture = @"~/Resources/Snake.JPG",
                Rating = 1
            },
            new Hero()
            {
                HeroId = 2,
                Name = "Maximus",
                Age = 30,
                City = "Rome",
                Picture =  @"~/Resources/Maximus.JPG",
                Rating = 4
            },
            new Hero()
            {
                HeroId = 3,
                Name = "Hercules",
                Age = 31,
                City = "Athens",
                Picture =  @"~/Resources/Hercules.JPG",
                Rating = 5
            },
            new Hero()
            {
                HeroId = 4,
                Name = "Hades",
                Age = 29,
                City = "Pharos",
                Picture = @"~/Resources/Hades.JPG",
                Rating = 3
            },
            new Hero()
            {
                HeroId = 5,
                Name = "Zeus",
                Age = 33,
                City = "Athens",
                Picture = @"~/Resources/Zeus.JPG",
                Rating = 5
            },
            new Hero()
            {
                HeroId = 6,
                Name = "Marcus Aurelius",
                Age = 35,
                City = "Sicily",
                Picture = @"~/Resources/MarcusAurelius.JPG",
                Rating = 2
            },
            new Hero()
            {
                HeroId = 7,
                Name = "Cyclops",
                Age = 15,
                City = "Sparta",
                Picture = @"~/Resources/Cyclops.JPG",
                Rating = 4
            },
            new Hero()
            {
                HeroId = 8,
                Name = "Link",
                Age = 900,
                City = "Toledo",
                Picture = @"~/Resources/Link.JPG",
                Rating = 5
            },
            new Hero()
            {
                HeroId = 9,
                Name = "Wolverine",
                Age = 241,
                City = "Krasnoyarsk",
                Picture = @"~/Resources/Wolverine.JPG",
                Rating = 4
            },
            new Hero()
            {
                HeroId = 10,
                Name = "Colossus",
                Age = 50,
                City = "Rodes",
                Picture = @"~/Resources/Colossus.JPG",
                Rating = 3
            }};
            heroList.ForEach(h => Context.Hero.Add(h));
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Creates and saves the Rating Historial objects
        /// </summary>
        public void GenerateRatingHistorials()
        {
            if (Context.RatingHistorial.Any())
                return;

            var random = new Random();
            int ratingHistorialId = 0;
            foreach (var hero in Context.Hero)
            {
                var numberOfRates = random.Next(1, 6);
                for (int i=0; i<numberOfRates; i++) // From 1 to 5 Ratings
                {
                    var rating = random.Next(0, 5); // Random rating between 0 and 5
                    var historial = new RatingHistorial
                    {
                        RatingHistorialId = ++ratingHistorialId,
                        HeroId = hero.HeroId,
                        Rating = rating,
                        RaterName = NameHelper.RaterNames[random.Next(i, 13)]
                    };
                    Context.RatingHistorial.Add(historial);
                }
            }
            Context.SaveChanges();
        }

        /// <summary>
        /// Maps each hero entity to their respective view model
        /// </summary>
        /// <returns>A list of Hero view models</returns>
        public List<HeroViewModel> MapHeroes()
        {
            var modelList = new List<HeroViewModel>();
            foreach (var hero in Context.Hero)
            {
                var model = new HeroViewModel();
                model.HeroId = hero.HeroId;
                model.HeroName = hero.Name;
                model.Age = hero.Age;
                model.City = hero.City;
                model.Image = hero.Picture;
                model.Rating = hero.Rating;
                // Get rating historial for by hero Id
                var ratingsForHero = GetRatingHistorialsByHero(hero.HeroId);
                // Fill up the rating history
                model.RatingHistory.AddRange(GetRatingViewModels(ratingsForHero));
                // Get rating average of hero
                model.RateAverage = RatesHelper.GetRatingAverage(ratingsForHero, hero.Rating);
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// Gets the rating historial for each hero from the DB Context.
        /// </summary>
        /// <param name="heroId">The id of the hero to get the historial</param>
        /// <returns>The list of the ratings for the hero</returns>
        public IQueryable<RatingHistorial> GetRatingHistorialsByHero(int heroId)
        {
            return Context.RatingHistorial.Where(r => r.HeroId == heroId);
        }

        /// <summary>
        /// Maps the hero's rating historials to their respective view models
        /// </summary>
        /// <param name="ratingHistorials">The list of rating historial objects</param>
        /// <returns>The List of mapped rating view models</returns>
        public List<RatingViewModel> GetRatingViewModels(IQueryable<RatingHistorial> ratingHistorials)
        {
            var ratingViewModels = new List<RatingViewModel>();
            foreach (var rating in ratingHistorials)
            {
                ratingViewModels.Add(new RatingViewModel
                {
                    RatingHistorialId = rating.RatingHistorialId,
                    Rating = rating.Rating,
                    RaterName = rating.RaterName
                });
            }
            return ratingViewModels;
        }

        /// <summary>
        /// Gets the list of Hero view models ordered descendingly by their rated average.
        /// </summary>
        /// <param name="modelList">The hero view model to order.</param>
        /// <returns>The ordered hero view model list.</returns>
        public List<HeroViewModel> OrderByRateAverage(List<HeroViewModel> modelList)
        {
            return modelList.OrderByDescending(x => x.RateAverage).ToList();
        }
        #endregion

        #region Log Exception
        /// <summary>
        /// Added the OnException method to log and debug errors in production.
        /// I was having trouble detecting a particular issue, and this helped me debug it.
        /// </summary>
        /// <param name="filterContext">Where the error information comes from.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            //Handle exception
            filterContext.ExceptionHandled = true;

            var logger = new Log(Context);
            logger.LogException(filterContext);

            // Redirect to desired view.
            filterContext.Result = RedirectToAction("Index", "Home");
        }
        #endregion
    }
}