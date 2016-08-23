using UnityEngine;
using System.Collections;

public class spawnManager : MonoBehaviour {

	public Sprite[] enemySprite;

	public GameObject[] primary;  //primary colors prefabs
	public GameObject[] secondary;//secondary colors prefabs
	public GameObject[] tertiary;//tertiary colors prefabs
	public GameObject bomb; //bomb prefabs
	public int spawnIndex; // random index for prefabs

	public float moveSpeed; // colors movement speed
	public float speedIncrease; // increase in movement speed 
	
	public Vector3 spawnPos; 
	public float xSpawn;
	public float xSpawnMin;
	public float xSpawnMax;
	public float ySpawn;

	public float spawnInterval; // intervals before spawning new color
	public float spawnIntervalMin;
	public float spawnIntervalMax;
	public float spawnIntervalMaxMin; //max value that the intervalMin can decrease
	public float spawnIntervalIncrease; // minus max and min interval (increase ndi spawning speed)

	public float bombInterval; // intervals before spawning new bomb
	public float bombIntervalMin;
	public float bombIntervalMax;
	public float bombIntervalMaxMin;//max value that the intervalMin can decrease
	public float bombIntervalIncrease; // minus max and min bomb interval (increase spawning speed)
	
	public int spawnRange; // random number from RangeMin and RangeMax
	public int primaryMax; 
	public int secondaryMax;
	public int tertiaryMax;
	public int spawnRangeMin;
	public int spawnRangeMax;
	
	private GameObject enemy;
	public float intervalSpeed;




	// Use this for initialization
	void Start () {
		spawnInterval = 0; //1st spawn of the game 
		bombInterval = Random.Range (bombIntervalMin, bombIntervalMax); //random bomb interval
	}
	
	// Update is called once per frame
	void Update () {
	
		if (spawnInterval > 0) {
			spawnInterval-=Time.deltaTime*intervalSpeed;
		} else { //if spawninterval is <=0
			spawnRange = Random.Range (spawnRangeMin, spawnRangeMax);
			xSpawn = Random.Range(xSpawnMin,xSpawnMax); //random xPos
			spawnPos = new Vector3(xSpawn,ySpawn,transform.position.z); //vector 3 Pos
			if(spawnRange>=0 && spawnRange<=primaryMax) // Max range of primary color, if spawnRange is < this max, primary color will be spawned
			{
				spawnIndex = Random.Range (0, primary.Length);
				enemy=(GameObject)Instantiate (primary[spawnIndex],spawnPos,primary[spawnIndex].transform.rotation);
				//enemy.GetComponent<SpriteRenderer>().sprite = enemySprite[globalVar.shopUseEnemy];
				//enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= enemySprite[globalVar.shopUseEnemy];
				enemy.GetComponent<enemyMove>().speed = moveSpeed;

			}
			else if(spawnRange>primaryMax && spawnRange<=secondaryMax)// Max range of secondary color, if spawnRange is < this max, secondary color will be spawned

			{
				spawnIndex = Random.Range (0, secondary.Length);
				enemy=(GameObject)Instantiate (secondary[spawnIndex],spawnPos,secondary[spawnIndex].transform.rotation);
				//enemy.GetComponent<SpriteRenderer>().sprite = enemySprite[globalVar.shopUseEnemy];
				//enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= enemySprite[globalVar.shopUseEnemy];
				enemy.GetComponent<enemyMove>().speed = moveSpeed;
			}
			else if(spawnRange>secondaryMax && spawnRange<tertiaryMax) // Max range of Tertiary color, if spawnRange is < this max, tertiary color will be spawned
			{
				spawnIndex = Random.Range (0, tertiary.Length);
				enemy=(GameObject)Instantiate (tertiary[spawnIndex],spawnPos,tertiary[spawnIndex].transform.rotation);
				//enemy.GetComponent<SpriteRenderer>().sprite = enemySprite[globalVar.shopUseEnemy];
				//enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= enemySprite[globalVar.shopUseEnemy];
				enemy.GetComponent<enemyMove>().speed = moveSpeed;
			}
			if(spawnIntervalMin > spawnIntervalMaxMin) //if the Min is > the MinMin
			{
				spawnIntervalMin -= spawnIntervalIncrease;
				spawnIntervalMax -= spawnIntervalIncrease;
			}
			int x = Random.Range (1,3); //random if the spawnRangeMin and Max will increase or not, 1 for not, 2 for increase
			if((x==2) && spawnRangeMax<tertiaryMax)
			{
				spawnRangeMin +=1;
				spawnRangeMax +=1;

			}

			moveSpeed +=speedIncrease;// every spawn movementSpeed increases
			spawnInterval = Random.Range (spawnIntervalMin, spawnIntervalMax);// random interval again
		}

		if (bombInterval > 0) {
			bombInterval -= Time.deltaTime*intervalSpeed;
		} else {
			xSpawn = Random.Range(xSpawnMin,xSpawnMax);
			spawnPos = new Vector3(xSpawn,ySpawn,transform.position.z);
			enemy=(GameObject)Instantiate (bomb,spawnPos,bomb.transform.rotation);
			enemy.GetComponent<enemyMove>().speed = moveSpeed;
			//enemy.GetComponent<SpriteRenderer>().sprite = enemySprite[globalVar.shopUseEnemy];
			if(bombIntervalMin > bombIntervalMaxMin)
			{
				bombIntervalMin -= bombIntervalIncrease;
				bombIntervalMax -= bombIntervalIncrease;
			}
			bombInterval = Random.Range (bombIntervalMin, bombIntervalMax);
		}

	}


}
