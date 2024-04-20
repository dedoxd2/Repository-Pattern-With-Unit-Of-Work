using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOfW.Core;
using RepositoryPatternWithUOfW.Core.Interfaces;
using RepositoryPatternWithUOfW.Core.Models;
using RepositoryPatternWithUOfW.EF;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
       /*   private readonly IBaseRepository<Author> _authorsRepository;
        public AuthorsController(IBaseRepository<Author> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }
       */


        

        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetById(int Id)
        {
            return Ok(_unitOfWork.Authors.GetById(Id));
        }


        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            return Ok(await _unitOfWork.Authors.GetByIdAsync(Id));
        }


    }

}
 