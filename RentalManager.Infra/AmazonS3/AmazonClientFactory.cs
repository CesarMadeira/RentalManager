using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Options;
using RentalManager.Infra.AmazonS3.Model;

namespace RentalManager.Infra.AmazonS3;

public class AmazonClientFactory
{
    IAmazonS3 _clientS3;

    public AmazonClientFactory(IOptions<AmazonSettings> amazonSettings)
    {
        _clientS3 = new AmazonS3Client(
            amazonSettings.Value.AwsAccessKeyId,
            amazonSettings.Value.AwsSecretAccessKey,
            RegionEndpoint.USEast1);
    }

    public IAmazonS3 GetS3Service()
    {
        return _clientS3;
    }
}
