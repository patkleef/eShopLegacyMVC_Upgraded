using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eShopLegacyMVC.Models
{
    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
}