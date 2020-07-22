using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellySubaruParser.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CarInfoEX
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        public static void WriteToConsole( this CarInfo _this )
        {
            Console.WriteLine( "VIT:" + _this.Vin );
            Console.WriteLine( "Price:" + _this.Price );
            Console.WriteLine( "Image URL:" + _this.ImageURL );
        }
    }
}
