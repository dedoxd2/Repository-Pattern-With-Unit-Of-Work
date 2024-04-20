using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOfW.Core.Consts;
using RepositoryPatternWithUOfW.Core.Interfaces;
using RepositoryPatternWithUOfW.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBaseRepository<Book> _booksRepository;
        private readonly IBaseRepository<Author> _authorsRepository;



        public BooksController(IBaseRepository<Book> booksRepository , IBaseRepository<Author> authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;

        }

        [HttpGet]
        public IActionResult Get(int id) 
        {
        return Ok(_booksRepository.GetById(id));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            return Ok(_booksRepository.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name) 
        {
            return Ok(_booksRepository.Find(x => x.Title == name , new[] {"Author"}));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors(string name) 
        {
            return  Ok(_booksRepository.FindAll(x => x.Title.Contains (name ) , new[] {"Authors"}));
        }


        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered(string name)
        {
            return Ok(_booksRepository.FindAll(b => b.Title.Contains(name) ,null,null, b => b.Id ,OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult Post([FromBody]Book book) // We can Solve Author Issue by Using DTO
        {

            Author author = _authorsRepository.GetById(book.AuthorId);
            book.Author = author;  

            return Ok(_booksRepository.Add(book));
        }


    }
}
