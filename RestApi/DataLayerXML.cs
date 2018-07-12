using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace RestApi
{
    /// <summary>
    /// Data layer used to access the XML Article file
    /// </summary>
    internal static class DataLayerXML
    {
        private static readonly string xmlFullPath = AppContext.BaseDirectory + "App_Data/Articles.xml";

        #region Public methods
        public static string Get()
        {
            List<XElement> xElements = GetRoot();
            string json = XelementToArtigoToJson(xElements);
            return json;
        }

        public static string Get(int[] ids)
        {
            List<XElement> xElements = GetRoot();

            for (int i = 0; i < ids.Length; i++)
            {
                xElements = GetIdFrom(xElements, ids[i]);
            }
            string json = XelementToArtigoToJson(xElements);
            return json;
        }
        #endregion

        #region Private methods
        private static List<XElement> GetRoot()
        {
            XElement xElement = XElement.Load(xmlFullPath);
            return xElement.Elements("article").Select(x => x).ToList();
        }

        private static List<XElement> GetIdFrom(List<XElement> xElements, int id)
        {
            bool perdicate(XElement x) => (int)x.Element("id") == id;
            List<XElement> returnList = xElements.Where(perdicate).ToList();

            if (returnList.Count == 0 || returnList[0].Element("description") == null)
            {
                returnList = xElements.Where(perdicate).Elements("article").ToList();
            }
            return returnList;
        }

        private static string XelementToArtigoToJson(List<XElement> xElements)
        {
            List<Article> artigos = xElements.Select(
                x => new Article((int)x.Element("id"),
                                (string)x.Element("name"),
                                (string)x.Element("description"))
            ).ToList();

            string json = JsonConvert.SerializeObject(artigos);
            return json;
        }
        #endregion
    }
}