using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Reactive.Linq;


namespace gameForMood.Controllers.MinIO
{
    [Route("api/minio")]
    [ApiController]
    public class MinIOHandler(IMinioClient minioClient) : ControllerBase
    {
        private readonly IMinioClient _minioClient = minioClient;
        private readonly string _bucketName = "vacationimages";

        [HttpGet("getFile")]
        public async Task<IActionResult> getFileByName(string filename)
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(filename)
                .WithExpiry(1200);

            var result = await minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostFile(IFormFile file, string guid)
        {
            try
            {
                var beArgs = new BucketExistsArgs()
                    .WithBucket(_bucketName);

                bool found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(_bucketName);
                    await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }

                var contentType = file.Headers.Where(i => i.Key == "Content-Type").First().Value;


                using (var stream = file.OpenReadStream())
                {
                    var putObjectArgs = new PutObjectArgs()
                   .WithBucket(_bucketName)
                   .WithObject(guid)
                   .WithStreamData(stream)
                   .WithObjectSize(stream.Length)
                   .WithContentType(contentType);
                    var result = await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                    return Ok(result);
                }
            }
            catch (MinioException e)
            {
                Console.WriteLine($"File Upload Error: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutFile(string fileName, IFormFile file, string guid)
        {
            try
            {
                var args = new RemoveObjectArgs()
                .WithBucket(_bucketName)
                    .WithObject(fileName);

                await _minioClient.RemoveObjectAsync(args).ConfigureAwait(false);

                var beArgs = new BucketExistsArgs()
                    .WithBucket(_bucketName);

                var contentType = file.Headers.Where(i => i.Key == "Content-Type").First().Value;

                using (var stream = file.OpenReadStream())
                {
                    var putObjectArgs = new PutObjectArgs()
                   .WithBucket(_bucketName)
                   .WithObject(guid)
                   .WithStreamData(stream)
                   .WithObjectSize(stream.Length)
                   .WithContentType(contentType);
                    var result = await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            if (_minioClient is null) throw new ArgumentNullException(nameof(_minioClient));

            try
            {
                var args = new RemoveObjectArgs()
                .WithBucket(_bucketName)
                    .WithObject(fileName);

                Console.WriteLine("Running example for API: RemoveObjectAsync");
                await _minioClient.RemoveObjectAsync(args).ConfigureAwait(false);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
