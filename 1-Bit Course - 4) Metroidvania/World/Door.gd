extends Area2D

export(Resource) var connection = null
export(String, FILE, "*.tscn") var new_level_path = ""
var active = true

func _on_Door_body_entered(Player):
	if active == true:
		print("hit door")
		Player.emit_signal("hit_door", self)
		active = false
