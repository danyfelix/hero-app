using System;
using System.Drawing;
using System.Linq;

namespace HeroApp.Helpers
{
    public static class RatesHelper
    {
        /// <summary>
        /// Get Rating Average Method sums all the ratings for a particular hero
        /// and then divides it by the total number.
        /// </summary>
        /// <param name="ratings">Total number of ratings of the hero</param>
        /// <param name="currentRating">Current or last rating of the hero</param>
        /// <returns>Returns the average rating of the hero.</returns>
        public static decimal GetRatingAverage(IQueryable<RatingHistorial> ratings, int? currentRating)
        {
            var sumOfRatings = ratings.Sum(x => x.Rating); // + currentRating; ...we won't use the current rating to avoid confusion.
            var ratingsCount = ratings.Count(); // + 1; ...if we later decide to use current rating well add this line as well.
            return Convert.ToDecimal(sumOfRatings) / Convert.ToDecimal(ratingsCount);
        }

        #region Obsolete Code
        /// <summary>
        /// Used this method to convert image to Byte array 
        /// for saving it to the Database
        /// </summary>
        /// <param name="img">The image to be converted.</param>
        /// <returns>A byte array representing the image.</returns>
        [Obsolete]
        internal static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        /// <summary>
        /// Used this method to convert the byte array of an image
        /// into a base 64 enconded string to be able to display it as content
        /// in the browser.
        /// </summary>
        /// <param name="byteData">The byte array image.</param>
        /// <returns>The base 64 enconded string representing the image.</returns>
        [Obsolete]
        internal static string ByteImageToString(byte[] byteData)
        {
            //Convert byte array to base64string   
            string imreBase64Data = Convert.ToBase64String(byteData);
            return string.Format("data:image/jpg;base64,{0}", imreBase64Data);
        }
        #endregion
    }
}