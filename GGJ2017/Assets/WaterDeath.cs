using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour {
public Transform car;
void Update(){
        Vector3 pos = car.position;
        pos.y = transform.position.y;
        transform.position = pos;
    }
    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            // GameOver.Instance.GAMEOVER();
            ScreenController.Instance.InterruptWindow();
        }
	}
}
