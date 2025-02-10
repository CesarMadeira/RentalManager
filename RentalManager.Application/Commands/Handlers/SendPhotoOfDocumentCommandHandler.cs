using Amazon.S3.Transfer;
using RentalManager.Application.Commands.Requests;
using RentalManager.Application.Commands.Responses;
using RentalManager.Application.Interfaces.Commands;
using RentalManager.Domain.Exceptions;
using RentalManager.Domain.Interfaces.Respositories;
using RentalManager.Infra.AmazonS3;

namespace RentalManager.Application.Commands.Handlers;

public class SendPhotoOfDocumentCommandHandler : ISendPhotoOfDocumentCommandHandler
{
    private readonly AmazonClientFactory _amazonClientFactory;
    private readonly IDeliveryPersonRepository _deliveryPersonRepository;

    public SendPhotoOfDocumentCommandHandler(
        AmazonClientFactory amazonClientFactory,
        IDeliveryPersonRepository deliveryPersonRepository)
    {
        _amazonClientFactory = amazonClientFactory;
        _deliveryPersonRepository = deliveryPersonRepository;
    }

    public async Task<SendPhotoOfDocumentCommandResponse> Handle(SendPhotoOfDocumentCommandRequest request)
    {
        string base64Data = request.ImagemBase64.Contains(",")
            ? request.ImagemBase64.Split(',')[1]
            : request.ImagemBase64;

        byte[] imageBytes = Convert.FromBase64String(base64Data);

        if (!IsPng(imageBytes) && !IsBmp(imageBytes))
        {
            throw new BusinessException("Apenas arquivos PNG e BMP são aceitos.");
        }

        var deliveryPerson = await _deliveryPersonRepository.Get(request.Id);
        if (deliveryPerson == null)
        {
            throw new BusinessException($"Entregador não existe!");
        }

        var bucketName = "rentalmanager-dev";
        var fileName = $"{request.Id}/documents/cnh.png";

        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        {
            using (var utility = new TransferUtility(_amazonClientFactory.GetS3Service()))
            {
                var requestS3 = new TransferUtilityUploadRequest
                {
                    InputStream = ms,
                    BucketName = bucketName,
                    Key = fileName
                };
                await utility.UploadAsync(requestS3);
            }
        }

        deliveryPerson.SetDocumentImage($"https://{bucketName}.s3.us-east-1.amazonaws.com/{fileName}");

        await _deliveryPersonRepository.Save(deliveryPerson);

        return new SendPhotoOfDocumentCommandResponse { URL = deliveryPerson.DocumentImage };
    }

    private static bool IsPng(byte[] bytes)
    {
        return bytes.Length >= 8 &&
               bytes[0] == 0x89 &&
               bytes[1] == 0x50 &&
               bytes[2] == 0x4E &&
               bytes[3] == 0x47 &&
               bytes[4] == 0x0D &&
               bytes[5] == 0x0A &&
               bytes[6] == 0x1A &&
               bytes[7] == 0x0A;
    }

    private static bool IsBmp(byte[] bytes)
    {
        return bytes.Length >= 2 &&
               bytes[0] == 0x42 &&
               bytes[1] == 0x4D;
    }
}