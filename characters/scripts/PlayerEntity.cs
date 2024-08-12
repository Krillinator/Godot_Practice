using Godot;

namespace CsharpTest.characters.scripts;

// TODO - Fix SNAPPING

public partial class PlayerEntity : CharacterBody2D {
    
    [Export] private Sprite2D _playerSprite;
    [Export] private float _playerSpeed = 400f;
    [Export] private float _acceleration = 2000f;
    [Export] private float _deceleration = 9000f;
    [Export] private int _health = 100;
    
    // Get the gravity from the project settings so you can sync with rigid body nodes.
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private float _lastDirection;
    
    public override void _PhysicsProcess(
        double delta
    ) {
        base._PhysicsProcess(delta);

        var velocity = Velocity;
        var direction = Input.GetAxis("move_left", "move_right");

        // Apply Gravity
        velocity.Y += _gravity * (float)delta;

        /* DEBUGGING */
        GD.Print(IsDirectionChanged(_lastDirection, direction));
        GD.Print($"direction: {direction}");
        GD.Print($"lastDirection: {_lastDirection}");
        
        Velocity = Handle_Movement(velocity, direction, (float)delta);
        
        if (direction != 0) {
            _lastDirection = direction;   
        }
        
        MoveAndSlide();
    }

    // TODO - When turning around at the same point the acceleration reaches 400, player then slides instead of stopping
    private Vector2 Handle_Movement(Vector2 velocity, float direction, float delta) {

        if (IsDirectionChanged(_lastDirection, direction)) {
            velocity.X = 0;
        }
        
        // If we're moving
        if (direction != 0) {
            velocity.X += direction * _acceleration * delta;
            GD.Print($"player velocity: {velocity}");

            velocity.X = Mathf.Clamp(velocity.X, -_playerSpeed, _playerSpeed);
        } else {
            GD.Print($"player decelerating: {velocity}");

            // Decelerate
            switch (velocity.X) {
                case > 0:
                    velocity.X -= _deceleration * delta;
                    velocity = Check_Overshoot(velocity);
                    break;
                case < 0:
                    velocity.X += _deceleration * delta;
                    velocity = Check_Overshoot(velocity);
                    break;
            }
        }

        return velocity;
    }
    
    private Vector2 Check_Overshoot(Vector2 velocity) {
        if (velocity.X > 0) velocity.X = 0;
        if (velocity.X < 0) velocity.X = 0;

        return velocity;
    }
    
    private bool IsDirectionChanged(float lastDirection, float currentDirection) {
        return (lastDirection < 0 && currentDirection > 0) || (lastDirection > 0 && currentDirection < 0);
    }
    
    
    
}

// TODO Space Movement (only momentum, no resistance) 
// velocity.X += direction * _acceleration * (float)delta;

// TODO Limits movement to -300 & 300 (left & right)
// velocity.X = Mathf.Clamp(velocity.X, -_playerSpeed, _playerSpeed);