using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InitialState : State {

	static List<AttackMeta> attacksMeta = new List<AttackMeta>() {
		new AttackMeta(
			name: "Fire ball",
			power: 5,
			attackType: AttackType.CLOSE
		),
		new AttackMeta(
			name: "Ice ball",
			power: 3,
			attackType: AttackType.CLOSE
		)
	};

	static CharacterStat characterStat= new CharacterStat(
		name: "John",
		health: 20,
		strenth: 20,
		speed: 20,
		intelligence: 15,
		skill: 10,
		constition: 20
	);

	CharacterMeta[] playableCharactersMeta = {
		new CharacterMeta(
			tileCoord: new Vector2I(5, 5),
			characterPath : "res://mobs/scenes/playable_character.tscn",
			attacks : attacksMeta
		),
		new CharacterMeta(
			tileCoord: new Vector2I(0, 7),
			characterPath : "res://mobs/scenes/playable_character.tscn",
			attacks : attacksMeta

		),
		new CharacterMeta(
			tileCoord: new Vector2I(5, 0),
			characterPath : "res://mobs/scenes/playable_character.tscn",
			attacks : attacksMeta			
		),
	};

	CharacterMeta[] enemyCharactersMeta = {
		new CharacterMeta(
			tileCoord: new Vector2I(7, 2),
			characterPath : "res://mobs/scenes/enemy_character.tscn",
			attacks : attacksMeta
		),
		new CharacterMeta(
			tileCoord: new Vector2I(6, 1),
			characterPath : "res://mobs/scenes/enemy_character.tscn",
			attacks : attacksMeta
		),
		new CharacterMeta(
			tileCoord: new Vector2I(6, 3),
			characterPath : "res://mobs/scenes/enemy_character.tscn",
			attacks : attacksMeta
		),
		new CharacterMeta(
			tileCoord: new Vector2I(3, 7),
			characterPath : "res://mobs/scenes/enemy_character.tscn",
			attacks : attacksMeta
		),
		new CharacterMeta(
			tileCoord: new Vector2I(3, 9),
			characterPath : 
			
			"res://mobs/scenes/enemy_character.tscn",
			attacks : attacksMeta
		),
	};


	[Export]
	TileMap tilemap;


	public override void enter()
	{
		MapEntities.map = tilemap;
		MapEntities.selectedCharacter = null;
		MapEntities.cursorCoords = new Vector2I(0, 0);
		
		loadCharacters();
		loadEnemies();

		MapEntities.playableCharacterCount = MapEntities.playableCharacters.Count();
		MapEntities.enemyCharacterCount = MapEntities.enemyCharacters.Count();
		
	}

	public override void physicsUpdate(double _delta)
	{
		EmitSignal(SignalName.StateChange, this, "ExploreState");
	}

	private void loadCharacters() {
		for (int i = 0; i < playableCharactersMeta.Length; i++) {
			PlayableCharacter character = Character.instantiate(
				MapEntities.map.MapToLocal(playableCharactersMeta[i].tileCoord),
				playableCharactersMeta[i].characterPath
			) as PlayableCharacter;

			character.setAttacks(playableCharactersMeta[i].attacks);
			character.setCharacterStats(characterStat);
			character.moveSteps = character.getCharacterStats().speed;
			MapEntities.map.GetNode("playableCharacters").AddChild(character);
			MapEntities.playableCharacters.Add(character);
		}
	}

	private void loadEnemies() {
		for (int i = 0; i < enemyCharactersMeta.Length; i++) {
			EnemyCharacter character = Character.instantiate(
				MapEntities.map.MapToLocal(enemyCharactersMeta[i].tileCoord),
				enemyCharactersMeta[i].characterPath
			) as EnemyCharacter;

			character.setAttacks(enemyCharactersMeta[i].attacks);
			character.setCharacterStats(characterStat);
			character.moveSteps = character.getCharacterStats().speed;
			MapEntities.map.GetNode("enemyCharacters").AddChild(character);
			MapEntities.enemyCharacters.Add(character);
		}
	}
}
