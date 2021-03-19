using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;

    //Lista que contiene todos los niveles que se han creado
    public List<LevelBlock> allThelevelBlocks = new List<LevelBlock>(); 
    //Lista de los bloques que tenemos ahora mismo en pantalla
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); 
    //Punto inicial donde empezará a crearse el primer nivel de todos
    public Transform levelInitialPoint;

    bool isGeneratingInitialBloc = false;
    private void Awake() {
        sharedInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateInitialBlocks() {
        isGeneratingInitialBloc = true;
        for (int i = 0; i < 3; i++){
            AddNewBlock();
        }
        isGeneratingInitialBloc = false;
    }

    public void AddNewBlock() {
        //seleccionamos un bloque aleatorio entre los que tenemos disponibles
        int randomIndex = Random.Range(0,allThelevelBlocks.Count);

        if(isGeneratingInitialBloc)
        {
            randomIndex = 0;
        }

        // Obtenemos un copia del bloque de la posición randoIndex
        LevelBlock block = (LevelBlock) Instantiate (allThelevelBlocks[randomIndex]);
        // Le indicamos quien es el padre, al realizar la instanciacion este pierde su posición.
        block.transform.SetParent(this.transform, false);

        Vector3 blockPosition = Vector3.zero;

        if(currentLevelBlocks.Count == 0){
            // Se coloca el primer bloque de la pantalla
            Debug.Log("incluimos el primer bloque");
            blockPosition = levelInitialPoint.position;
            Debug.Log("El primer bloque se encuentra " + blockPosition);
        } else {
            // Ya tenego bloques en pantalla, lo empalmo al último disponible
            Debug.Log("Estamos en el bloque " + currentLevelBlocks.Count);
            blockPosition = currentLevelBlocks[currentLevelBlocks.Count-1].exitPoint.position;
            Debug.Log("El bloque  " + currentLevelBlocks.Count + " se enceuntra "+ blockPosition);
        }

        block.transform.position = blockPosition;
        Debug.Log("Colocamos el bloque en la posicion " + block.transform.position);
        currentLevelBlocks.Add(block);
    }

    public void RemoveOldBlock() {
        LevelBlock block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        Destroy (block.gameObject);
    }

    void RemoveAllTheBlocks() {
        currentLevelBlocks.Clear();
    }

    public void RestoreGame() {
        RemoveAllTheBlocks();
        GenerateInitialBlocks();
    }
}
