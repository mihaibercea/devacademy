﻿using System.Text.Json.Serialization;

namespace WebApp.Controllers
{
    public class BookReviewDTO
    {
        public string ISBN10 { get; set; }
        public string ReviewScore { get; set; }
        public string ReviewText { get; set; }
    }
}
