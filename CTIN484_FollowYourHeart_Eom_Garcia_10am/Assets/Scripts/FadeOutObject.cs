using UnityEngine;
using System.Collections;

public class FadeOutObject : MonoBehaviour {

    public int fadeSpeed = 3;
    private bool isDone = false;
    private Color matCol;
    private Color newColor;
    private float alfa = 0;
	private bool start = false;
	private GameObject player;

    // Use this for initialization

    void Start()
    {
        matCol = transform.parent.renderer.material.color;
    }
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			player = other.gameObject;
			if(this.gameObject.name == "Mother_T"){
				if(Globals.deathMother == true){
					start = true;	
					player.GetComponent<CharacterMovement>().inCutscene = true;
				}	
			}
		}
	}
 

    // Update is called once per frame

    void Update()
    {
        if (!isDone && start)
        {
            alfa = transform.parent.renderer.material.color.a - Time.deltaTime/(fadeSpeed);
            newColor = new Color(matCol.r, matCol.g, matCol.b, alfa);
            transform.parent.renderer.material.SetColor("_Color", newColor);
			if(alfa <= 0){
				isDone = true;
				player.GetComponent<CharacterMovement>().inCutscene = false;
				Destroy(this.gameObject.transform.parent.gameObject);
			}
        }
    }
}
