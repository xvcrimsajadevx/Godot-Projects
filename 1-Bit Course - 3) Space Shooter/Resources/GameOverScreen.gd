extends Node

onready var highscoreLabel = $HighscoreLabel

func _ready():
	set_highscore_label()

func _process(_delta):
	if Input.is_action_just_pressed("ui_cancel"):
	# warning-ignore:return_value_discarded
		get_tree().change_scene("res://Resources/StartMenu.tscn")

func set_highscore_label():
	var save_data = SaveAndLoad.load_data_from_file()
	highscoreLabel.text = "Highscore: " + str(save_data.highscore)
