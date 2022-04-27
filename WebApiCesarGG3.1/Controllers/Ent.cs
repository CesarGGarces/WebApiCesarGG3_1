using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCesarGG3._1.Model;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using IoFile = System.IO.File;

namespace WebApiCesarGG3._1.Controllers
{
    [Route("apiCesar/[controller]")]
    [ApiController]
    public class Ent : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Ent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                var rutaExterna = _configuration.GetValue<string>("RutaExterna");
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(rutaExterna);
            respuesta.EnsureSuccessStatusCode();
            string ret = await respuesta.Content.ReadAsStringAsync();

            EntJson? ent = JsonSerializer.Deserialize<EntJson>(ret);

            var x = 0;
            var cat0 = "";
            var cat = "";
            List<Distinta> distinta = new List<Distinta>();
            foreach (var entrada in ent.entries)
            {
                cat0 = entrada.Category;
                if (cat != cat0 && cat != "")
                {
                    distinta.Add(new Distinta() { Category = cat, Count = x });
                    x = 0;
                }
                cat = entrada.Category;
                x++;
            }

            string d = JsonSerializer.Serialize(distinta);

            return d;
            }
            catch (Exception e)
            {
                IoFile.WriteAllText("LogApiCesar.txt", e.Message);
                return e.Message;
            }
        }


        [HttpGet("{val}")]
        public async Task<string> GetAsync(bool val)
        {
            try { 
            var rutaExterna = _configuration.GetValue<string>("RutaExterna");
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(rutaExterna);
            respuesta.EnsureSuccessStatusCode();
            string ret = await respuesta.Content.ReadAsStringAsync();

            EntJson? ent = JsonSerializer.Deserialize<EntJson>(ret);

            var q = from di in ent.entries
                    where di.HTTPS == val
                    select di;

            List<Entrada> resHttps = new List<Entrada>();
            foreach (var rHttps in q)
            {
                resHttps.Add(new Entrada()
                {
                    API = rHttps.API,
                    Description = rHttps.Description,
                    Auth = rHttps.Auth,
                    HTTPS = rHttps.HTTPS,
                    Cors = rHttps.Cors,
                    Link = rHttps.Link,
                    Category = rHttps.Category
                });




            }

            string d = JsonSerializer.Serialize(resHttps);

            return d;
            }
            catch (Exception e)
            {
                IoFile.WriteAllText("LogApiCesar.txt", e.Message);
                return e.Message;
            }
        }


    }
}
