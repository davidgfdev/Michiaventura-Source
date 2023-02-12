using UnityEngine;

public class scr_stormTrigger : MonoBehaviour
{
    scr_climasystem climaController;

    private void Awake() {
        climaController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_climasystem>();    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) climaController.alternateStorm();
        GameObject[] strikes = GameObject.FindGameObjectsWithTag("Lightning");
        foreach (GameObject strike in strikes){
            Destroy(strike);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) climaController.alternateStorm();
    }
}

