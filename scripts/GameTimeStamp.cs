using Godot;

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

    [Signal] public delegate void OnMinuteChangedEventHandler();

    public Season season;
	public int year;
    public int day;
    public int hour;
    public double minute;

    public struct TickOverDto
    {
        public bool yearChanged;
        public bool seasonChanged;
        public bool dayChanged;
        public bool hourChanged;
        public bool minuteChanged;
    }

    public GameTimeStamp(int year, Season season, int day, int hour, double minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public GameTimeStamp Clone()
    {
        return new GameTimeStamp(year, season, day, hour, minute);
    }

    public TickOverDto UpdateClock(double delta)
    {
        bool yearChanged = false;
        bool seasonChanged = false;
        bool dayChanged = false;
        bool hourChanged = false;
        bool minuteChanged = Mathf.Floor(minute) != Mathf.Floor(minute + delta);

        minute += delta;

        if (minute >= 60d)
        {
            minute -= 60d;
            hour++;
            hourChanged = true;
        }

        if(hour >= 24)
        {
            hour = 0;
            day++;
            dayChanged = true;
        }

        if(day > 30)
        {
            day = 1;
            if(season == Season.Winter)
            {
                season = Season.Spring;
                year++;
                yearChanged = true;
            }
            else
            {
                season++;
                seasonChanged = true;
            }
        }

        return new TickOverDto()
        {
            yearChanged = yearChanged,
            seasonChanged = seasonChanged,
            dayChanged = dayChanged,
            hourChanged = hourChanged,
            minuteChanged = minuteChanged
        };
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        return (DayOfTheWeek)((YearsToDays(year) + SeasonsToDays(season) + (day - 1)) % 7);
    }

    public double GetTotalMinutes()
    {
        return YearsToMinutes(year) + SeasonsToMinutes(season) + DaysToMinutes(day) + HoursToMinutes(hour) + minute;
    }

    public string GetHourMinuteString()
    {
        return $"{hour.ToString().PadLeft(2,'0')}:{((int)minute).ToString().PadLeft(2,'0')}";
    }

    public override string ToString()
    {
        return $"{GetDayOfTheWeek()}, {season} {day}, {GetHourMinuteString()}, Year {year}";
    }

    public static int YearsToDays(int years)
    {
        return years * 120;
    }

    public static double YearsToMinutes(int years)
    {
        return years * 172800;
    }

    public static int SeasonsToDays(Season season)
    {
        return (int)season * 30;
    }

    public static double SeasonsToMinutes(Season season)
    {
        return (int)season * 43200;
    }

    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    public static double DaysToMinutes(int days)
    {
        return days * 1440;
    }

    public static double HoursToMinutes(int hours)
    {
        return hours * 60;
    }

    public static double DifferenceInMinutes(GameTimeStamp gameTimeStampFrom, GameTimeStamp gameTimeStampTo)
    {
        return gameTimeStampTo.GetTotalMinutes() - gameTimeStampFrom.GetTotalMinutes(); 
    }
}