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

        public BooksController(IBaseRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
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


    }
}
