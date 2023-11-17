//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
using UnityEngine;
public class Goblin_v1 : MonoBehaviour{

        public bool isPlayerOnCollisionEnter2D(){
            //int distancePositions = this.transform.position - this.posPlayer.position;
            //return distancePositions <= 1;
            return false;
        }

        public Rigidbody2D rb;
        public Transform[] pointsToMove;
        public Transform posPlayer { get; set; }
        public GameObject gameObject { get; set; }

        public Behavior_Goblin behavior_Goblin;
        public int life = 100;

        public bool collidePointsRound = false;
        public bool collideRat;
        //eventos
       // public UnityEvent receivedDamage;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
            posPlayer = GameObject.FindWithTag("rat").transform;
            behavior_Goblin = new Behavior_Goblin(this);
            this.collideRat = false;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
        // Verifica se a colisão envolve o objeto atual
        if (collision.gameObject.CompareTag("PointsRound")) {
            this.collidePointsRound = true;
            Debug.Log("Colisão com o alvo!");
        }

         if (collision.gameObject.CompareTag("rat")) {
            this.collideRat = true;
            Debug.Log("Colisão com o alvo!");
        }
    }

        void Update() { 
            behavior_Goblin.createBehavior();
            
        }


}
