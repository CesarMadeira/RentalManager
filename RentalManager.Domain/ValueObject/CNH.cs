using RentalManager.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace RentalManager.Domain.ValueObject;

public class CNH
{
    private readonly string _value;

    public CNH(string value)
    {
        if (!IsValid(value))
            throw new BusinessException($"CNH inválida. {nameof(value)}");
        _value = value;
    }

    public string Value => _value;

    public override string ToString() => _value;

    public override bool Equals(object obj)
    {
        if (obj is CNH other)
            return _value == other._value;
        return false;
    }

    public override int GetHashCode() => _value.GetHashCode();

    private bool IsValid(string cnh)
    {
        cnh = cnh?.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
        if (cnh.Length != 11 && cnh.Length != 12)
            return false;
        if (!Regex.IsMatch(cnh, @"^\d+$"))
            return false;
        return true;
    }
}
