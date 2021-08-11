using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneScript : MonoBehaviour
{
    public Side currentSide;
    public Side myOwner;
    public ZoneState2 state;

    public GameObject cardOnZone;

    public GameObject[] adjacents;

    public int currentGain;
    

    public GameObject symbol;

    // Objeto para fixar a posição da carta enquanto estiver na zona

    public GameObject cardPosition;

    // Animação

    public TextMeshPro slotTXT;


    public Animator slotTXTanim;

    public TextMeshPro slotEnergyTXT;
    public int towerEnergy;
    int tmpEnergy;
    bool tmpBool;

    // Modelo

    public ZoneModel zModel;

    // Dormindo

    public bool sleeping;

    public bool blocked;

    //efeitos tirados das cartas

    [System.Serializable]
    public class TowerSkill
    {
        public bool available;
        public NeoCard.Status skill;

        // String que pega o nome do tipo de skill
        public string typeString;
    }

    public TowerSkill[] towerSkills;


    // Função temporária para quando acontecer clique na torre
    //ButtonFunction tmpFunction;

    private void OnMouseDown()
    {
        // Ação da torre()

        if (GameManager.Instance.GState == GameStates.Match)
        {
            if (!TurnActions.Instance.attacking)
            {
                // Detectando se tem carta na torre
                if (cardOnZone != null)
                {
                    // Detectando se o turno atual é o do jogador do dono dessa torre
                    if (NeoSides.Instance.current == myOwner)
                    {

                        // Detectando se é a fase de ação
                        if (TurnActions.Instance.TPhase == TurnPhase.Action)
                        {

                            if (NeoSlot.Instance.zoneList[TurnActions.Instance.currentSlot].gameObject == this.gameObject)
                            {

                                for (int i = 0; i < 2; i++)
                                {
                                    // Detectando se o período da skill é o correspondente do período da torre (dia ou noite)
                                    print("2-VeioNoFor");
                                    if (towerSkills[i].skill.side == currentSide)
                                    {

                                        // Detectando se o custo da habilidade é o menor que a energia atual, pra saber se há energia suficiente pra usá-la
                                        print("3-LadoCerto");

                                        if (!sleeping || !blocked)
                                        {

                                            if (towerSkills[i].skill.cost <= Energy2.Instance.energy[(int)myOwner])
                                            {
                                                towerSkills[i].available = true;

                                                // Colocando as habilidades da torre em evidência

                                                Battle.Instance.ClearSkillList();

                                                Battle.Instance.SetSkill(this.gameObject);

                                            }

                                            UIButton.Instance.ChangeFunction(ButtonFunction.Tower);


                                        }

                                    }

                                    else
                                    {
                                        towerSkills[i].available = false;
                                    }
                                }

                            }


                        }
                    }


                }
            }


            //Alvo


            if (TurnActions.Instance.attacking)
            {
                if (myOwner != Battle.Instance.actor.GetComponent<ZoneScript>().myOwner)
                {
                    if (cardOnZone != null)
                    {
                        if (cardOnZone.GetComponent<CardScript2>().cardInfo.cardType == CardTypes.Criatura)
                        {
                            Battle.Instance.target = this.gameObject;

                            UI.Instance.UpdateBattlePanel();

                            print("Atacável");
                        }
                    }
                }

                else if (Battle.Instance.tmpSkill[Battle.Instance.skillIndex].skill.type == SkillType.Renegerate)
                {
                    if (cardOnZone != null)
                    {
                        if (cardOnZone.GetComponent<CardScript2>().cardInfo.cardType == CardTypes.Criatura)
                        {
                            Battle.Instance.target = this.gameObject;

                            UI.Instance.UpdateBattlePanel();

                            print("Curável");
                        }
                    }
                }
            }

            NeoSlot.Instance.selectedTower = this.gameObject;
        }

        

    }

    private void OnMouseEnter()
    {
        UI.Instance.mouseOverTower = true;
    }

    private void OnMouseExit()
    {
        UI.Instance.mouseOverTower = false;
    }

    private void Update()
    {
        if (state == ZoneState2.Filled && cardOnZone != null)
        {
            if (cardOnZone.transform.position != cardPosition.transform.position)
            {
                cardOnZone.transform.position = Vector3.Lerp(cardOnZone.transform.position, cardPosition.transform.position, 5 * Time.deltaTime);
            }
        }
    }

    public void ShowText(bool plus, int amount)
    {
        slotTXT.gameObject.SetActive(true);

        if (plus)
        {
            slotTXT.text = "+" + amount;
        }

        else 
        {
            if (amount > 0)
            {

                slotTXT.text = "-" + amount;
            }                

              
        }

        slotTXTanim.SetTrigger("Up");
    }

    public void SetEnergy()
    {
        towerEnergy = cardOnZone.GetComponent<CardScript2>().mana;

        EnergyText();

    }


    public void EnergyText()
    {
        if (cardOnZone != null)
        {
            if (!slotEnergyTXT.gameObject.activeSelf)
            {
                slotEnergyTXT.gameObject.SetActive(true);
            }

            if (towerEnergy < 0)
            {
                towerEnergy = 0;
            }

            slotEnergyTXT.text = towerEnergy.ToString();
        }

        else
        {
            slotEnergyTXT.gameObject.SetActive(false);
        }
    }

    public void SetCardOnZone(GameObject card)
    {
        cardOnZone = card;

        towerEnergy = card.GetComponent<CardScript2>().mana;

    }

    public void Clear()
    {
        towerEnergy = 0;
        towerSkills = null;
    }

    public void AbsorbSkills()
    {
        for (int i = 0; i < 2; i++)
        {
            if (cardOnZone.GetComponent<CardScript2>().status[i] != null)
            {
                towerSkills[i].skill = cardOnZone.GetComponent<CardScript2>().status[i];
                print(towerSkills[i]);
            }
        }
    }

    public void ChangeEnergy(int energy)
    {
        tmpEnergy = towerEnergy;

        towerEnergy += energy;

        if (energy >= tmpEnergy)
        {
            tmpBool = true;
        }

        else if(energy < tmpEnergy)
        {
            tmpBool = false;
        }

        ShowText(tmpBool, energy);
        StartCoroutine(UpdateEnergyTXT());

    }

    public void CheckLife()
    {
        if (towerEnergy < 1)
        {
            Cemetery.Instance.AddToCemetery(cardOnZone, (int)cardOnZone.GetComponent<CardScript2>().myOwner);
            zModel.DestroyModel();
        }
    }

    public IEnumerator UpdateEnergyTXT()
    {
        print("Contagem");
        yield return new WaitForSeconds(0.05f);

        if (tmpEnergy > 0)
        {
            if (tmpEnergy > towerEnergy)
            {
                tmpEnergy--;
            }

            else if (tmpEnergy < towerEnergy)
            {
                tmpEnergy++;
            }

            slotEnergyTXT.text = tmpEnergy.ToString();

            CheckLife();
        }

        else
        {

            if (tmpEnergy != towerEnergy)
            {
                tmpEnergy--;

                Energy2.Instance.SubtractEnergy((int)myOwner, 1);
                UI.Instance.UpdateScore();

            }
        }

        if (tmpEnergy != towerEnergy)
        {
            StartCoroutine(UpdateEnergyTXT());
        }

        else
        {
            if (towerEnergy < 0)
            {
                towerEnergy = 0;
                tmpEnergy = 0;
            }
        }

    }

}

public enum ZoneState2
{
    Empty,
    Filled,
}


