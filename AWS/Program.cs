using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AWS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string bucketName = "saramanogs3-gms";

            Console.WriteLine("Enter file path of the file to be uplodated: ");

            IAmazonS3 client = new AmazonS3Client("", "", RegionEndpoint.APSouth1); 
            UploadFile(Console.ReadLine(), bucketName, client);
            Console.ReadLine();

        }

        private static async Task UploadFile(string filePath, string bucketName, IAmazonS3 client)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string[] fileAttributes = fileName.Split('.');

                PutObjectRequest putrequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,
                    ContentType = "image/jpeg"

                };

                putrequest.Metadata.Add("metatest", "trying file upload");
                PutObjectResponse response = await client.PutObjectAsync(putrequest);
                
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
