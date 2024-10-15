using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{

    public int hitPoints = 0;
    public float lifespan = 500f;

    public GameObject organismPrefab;

    protected float howLongLivedSoFar = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }




    public void SpawnOrganism()
    {
        GameObject newOrganism = Instantiate(organismPrefab);
        newOrganism.transform.position = Vector3.zero; // new Vector3(0,0,0)
    }

    // Update is called once per frame
    protected void Update()
    {
        howLongLivedSoFar += Time.deltaTime;

        ShouldIDie();

    }


    void ShouldIDie()
    {
        if(howLongLivedSoFar > lifespan)
        {
            Destroy(gameObject);
        } 
    }

}
