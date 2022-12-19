extends Node

func _process(_delta):
	if Input.is_action_just_pressed("ui_cancel"):
# warning-ignore:return_value_discarded
		get_tree().change_scene("res://Resources/StartMenu.tscn")
