extends Mob
class_name Enemy

var data = preload("res://global/data.gd")

var SPEED : float = 100.0
const JUMP_VELOCITY : float = -400.0

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

@export var direction : int = -1
@export var enemy_scale : int = 1

func _ready():
	var raycast: RayCast2D = self.get_node("raycast")
	var sprite : AnimatedSprite2D = self.get_node("sprite")
	var collision  = self.get_node("collision")

	raycast.scale = raycast.scale * enemy_scale
	sprite.scale = sprite.scale * enemy_scale
	collision.scale = collision.scale * enemy_scale


	if direction == 1:
		sprite.flip_h = true
		raycast.rotation_degrees = 180
	else: 
		sprite.flip_h = false
		raycast.rotation_degrees = 0

func _physics_process(delta):
	if not is_on_floor():
		velocity.y += gravity * delta

	direction = if_colliding()
	
	if direction:
		velocity.x = direction * SPEED
	else:	
		velocity.x = move_toward(velocity.x, 0, SPEED)
	
	move_and_slide()

func damage_player():
	data.player_health = data.player_health - 1
	
func if_colliding() -> int:
	
	var raycast: RayCast2D = self.get_node("raycast")
	var sprite : AnimatedSprite2D = self.get_node("sprite")
	
	if raycast.is_colliding:
		var object = raycast.get_collider()

		if object != null:
			
			if object.name == "player" or object.name == "Bullet":
				return direction

			sprite.flip_h = !sprite.flip_h
			raycast.rotation_degrees += 180
			
			if direction == -1:
				return 1
			else: 
				return -1

	return direction

func take_damage():
	set_health(get_health() - 1)
