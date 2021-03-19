using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("El conejo ha tocado el leaveTigger");
        LevelGenerator.sharedInstance.AddNewBlock();
        LevelGenerator.sharedInstance.RemoveOldBlock();
    }
}
