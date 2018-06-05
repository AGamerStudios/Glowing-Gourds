using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour{
    public string waveName;
    public GameObject[] enemyTypes;
	[Range(1, 50)]
	public int count;
	[Range(0.5f,5f)]
    public float rate;
	[Range(0f,10f)]
    public float delay;
}