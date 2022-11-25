tool

extends Area2D

enum Potion { HEALTH, MANA }
export(Potion) var type = Potion.HEALTH

# Called when the node enters the scene tree for the first time.
func _ready():
	if type == Potion.MANA:
		$Sprite.region_rect.position.x = 8
	else:
		$Sprite.region_rect.position.x = 0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Engine.editor_hint:
		if type == Potion.MANA:
			$Sprite.region_rect.position.x = 8
		else:
			$Sprite.region_rect.position.x = 0

func _on_Potion_body_entered(body):
	if body.name == "Player":
		body.add_potion(type)
		get_tree().queue_delete(self)
