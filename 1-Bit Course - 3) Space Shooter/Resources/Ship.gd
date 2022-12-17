extends Area2D

const Bullet = preload("res://Resources/Bullet.tscn")
const ExplosionEffect = preload("res://Resources/ExplosionEffect.tscn")

export(int) var SPEED = 100

func _process(delta):
	if Input.is_action_pressed("ui_up"):
		position.y -= SPEED * delta
	if Input.is_action_pressed("ui_down"):
		position.y += SPEED * delta
	if Input.is_action_just_pressed("ui_accept"):
		fire_bullet()

func fire_bullet():
	var bullet = Bullet.instance()
	var main = get_tree().current_scene
	main.add_child(bullet)
	bullet.global_position = global_position


func _on_Ship_area_entered(area):
	area.queue_free()
	queue_free()

func _exit_tree():
	var main = get_tree().current_scene
	var explosionEffect = ExplosionEffect.instance()
	main.call_deferred("add_child", explosionEffect)
	explosionEffect.global_position = global_position
