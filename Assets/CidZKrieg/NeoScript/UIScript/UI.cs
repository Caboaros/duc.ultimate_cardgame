using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    // Singleton
    public static UI Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    //

    public TextMeshProUGUI turnCountTXT;
    public TextMeshProUGUI currentPhaseTXT;
    public TextMeshProUGUI currentZoneTXT;

    public TextMeshProUGUI[] energyTXT;

    public Button SummonButton;

    public Button NextButton;

    //public GameObject selectedButton;

    public int[] energyValue;

    //Mouse sob algo
    public bool mouseOverObject;

    public bool mouseOverCard;

    public bool mouseOverTower;

    public UnityEvent UICardEvent;

    // Tempo

    public TextMeshProUGUI timeTXT;

    // Tela de transição

    public TransitionScreen tScreen;



    // Painel de Batalha

    public Animator devvi;

    public BattlePanel bPanel;


    private void Start()
    {
        UpdateText();

        StartCoroutine(UpdateScoreText());

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (SummonButton.enabled)
            {
                if (!mouseOverObject)
                {
                    if (!mouseOverCard && !mouseOverTower)
                    {
                        ShowButton(SummonButton, false);
                    }

                }
            }

            UICardEvent.Invoke();

        }
    }

    public void UpdateText()
    {
        turnCountTXT.text = "Turn " + TurnActions.Instance.turnCount.ToString();
        currentPhaseTXT.text = TurnActions.Instance.TPhase.ToString();
        currentZoneTXT.text = "Zone " + (TurnActions.Instance.currentSlot + 1);
    }

    public void ShowButton(Button button, bool show)
    {
        button.gameObject.SetActive(show);
    }

    public void MouseOverObject(bool over)
    {
        mouseOverObject = over;
    }

    
    public void HideObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void UpdateAnswerTimeTXT(int value)
    {
        if (value >= 0)
        {
            if (!timeTXT.gameObject.activeSelf)
            {
                timeTXT.gameObject.SetActive(true);
            }

            timeTXT.text = "Next: " + value.ToString();

            if (value <= 5)
            {
                timeTXT.gameObject.GetComponent<Animator>().SetTrigger("Red");
            }
        }

        else
        {
            timeTXT.gameObject.SetActive(false);
        }
    }

    public void Transition()
    {
        tScreen.Transition();
    }

    public void UpdateScore()
    {
        StartCoroutine(UpdateScoreText());
    }

    IEnumerator UpdateScoreText()
    {
        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < 2; i++)
        {
            if (energyValue[i] < Energy2.Instance.energy[i])
            {
                energyValue[i]++;
            }

            else if (energyValue[i] > Energy2.Instance.energy[i])
            {
                energyValue[i]--;
            }

            energyTXT[i].text = energyValue[i].ToString();
        }

        if (energyValue[0] != Energy2.Instance.energy[0] || energyValue[1] != Energy2.Instance.energy[1])
        {
            StartCoroutine(UpdateScoreText());
        }
    }


    // Painel de batalha

    public void ShowBattlePanel(bool b)
    {
        bPanel.myAnimator.SetBool("Show", b);

        bPanel.ShowButtons(b);

        Battle.Instance.skillChoosing = b;
    }

    // Atualizando o texto do painel de batalha

    public void UpdateBattlePanel()
    {
        bPanel.UpdatePanel();
        /*if (Battle.Instance.tmpSkill[0] != null)
        {
            bPanel.valueTXT[0].text = Battle.Instance.tmpSkill[0].value.ToString();
        }

        if (Battle.Instance.target != null)
        {
            bPanel.valueTXT[1].text = Battle.Instance.target.GetComponent<ZoneScript>().towerEnergy.ToString();

            bPanel.NextButton.gameObject.SetActive(true);
        }

        else
        {
            bPanel.gameObject.SetActive(false);
        }*/
    }

}
