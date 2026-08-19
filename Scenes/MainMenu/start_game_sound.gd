extends Button

@export var sound : AudioStreamPlayer2D

func _on_pressed() -> void:
	sound.play()
