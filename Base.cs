using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {
    public int hp;
    [Range(1,100)]
	public int totalHP;
	public Slider healthBar;

    void Start(){
        hp = totalHP;
    }

	void Update()
    {
        healthBar.value = hp;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (hp <= 0){
			GameObject.FindObjectOfType<LevelManager>().LevelOver(false);
            Destroy(gameObject);
        }
    }

    public void RecieveDamage(int damage){
        hp -= damage;
    }
}
