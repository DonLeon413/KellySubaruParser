using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KellySubaruParser.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class NodesUtils
    {
        /// <summary>
        /// Get all subnodes and nodes by html class name
        /// </summary>
        /// <param name="nodes">HtmlNode enumerator</param>
        /// <param name="className">class name</param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetSubNodesByClassName( IEnumerable<HtmlNode> nodes, String className )
        {
            List<HtmlNode> result = new List<HtmlNode>();
            Boolean all_classes = String.IsNullOrWhiteSpace( className );

            foreach( var node in nodes )
            {
                var attr = node.Attributes["class"];

                if( null != attr && ( all_classes || 
                                      0 == String.Compare( attr.Value, className, true ) ) )
                {
                    result.Add( node );
                }
                IEnumerable<HtmlNode> sub_nodes = GetSubNodesByClassName( node.ChildNodes, className );
                if( sub_nodes != null )
                {
                    result.AddRange( sub_nodes );
                }
            }

            return result;
        }
    
        /// <summary>
        /// GEt 'body' node
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static HtmlNode GetBody( this HtmlDocument _this )
        {
            var all_nodes = _this.DocumentNode.Descendants( 0 );

            return ( null != all_nodes ? all_nodes.Where( n => String.Compare( "body", n.Name, true ) == 0 ).
                             FirstOrDefault() : null );
        }
    }
}
