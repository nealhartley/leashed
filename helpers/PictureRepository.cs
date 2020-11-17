using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace leashed.helpers
{
    public class PictureRepository : IPictureRepository
    {
        private string s3NinjaAccessKey = "AKIAIOSFODNN7EXAMPLE";
        private string s3NinjaSecretKey = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY";
        private string s3BucketName = "s3test";
        private AmazonS3Config s3Config = new AmazonS3Config();
        
        private static IAmazonS3 s3Client;
        public PictureRepository(){
            s3Config.ServiceURL= "http://192.168.99.100:9444/s3/";
            s3Client = new AmazonS3Client(
                s3NinjaAccessKey,
                s3NinjaSecretKey,
                s3Config
            );
        }

        public string getImageUrl(string key)
        {

            return "not implemented";
        }

        public bool canAccessImage(string id)
        {

            return false;
        }

        public async Task<PutBucketResponse> setupBucket()
        {   
             var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = s3BucketName,
                        UseClientRegion = true
                    };
            PutBucketResponse putBucketResponse = await s3Client.PutBucketAsync(putBucketRequest);
            return putBucketResponse;

        }

        public Task<string> uploadImageURL(string key, double duration)
        {   
            var url = GeneratePreSignedURL(key, s3BucketName, 1);
            return Task.FromResult(url);
        }

        private static string GeneratePreSignedURL(string objectKey, string bucketName, double duration)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key        = objectKey,
                Verb       = HttpVerb.PUT,
                Expires    = DateTime.UtcNow.AddHours(duration)
            };

           string url = s3Client.GetPreSignedURL(request);
           return url;
        }
    }
}