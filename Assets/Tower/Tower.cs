using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 500;
    [SerializeField] float buildDelay = 1;

    void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdrawal(cost);
            return true;
        }
        
        return false;   
    }

    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(buildDelay);
        transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(buildDelay);
        transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(buildDelay);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
}
