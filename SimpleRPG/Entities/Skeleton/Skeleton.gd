extends KinematicBody2D

# Node References
var player

# Random number generator
var rng = RandomNumberGenerator.new()

# Movement variables
export var speed = 25
var direction : Vector2
var last_direction = Vector2(0, 1)
var bounce_countdown = 0

# Animation variables
var other_animation_playing = false

# Called when the node enters the scene tree for the first time.
func _ready():
	player = get_tree().root.get_node("Root/Player")
	rng.randomize()

func _on_Timer_timeout():
	# Calculate position of player relative to skeleton
	var player_relative_position = player.position - position
	
	if player_relative_position.length() <= 16:
		# If player is near, don't move but turn toward it
		direction = Vector2.ZERO
		last_direction = player_relative_position.normalized()
	elif player_relative_position.length() <= 100 and bounce_countdown == 0:
		# If player is within range, move towards it
		direction = player_relative_position.normalized()
	elif bounce_countdown == 0:
		#If player is too far, randomly decide whether to stand still or where to move
		var random_number = rng.randf()
		if random_number < 0.05:
			direction = Vector2.ZERO
		elif random_number < 0.1:
			direction = Vector2.DOWN.rotated(rng.randf() * 2 * PI)
	
	# Update bounce countdown
	if bounce_countdown > 0:
		bounce_countdown = bounce_countdown - 1

func _physics_process(delta):
	var movement = direction * speed * delta
	
	var collision = move_and_collide(movement)
	if collision != null and collision.collider.name != "Player":
		direction = direction.rotated(rng.randf_range(PI/4, PI/2))
		bounce_countdown = rng.randi_range(2, 5)
	
	# Animate skeletion based on direction
	if not other_animation_playing:
		animates_monster(direction)

func get_animation_direction(direction: Vector2):
	var norm_direction = direction.normalized()
	if norm_direction.y >= 0.707:
		return "down"
	elif norm_direction.y <= -0.707:
		return "up"
	elif norm_direction.x <= -0.707:
		return "left"
	elif norm_direction.x >= 0.707:
		return "right"
	else:
		return "down"

func animates_monster(direction: Vector2):
	if direction != Vector2.ZERO:
		# Gradually update last_direction to counteract bounce of analog stick
		last_direction = direction
		
		# Choose walk animation based on movement direction
		var animation = get_animation_direction(last_direction) + "_walk"
		
		# Play walk animation
		$AnimatedSprite.play(animation)
	else:
		# Choose idle animation based on movement direction
		var animation = get_animation_direction(last_direction) + "_idle"
		$AnimatedSprite.play(animation)
