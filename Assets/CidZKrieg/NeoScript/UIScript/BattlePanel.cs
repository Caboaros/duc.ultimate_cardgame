using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public enum PanelStates
{
    Skill = 0,
    Battle = 1,
}


public class BattlePanel : MonoBehaviour
{
    public Animator myAnimator;

    //Escolha de Skill

    public GameObject skillInfoContainer;

    public TextMeshProUGUI skillTypeTXT;

    public TextMeshProUGUI skillDescriptionTXT;

    public TextMeshProUGUI costTXT;

    public TextMeshProUGUI skillIconTXT;

    public Button rightArrow;

    public Button leftArrow;

    //Batalha

    public GameObject battleInfoContainer;

    public TextMeshProUGUI[] valueTXT;

    public TextMeshProUGUI vsTXT;

    public TextMeshProUGUI battleTypeTXT;

 

    public Button NextButton;

    public Button BackButton;


    public PanelStates PStates;


    public UnityEvent[] BackButtonEvent;

    public UnityEvent[] NextButtonEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdatePanel();
        }
    }

    public void UpdatePanel()
    {
        // Índice

        int i = Battle.Instance.skillIndex;

        // Skill
        if (PStates == PanelStates.Skill)
        {
            

            // Setas

            if (Battle.Instance.tmpSkill[1] == null)
            {
                rightArrow.gameObject.SetActive(false);
                leftArrow.gameObject.SetActive(false);
            }

            else
            {
                if (i == 0)
                {
                    rightArrow.gameObject.SetActive(true);
                    leftArrow.gameObject.SetActive(false);
                }

                else if (i == 1)
                {
                    rightArrow.gameObject.SetActive(false);
                    leftArrow.gameObject.SetActive(true);
                }
            }

            // Texto

            skillTypeTXT.text = Battle.Instance.tmpSkill[i].skill.type.ToString();

            CardVisual.Instance.DefineSoleDescription(Battle.Instance.tmpSkill[i]);

            skillDescriptionTXT.text = CardVisual.Instance.tmpString;

            costTXT.text = Battle.Instance.tmpSkill[i].cost.ToString();

            if (Battle.Instance.tmpSkill[i].side == Side.Day)
            {
                skillIconTXT.text = "1";
            }

            else
            {
                skillIconTXT.text = "2";
            }



        }

        if (PStates == PanelStates.Battle)
        {
            //vsTXT.text = "VS";

            battleTypeTXT.text = Battle.Instance.tmpSkill[i].skill.type.ToString();

            if (Battle.Instance.situation == SkillType.Attack)
            {
                vsTXT.text = "VS";
            }

            else if (Battle.Instance.situation == SkillType.Steal)
            {
                vsTXT.text = "<";
            }

            else if (Battle.Instance.situation == SkillType.Renegerate)
            {
                vsTXT.text = ">";
            }

            if (Battle.Instance.situation == SkillType.Attack || Battle.Instance.situation == SkillType.Steal || Battle.Instance.situation == SkillType.Renegerate)
            {
                valueTXT[0].text = Battle.Instance.tmpSkill[i].value.ToString();

                if (Battle.Instance.target == null)
                {
                    valueTXT[1].text = "0";
                    NextButton.gameObject.SetActive(false);
                }

                else
                {
                    valueTXT[1].text = Battle.Instance.target.GetComponent<ZoneScript>().towerEnergy.ToString();
                    NextButton.gameObject.SetActive(true);
                }

            }

        }

    }

    public void ChangePanelState(int P)
    {
        PStates = (PanelStates)P;
        CheckState();
    }

    public void CheckState()
    {
        if (PStates == PanelStates.Battle)
        {
            battleInfoContainer.SetActive(true);
            skillInfoContainer.SetActive(false);
        }

        else if (PStates == PanelStates.Skill)
        {
            battleInfoContainer.SetActive(false);
            skillInfoContainer.SetActive(true);
        }
    }

    public void BackInvoke()
    {
        BackButtonEvent[(int)PStates].Invoke();
    }

    public void NextInvoke()
    {
        NextButtonEvent[(int)PStates].Invoke();
    }

    public void ShowButtons(bool b)
    {
        BackButton.gameObject.SetActive(b);
        NextButton.gameObject.SetActive(b);
    }
}
