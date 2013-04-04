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
	private string propertyName;

    // Use this for initialization

    void Start()
    {
       // matCol = transform.parent.renderer.material.color;
    }
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "_Hero"){
			player = other.gameObject;
			if(this.gameObject.name == "Mother_T"){
				matCol = transform.parent.renderer.material.color;
				propertyName = "_Color";
				if(Globals.deathMother == true){
					start = true;	
					player.GetComponent<CharacterMovement>().inCutscene = true;
				}	
			}
			
			if(this.gameObject.name == "Lake"){
				matCol = transform.parent.renderer.material.GetColor("_horizonColor");
				propertyName = "_horizonColor";
				start = true;
			}
		}
	}
 

    // Update is called once per frame

    void Update()
    {
        if (!isDone && start)
        {
			
            alfa = transform.parent.renderer.material.GetColor(propertyName).a - Time.deltaTime/(fadeSpeed);
            newColor = new Color(matCol.r, matCol.g, matCol.b, alfa);
            transform.parent.renderer.material.SetColor(propertyName, newColor);
			if(alfa <= 0){
				isDone = true;
				//player.GetComponent<CharacterMovement>().inCutscene = false;
				Destroy(this.gameObject.transform.parent.gameObject);
			}
        }
    }
}
