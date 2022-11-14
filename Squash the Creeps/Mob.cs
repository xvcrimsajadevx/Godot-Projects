using Godot;
using System;

public class Mob : KinematicBody
{
    [Export]
    public int MinSpeed = 10;
    [Export]
    public int MaxSpeed = 18;

    private Vector3 _velocity = Vector3.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(_velocity);
    }

    // Function will be called from main scene
    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        // Position mob and turn it to look at player
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        // Rotate randomly so it doesn't move exactly towards the player
        RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, -Mathf.Pi / 4.0));

        // Calculate random speed
        float randomSpeed = (float)GD.RandRange(MinSpeed, MaxSpeed);
        // Calculate forward velocity that represents speed
        _velocity = Vector3.Forward * randomSpeed;
        // Rotate vector based on mob's Y rotation to move in direction it's looking
        _velocity = _velocity.Rotated(Vector3.Up, Rotation.y);
    }

    public void OnVisibilityNotifierScreenExited()
    {
        QueueFree();
    }
}
