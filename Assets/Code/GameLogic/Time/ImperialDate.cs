using System;
using UnityEngine;

[Serializable]
public struct ImperialDate : IComparable<ImperialDate>, IEquatable<ImperialDate>
{
    // Date info
    [SerializeField] private int day;
    [SerializeField] private int month;
    [SerializeField] private int year;
    [SerializeField] private int millennium;

    public int Day => day;
    public int Month => month;
    public int Year => year;
    public int Millennium => millennium;

    // Lookup tables for month info
    private static readonly int[] DaysInMonths = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    private static readonly string[] AbrMonthNames = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
    private static readonly string[] MonthNames = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

    public ImperialDate(int day, int month, int year, int millennium)
    {
        this.millennium = Math.Max(1, millennium); // Ensures that 0 or less aren't included
        this.year = Math.Clamp(year, 0, 999); // Keeps any years within format
        this.month = Math.Clamp(month, 1, 12); // Must be valid month
        
        // Ensures that days are valid in each month
        int maxDays = DaysInMonths[this.month - 1];
        this.day = Math.Clamp(day, 1, maxDays);
    }

    // TIME PROGRESSION
    public ImperialDate AddDay(out bool monthChanged, out bool yearChanged)
    {
        return AddDays(1, out monthChanged, out yearChanged);
    }

    public ImperialDate AddDays(int daysToAdd, out bool monthChanged, out bool yearChanged)
    {
        monthChanged = false;
        yearChanged = false;

        int newDay = day + daysToAdd;
        int newMonth = month;
        int newYear = year;
        int newMillennium = millennium;
        while (newDay > DaysInMonths[newMonth - 1])
        {
            newDay -= DaysInMonths[newMonth - 1];
            newMonth++;
            monthChanged = true;

            if (newMonth > 12)
            {
                newMonth = 1;
                newYear++;
                yearChanged = true;

                if (newYear > 999)
                {
                    newYear = 0;
                    newMillennium++;
                }
            }
        }
        return new ImperialDate(newDay, newMonth, newYear, newMillennium);
    }

    // Overload for when month and year change isn't needed to know
    public ImperialDate AddDays(int daysToAdd)
    {
        return AddDays(daysToAdd, out _, out _);
    }

    // FORMATTING
    public override string ToString()
    {
        string monthStr = AbrMonthNames[month - 1];
        return $"{day:D2} {monthStr}, {year:D3}.M{millennium}";
    }

    public  string ToLongString()
    {
        string suffix = (day % 10 == 1 && day != 11) ? "st" :
                        (day % 10 == 2 && day != 12) ? "nd" :
                        (day % 10 == 3 && day != 13) ? "rd" : 
                        "th";

        string fullMonth = MonthNames[month - 1];
        return $"{day}{suffix} of {fullMonth}, {year:D3}.M{millennium}";
    }

    // COMPARISON
    public int CompareTo(ImperialDate other)
    {
        if (millennium != other.millennium) return millennium.CompareTo(other.millennium);
        if (year != other.year) return year.CompareTo(other.year);
        if (month != other.month) return month.CompareTo(other.month);
        return day.CompareTo(other.day);
    }

    public bool Equals(ImperialDate other) => (day == other.day) && (month == other.month) && (year == other.year) && (millennium == other.millennium);

    public override bool Equals(object obj) => obj is ImperialDate other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(day, month, year, millennium);

    // OPERATORS
    public static bool operator ==(ImperialDate a, ImperialDate b) => a.Equals(b);
    public static bool operator !=(ImperialDate a, ImperialDate b) => !a.Equals(b);
    public static bool operator <(ImperialDate a, ImperialDate b) => a.CompareTo(b) < 0;
    public static bool operator >(ImperialDate a, ImperialDate b) => a.CompareTo(b) > 0;
    public static bool operator <=(ImperialDate a, ImperialDate b) => a.CompareTo(b) <= 0;
    public static bool operator >=(ImperialDate a, ImperialDate b) => a.CompareTo(b) >= 0;
}
