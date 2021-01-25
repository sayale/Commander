using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Commander.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Commander.Controllers
{
    
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            
            return CreatedAtRoute(nameof(GetCommandById), new { commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commads/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
                return NotFound();
            
            _mapper.Map(commandUpdateDto,commandModel);

            _repository.UpdateCommand(commandModel);

            _repository.SaveChanges();

            return NoContent();
        } 

        //PATCH api/commads/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch,commandModel);

            _repository.UpdateCommand(commandModel);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/command/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repository.GetCommandById(id);
            if (commandModel == null)
                return NotFound();

            _repository.DeleteCommand(commandModel);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}