using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JAM_BITES.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace JAM_BITES.Models
{
    public class RespuestaNoticias
    {
        public string? Status { get; set; }
        public List<ArticuloNoticia> Items { get; set; } = new List<ArticuloNoticia>();
    }

    public class ArticuloNoticia
    {
        public string? Title { get; set; }
        public string? Snippet { get; set; }
        public string? Publisher { get; set; }
        public long Timestamp { get; set; }
        public string? NewsUrl { get; set; }
        public Image? Images { get; set; }
        public bool HasSubnews { get; set; }
        public List<Subnews> Subnews { get; set; } = new List<Subnews>();
    }

    public class Image
    {
        public string? Thumbnail { get; set; }
        public string? ThumbnailProxied { get; set; }
    }

    public class Subnews
    {
        public string? Title { get; set; }
        public string? Snippet { get; set; }
        public string? Publisher { get; set; }
        public long Timestamp { get; set; }
        public string? NewsUrl { get; set; }
        public Image? Images { get; set; }
    }
}
