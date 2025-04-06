using Godot;

public partial class StatusBar : Control
{
    [Export] public ItemSlot playerCurrentToolItemSlot;
    [Export] public Label dateLabel;
    [Export] public Label timeLabel;

    private GameTimeStamp gameTimeStamp;

    public override void _Ready()
    {
        base._Ready();
        Player.GetInstance().CurrentToolChanged += CurrentToolChangedCallback;
        GameTimeManager.GetInstance().OnMinuteChanged += GameTimeMinuteChangedCallback;
        GameTimeManager.GetInstance().OnDayChanged += GameTimeDayChangedCallback;
        gameTimeStamp = GameTimeManager.GetInstance().gameTimeStamp;

        GameTimeMinuteChangedCallback();
        GameTimeDayChangedCallback();
    }

    private void CurrentToolChangedCallback(ToolResource tool)
    {
        playerCurrentToolItemSlot.SetItem(tool);
    }

    private void GameTimeMinuteChangedCallback()
    {
        timeLabel.Text = gameTimeStamp.GetHourMinuteString();
    }

    private void GameTimeDayChangedCallback()
    {
        dateLabel.Text = $"{gameTimeStamp.season}, Year {gameTimeStamp.year}\nDay {gameTimeStamp.day} ({gameTimeStamp.GetDayOfTheWeek()})";
    }
}
