using Godot;

namespace CsharpTest.characters.scripts;

public partial class Player : Node2D {
    
    private int _health = 100;
    private int _armor = 100;
    private int _speed = 100;
    private int _jumpSpeed = 100;
    private int _gravity = 100;
    private int _slideSpeed = 100;
    private int _slideDirection = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        GD.Print("------------");
        GD.Print("Player Ready");
        GD.Print("Health: " + _health);
        GD.Print("Armor: " + _armor);
        GD.Print("Speed: " + _speed);
        GD.Print("------------");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(
        double delta
    ) {
    }
}