namespace SD.pesel;

public class PersonId
{
    private readonly string _id;

    public PersonId(string Id)
    {
        _id = Id;
    }
    /// <summary>
    /// Get full year from PESEL
    /// </summary>
    /// <returns></returns>
    public int GetYear()
    {
        string id = _id;
        string year = id.Substring(startIndex: 0, length: 2);
        int yearInt = int.Parse(year);

        string month = id.Substring(startIndex: 2, length: 2);
        int monthInt = int.Parse(month);

        if (monthInt > 20) // 2000-2099
        {
            yearInt += 2000;
            monthInt -= 20;
        }
        else // 1900-1999
        {
            yearInt += 1900;
        }

            return yearInt;
    }

    /// <summary>
    /// Get month from PESEL
    /// </summary>
    public int GetMonth()
    {
        string month = _id.Substring(2, 2);
        int monthInt = int.Parse(month);

        if (monthInt > 80) monthInt -= 80;
        else if (monthInt > 20) monthInt -= 20;

        return monthInt;
    }

    /// <summary>
    /// Get day from PESEL
    /// </summary>
    /// <returns></returns>
    public int GetDay()
    {
        string id = _id;
        string result = id.Substring(startIndex:4,length:2);
        int resultInt = int.Parse(result);

        return resultInt;
    }

    /// <summary>
    /// Get year of birth from PESEL
    /// </summary>
    /// <returns></returns>
    public int GetAge()
    {
        int birthYear = GetYear();
        int birthMonth = GetMonth();
        int birthDay = GetDay();

        DateTime birthDate = new DateTime(birthYear, birthMonth, birthDay);
        DateTime today = DateTime.Today;

        int age = today.Year - birthDate.Year;

        if (today < birthDate.AddYears(age))
        {
            age--; // Not had birthday yet this year
        }

        return age;
    }

    /// <summary>
    /// Get gender from PESEL
    /// </summary>
    /// <returns>m</returns>
    /// <returns>k</returns>
    public string GetGender()
    {
        int genderDigit = int.Parse(_id.Substring(9, 1));
        return genderDigit % 2 == 0 ? "k" : "m";
    }

    /// <summary>
    /// check if PESEL is valid
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        if (_id.Length != 11)
        {
            return false;
        }

        int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        int sum = 0;

        // Obliczanie sumy wagowej dla pierwszych 10 cyfr
        for (int i = 0; i < 10; i++)
        {
            sum += weights[i] * int.Parse(_id[i].ToString());
        }

        // Obliczanie cyfry kontrolnej
        int controlDigit = (10 - (sum % 10)) % 10;

        // Porównanie obliczonej cyfry kontrolnej z ostatnią cyfrą PESEL
        return controlDigit == int.Parse(_id[10].ToString());
    }
}