public class GameTimeStamp
{
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public enum DayOfTheWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public Season season;
	public int year;
    public int day;
    public int hour;
    public double minute;

    public GameTimeStamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void UpdateClock(double delta)
    {
        minute += delta;

        if (minute >= 60d)
        {
            minute -= 60d;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
            day++;
        }

        if(day > 30)
        {
            day = 1;
            if(season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        return (DayOfTheWeek)((YearsToDays(year) + SeasonsToDays(season) + (day - 1)) % 7);
    }

    public string GetHourMinuteString()
    {
        return  $"{hour.ToString().PadLeft(2,'0')}:{minute.ToString().PadLeft(2,'0')}";
    }

    public override string ToString()
    {
        return $"{GetDayOfTheWeek()}, {season} {day}, {GetHourMinuteString()}, Year {year}";
    }

    public static int HoursToMinutes(int hours)
    {
        return hours * 60;
    }

    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    public static int SeasonsToDays(Season season)
    {
        return (int)season * 30;
    }

    public static int YearsToDays(int years)
    {
        return years * 120;
    }

}
