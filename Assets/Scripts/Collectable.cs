using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    bool isCollected = false;

    void ShowCoin()
    {
        // esta linea activa el sprite (img)
        this.GetComponent<SpriteRenderer>().enabled = false;
        //luego hay que activa el collider
        this.GetComponent<CircleCollider2D>().enabled = false;
        isCollected = false;
    }

    void HideCoin()
    {
        // esta linea quita el sprite (img)
        this.GetComponent<SpriteRenderer>().enabled = false;
        //luego hay que quitar el collider
        this.GetComponent<CircleCollider2D>().enabled = false;
        isCollected = true;
    }

    void CollectCoin()
    {
        
        HideCoin();

        //notificar al manager que hemos recolectado una nueva modena
        Debug.Log("Enviamos evento al manager");
        GameManager.sharedInstance.CollectCoin();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {

        if(otherCollider.CompareTag("Player"))
        {
            Debug.Log("Evento collaider conejo - moneda");
            CollectCoin();
        }
        
    }
}
