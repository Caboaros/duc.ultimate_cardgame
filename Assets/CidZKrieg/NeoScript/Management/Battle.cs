using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    // Singleton

    public static Battle Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public SkillType situation;

    // Objetos que agem (Torres)

    public GameObject actor;
    public GameObject target;

    // Skills

    // Array que coloca em evidência as habilidades disponíveis da torre que está agindo
    public NeoCard.Status[] tmpSkill = new NeoCard.Status[2];

    // Índice que serve para escolher uma das habilidades na Array acima (valor alterado com os botões de seta no painel de batalha)
    public int skillIndex;

    // bool para detectar se está havendo escolha de skills
    public bool skillChoosing;

    public void ChangeState(SkillType s)
    {
        situation = s;
    }


    // definindo ator e alvo

    /*public void SetActor(GameObject card)
    {
        actor = NeoSlot.Instance.zoneList[card.GetComponent<CardScript2>().myIndex];

        //attacker = card;
    }

    public void SetTarget(GameObject card)
    {
        target = NeoSlot.Instance.zoneList[card.GetComponent<CardScript2>().myIndex];
        //target = card;
    }*/







    //Método que põe em evidência as habilidades temporárias da torre que está agindo
    public void SetSkill(GameObject tower)
    {

        ZoneScript tmpScript = tower.GetComponent<ZoneScript>();

        // Checando quais habilidades estão disponíveis
        for (int i = 0; i < 2; i++)
        {
            if (tmpScript.towerSkills[i] != null)
            {
                if (tmpScript.towerSkills[i].available)
                {
                    tmpSkill[i] = tmpScript.towerSkills[i].skill;
                }
            }
        }
        
        // Determinando a segunda habilidade temporária como a primeira, caso a primeira esteja vazia
        if (tmpSkill[0] == null && tmpSkill[1] != null)
        {
            tmpSkill[0] = tmpSkill[1];
            tmpSkill[1] = null;
        }

    }

    public void SetAttacker(GameObject tower , NeoCard.Status status)
    {
        actor = tower;

        tmpSkill[0] = status;
    }

    public void SetCurrentAttacker()
    {
        actor = NeoSlot.Instance.zoneList[TurnActions.Instance.currentSlot];
    }

    public void SetTarget(GameObject tower)
    {
        target = tower;
    }

    public void ClearActors()
    {
        actor = null;
        target = null;
    }

    public void ClearSkillList()
    {
        tmpSkill[0] = null;
        tmpSkill[1] = null;
    }

    public void ResetIndex()
    {
        skillIndex = 0;
    }

    public void ChangeSkillIndex(int i)
    {
        skillIndex = i;
    }

    public void SetSituation()
    {
        situation = tmpSkill[skillIndex].skill.type;
    }

    public void NewAction()
    {
        if (situation == SkillType.Attack)
        {
            ActionStack.Instance.NewAttack(actor, target, tmpSkill[skillIndex].value, tmpSkill[skillIndex].cost);
        }

        else if (situation == SkillType.Steal)
        {
            ActionStack.Instance.NewSteal(actor, target, tmpSkill[skillIndex].value, tmpSkill[skillIndex].cost);
        }

        else if (situation == SkillType.Renegerate)
        {
            ActionStack.Instance.NewRegenerate(actor, target, tmpSkill[skillIndex].value, tmpSkill[skillIndex].cost);
        }
    }
}
