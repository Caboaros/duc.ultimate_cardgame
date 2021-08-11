using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrid : MonoBehaviour
{
    public GameObject empty;

    public List<GameObject> slotList = new List<GameObject>();

    public int iTest = 0;

    public void NewSlot()
    {
        GameObject tmpSlot = Instantiate(empty, transform.position, transform.rotation);
        tmpSlot.transform.SetParent(transform);
        tmpSlot.transform.localScale = new Vector3(1,1,1);
        slotList.Add(tmpSlot);
        iTest++;
        tmpSlot.gameObject.name = "Empty" + iTest.ToString();
    }

    public void RemoveSlot(int i)
    {
        Destroy(slotList[i]);
        slotList.Remove(slotList[i]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NewSlot();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Destroy(slotList[0]);
            slotList.Remove(slotList[0]);
        }
    }
}
