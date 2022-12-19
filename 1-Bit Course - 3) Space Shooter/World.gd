extends Node

var score = 0 setget set_score

onready var scoreLabel = $ScoreLabel

func set_score(value):
	score = value
	scoreLabel.text = "Score: " + str(score)

func _on_Ship_player_death():
	yield(get_tree().create_timer(1), "timeout")
# warning-ignore:return_value_discarded
	get_tree().change_scene("res://Resources/GameOverScreen.tscn")
