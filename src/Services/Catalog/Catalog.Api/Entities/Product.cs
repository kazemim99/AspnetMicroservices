using Catalog.Api.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Entities
{
    public class Product : BaseEntity
    {

        [BsonElement("Name")]
        public string Name { get; set; }
        public string Categroy { get; set; }
        public string Summery { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }


  
    }


}
