using Godot;
using System;

public partial class Chessboard : TileMapLayer
{
	public override void _PhysicsProcess(double delta)
	{
		Vector2 mapCords = MapToLocal((Vector2I)GetGlobalMousePosition());
		mapCords /= new Vector2(10000, 10000);
		mapCords += new Vector2(1, 1);
		mapCords = new Vector2(Mathf.Round(mapCords.X), Mathf.Round((mapCords.X)));
		GD.Print(mapCords);
	}
}
