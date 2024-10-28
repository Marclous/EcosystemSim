using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{

    public int hitPoints = 0;
    public float lifespan = 500f;

    public GameObject organismPrefab;

    protected float howLongLivedSoFar = 0;

    public delegate void DeathEventHandler();
    public event DeathEventHandler OnDeath;

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (OnDeath != null)
        {
            OnDeath.Invoke();
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public virtual void Attack(Organism opponent) {
        
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
        if(hitPoints <= 0) {
            Destroy(gameObject);
        }
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
