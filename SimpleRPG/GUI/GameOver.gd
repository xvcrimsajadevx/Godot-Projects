extends ColorRect

var go_selected_menu

func _ready():
	set_process_input(false)
	go_selected_menu = 0
	change_game_over_menu_color()

func change_game_over_menu_color():
	$GORestart.color = Color.crimson
	$GOQuit.color = Color.crimson
	
	match go_selected_menu:
		0:
			$GORestart.color = Color.greenyellow
		1:
			$GOQuit.color = Color.greenyellow

func _input(event):
	if Input.is_action_just_pressed("ui_right"):
		go_selected_menu = (go_selected_menu + 1) % 2
		change_game_over_menu_color()
	elif Input.is_action_just_pressed("ui_left"):
		if go_selected_menu > 0:
			go_selected_menu = go_selected_menu - 1
		else:
			go_selected_menu = 1
		change_game_over_menu_color()
	elif Input.is_action_just_pressed("attack"):
		match go_selected_menu:
			0:
				# Restart Game
				get_tree().change_scene("res://Scenes/Main.tscn")
				get_tree().paused = false
			1:
				# Quit Game
				get_tree().quit()
