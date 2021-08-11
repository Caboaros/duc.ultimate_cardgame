using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStack : MonoBehaviour
{
    public static ActionStack Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [System.Serializable]
    public class Action
    {
        public ActionType type;
        public GameObject actor, target;

        // Jogador
        public Side player;

        // Zona alvo ao invocar
        public int zone;

        //
        public int amount;

        //
        public int cost;

        // Compra
        public bool fromDeck;

        // Skill
        public NeoCard.Status skill;
    }

    public List<Action> actionList = new List<Action>();

    public Stack<Action> actionStack = new Stack<Action>();

    // Nova ação

    // Compra através do Deck
    public void NewDrawFromDeck(Side playSide)
    {
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Draw;

        tmpAction.player = playSide;

        tmpAction.fromDeck = true;

        actionList.Add(tmpAction);
    }

    // Compra através de outra fonte
    public void NewDraw(GameObject card, Side playSide)
    {
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Draw;

        tmpAction.actor = card;
        tmpAction.player = playSide;

        actionList.Add(tmpAction);
    }

    public void NewSummon(GameObject card, int zoneInt, Side playSide)
    {
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Summon;

        tmpAction.actor = card;

        tmpAction.zone = zoneInt;

        tmpAction.player = playSide;

        actionList.Add(tmpAction);
    }

    public void NewAttack(GameObject actor, GameObject target, int amount, int cost) //, Side attackSide, Side targetSide, int targetZone, int Amount)
    {
        print("Novo Ataque");
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Attack;

        tmpAction.actor = actor;

        tmpAction.target = target;

        tmpAction.amount = amount;

        tmpAction.cost = cost;

        actionList.Add(tmpAction);
    }

    public void NewSteal(GameObject actor, GameObject target, int amount, int cost)
    {
        print("Novo Roubo");
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Steal;

        tmpAction.actor = actor;

        tmpAction.target = target;

        tmpAction.amount = amount;

        tmpAction.cost = cost;

        actionList.Add(tmpAction);
    }

    public void NewRegenerate(GameObject actor, GameObject target, int amount, int cost)
    {
        print("Nova Regeneração");
        Action tmpAction = new Action();

        tmpAction.type = ActionType.Regenerate;

        tmpAction.actor = actor;

        tmpAction.target = target;

        tmpAction.amount = amount;

        tmpAction.cost = cost;

        actionList.Add(tmpAction);
    }

    // Executar a ação
    public void DrawAction()
    {

    }

    public void StackAll()
    {
        if (actionList != null)
        {
            foreach (Action act in actionList)
            {
                actionStack.Push(act);
            }
        }

    }

    public void ClearList()
    {
        actionList.Clear();
    }
}

public enum ActionType
{
    Draw,
    MakeEnergy,
    Summon,

    Attack,
    Steal,
    Regenerate,

}

