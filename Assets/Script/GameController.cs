using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	// Use this for initialization
	public GameObject snakePrefab;
	public Snake head;
	public Snake tail;
	public int NESW;
	public Vector2 nextPos;
	public int maxSize;
	public int currentSize;
	public int xbound;
	public int ybound;
	public GameObject foodPrefab;
	public GameObject currentFood;
	public int score;
	public Text scoreText;
	public int numFood;
	public int xFoodPos; 
	public int yFoodPos;

    public GameObject spawn;

	void OnEnable(){
		Snake.hit  += hit;

	}

	void OnDisable(){
		Snake.hit -= hit;
	}

	void Start () {
		InvokeRepeating ("TimerInvoke", 0, .1f);
		FoodFunction ();
	}
	
	// Update is called once per frame
	void Update () {
		ComChangeD ();

	}
		
		


	void TimerInvoke(){
	
		Movement ();
		StartCoroutine (checkVisible ());
		if (currentSize >= maxSize) {
			tailFunction ();

		} else {
		
			currentSize++;
		}
	}

	void Movement() {
	
		GameObject temp;
		nextPos = head.transform.position;
		switch (NESW) {

		case 0:
			nextPos = new Vector2 (nextPos.x, nextPos.y + 1);
			break;
		case 1:
			nextPos = new Vector2 (nextPos.x +1, nextPos.y);
			break;
		case 2:
			nextPos = new Vector2 (nextPos.x, nextPos.y -1);
			break;
		case 3:
			nextPos = new Vector2 (nextPos.x -1, nextPos.y);
			break;
		}
		temp = (GameObject)Instantiate (snakePrefab, nextPos, transform.rotation);
		head.setnext (temp.GetComponent<Snake>());
		head = temp.GetComponent <Snake> ();
		return;
	}

	void ComChangeD()
	{
		if (NESW != 2 && Input.GetKeyDown (KeyCode.W) || NESW != 2 && Input.GetKeyDown(KeyCode.UpArrow)) {
		
			NESW = 0;
		}

		if (NESW != 3 && Input.GetKeyDown (KeyCode.D) || NESW != 3 && Input.GetKeyDown(KeyCode.RightArrow)) {

			NESW = 1;
		}
		if (NESW != 0 && Input.GetKeyDown (KeyCode.S) || NESW != 0 && Input.GetKeyDown(KeyCode.DownArrow)) {

			NESW = 2;
		}
		if (NESW != 1 && Input.GetKeyDown (KeyCode.A) || NESW != 1 && Input.GetKeyDown(KeyCode.LeftArrow)) {

			NESW = 3;
		}
	}
	void tailFunction()
	{
		Snake tempSnake = tail;
		tail = tail.getNext ();
		tempSnake.RemoveTail ();
	}

	void FoodFunction ()
	{
		
		xFoodPos = Random.Range (-xbound, xbound);
		yFoodPos = Random.Range (-ybound, ybound);

        //Muda a posiçao do spawn
        spawn.transform.position = new Vector2(xFoodPos, yFoodPos);


        //Só vai spawnar a comida se a bool spawn_em_cima do script do snake estiver como false
        if (Snake.spawn_em_cima == false)
        {
            currentFood = (GameObject)Instantiate(foodPrefab, spawn.transform.position, transform.rotation);
        }
        
        //Se ela estiver como true, chama de novo a FoodFunction para gerar uma nova posiçao do spawn
        else
        {
            FoodFunction();
        }

			

		StartCoroutine (CheckRender (currentFood));


	}
		
			

			

	IEnumerator CheckRender(GameObject IN)
	{
		yield return new WaitForEndOfFrame ();

		if (IN.GetComponent <Renderer> ().isVisible == false) {
			
			if (IN.tag == "food") {
				Destroy (IN);
				
				FoodFunction ();

			}
		}

	}

	void hit(string WhatWasSent){
		if (WhatWasSent == "Food") {
			
			FoodFunction ();
			maxSize++;
			score = score + 100;
			scoreText.text  = score.ToString(); 
			int temp = PlayerPrefs.GetInt ("HighScore"); 
			if (score > temp) {
				PlayerPrefs.SetInt ("HighScore", score);
			}
				
		}
		if (WhatWasSent == "Snake") {
		
			CancelInvoke ("TimerInvoke");
			Exit ();
		}
	}
	public void Exit(){
		SceneManager.LoadScene (0);
	
	}


	IEnumerator checkVisible()
	{
		yield return new WaitForEndOfFrame ();
		if (!head.GetComponent<Renderer> ().isVisible) {
			Exit ();
		
		}
	}
}

