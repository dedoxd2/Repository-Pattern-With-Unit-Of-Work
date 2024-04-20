using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOfW.Core.Interfaces;
using RepositoryPatternWithUOfW.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IBaseRepository<Author> _authorsRepository;

        public AuthorsController(IBaseRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }


        [HttpGet]
        public IActionResult GetById(int Id)
        {
            return Ok(_authorsRepository.GetById(Id));
        }


        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _authorsRepository.GetByIdAsync(Id));
        }


    }

}
 