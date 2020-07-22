using HtmlAgilityPack;
using KellySubaruParser.Extensions;
using KellySubaruParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KellySubaruParser
{
    class Program
    {
        static void Main( string[] args )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Ssl3;

            var url = @"https://www.kellysubaru.com/used-inventory/index.htm";

            try
            {
                var web = new HtmlWeb();
                var doc = web.Load( url ); // Load

                var body_node = doc.GetBody(); // Get body

                // Get page cars nodes
                var page_cars = NodesUtils.GetSubNodesByClassName( body_node.ChildNodes,
                                                         "item notshared green-vehicle inv-type-used" );

                IEnumerable<CarInfo> cars_data = NodesToCarInos( page_cars );

                var car_data = cars_data.Skip( 1 ).Take( 1 ).FirstOrDefault();
                if( null != car_data)
                {
                    car_data.WriteToConsole();
                }

            } catch( Exception ex )
            {
                Console.WriteLine( String.Format( "Error: {0}", ex.Message ) );
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private static IEnumerable<CarInfo> NodesToCarInos( IEnumerable<HtmlNode> nodes )
        {
            List<CarInfo> result = new List<CarInfo>(); 

            foreach( var car_item in nodes )
            {
                String vin = String.Empty;
                String price = String.Empty;
                String url = String.Empty;

                var found_nodes = NodesUtils.GetSubNodesByClassName( car_item.ChildNodes,
                                                                     "hproduct auto subaru" );
                if( null != found_nodes )
                {
                    var node = found_nodes.FirstOrDefault();
                    if( null != node )
                    {
                        var attr = node.Attributes["data-vin"];
                        if( null != attr )
                        {
                            vin = attr.Value;
                        }
                    }
                }

                found_nodes = NodesUtils.GetSubNodesByClassName( car_item.ChildNodes, 
                                                                 "internetPrice final-price" );
                
                if( null != found_nodes )
                {
                    var node = found_nodes.First();
                    if( null != node )
                    {
                        found_nodes = NodesUtils.GetSubNodesByClassName( node.ChildNodes, "value" );
                        node = found_nodes.FirstOrDefault();
                        if( null != node )
                        {
                            price = node.InnerText;
                        }
                    }
                }
                
                found_nodes = NodesUtils.GetSubNodesByClassName( car_item.ChildNodes, "photo thumb" );
                if( null != found_nodes )
                {
                    var node = found_nodes.FirstOrDefault();
                    if( null != node )
                    {
                        var attr = node.Attributes["src"];
                        if( null != attr )
                        {
                            url = attr.Value;
                        }
                    }

                }

                result.Add( new CarInfo( vin, price, url ) );
            }

            return result;
        }
    }
}
