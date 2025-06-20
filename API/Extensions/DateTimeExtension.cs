using System.Security.Cryptography.X509Certificates;

namespace API.Extension;

public static class DateTimeExtension
{

    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - dob.Year;
        if (dob > today.AddYears(-age)) return -age;
        return age;
  }
}