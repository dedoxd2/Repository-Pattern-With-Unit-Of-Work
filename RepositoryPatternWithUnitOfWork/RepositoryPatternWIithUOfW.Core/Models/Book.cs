using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.Core.Models
{
    public class Book
    {

        public int Id { get; set; }
        [Required , MaxLength(250)]
        public string Title { get; set; }   

        
        public int AuthorId {  get; set; }
       // [JsonIgnore]
        public Author Author { get; set; }

    }
}
