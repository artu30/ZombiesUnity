using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item_Prefab : MonoBehaviour {

	public float timeToLive = 10f;
	public bool canBeDestroyed = true;
    public bool hasBeenEquipped = false;

    void Update () {
        checkTime();
	}

    // Funcion que checkea si el objeto desaparece antes de ser recogido
    protected void checkTime(){
        if (timeToLive < 0 && canBeDestroyed){
            destroyItem();
        }
        else{
            if (timeToLive >= 0){
                timeToLive -= Time.deltaTime;
            }
        }
    }

    // Funcion a la que llama el Player cuando colisiona con un objeto
    // Cada objeto la personalizara con sus acciones
    public virtual void getItem (CharacterCollisions player){
        if (!hasBeenEquipped) {
            Debug.Log("Object equipped, animation stops.");
            canBeDestroyed = false;
            hasBeenEquipped = true;
            destroyParent();
            gameObject.GetComponent<Animator>().enabled = false;
        }
        else {
            Debug.Log("Object already equipped");
        }
    }

    // Funcion que destruye el objeto y su gameObject padre vacio
    public void destroyItem() {
        if (transform.parent != null) {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
        Debug.Log("Item " + gameObject.name + " destroyed");
    }

    // Funcion que destruye solo al parent del objeto que se crea cuando spawnea
    public void destroyParent() {
        if (transform.parent != null) {
            GameObject parent = transform.parent.gameObject;
            transform.parent = null;
            Destroy(parent);
        }
    }

    public void setVisible(bool visible) {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) { renderer.enabled = visible; }
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs) {
            r.enabled = visible;
        }
    }
}
