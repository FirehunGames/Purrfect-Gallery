using UnityEngine;
using System.Collections;

public class BasicControls : MonoBehaviour
{
    public GameObject controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(hideControls());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator hideControls()
    {



        yield return new WaitForSeconds(7f);

        controls.SetActive(false);

    }
}
