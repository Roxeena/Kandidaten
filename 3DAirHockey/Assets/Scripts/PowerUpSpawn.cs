using UnityEngine;
/* Author: Malin Ejdbo
 * Last change date: 2018-04-17
 * Day of correction: 
 * Checked by: 
 * Date of check: 
 * Comment: 
*/

//Script that spawns random prefabs at a random place at random times, there is a cooldown tho. 
//There is an array with a number of predefined positions where the power ups can spawn.
public class PowerUpSpawn : MonoBehaviour {

    public Transform[] teleport;                            //Array with different predefined positions where to spawn power ups
    public GameObject[] prefeb;                             //Array with the different prefabs, different power up objects.
    public float maxCooldown = 20.0f, minCooldown = 5.0f;   //Maximum and minimum time between two spawns
    private bool cooldown = true, randomTimeSet = false;    //Tells if cooldown is activa or not, and if there is a spawn time decided yet
    private float cooldownTime;                             //The time left of cool down
    private int num_teleports, num_prefabs;                 //number of spawn spots and number of different power ups
    private double precision = 0.01;                        //Precision when comparing floats

    // Use this for initialization
    void Start () {
        //Check how many prefabs and spawn places there are
        num_teleports = teleport.Length;
        num_prefabs = prefeb.Length;
	}
	
	// Update is called once per frame
	void Update () {
        //If cooldown is active
        if(cooldown)
        {
            //If the cool down has no time, set a new time to it
            if (!randomTimeSet)
            {
                cooldownTime = Random.Range(minCooldown, maxCooldown);
                randomTimeSet = true;   //Now it is set, to avoid new cooldown times every frame
            }

            //Count down the cooldown time
            cooldownTime -= Time.deltaTime;
            //Debug.Log("Cooldown:" + cooldownTime);

            //If the cooldown is over, turn it off
            if (cooldownTime < precision)
                cooldown = false;
        }
        //else the cooldown is not active
        else
        {
            spawn();                //Spawn
            cooldown = true;        //Activate cooldown
            randomTimeSet = false;  //Tell program to calculate a new time for cooldown time
        }
    }

    public void spawn()
    { 
        //This will spawn only one prefeb
        int position_num = Random.Range(0, num_teleports);  //Random spawn place
        int prefeb_num = Random.Range(0, num_prefabs);      //Random kind of power up

        //Create a new power up object in the scene
        Instantiate(prefeb[prefeb_num], teleport[position_num].position, teleport[position_num].rotation);  
    }
}