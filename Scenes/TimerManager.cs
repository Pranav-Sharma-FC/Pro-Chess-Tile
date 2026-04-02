using Godot;
using System;

public partial class TimerManager : Node
{
[Export] public Label WhiteLabel;
[Export] public Label BlackLabel;
[Export] public Label WhiteManaLabel;
[Export] public Label BlackManaLabel;


private float whiteTime = 600f;
private float blackTime = 600f;

private int whiteMana = 1;
private int blackMana = 1;
private float manaTimer = 0f;
private float manaInterval = 30f;

private bool isWhiteTurn = true;

public override void _Process(double delta)
{
float d = (float)delta;

if (isWhiteTurn)
{
	whiteTime -= d;
 }

else
{
	blackTime -= d;
}
// mana timer
manaTimer += d;
if (manaTimer >= manaInterval)
{
	manaTimer = 0f;
	whiteMana += 1;
	blackMana += 1;
}

UpdateUI();
}

private void UpdateUI()
{
WhiteLabel.Text = "White: " + FormatTime(whiteTime);
BlackLabel.Text = "Black: " + FormatTime(blackTime);
WhiteManaLabel.Text = "White Mana: " + whiteMana;
BlackManaLabel.Text = "Black Mana: " + blackMana;
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
