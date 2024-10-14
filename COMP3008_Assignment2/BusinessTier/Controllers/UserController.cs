using DataTier.Database;
using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessTier.Controllers
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
            return NotFound($"No profiles in database matching {email}");
        }

        // retrieve LIST of user profiles by email
        [HttpGet]
        [Route("getprofiles/{email}")]
        public IActionResult GetProfilesByEmail(string email)
        {
            List<UserProfile> profiles = ProfilesOps.GetProfilesByEmail(email);
            if (profiles != null)
            {
                return Ok(profiles);
            }
            return NotFound($"No profiles in database matching {email}");
        }

        // retrieve LIST of user profiles by email
        [HttpGet]
        [Route("getprofilesbyid/{id}")]
        public IActionResult GetProfilesByID(int id)
        {
            List<UserProfile> profiles = ProfilesOps.GetProfilesByID(id);
            if (profiles != null)
            {
                return Ok(profiles);
            }
            return NotFound($"No profiles in database matching {id}");
        }

        // retrieve LIST of user profiles by last name
        [HttpGet]
        [Route("getprofilesbylastname/{lastName}")]
        public IActionResult GetProfilesByLastName(string lastName)
        {
            List<UserProfile> profiles = ProfilesOps.GetProfilesByLastName(lastName);
            if (profiles != null)
            {
                return Ok(profiles);
            }
            return NotFound($"No profiles in database matching {lastName}");
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
            Console.WriteLine(email);
            if(ProfilesOps.Delete(email))
            {
                return Ok("User profile successfully deleted");
            }
            return BadRequest("User profile delete failed");
        }

        // get all profiles
        [HttpGet]
        [Route("getprofiles")]
        public IActionResult GetProfiles()
        {
            List<UserProfile> profiles = ProfilesOps.GetAll();
            if (profiles != null)
            {
                return Ok(profiles);
            }
            return NotFound("No profiles in database");
        }
    }
}
