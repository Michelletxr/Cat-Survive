//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
using UnityEngine;
public class Goblin_v1 : MonoBehaviour{

        //definir as condições
        public bool isPlayerAround(){
            return posPlayer.gameObject != null;

        }

        public bool isLifeZero(){
            //return life == 0;
            return false;
        }

        public bool isPlayerOnCollisionEnter2D(){
            //int distancePositions = this.transform.position - this.posPlayer.position;
            //return distancePositions <= 1;
            return false;
        }

        public Condition isRatOnCollision(){
            return new Condition(() => {
                return this.colliderRat;
            });
        }


        public Rigidbody2D rb;
        public Transform[] pointsToMove;
        public Transform posPlayer { get; set; }
        public GameObject gameObject { get; set; }

        public Behavior_Goblin behavior_Goblin;
        public int life = 100;

        public bool colliderRat = false;


        void Start()
        {
            Debug.Log("INICIAR bt");
            rb = GetComponent<Rigidbody2D>();
            posPlayer = GameObject.FindWithTag("rat").transform;

            behavior_Goblin = new Behavior_Goblin(this);
           // behavior_Goblin.createBehavior();
        }

        private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a colisão envolve o objeto atual
        if (collision.gameObject.CompareTag("rat"))
        {
            this.colliderRat = true;
            Debug.Log("Colisão com o alvo!");
            
            // Faça algo quando houver colisão com o objeto marcado como "Alvo"
        }
    }

        void Update(){
            //behavior_Goblin.MoveToWall().Execute();
            //Debug.Log("update");
            behavior_Goblin.createBehavior();
            
        }


}
