using System.Collections.Generic;
using System.Xml.Serialization;
namespace ServerService.Entity.Dto.Dha
{
    public class DhaStandardNewsDto
    {

        [XmlRoot(ElementName = "item")]
        public class Item
        {
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "category")]
            public string Category { get; set; }
            [XmlElement(ElementName = "author")]
            public string Author { get; set; }
            [XmlElement(ElementName = "location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "guid")]
            public string Guid { get; set; }
            [XmlElement(ElementName = "copyright")]
            public string Copyright { get; set; }
            [XmlElement(ElementName = "language")]
            public string Language { get; set; }
            [XmlElement(ElementName = "pubDate")]
            public string PubDate { get; set; }
            [XmlElement(ElementName = "public")]
            public string Public { get; set; }
            [XmlElement(ElementName = "photos")]
            public List<InnerText> Photos { get; set; }
            [XmlElement(ElementName = "photoshd")]
            public List<InnerText> PhotosHd { get; set; }

            [XmlElement(ElementName = "videos")]
            public InnerText Video { get; set; }
            [XmlElement(ElementName = "videosextension")]
            public InnerText VideosExtension { get; set; }
            
            [XmlElement(ElementName = "videossize")]
            public InnerText VideosSize { get; set; }
            [XmlElement(ElementName = "videosdescription")]
            public InnerText VideosDescription { get; set; }

            [XmlElement(ElementName = "videosuniqueid")]
            public InnerText VideosUniqueId { get; set; }
        }

        public class InnerText
        {
            [XmlText]
            public string Text { get; set; }
        }
     
        [XmlRoot(ElementName = "channel")]
        public class Channel
        {
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "link")]
            public string Link { get; set; }
            [XmlElement(ElementName = "lastBuildDate")]
            public string LastBuildDate { get; set; }
            [XmlElement(ElementName = "pubDate")]
            public string PubDate { get; set; }
            [XmlElement(ElementName = "generator")]
            public string Generator { get; set; }
            [XmlElement(ElementName = "item")]
            public List<Item> Item { get; set; }
        }

        [XmlRoot(ElementName = "rss")]
        public class Rss
        {
            [XmlElement(ElementName = "channel")]
            public Channel Channel { get; set; }
            [XmlAttribute(AttributeName = "version")]
            public string Version { get; set; }
        }
    }
}

