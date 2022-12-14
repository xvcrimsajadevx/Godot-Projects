extends "res://Player/Projectile.gd"

const EnemyDeathEffect = preload("res://Effects/EnemyDeathEffect.tscn")
const BRICK_LAYER_BIT = 5

func _ready():
	pass # Replace with function body.

func _on_Hitbox_body_entered(body):
	if body.get_collision_layer_bit(BRICK_LAYER_BIT):
		body.queue_free()
		Utils.instance_scene_on_main(EnemyDeathEffect, body.global_position + Vector2(8, 16))
	._on_Hitbox_body_entered(body)
