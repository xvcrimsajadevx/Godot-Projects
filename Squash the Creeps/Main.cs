using Godot;
using System;

public class Main : Node
{
    [Export]
    public PackedScene MobScene;

    public override void _Ready()
    {
        GD.Randomize();
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
    }
}
