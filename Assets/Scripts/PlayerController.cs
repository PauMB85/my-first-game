using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    public float jumpForce = 6.0f;
    public float runningSpeed = 1.0f;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayerMask;
    public Animator animator;

    private Vector3 startPosition;


    void Awake() {
        rigidBody = GetComponent<Rigidbody2D> ();
        sharedInstance = this;
        startPosition = this.transform.position;
        animator.SetBool("isAlive", true);
    }

    void Start() {
        
    }

    // Start is called before the first frame update
    public void StartGame() {
        animator.SetBool("isAlive", true);
        this.transform.position = startPosition;
        rigidBody.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Si se pulsa el botón izq del ratón se lanza la acción de 
        if(GameManager.sharedInstance.currentGameState == GameState.inTheGame){ 

            if(Input.GetMouseButtonDown(0)) {
                Debug.Log("Botón izquierdo del ratón pulsado !");
                Jump();
            }

            //Actualizamos el valor de isGrounded
            animator.SetBool("isGrounded",IsOnTheFloor());
        }

        
    }

    // Unity llama a este métodad cada intervalo de tiempo fijo, es ideal donde añadir fuerzas constantes a la fisica, velocidad... movimientos..
    void FixedUpdate() {
        
        if(GameManager.sharedInstance.currentGameState == GameState.inTheGame){
            if( rigidBody.velocity.x < runningSpeed ) {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }
        
        
    }

    /**
    * Metodo que se encarga de realizar la acción de saltar
    */
    void Jump () {
        if(IsOnTheFloor()) {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }



    /**
    * Método que comprueba si el objeto (conejo) está en el suelo
    */
    bool IsOnTheFloor () {
        bool isOnTheFloor = false;

        //Raycast se comprueba si el objeto(conejo) esta tocando el suelo.
        //Se lanza un rayo(raycast) desde la posición del conejo(trasnform.position) hacia abajo(Vector2.down) de longitud 1.0 y comprueba si toca el suelo(groundLayerMask.value)
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value)) {
            isOnTheFloor = true;
        }

        return isOnTheFloor;
    }

    public void KillPlayer () {
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);
    }
}
