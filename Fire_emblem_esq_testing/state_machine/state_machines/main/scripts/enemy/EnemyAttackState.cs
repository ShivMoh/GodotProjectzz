using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class EnemyAttackState : State {

	private CombatUtility combatUtility;

	private List<bool> removalList;
	public override void enter()
	{
		GD.Print("Am on enemy attack state");

		this.combatUtility = new CombatUtility(
			MapEntities.map,
			MapEntities.characters,
			MapEntities.selectedCharacter
		);

		this.removalList = new List<bool>();
	}

	public override void physicsUpdate(double _delta)
	{
		
		foreach (Character character in MapEntities.targetedCharacters)
		{
			bool remove = this.combatUtility.attackCharacter(MapEntities.selectedCharacter, character, MapEntities.chosenAttack);
			removalList.Add(remove);
		}


		// GD.Print(this.removalList.Count());
		foreach (bool item in this.removalList)
		{
			if (item == true) {
				Character removedCharacter = MapEntities.targetedCharacters.ElementAt(removalList.IndexOf(item));
				MapEntities.targetedCharacters.Remove(removedCharacter);
				removedCharacter.QueueFree();
				// MapEntities.characters.RemoveAt(removalList.IndexOf(item));
				
			}
		}
		
		// MapEntities.count = 0;
		EmitSignal(SignalName.StateChange, this, "EnemyTargetSelectionState");

		// removalList.Clear();


		// this.targetedCopies = MapEntities.targetedCharacters;
	}
}
