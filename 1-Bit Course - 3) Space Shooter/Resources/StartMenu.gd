extends Node

func _process(_delta):
	if Input.is_action_just_pressed("ui_accept"):
	# warning-ignore:return_value_discarded
		get_tree().change_scene("res://World.tscn")
	if Input.is_action_just_pressed("ui_cancel"):
		get_tree().quit()
