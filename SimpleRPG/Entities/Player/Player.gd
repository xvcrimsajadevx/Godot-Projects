extends KinematicBody2D

# Player Movement Speed
export var speed = 75

func _physics_process(delta):
	# Get Player input
	var direction: Vector2
	direction.x = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	direction.y = Input.get_action_strength("ui_down") - Input.get_action_strength("ui_up")
	
	# If input is digital, normalize it for diagonal movement
	if abs(direction.x) == 1 and abs(direction.y) == 1:
		direction = direction.normalized()
		
	# Apply Movement
	var movement = speed * direction * delta
	move_and_collide(movement)
