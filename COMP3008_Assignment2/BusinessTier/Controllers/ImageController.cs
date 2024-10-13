using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        /*
         *  Controller for uploading images;
         *  Haven't done the logic for attaching to profile yet;
         *  
         *  USAGE(Postman):
         *      1.) Set method to post
         *      2.) Set URL to localhost:5186/api/image
         *      3.) Choose 'Body' and 'form-data'
         *      4.) In 'Key' column, type 'fileName'
         *      5.) In dropdown, choose 'File'
         *      6.) File can then be sent (will be uploaded to directory
         *      in BusinessWebAPI called 'ProfilePictures')
         */
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile fileName, [FromForm] string newFileName)
        {
            Console.WriteLine("ENTRY");
            if (fileName == null || fileName.Length == 0)
            {
                return BadRequest("Error: File does not exist!");
            }

            try
            {
                var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ProfilePictures"); // where the image will be uploaded

                if (!Directory.Exists(fileDirectory)) // if folder doesn't exist
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                var fileExtension = Path.GetExtension(fileName.FileName);
                var tempName = $"{newFileName}{fileExtension}";
                var filePath = Path.Combine(fileDirectory, tempName);

                using (var stream = new FileStream(filePath, FileMode.Create)) // creates new file or overwrites existing one
                {
                    await fileName.CopyToAsync(stream); // copies file to file stream
                }

                return Ok("Image successfully uploaded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
