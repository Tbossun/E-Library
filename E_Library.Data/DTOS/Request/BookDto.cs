﻿using E_Library.Data.Enums;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.DTOS.Request
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }

        public string ISBN { get; set; }
        public string ImageURL { get; set; }  = string.Empty;
        public DateTime YearPublished { get; set; }
        public string CategoryId { get; set; }

        

        public BookLanguage Language { get; set; }
        public BookFormat Format { get; set; }
        public BookAvailability Availability { get; set; }
    }
}
