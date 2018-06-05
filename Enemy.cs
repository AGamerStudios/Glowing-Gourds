using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {
    [Range(1,100)]
    public int totalHP;
    [Range(1,100)]
    public int damage;
    [Range(0f,100f)]
    public float attackCooldown;
    public Slider healthBar;
    [HideInInspector]
    public int hp;
    [Range(0f,10f)]
    public float speed;
    public int value;
    public GameObject hitNoise;
    private bool movmentEnabled = true;
    private bool touchingBase = false;
    private float currentAttackCooldown;
    
	void Start () {
        healthBar.maxValue = totalHP;
		hp = totalHP;
	}
	
	void Update ()
    {
        healthBar.value = hp;

        if(GetComponent<Animator>().GetBool("IsDying"))
        {
            movmentEnabled = false;
        }

        if(hp <= 0)
        {
            HandleEnemyDeath();
        }

        if(movmentEnabled){
            MoveEnemy();
        }

        if(touchingBase){
            AttackBase();
        }
    }

    private void HandleEnemyDeath()
    {
        if (!GetComponent<Animator>().GetBool("IsDying"))
        {
            FindObjectOfType<ScoreKeeper>().IncrementScore(value);
            Destroy(transform.GetChild(0).gameObject);
            Destroy(GetComponent<Collider2D>());
            gameObject.tag = "Untagged";
        }
        GetComponent<Animator>().SetBool("IsDying", true);
    }

    private void AttackBase()
    {
        if (currentAttackCooldown <= 0){
            FindObjectOfType<Base>().RecieveDamage(damage);
            currentAttackCooldown = attackCooldown;
        }
        else{
            currentAttackCooldown -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "Base"){
            movmentEnabled = false;
            touchingBase = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        movmentEnabled = true;
        touchingBase = false;
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.Lerp(
            transform.position, 
            new Vector2(transform.position.x + speed, transform.position.y), 
            Time.deltaTime
        );
    }

    public void RecieveDamage(int damage){
        AudioSource hitAudio = Instantiate(
            hitNoise, 
            Vector3.zero, 
            Quaternion.identity
        ).GetComponent<AudioSource>();
        hitAudio.pitch = Random.Range(0.9f, 1.1f);
        hitAudio.Play();
        hp -= damage;
    }
}