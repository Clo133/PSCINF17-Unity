using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Label 
{
	string value ;
	Vector2 position;
	public Label(string value_, Vector2 position_)
	{
		value = value_;
		position = position_;
	}
}
