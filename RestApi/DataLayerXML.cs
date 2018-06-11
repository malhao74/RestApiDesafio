using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace RestApi
{
    public class DataLayerXML
    {
        #region Declaracao de variaveis.
        private static readonly string xmlFullPath = AppContext.BaseDirectory + "App_Data\\Artigos.xml";
        #endregion

        #region Metodos publicos
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

        #region Metodos privados
        private static List<XElement> GetRoot()
        {
            XElement xElement = XElement.Load(xmlFullPath);
            return xElement.Elements("artigo").Select(x => x).ToList();
        }

        private static List<XElement> GetIdFrom(List<XElement> xElements, int id)
        {
            bool perdicate(XElement x) => (int)x.Element("id") == id;
            List<XElement> returnList = xElements.Where(perdicate).ToList();

            if (returnList.Count == 0 || returnList[0].Element("descricao") == null)
            {
                returnList = xElements.Where(perdicate).Elements("artigo").ToList();
            }
            return returnList;
        }

        private static string XelementToArtigoToJson(List<XElement> xElements)
        {
            List<Artigo> artigos = xElements.Select(
                x => new Artigo((int)x.Element("id"),
                                (string)x.Element("nome"),
                                (string)x.Element("descricao"))
            ).ToList();

            string json = JsonConvert.SerializeObject(artigos);
            return json;
        }
        #endregion
    }
}