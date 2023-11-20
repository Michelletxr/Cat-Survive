//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
using UnityEngine;
public class Rat_v1 : MonoBehaviour {
 
    public Rigidbody2D rb;
    public Collider2D ratCollider;
    public Behavior_Rat behavior_rat;
    public bool collideCheese = false;
    public bool collideEnemy = false;
    public int totalCheeseCollet = 0;
    public int life = 100;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ratCollider = GetComponent<Collider2D>();
        
        GameObject maze = GameObject.Find("MainMaze");
        
        behavior_rat = new Behavior_Rat(this, maze.GetComponent<MainMaze>());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Verifica se a colisão envolve o objeto atual
        if (collision.gameObject.CompareTag("item")){
            this.collideCheese = true;
            Debug.Log("Colisão com o alvo item!");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy")){
            this.collideEnemy = false;
            Debug.Log("enemy is  enter trigger colision");
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy") && !ratCollider.enabled){
            this.collideEnemy = false;
            Debug.Log("enemy is  exit trigger colision");
        }
    }



    void Update() {
        if(behavior_rat != null){
            behavior_rat.createBehavior();
        }
    }


}
