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

    public GameTimeStamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
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

    public string GetHourMinuteString()
    {
        return  $"{hour.ToString().PadLeft(2,'0')}:{((int)minute).ToString().PadLeft(2,'0')}";
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
