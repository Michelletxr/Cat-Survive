//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
public class Goblin : MonoBehaviour{

        //definir as condições
        bool isPlayerAround(){
            return posPlayer.gameObject != null;

        }

        bool isLifeZero(){
            return life == 0;
        }

        bool isPlayerOnCollisionEnter2D(){
            int distancePositions = this.transform.position - this.agent.posPlayer;
            return distancePositions <= 1;
        }


        public Rigidbody2D rb;
        public Transform posPlayer;

        public int life = 100;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            posPlayer = GameObject.FindWithTag("player").transform;
        }

        void Update()
        {
            
            
    }


}
