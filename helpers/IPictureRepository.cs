using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace leashed.helpers
{
    public interface IPictureRepository
    {
        string getImageUrl(string key);


         bool canAccessImage(string id);

        Task<PutBucketResponse> setupBucket();

        Task<string> uploadImageURL(string key, double duration);


    }
}