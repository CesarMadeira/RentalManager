using RentalManager.Domain.Exceptions;
using RentalManager.Domain.ValueObject;

namespace RentalManager.Domain.Entities
{
    public class DeliveryPerson
    {
        public DeliveryPerson(string id, string name, string cnpj, DateTime dateOfBirth,
            string documentNumber, string documentType, string documentImage)
        {
            Id = id;
            Name = name;
            CNPJ = new CNPJ(cnpj);
            DateOfBirth = dateOfBirth;
            DocumentNumber = new CNH(documentNumber);
            DocumentImage = documentImage;

            if (documentType != "A" &&
                documentType != "B" &&
                documentType != "A+B")
                throw new BusinessException("Categoria da CNH não permitida!");

            DocumentType = documentType;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public CNPJ CNPJ { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public CNH DocumentNumber { get; private set; }
        public string DocumentType { get; private set; }
        public string DocumentImage { get; private set; }
    }
}
