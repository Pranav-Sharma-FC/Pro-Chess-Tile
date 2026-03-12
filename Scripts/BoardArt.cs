using Godot;
using System;

public partial class BoardArt : TileMapLayer
{
	[Signal]
	public delegate void OnBoardArrivedEventHandler(Vector2I Position);
	public void Pressed()
	{
			Vector2 mouseWorldPos = GetGlobalMousePosition();
			Vector2I cell = LocalToMap(ToLocal(mouseWorldPos));
			EmitSignalOnBoardArrived(cell);
			GD.Print("BoardArrived");
			//}
	}
}
