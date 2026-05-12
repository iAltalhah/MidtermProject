using System.Collections.Generic;
using UnityEngine;

public class MoveRuller : MonoBehaviour
{
    public List<GameObject> rullers = new List<GameObject>();

    private int currentRuller = 0;

    [HideInInspector]
    public int[] numberArray = { 0, 0, 0, 0 };
   
    private PadLockPassword lockPassword;
    public bool canRotate = false;
   
    void Start()
    {
        lockPassword = GetComponent<PadLockPassword>();
    }

    void Update()
    {
        SelectRuller();
        RotateRuller();

        lockPassword.Password();
    }

    void SelectRuller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                for (int i = 0; i < rullers.Count; i++)
                {
                    if (hit.transform.gameObject == rullers[i])
                    {
                        currentRuller = i;

                        Debug.Log("Selected Ruller: " + i);
                    }
                }
            }
        }
    }

    void RotateRuller()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            rullers[currentRuller].transform.Rotate(-36, 0, 0);

            numberArray[currentRuller]++;

            if (numberArray[currentRuller] > 9)
                numberArray[currentRuller] = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            rullers[currentRuller].transform.Rotate(36, 0, 0);

            numberArray[currentRuller]--;

            if (numberArray[currentRuller] < 0)
                numberArray[currentRuller] = 9;
        }
    }
}