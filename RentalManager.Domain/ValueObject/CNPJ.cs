using RentalManager.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace RentalManager.Domain.ValueObject;

public class CNPJ
{
    private readonly string _value;

    public CNPJ(string value)
    {
        if (!IsValid(value))
            throw new BusinessException($"CNPJ inválido. {nameof(value)}");
        _value = value;
    }

    public string Value => _value;

    public override string ToString() => _value;

    public override bool Equals(object obj)
    {
        if (obj is CNPJ other)
            return _value == other._value;
        return false;
    }

    public override int GetHashCode() => _value.GetHashCode();

    private bool IsValid(string cnpj)
    {
        cnpj = cnpj?.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
        if (cnpj.Length != 14 || !Regex.IsMatch(cnpj, @"^\d+$"))
        {
            return false;
        }
        var digit1 = CalculateDigit(cnpj.Substring(0, 12), 5);
        var digit2 = CalculateDigit(cnpj.Substring(0, 12) + digit1, 6);
        return cnpj.EndsWith($"{digit1}{digit2}");
    }

    private int CalculateDigit(string cnpjBase, int peso)
    {
        int soma = 0;
        for (int i = 0; i < cnpjBase.Length; i++)
        {
            soma += int.Parse(cnpjBase[i].ToString()) * peso;
            peso = peso == 2 ? 9 : peso - 1;
        }

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
