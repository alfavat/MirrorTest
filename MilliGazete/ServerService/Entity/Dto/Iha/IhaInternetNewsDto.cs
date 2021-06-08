using System.Collections.Generic;
using System.Xml.Serialization;
namespace ServerService.Entity.Dto.Iha
{
    public class IhaInternetNewsDto
    {
        [XmlRoot(ElementName = "HaberID")]
        public class HaberID
        {
            [XmlAttribute(AttributeName = "VideoBekleniyor")]
            public string VideoBekleniyor { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "path")]
        public class Path
        {
            [XmlAttribute(AttributeName = "FotografID")]
            public string FotografID { get; set; }
            [XmlAttribute(AttributeName = "HaberID")]
            public string HaberID { get; set; }
            [XmlAttribute(AttributeName = "Sira")]
            public string Sira { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "VideoID")]
            public string VideoID { get; set; }
            [XmlAttribute(AttributeName = "Seslendirilmis")]
            public string Seslendirilmis { get; set; }
        }

        [XmlRoot(ElementName = "image")]
        public class Image
        {
            [XmlElement(ElementName = "path")]
            public Path Path { get; set; }
        }

        [XmlRoot(ElementName = "images")]
        public class Images
        {
            [XmlElement(ElementName = "image")]
            public List<Image> Image { get; set; }
        }

        [XmlRoot(ElementName = "item")]
        public class Item
        {
            [XmlElement(ElementName = "HaberID")]
            public HaberID HaberID { get; set; }
            [XmlElement(ElementName = "Kategori")]
            public string Kategori { get; set; }
            [XmlElement(ElementName = "Sehir")]
            public string Sehir { get; set; }
            [XmlElement(ElementName = "Ulke")]
            public string Ulke { get; set; }
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "pubDate")]
            public string PubDate { get; set; }
            [XmlElement(ElementName = "SonHaberGuncellenmeTarihi")]
            public string SonHaberGuncellenmeTarihi { get; set; }
            [XmlElement(ElementName = "SonFotografEklenmeTarihi")]
            public string SonFotografEklenmeTarihi { get; set; }
            [XmlElement(ElementName = "SonVideoEklenmeTarihi")]
            public string SonVideoEklenmeTarihi { get; set; }
            [XmlElement(ElementName = "NewsID")]
            public string NewsID { get; set; }
            [XmlElement(ElementName = "images")]
            public Images Images { get; set; }
            [XmlElement(ElementName = "videos")]
            public Videos Videos { get; set; }
        }

        [XmlRoot(ElementName = "Path_Gif")]
        public class Path_Gif
        {
            [XmlAttribute(AttributeName = "VideoID")]
            public string VideoID { get; set; }
            [XmlAttribute(AttributeName = "HaberID")]
            public string HaberID { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "video")]
        public class Video
        {
            [XmlElement(ElementName = "path")]
            public Path Path { get; set; }
            [XmlElement(ElementName = "Path_Gif")]
            public Path_Gif Path_Gif { get; set; }
        }

        [XmlRoot(ElementName = "videos")]
        public class Videos
        {
            [XmlElement(ElementName = "video")]
            public Video Video { get; set; }
        }

        [XmlRoot(ElementName = "channel")]
        public class Channel
        {
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "language")]
            public string Language { get; set; }
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
