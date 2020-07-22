using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellySubaruParser
{
    /// <summary>
    /// Car info
    /// </summary>
    public class CarInfo
    {
        /// <summary>
        /// Car VIN
        /// </summary>
        public String Vin
        {
            get;
            private set;
        }

        /// <summary>
        /// Car price
        /// </summary>
        public String Price
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ImageURL
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vin"></param>
        /// <param name="price"></param>
        /// <param name="imageURL"></param>
        public CarInfo( String vin, String price, String imageURL )
        {
            this.Vin = vin;
            this.Price = price;
            this.ImageURL = imageURL;
        }
    }
}
