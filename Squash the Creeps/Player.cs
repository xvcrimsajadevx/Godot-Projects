using Godot;
using System;

public class Player : KinematicBody
{
    // How fast the player moves - meters/second
    [Export]
    public int Speed = 14;
    // Downward acceleration when in the air - meters/second^2
    [Export]
    public int FallAcceleration = 75;
    // Vertical impulse applied to character upon jumping - meters/second
    [Export]
    public int JumpImpulse = 20;
    // Vertical impulse applied to character upon bouncing over a mob - meters/second
    [Export]
    public int BounceImpulse = 16;

    private Vector3 _velocity = Vector3.Zero;

    public override void _PhysicsProcess(float delta)
    {
        // Local variable to store the input direction
        var direction = Vector3.Zero;

        // We check for each move input and update the direction accordingly
        if (Input.IsActionPressed("move_right"))
        {
            direction.x += 1f;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction.x -= 1f;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction.z += 1f;
        }
        if (Input.IsActionPressed("move_forward"))
        {
            direction.z -= 1f;
        }

        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
            GetNode<Spatial>("Pivot").LookAt(Translation + direction, Vector3.Up);
        }

        // Ground velocity
        _velocity.x = direction.x * Speed;
        _velocity.z = direction.z * Speed;
        // Vertical velocity
        _velocity.y -= FallAcceleration * delta;

        // Jumping
        if (IsOnFloor() && Input.IsActionJustPressed("jump"))
        {
            _velocity.y += JumpImpulse;
        }

        //Moving the character
        _velocity = MoveAndSlide(_velocity, Vector3.Up);
        
        for (int index = 0; index < GetSlideCount(); index++)
        {
            // Check collisions that occured in frame
            KinematicCollision collision = GetSlideCollision(index);

            // If we collide with monster...
            if (collision.Collider is Mob mob && mob.IsInGroup("mob"))
            {
                // ... check that we are hitting it from above
                if (Vector3.Up.Dot(collision.Normal) > 0.1f)
                {
                    // If so, we squash it and bounce
                    mob.Squash();
                    _velocity.y = BounceImpulse;
                }
            }
        }
    }
}
