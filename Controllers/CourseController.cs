using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Authorization;
namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] 
    
    public class CourseController : ControllerBase
    {
       private readonly DatabaseContext _context;
       

        public CourseController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses(){
            return await _context.Courses.ToArrayAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(long id){
            var course = await _context.Courses.FindAsync(id);
            if (course == null) {
                return NotFound();
            }
            return course;
        }
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course){
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourse",new {id = course.id}, course);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(long id){
            var course = await _context.Courses.FindAsync(id);
            if (course == null) {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return course;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> PostCourse(long id, Course course){
            if (id != course.id) {
                return BadRequest();
            }
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourse", new { id = course.id }, course);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<Course>> PatchCourse(long id, Course newCourse){
            
            if (id != newCourse.id) {
                return BadRequest();
            }
            var course = await _context.Courses.FindAsync(id);
            if(course == null){
                return NotFound();
            }
            course.name = newCourse.name != null ? newCourse.name : course.name;
            course.code = newCourse.code != null ? newCourse.code : course.code;
            course.career = newCourse.career != null ? newCourse.career : course.career;
            course.credits = newCourse.credits != null ? newCourse.credits : course.credits;
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourse", new { id = course.id }, course);
        }

    }
}