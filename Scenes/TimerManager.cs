using Godot;
using System;

public partial class TimerManager : Node
{
[Export] public Label WhiteLabel;
[Export] public Label BlackLabel;

private float whiteTime = 600f;
private float blackTime = 600f;

private bool isWhiteTurn = true;

public override void _Process(double delta)
{
float d = (float)delta;

if (isWhiteTurn)
whiteTime -= d;
else
blackTime -= d;

UpdateUI();
}

private void UpdateUI()
{
WhiteLabel.Text = "White: " + FormatTime(whiteTime);
BlackLabel.Text = "Black: " + FormatTime(blackTime);
}

private string FormatTime(float time)
{
int t = Mathf.Max(0, (int)time);
int min = t / 60;
int sec = t % 60;
return $"{min:00}:{sec:00}";
}

public override void _Input(InputEvent @event)
{
if (@event.IsActionPressed("ui_accept"))
isWhiteTurn = !isWhiteTurn;
}

public void EndTurn()
{
	isWhiteTurn = !isWhiteTurn;
}
}
