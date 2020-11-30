using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMakerScript : MonoBehaviour
{
    // we should make a new enemy every 3 sec or 5 sec etc. (done)
    // each enemy should come up to a random location. (done)
    // each enemy should shoot up wards an then go back down (done)
    // each enemy should have a different image (done)
    // y value should be -32 in the beigninig (done)
    // x should vary between -55 to 55 (done)
    // z should be 7 (done)

    public GameObject enemyGo;
    public Sprite[] myimages;
    int forcePower = 1150;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("makeAnEnemyAction", 0, 2);
    }

    void makeAnEnemyAction()
    {
        GameObject newEnemyGo = (GameObject)Instantiate(enemyGo) as GameObject;
        float x = Random.Range(-55.0f, 55.0f);
        float y = -32;
        float z = 7;
        // creating enemies at random locations.
        newEnemyGo.transform.position = new Vector3(x, y, z);
        // Create random enemies.
        newEnemyGo.GetComponent<SpriteRenderer>().sprite = myimages[Random.Range(0,myimages.Length)];
        // Creating an upward force
        newEnemyGo.GetComponent<Rigidbody2D>().AddForce(Vector3.up*forcePower);
    }
}
