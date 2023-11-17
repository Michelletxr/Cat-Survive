//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
using UnityEngine;
public class Rat_v1 : MonoBehaviour {
 
    public Rigidbody2D rb;
    public Collider2D ratCollider;
    public Behavior_Rat behavior_rat;
    public bool collideCheese = false;
    public int totalCheeseCollet = 0;
    public int life = 100;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ratCollider = GetComponent<Collider2D>();
        
        GameObject maze = GameObject.Find("MainMaze");
        
        behavior_rat = new Behavior_Rat(this, maze.GetComponent<MainMaze>());

        if (ratCollider != null)
        {
            ratCollider.enabled = false; // Desabilita o collider
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Verifica se a colisão envolve o objeto atual
        if (collision.gameObject.CompareTag("cheese")){
            this.collideCheese = true;
            Debug.Log("Colisão com o alvo!");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy")){
            this.life = this.life - 10;
            if(ratCollider.enabled) {  ratCollider.enabled = false; }
            Debug.Log("Colisão com o alvo!");
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("enemy") && !ratCollider.enabled){
            ratCollider.enabled = true;
            Debug.Log("Colisão com o alvo!");
        }
        Debug.Log("Golin entrou na area de colisão");
    }

    public void DisableTheCollider()
    {
        // Verifica se o Collider existe
        if (ratCollider != null){
            // Desabilita o Collider
            ratCollider.enabled = false;
        }
        else {
            Debug.LogWarning("Collider não atribuído ou não encontrado!");
        }
    }

    void Update() {
        if(behavior_rat != null){
            behavior_rat.createBehavior();
        }
    }


}
