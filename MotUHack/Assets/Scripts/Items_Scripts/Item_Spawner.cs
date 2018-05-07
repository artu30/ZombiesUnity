using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spawner : MonoBehaviour {

	public List<List_Element> itemList;
	public bool DestroySpawnerAtEnd = false;
	private float timeElapsed = 0f;
	private int numElementsAppeared = 0;
	
	void Update () {
		timeElapsed += Time.deltaTime;
		for (int i = 0; i < itemList.Count; i++) {
			if (timeElapsed >= itemList[i].timeToAppear && !itemList[i].hasAppeared){
				itemList [i].hasAppeared = true;
				numElementsAppeared++;
				GameObject item = Instantiate (itemList[i].item, this.transform.position, this.transform.rotation);
                Item_Prefab it = item.GetComponentInChildren<Item_Prefab>();
                it.timeToLive = itemList[i].timeToLive;
            }
		}
		if (DestroySpawnerAtEnd && numElementsAppeared == itemList.Count) {
			Destroy (gameObject);
		}
	}
}
