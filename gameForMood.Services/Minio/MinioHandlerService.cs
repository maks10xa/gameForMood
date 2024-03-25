using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Minio;

namespace gameForMood.Services.Minio
{
    public class MinioHandlerService(IMinioClient minioClient)
    {
        private readonly IMinioClient _minioClient = minioClient;
        private readonly string _bucketName = "vacationimages";

        public async Task<string> getFileByName(string filename)
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(filename)
                .WithExpiry(1200);

            var result = await _minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);
            return result;
        }

        public async Task<IEnumerable<string>> getFilesArrayByNames(string[] filenames)
        {
            string[] result = new string[filenames.Length];



            for (int i = 0; i < filenames.Length; i++)
            {
                var args = new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(filenames[i])
                .WithExpiry(1200);

                result[i] = await minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);
            }

            return result;
        }

        public async Task PostFile(IFormFile file, string guid, CancellationToken ct)
        {
            try
            {
                var beArgs = new BucketExistsArgs()
                    .WithBucket(_bucketName);

                bool found = await _minioClient.BucketExistsAsync(beArgs, ct).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(_bucketName);
                    await _minioClient.MakeBucketAsync(mbArgs, ct).ConfigureAwait(false);
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
                    await _minioClient.PutObjectAsync(putObjectArgs, ct).ConfigureAwait(false);
                }
            }
            catch (MinioException e)
            {
                Console.WriteLine($"File Upload Error: {0}", e.Message);
            }
        }

        public async Task PutFile(string fileName, IFormFile file, string guid, CancellationToken ct)
        {
            try
            {
                if (fileName != Constants.defaultImage)
                {
                    var args = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(fileName);

                    await _minioClient.RemoveObjectAsync(args, ct).ConfigureAwait(false);
                }

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
                    await _minioClient.PutObjectAsync(putObjectArgs, ct).ConfigureAwait(false);
                }
            }
            catch (MinioException e)
            {
                Console.WriteLine($"Error: {0}", e.Message);
            }
        }

        public async Task DeleteFile(string fileName, CancellationToken ct)
        {
            if (_minioClient is null) throw new ArgumentNullException(nameof(_minioClient));

            if (fileName != Constants.defaultImage)
            {
                try
                {
                    var args = new RemoveObjectArgs()
                    .WithBucket(_bucketName)
                        .WithObject(fileName);

                    Console.WriteLine("Running example for API: RemoveObjectAsync");
                    await _minioClient.RemoveObjectAsync(args, ct).ConfigureAwait(false);
                }
                catch (MinioException e)
                {
                    Console.WriteLine($"File Remove Error: {0}", e.Message);
                }
            }
        }
    }
}
