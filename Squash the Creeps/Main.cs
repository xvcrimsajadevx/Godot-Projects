using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene MobScene;

    public override void _Ready()
    {
        GD.Randomize();
        GetNode<Control>("UserInterface/Retry").Hide();
    }

    public void OnMobTimerTimeout()
    {
        // Creates new instance of mob scene
        Mob mob = (Mob)MobScene.Instance();

        // Choose random location on SpawnPath
        // Store reference to SpawnLocation node
        var mobSpawnLocation = GetNode<PathFollow>("SpawnPath/SpawnLocation");
        // Give it a random offset
        mobSpawnLocation.UnitOffset = GD.Randf();

        Vector3 playerPosition = GetNode<Player>("Player").Transform.origin;
        mob.Initialize(mobSpawnLocation.Translation, playerPosition);

        AddChild(mob);

        // Connect mob to score label to update score as mob is squashed
        mob.Connect(nameof(Mob.Squashed), GetNode<ScoreLabel>("UserInterface/ScoreLabel"), nameof(ScoreLabel.OnMobSquashed));
    }

    public void OnPlayerHit()
    {
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Control>("UserInterface/Retry").Show();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept") && GetNode<Control>("UserInterface/Retry").Visible)
        {
            // Restarts current scene
            GetTree().ReloadCurrentScene();
        }
    }
}
