using DW4.DAL;
using DW4.Model;
using DW4.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Task = DW4.Model.Task;

namespace DW4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("CreateTask")]
        public IActionResult CreateTask(TaskCreateViewModel model)
        {
            try
            {
                var session = _context.Sessions.FirstOrDefault(x => x.Token == model.Token);
                if(session == null)
                    return BadRequest("Invalid token");
                var user = _context.Users.FirstOrDefault(x => x.Email == session.Email);
                
                var assignedToUid = _context.Users.FirstOrDefault(x => x.Id == model.AssignedToUid);
                if (assignedToUid == null) return BadRequest("assignedToUid invalid!");

                var task = new Task()
                {
                    CreateByUid = user.Id,
                    CreatedByName = user.Name,
                    AssignedToName = assignedToUid.Name,
                    AssignedToUid = assignedToUid.Id,
                    Description = model.Description,
                };
                _context.Tasks.Add(task);
                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("createdby/{token}")]
        public IActionResult CreatedBy(string token)
        {
            try
            {
                var session = _context.Sessions.FirstOrDefault(x => x.Token == token);
                if (session == null)
                    return BadRequest("Invalid token");
                var user = _context.Users.FirstOrDefault(x => x.Email == session.Email);
                var tasks = _context.Tasks.Where(x => x.CreateByUid == user.Id).ToList();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("assignedto/{token}")]
        public IActionResult AssignedTo(string token)
        {
            try
            {
                var session = _context.Sessions.FirstOrDefault(x => x.Token == token);
                if (session == null)
                    return BadRequest("Invalid token");
                var user = _context.Users.FirstOrDefault(x => x.Email == session.Email);
                var tasks = _context.Tasks.Where(x => x.AssignedToUid == user.Id).ToList();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{taskUid}")]
        public IActionResult Update(string taskUid, TaskUpdateViewModel model)
        {

            try
            {
                var session = _context.Sessions.FirstOrDefault(x => x.Token == model.Token);
                if (session == null)
                    return BadRequest("Invalid token");

                var user = _context.Users.FirstOrDefault(x => x.Email == session.Email);
                var task = _context.Tasks.FirstOrDefault(x => x.Id == taskUid);
                if (task == null) return BadRequest("task not found!");

                if (task.AssignedToUid != user.Id)
                {
                    return BadRequest("access denied!");
                }

                task.Done = model.Done;
                _context.Tasks.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpDelete("{taskUid}")]
        public IActionResult Delete(string taskUid, TokenViewModel model)
        {
            try
            {
                var session = _context.Sessions.FirstOrDefault(x => x.Token == model.Token);
                if (session == null)
                    return BadRequest("Invalid token");


                var user = _context.Users.FirstOrDefault(x => x.Email == session.Email);
                var task = _context.Tasks.FirstOrDefault(x => x.Id == taskUid);
                if (task == null) return NotFound("task not found!");
                

                if (task.CreateByUid != user.Id)
                {
                    return BadRequest("access denied!");
                }

                _context.Remove(task);
                _context.SaveChanges();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        


    }
}