﻿using E_Library.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.DTOS.Response
{
    public class BookResponseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublisherId { get; set; }

        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public DateOnly YearPublished { get; set; }
        public string CategoryId { get; set; }

        public BookLanguage Language { get; set; }
        public BookFormat Format { get; set; }
        public BookAvailability Availability { get; set; }
    }
}
