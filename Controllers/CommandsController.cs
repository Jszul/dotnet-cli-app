using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CmdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;
        // Constructor Dependency Injection 
        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        //GET           api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommandItems()
        {
         
            return _context.CommandItems;
        }

        //GET:          api/commands/n            gets unique command
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return commandItem;
        }
        //POST:             api/commands 
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();
            //CreatedatAction keyword
            // passing back command object to get current post data
            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }
        [HttpPut("{id}")]        //PUT       api/commands/n
        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

                _context.Entry(command).State = EntityState.Modified; //EF specific funtionality 
                _context.SaveChanges();

                return NoContent();
        }
        //DELETE        api/commands/n

        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;

        }

    }
}