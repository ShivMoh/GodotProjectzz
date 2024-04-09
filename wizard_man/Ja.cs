using Godot;
using System;

public partial class Ja : StaticBody3D
{

	
	public mage mage;

	[Export]
	public bool active;

	private Area3D areaDetector;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		areaDetector = GetNode<Area3D>("Area3D");
		areaDetector.MouseEntered += MouseOver;
		areaDetector.MouseExited += MouseOut;

		// if (active) {
			
		// 	changeColor(new Color(0, 0, 1));
		// } 
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}


	public override void _InputEvent(Camera3D camera, InputEvent @event, Vector3 position, Vector3 normal, int shapeIdx)
	{
		base._InputEvent(camera, @event, position, normal, shapeIdx);
		
		if(@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left) {
			GD.Print("Here", GlobalPosition);
			mage.setToPosition(GlobalPosition);
			mage.move = true;
		
		}

		
	}

	public void MouseOver() {
		// active = true;
		changeColor(new Color(0, 0, 1));
	}

	public void MouseOut() {
		// active = false;
		changeColor(new Color(1, 0, 0));
	}

	public void changeColor(Color color) {
		MeshInstance3D mes = GetNode<MeshInstance3D>("mesh");
		StandardMaterial3D material = mes.GetSurfaceOverrideMaterial(0) as StandardMaterial3D;
		material.AlbedoColor = color;
	}
	
}


