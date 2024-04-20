using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RepositoryPatternWithUOfW.Core;
using RepositoryPatternWithUOfW.Core.Consts;
using RepositoryPatternWithUOfW.Core.Interfaces;
using RepositoryPatternWithUOfW.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        /*private readonly IBaseRepository<Book> _booksRepository;
        private readonly IBaseRepository<Author> _authorsRepository;
        public BooksController(IBaseRepository<Book> booksRepository , IBaseRepository<Author> authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
        }
*/

        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }


        [HttpGet]
        public IActionResult Get(int id) 
        {
        return Ok(_unitOfWork.Books.GetById(id));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll() 
        {
            return Ok(_unitOfWork.Books.GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name) 
        {
            return Ok(_unitOfWork.Books.Find(x => x.Title == name , new[] {"Author"}));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors(string name) 
        {
            return  Ok(_unitOfWork.Books.FindAll(x => x.Title.Contains (name ) , new[] {"Authors"}));
        }


        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered(string name)
        {
            return Ok(_unitOfWork.Books.FindAll(b => b.Title.Contains(name) ,null,null, b => b.Id ,OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult Post([FromBody]Book book) // We can Solve Autho r Issue by Using DTO
        {

            Author author = _unitOfWork.Authors.GetById(book.AuthorId);
            book.Author = author;
            var localBook = _unitOfWork.Books.Add(book);
            _unitOfWork.Complete();
           // _unitOfWork.Books.CustomMethodForBooks();
            return Ok(localBook);
        }


    }
}
