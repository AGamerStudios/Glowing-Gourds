using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueObject{
	public string characterName;
	[TextArea(1,3)]
	public string[] sentences;
}
