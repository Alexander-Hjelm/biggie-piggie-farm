using Godot;

public partial class StatusBar : Control
{
    [Export] public ItemSlot playerCurrentItemItemSlot;
    [Export] public Label dateLabel;
    [Export] public Label timeLabel;

    private GameTimeStamp gameTimeStamp;

    public override void _Ready()
    {
        base._Ready();
        Player.GetInstance().CurrentItemChanged += CurrentItemChangedCallback;
        GameTimeManager.GetInstance().OnMinuteChanged += GameTimeMinuteChangedCallback;
        GameTimeManager.GetInstance().OnDayChanged += GameTimeDayChangedCallback;
        gameTimeStamp = GameTimeManager.GetInstance().gameTimeStamp;

        GameTimeMinuteChangedCallback();
        GameTimeDayChangedCallback();
    }

    private void CurrentItemChangedCallback(ItemResource item)
    {
        playerCurrentItemItemSlot.SetItem(item);
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
