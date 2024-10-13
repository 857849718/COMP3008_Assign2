using DataTier.Database;
using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // create new profile
        [HttpPost]
        public IActionResult CreateNewProfile([FromBody] UserProfile profile)
        {
            if (ProfilesOps.Insert(profile))
            {
                return Ok("User profile successfully created");
            }
            return BadRequest("User profile creation failed");
        }

        // retrieve user profile by email
        [HttpGet]
        [Route("{email}")]
        public IActionResult GetProfileByEmail(string email)
        {
            UserProfile profile = ProfilesOps.GetProfileByEmail(email);
            if (profile != null)
            {
                return Ok(profile);
            }
            return NotFound("User profile not found");
        }

        // update profile
        [HttpPatch]
        public IActionResult UpdateProfile([FromBody] UserProfile profile)
        {
            if(ProfilesOps.Update(profile))
            {
                return Ok("User profile successfully updated");
            }
            return BadRequest("User profile update failed");
        }

        // delete profile
        [HttpDelete]
        [Route("{email}")]
        public IActionResult DeleteProfile(string email)
        {
            if(ProfilesOps.Delete(email))
            {
                return Ok("User profile successfully deleted");
            }
            return BadRequest("User profile delete failed");
        }
    }
}
