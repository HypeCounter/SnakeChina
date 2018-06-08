using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {


	private Snake next;
	static public Action <string> hit;
    public static bool spawn_em_cima;

	public void setnext(Snake IN)
	{
		next = IN;

	}

	public  Snake getNext() {
	
		return next;
	}

	public void RemoveTail(){
	
		Destroy (this.gameObject);

	
	}

	void OnTriggerEnter2D (Collider2D other){
	
		if (hit != null) {
			hit (other.transform.tag);

		}
		if (other.tag == "Food"){
			Destroy (other.gameObject);
           

		}

        
	}


    //Checar se está em cima
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name =="Spawn")
        {
            spawn_em_cima = true;
            print("Cobra em cima do spawn");
        }
    }

    //Checar se saiu
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Spawn")
        {
            spawn_em_cima = false;
            print("Cobra fora do spawn");
        }
    }

}
