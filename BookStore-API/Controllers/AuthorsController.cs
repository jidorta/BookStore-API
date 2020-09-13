using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_API.Contracts;
using BookStore_API.Data;
using BookStore_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    /// <summary>
    /// Endpoint used to inter with the Author in the book store's database
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;


        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Authors
        /// </summary>
        /// <returns>List of Authors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var authors = await _authorRepository.FindAll();
                var response = _mapper.Map<IList<AuthorDTO>>(authors);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} -  {e.InnerException}");
            }

        }

        /// <summary>
        /// Get An Author by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An Author record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors(int id)
        {
            try
            {
                var author = await _authorRepository.FindById(id);
                if (author == null)
                {
                    return NotFound();
                }
                var response = _mapper.Map<AuthorDTO>(author);
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} -  {e.InnerException}");

            }

        }

        /// <summary>
        /// Get An Author by Id 
        /// </summary>
        /// <param name="authorDTO"></param>
        /// <returns>An Author record</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                if (authorDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);
                var isSuccess = await _authorRepository.Create(author);
                
                if (!isSuccess)
                {
                    return InternalError($"Author creation failed");
                }

                return Created("Create", new { author });


                   
                
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }



        /// <summary>
        /// Update An Author by Id 
        /// </summary>
        ///  /// <param name="id"></param>
        /// <param name="authorDTO"></param>
        /// <returns>An Author record</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id,[FromBody] AuthorUpdateDTO authorDTO)
        {
            try
            {

              

                if (id < 1 || authorDTO == null || id != authorDTO.Id)
                {
                    return BadRequest();
                }

                var isExist = await _authorRepository.isExists(id);

                if (!isExist)
                {
                    return NotFound();
                }


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var author = _mapper.Map<Author>(authorDTO);
                var isSuccess = await _authorRepository.Update(author);

                if (!isSuccess)
                {
                    return InternalError($"Update Operation Failed");
                }
                return NoContent();


            }catch(Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }



        /// <summary>
        /// Update An Author by Id 
        /// </summary>
        ///  /// <param name="id"></param>
        /// <returns>An Author record</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                var isExist = await _authorRepository.isExists(id);

                if (!isExist)
                {
                    return NotFound();
                }

                var isSuccess = await _authorRepository.Delete(author);

                if (!isSuccess)
                {
                    return InternalError($"Author Delete Failed");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }


                private ObjectResult InternalError(string message)
        {
            return StatusCode(500, "Something went wron.Please contact the Administrador");
        }
    }
}
