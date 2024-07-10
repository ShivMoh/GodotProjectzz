using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class StateMachineManager : Node {
	
	[Export]
	TileMap tilemap;

	List<StateMachine> stateMachines;

	StateMachine currentStateMachine;
    public override void _EnterTree()
    {
		stateMachines = new List<StateMachine>();

		stateMachines.Add(new OpenWorldStateMachine());
		stateMachines.Add(new CombatStateMachine());

		this.currentStateMachine = stateMachines.First();
	
    }

    public override void _Ready()
	{
		MapEntities.map = tilemap;
		foreach (StateMachine stateMachine in this.stateMachines)
		{
			GD.Print("this runs????");
			stateMachine.StateMachineChange += onChildTransition;
		}

		AddChild(currentStateMachine);
	}

	public void onChildTransition(StateMachine stateMachine, string stateMachineName) {
		
		if (currentStateMachine.GetType().Name == stateMachineName) return;

		StateMachine newStateMachine = this.stateMachines.FirstOrDefault( stateMachine => stateMachine.Name == stateMachineName, null);

		if (newStateMachine == null) return;

		currentStateMachine.QueueFree();

		currentStateMachine = newStateMachine;

		AddChild(currentStateMachine);

		GD.Print("Is this shit working or what??", currentStateMachine.GetType().Name);		
	}

	

}