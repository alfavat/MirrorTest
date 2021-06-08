using System.Collections.Generic;

using System.Xml.Serialization;
namespace ServerService.Entity.Dto.Iha
{
    public class IhaStandardNewsDto
    {

        [XmlRoot(ElementName = "item")]
        public class Item
        {
            [XmlElement(ElementName = "HaberKodu")]
            public string HaberKodu { get; set; }
            [XmlElement(ElementName = "UstKategori")]
            public string UstKategori { get; set; }
            [XmlElement(ElementName = "Kategori")]
            public string Kategori { get; set; }
            [XmlElement(ElementName = "Sehir")]
            public string Sehir { get; set; }
            [XmlElement(ElementName = "SonDakika")]
            public string SonDakika { get; set; }
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
            [XmlElement(ElementName = "images")]
            public Images Images { get; set; }
            [XmlElement(ElementName = "Aciklamalar")]
            public Aciklamalar Aciklamalar { get; set; }
            [XmlElement(ElementName = "videos")]
            public Videos Videos { get; set; }
        }

        [XmlRoot(ElementName = "image")]
        public class Image
        {
            [XmlAttribute(AttributeName = "HaberKodu")]
            public string HaberKodu { get; set; }
            [XmlAttribute(AttributeName = "ResimKodu")]
            public string ResimKodu { get; set; }
            [XmlAttribute(AttributeName = "filesize")]
            public string Filesize { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "images")]
        public class Images
        {
            [XmlElement(ElementName = "image")]
            public List<Image> Image { get; set; }
        }

        [XmlRoot(ElementName = "Aciklama")]
        public class Aciklama
        {
            [XmlAttribute(AttributeName = "HaberKodu")]
            public string HaberKodu { get; set; }
            [XmlAttribute(AttributeName = "ResimKodu")]
            public string ResimKodu { get; set; }
            [XmlText]
            public string Text { get; set; }
            [XmlAttribute(AttributeName = "VideoKodu")]
            public string VideoKodu { get; set; }
        }

        [XmlRoot(ElementName = "Aciklamalar")]
        public class Aciklamalar
        {
            [XmlElement(ElementName = "Aciklama")]
            public List<Aciklama> Aciklama { get; set; }
        }

        [XmlRoot(ElementName = "path_video")]
        public class Path_video
        {
            [XmlAttribute(AttributeName = "VideoKodu")]
            public string VideoKodu { get; set; }
            [XmlAttribute(AttributeName = "HaberKodu")]
            public string HaberKodu { get; set; }
            [XmlAttribute(AttributeName = "filesize")]
            public string Filesize { get; set; }
            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "path_poster")]
        public class Path_poster
        {
            [XmlAttribute(AttributeName = "VideoKodu")]
            public string VideoKodu { get; set; }
            [XmlAttribute(AttributeName = "HaberKodu")]
            public string HaberKodu { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "video")]
        public class Video
        {
            [XmlElement(ElementName = "path_video")]
            public Path_video Path_video { get; set; }
            [XmlElement(ElementName = "path_poster")]
            public Path_poster Path_poster { get; set; }
            [XmlElement(ElementName = "Aciklama")]
            public Aciklama Aciklama { get; set; }
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

