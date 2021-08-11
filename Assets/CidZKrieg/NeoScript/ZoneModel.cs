using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneModel : MonoBehaviour
{
    public Animator modelAnimator;

    public Animator myAnimator;

    public GameObject tmpModel;

    public Transform modelPosition;

    public void SetModel(GameObject model, GuildType guild)//, Animator animator)
    {
        tmpModel = Instantiate(model, modelPosition.position, modelPosition.rotation);

        tmpModel.transform.SetParent(modelPosition);

        tmpModel.GetComponent<ModelScript>().ChangeMaterial(CardVisual.Instance.monkColors[(int)guild]);

        tmpModel.SetActive(false);

        modelAnimator = model.GetComponent<Animator>();

        //modelChildren.gameObject.name = model.gameObject.name;

        StartAnimation();
    }

    public void StartAnimation()
    {
        myAnimator.SetTrigger("Show");
    }

    public void DestroyModel()
    {
        print("Destrua-me");
        myAnimator.SetTrigger("Die");
    }

    public void EnableModel()
    {
        print("ENABLE!!!!!!");
        tmpModel.gameObject.SetActive(true);
    }

    public void ClearModel()
    {
        tmpModel.gameObject.SetActive(false);

        tmpModel = null;
        //myAnimator = null;
    }
}
