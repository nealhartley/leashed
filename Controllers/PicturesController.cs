using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using leashApi.Models;
using leashed.helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace leashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("TestApp")] 
    public class PicturesController : Controller
    {

        private readonly ParkContext _context;
        private readonly IPictureRepository _pictureRepository;

        public PicturesController(ParkContext context, IPictureRepository pictureRepository)
        {
            _context = context;
            _pictureRepository = pictureRepository;
        }

        [HttpGet("upload/{name}")]
        public async Task<ActionResult<Picture>> GetUploadURL(string name)
        {
           // await _pictureRepository.setupBucket();
            string GUID = "{Guid.NewGuid()}";
            var signedURL = await _pictureRepository.uploadImageURL(GUID, 12);
            var picture = new Picture();
            picture.GivenName = name;
            picture.FileName = GUID;
            picture.URL = signedURL;
            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();
            Console.WriteLine("---------------------Picture Created-----------------------");
            Console.WriteLine(signedURL);

            return picture;
        }

        [HttpGet("image/{name}")]
        public async Task<ActionResult<Picture>> GetImageURL(string name)
        {
            var picture = await _context.Pictures.Where(x => x.GivenName == name).FirstOrDefaultAsync();
            Console.WriteLine("---------------------get picture-----------------------");
            Console.WriteLine(picture.URL);
            return picture;
        }

        [HttpGet("makebucket")]
        public async Task<ActionResult<PutBucketResponse>> MakeBucket()
        {
           
           
            return await _pictureRepository.setupBucket();
        }


     
    }
}