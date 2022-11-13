using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene MobScene;

    public int Score;

    public override void _Ready()
    {
        GD.Randomize();
    }

    public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
        GetNode<AudioStreamPlayer>("Music").Stop();
        
        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<AudioStreamPlayer>("DeathSound").Play();
	}

	public void NewGame()
	{
		Score = 0;
        GetTree().CallGroup("mobs", "queue_free");

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Position2D>("StartPosition");
        player.Start(startPosition.Position);

        var hud = GetNode<HUD>("HUD");
        hud.UpdateSore(Score);
        hud.ShowMessage("Get Ready!");

        GetNode<Timer>("StartTimer").Start();
        GetNode<AudioStreamPlayer>("Music").Play();
	}

    public void OnScoreTimerTimeout()
    {
        Score++;
        GetNode<HUD>("HUD").UpdateSore(Score);
    }

    public void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    public void OnMobTimerTimeout()
    {
        // Create a new instance of the Mob scene
        var mob = (Mob)MobScene.Instance();

        // Choose a random location on Path2D
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.Offset = GD.Randi();

        // Set the mob's direction perpendicular to the path direction
        float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        // Set the mob's position to a random location
        mob.Position = mobSpawnLocation.Position;

        // Add some randomness to the direction
        direction += (float)GD.RandRange(-Mathf.Pi /4, Mathf.Pi /4);
        mob.Rotation = direction;

        // Choose the velocity.
        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);
        
        AddChild(mob);
    }
}
