﻿
namespace WebApiCesarGG3._1.Model
{
    public class EntJson
    {
        public int count { get; set; }
        public Entrada[] entries { get; set; }


    }
    public class Entrada
    {
        // public int count { get; set; }
        public string API { get; set; }
        public string Description { get; set; }
        public string Auth { get; set; }
        public bool HTTPS { get; set; }
        public string Cors { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
    public class Distinta
    {
        public string Category { get; set; }
        public int Count { get; set; }

    }
}
