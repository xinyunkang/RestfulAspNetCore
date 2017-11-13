using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.API.Helpers;
using AutoMapper;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController: Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorFromRepo = _libraryRepository.GetAuthors();

            //map entities to DTOs. 
            //var authorsDto = new List<AuthorDto>();
            //foreach( var author in authorFromRepo)
            //{
            //    authorsDto.Add(new AuthorDto
            //    {
            //        Id = author.Id,
            //        Name = $"{author.FirstName} {author.LastName}",
            //        Genre = author.Genre,
            //        Age = author.DateOfBirth.GetCurrentAge()

            //    });
            //}

            //using automapper
            var authorsDto = Mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo);        
            return Ok(authorsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            //throw new Exception("random error for testing purpose");
            var authorRepository = _libraryRepository.GetAuthor(id);
            if (authorRepository == null)
            {
                return NotFound();
            }
            var authorDto = Mapper.Map<AuthorDto>(authorRepository);
            return Ok(authorDto);
        }
    }
}
