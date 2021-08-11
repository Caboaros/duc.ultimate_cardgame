using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurnPhase
{
    Energy = 0,
    Draw = 1,
    Action = 2,
    Resolution = 3,
    Next = 4,

}

public enum SubPhase
{
    Null,
    Discard,
}

public class TurnActions : MonoBehaviour
{
    public static TurnActions Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public int turnCount;
    public int currentSlot;

    public TurnPhase TPhase;

    public UnityEvent FirstDrawEvent;
    public UnityEvent EnergyEvent;
    public UnityEvent DrawEvent;
    public UnityEvent ResolutionEvent;
    public UnityEvent NextPhaseEvent;

    public UnityEvent DiscardEvent;

    public UnityEvent AnswerEvent;
    public UnityEvent StackEvent;

    // Evento de transição de câmera
    public UnityEvent CameraTransitate;
    public bool automaticPhase;

    public bool rotatingCamera;

    public UnityEvent automaticCheck;

    // Tempo de resposta padrão
    public int standartWaitTime;

    // Tempo de resposta atual
    public int currentTime = 0;
    bool downCounting;

    // Tempo de partida
    public float matchTime;

    // Animator do texto de descarte
    public Animator discardAnim;

    // Bools

    // Bool que define se pode haver a fase de energia ou não (Serve pros jogadores não coletarem sempre que mudarem de slot)
    public bool energy;

    // Bool que detecta se é o primeiro turno (Serve pro primeiro jogador não comprar carta logo no início da partida)
    public bool firstTurn;

    // Bool que bloqueia o avanço de fases
    public bool blocked;

    // Bool que define que o jogador pode/deve descartar cartas da mão
    public bool discard;

    // Bool que permite com que o jogador adversário faça uma jogada de resposta
    public bool answer;

    // Atacando
    public bool attacking;

    private void Start()
    {
        FirstDrawEvent.Invoke();
        //EnergyEvent.Invoke();

        //CheckAction();
    }

    // Método que avança as fases de ações para cada slot
    public void NextPhase()
    {

        if (!blocked)
        {
            if (GameManager.Instance.GState == GameStates.Match)
            {
                if (!discard)
                {
                    if (firstTurn && TPhase == TurnPhase.Energy)
                    {
                        TPhase = TurnPhase.Action;
                        firstTurn = false;
                    }

                    else
                    {
                        if (TPhase == TurnPhase.Energy)
                        {
                            if (TPhase != TurnPhase.Draw)
                            {
                                TPhase = TurnPhase.Draw;
                            }
                            //TPhase = TurnPhase.Action;
                        }

                        else if (TPhase == TurnPhase.Draw)
                        {
                            if (TPhase != TurnPhase.Action)
                            {
                                TPhase = TurnPhase.Action;
                            }
                            //TPhase = TurnPhase.Action;
                        }

                        else if (TPhase == TurnPhase.Action)
                        {
                            if (ActionStack.Instance.actionList.Count > 0)
                            {
                                if (TPhase != TurnPhase.Resolution)
                                {
                                    TPhase++;
                                }
                                print("IrPraResolução");
                            }

                            else
                            {
                                if (TPhase != TurnPhase.Next)
                                {
                                    TPhase = TurnPhase.Next;
                                }

                                //TPhase = TurnPhase.Next;
                                print("IrProNext");
                            }
                        }

                        else if (TPhase == TurnPhase.Resolution)
                        {
                            if (TPhase != TurnPhase.Next)
                            {
                                TPhase = TurnPhase.Next;
                            }

                            //TPhase = TurnPhase.Next;
                        }

                        else if (TPhase == TurnPhase.Next)
                        {
                            if (energy)
                            {
                                TPhase = TurnPhase.Energy;
                            }

                            else
                            {
                                TPhase = TurnPhase.Action;
                            }
                        }

                    }
                }

                else
                {
                    TPhase = TurnPhase.Next;
                    discard = false;
                }
            }
            

            

            UI.Instance.Transition();
            StartCoroutine(DelayToCheck(1));
        }

        //UI.Instance.Transition();
        //StartCoroutine(DelayToCheck(1));
    }




    // Método que avança o slot a ser jogado (NextEvent)
    public void CheckTurn()
    {
        if (currentSlot < 5)
        {
            if (NeoSlot.Instance.zoneList[currentSlot + 1].GetComponent<ZoneScript>().myOwner != NeoSides.Instance.current)
            {
                // Discard //
                if (!Players.Instance.playerData[(int)NeoSides.Instance.current].hasSummoned)
                {
                    if (!Players.Instance.playerData[(int)NeoSides.Instance.current].hasDiscarded)
                    {
                        if (HandScript.Instance.handLists[(int)NeoSides.Instance.current].cardsOnHand.Count > 0)
                        {
                            blocked = true;
                            discard = true;

                            discardAnim.SetBool("Hide", false);
                            return;
                        }

                    }
                }//==//


                CheckMatchEnd();
                if (GameManager.Instance.GState == GameStates.EndMatch)
                {
                    return;
                }

                NeoSides.Instance.ChangeSide();
                energy = true;

                rotatingCamera = true;
                CameraTransitate.Invoke();

                AudioManager.Instance.SFX.PlayEnvSFX("PassTurn");

            }


            currentSlot++;
        }

        else
        {
            // Discard
            if (!Players.Instance.playerData[(int)NeoSides.Instance.current].hasSummoned)
            {
                if (!Players.Instance.playerData[(int)NeoSides.Instance.current].hasDiscarded)
                {
                    if (HandScript.Instance.handLists[(int)NeoSides.Instance.current].cardsOnHand.Count > 0)
                    {
                        blocked = true;
                        discard = true;
                        discardAnim.SetBool("Hide", false);
                        return;
                    }

                }
            }//


            CheckMatchEnd();
            if (GameManager.Instance.GState == GameStates.EndMatch)
            {
                return;
            }

            Players.Instance.NoSummon();

            currentSlot = 0;

            NeoSlot.Instance.ZoneRotation();


            NeoSides.Instance.ChangeSide();

            energy = true;

            turnCount++;

            CameraTransitate.Invoke();
            rotatingCamera = true;

            //StartCoroutine(RotationTime());

            // Checagem da lista de espera

            WaitingList.Instance.CheckSleepingList();

            //Audio

            AudioManager.Instance.SFX.PlayEnvSFX("PassTurn");

        }

    }

    public void CheckMatchEnd()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Deck2.Instance.decks[i].cardDeck.Count < 1)
            {
                if (HandScript.Instance.handLists[i].cardsOnHand.Count < 1)
                {
                    Players.Instance.playerData[i].ending = true;
                }
            }

            else
            {
                Players.Instance.playerData[i].ending = false;
            }

        }

        if (Players.Instance.playerData[0].ending && Players.Instance.playerData[1].ending)
        {
            GameManager.Instance.ChangeGameStates(GameStates.EndMatch);

            if (Energy2.Instance.energy[0] > Energy2.Instance.energy[1])
            {
                Players.Instance.winner = 0;
            }

            else if (Energy2.Instance.energy[1] > Energy2.Instance.energy[0])
            {
                Players.Instance.winner = 1;
            }

            else if (Energy2.Instance.energy[0] == Energy2.Instance.energy[1])
            {
                Players.Instance.draw = true;
            }
        }
        
    }

    public void PointContabilize()
    {
        foreach (GameObject zone in NeoSlot.Instance.zoneList)
        {
            if (zone.GetComponent<ZoneScript>().myOwner == NeoSides.Instance.current)
            {
                Energy2.Instance.CheckSlot(zone);
            }
        }
    }

    public void ChangePhase(TurnPhase phase)
    {
        TPhase = phase;
        CheckAction();
    }

    // Delay para fazer checagem de ação a ser invocada na fase
    public IEnumerator DelayToCheck(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        CheckAction();
    }

    // Delay para avançar de fase, usado em fases automáticas
    public IEnumerator DelayToNext(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (GameManager.Instance.GState != GameStates.EndMatch)
        {
            NextPhase();
            UI.Instance.UpdateText();
        }
    }

    public IEnumerator DiscardDelay()
    {
        yield return new WaitForSeconds(0.5f);
        discardAnim.SetBool("Hide", true);
        StartCoroutine(DelayToNext(1.5f));
        
    }

    public void StartDiscardDelay()
    {
        StartCoroutine(DiscardDelay());
    }

    /*public IEnumerator RotationTime()
    {
        rotatingCamera = true;

        automaticCheck.Invoke();
        yield return new WaitForSeconds(2);

        rotatingCamera = false;
        automaticCheck.Invoke();

        StartCoroutine(DelayToNext(1));
    }*/

    public void CheckAction()
    {
        if (TPhase == TurnPhase.Energy)
        {
            automaticPhase = true;
            automaticCheck.Invoke();
            StartCoroutine(DelayToNext(1.5f));


            print("Energia");
            EnergyEvent.Invoke();
            energy = false;
        }

        if (TPhase == TurnPhase.Draw)
        {
            print("DrawAUTOOOO");
            automaticPhase = true;
            automaticCheck.Invoke();
            StartCoroutine(DelayToNext(1.5f));

            print("Compra");
            DrawEvent.Invoke();
        }

        if (TPhase == TurnPhase.Action)
        {
            automaticPhase = false;
            automaticCheck.Invoke();
        }

        if (TPhase == TurnPhase.Resolution)
        {
            automaticPhase = true;
            automaticCheck.Invoke();

            ResolutionEvent.Invoke();
        }

        if (TPhase == TurnPhase.Next)
        {
            automaticPhase = true;
            automaticCheck.Invoke();

            print("Next");
            NextPhaseEvent.Invoke();

            if (rotatingCamera)
            {
                StartCoroutine(DelayToNext(2.5f));

                rotatingCamera = false;

            }

            else
            {
                StartCoroutine(DelayToNext(1f));
            }

        }


    }


    // Bool de ataque

    public void SetAttacking(bool b)
    {
        attacking = b;
    }


    // TEMPO DE RESPOSTA

    public void StartCountTime()
    {
        currentTime += standartWaitTime;

        if (currentTime >= standartWaitTime * 2)
        {
            currentTime = standartWaitTime * 2;
        }

        if (!downCounting)
        {
            StartCoroutine(CounterTime());
        }

    }

    public IEnumerator CounterTime()
    {
        downCounting = true;
        blocked = true;
        answer = true;

        UI.Instance.UpdateAnswerTimeTXT(currentTime);
        yield return new WaitForSeconds(1f);
        currentTime--;

        if (currentTime >= 0)
        {
            StartCoroutine(CounterTime());          
        }

        else
        {
            UI.Instance.UpdateAnswerTimeTXT(currentTime);
            downCounting = false;
            blocked = false;
            answer = false;

            currentTime = 0;

            NextPhase();
            UI.Instance.UpdateText();

        }
    }

    // PILHA DE AÇÃO

    // Compra pelo Deck
    public void ActionDrawFromDeck()
    {
        ActionStack.Instance.NewDrawFromDeck(NeoSides.Instance.current);
    }

    // Compra por outra fonte

    public void ActionDraw()
    {
        ActionStack.Instance.NewDraw(GameManager.Instance.selectedCard, NeoSides.Instance.current);
    }

    public void ActionSummon()
    {
        ActionStack.Instance.NewSummon(GameManager.Instance.selectedCard, currentSlot, NeoSides.Instance.current);
    }

    public void ActionAttack()
    {

    }















    // Invocando Ação da pilha

    public void InvokeStack()
    {
        if (ActionStack.Instance.actionStack.Count > 0)
        {
            ActionStack.Action tmpAction = ActionStack.Instance.actionStack.Pop();

            if (tmpAction.type == ActionType.Summon)
            {

                CardToZone(tmpAction.actor, tmpAction.player, tmpAction.zone);
                UI.Instance.UpdateScore();

                // Colocando a torre em espera para que suas habilidades não possam ser ativadas no turno em que a carta foi invocada
                WaitingList.Instance.AddToActivationSleepList(NeoSlot.Instance.zoneList[tmpAction.zone].gameObject, SleepType.Activation, 1);

                NeoSlot.Instance.zoneList[tmpAction.zone].GetComponent<ZoneScript>().sleeping = true;

            }

            else if (tmpAction.type == ActionType.Draw)
            {
            }

            else if (tmpAction.type == ActionType.Attack)
            {
                print("Ataque");
                ResolveBattle(tmpAction.actor, tmpAction.target, tmpAction.amount, tmpAction.cost);
            }

            else if (tmpAction.type == ActionType.Steal)
            {
                print("Roubo");
                ResolveSteal(tmpAction.actor, tmpAction.target, tmpAction.amount, tmpAction.cost);

            }

            else if (tmpAction.type == ActionType.Regenerate)
            {
                print("Regeneração");
                ResolveRegeneration(tmpAction.actor, tmpAction.target, tmpAction.amount, tmpAction.cost);
            }

            ActionStack.Instance.actionList.Remove(tmpAction);

            InvokeStack();
        }

        else
        {
            ActionStack.Instance.ClearList();
            attacking = false;

            StartCoroutine(DelayToNext(1.5f));
        }
        
    }


    // Métodos

    // Compra de carta
    public void DrawCard()
    {
        if (Deck2.Instance.decks[(int)NeoSides.Instance.current].cardDeck.Count > 0)
        {
            Deck2.Instance.DrawFromDeck((int)NeoSides.Instance.current);
            HandScript.Instance.AddCardToHand(GameManager.Instance.selectedCard, (int)NeoSides.Instance.current);

            // Audio

            AudioManager.Instance.SFX.PlayCardSFX("Draw");
        }

    }

    // Invocação a partir da carta selecionada
    public void CardToZone(GameObject card, Side playSide, int zoneInt)
    {

        // OLD
        //CardScript2 cardScript = GameManager.Instance.selectedCard.GetComponent<CardScript2>();

        CardScript2 cardScript = card.GetComponent<CardScript2>();
        print("1");

        // OLD
        /*if (cardScript.mana > Energy2.Instance.energy[(int)cardScript.myOwner])
        {
            // Não invoca por falta de energia
        }*/

        // Invoca se houver energia
        if (cardScript.mana <= Energy2.Instance.energy[(int) playSide])
        {
            print("2");

            // Remove a carta da mão (se ela estiver)
            if (cardScript.myState == CardState.Hand)
            {
                HandScript.Instance.RemoveFromHand((int)cardScript.myOwner, cardScript.gameObject);
            }

            // OLD
            //cardScript.myIndex = Instance.currentSlot;

            // Muda índice da carta para índice da zona
            cardScript.myIndex = zoneInt;

            // Muda estado da carta para campo
            cardScript.ChangeCardState(CardState.Field);

            // Função para que a habilidade da carta não seja ativada no turno em que é invocada
            //cardScript.ChangeCardCondition(PlayingState.Awaiting);
            //cardScript.setTurn = turnCount;
            //cardScript.awaitingTurns = 1;

            // Colocando a carta na zona
            NeoSlot.Instance.zoneList[currentSlot].GetComponent<ZoneScript>().cardOnZone = cardScript.gameObject;

            NeoSlot.Instance.zoneList[currentSlot].GetComponent<ZoneScript>().state = ZoneState2.Filled;

            NeoSlot.Instance.zoneList[currentSlot].GetComponent<ZoneScript>().SetEnergy();

            NeoSlot.Instance.zoneList[currentSlot].GetComponent<ZoneScript>().AbsorbSkills();

            // Subtraindo energia do custo
            Energy2.Instance.SubtractEnergy((int)cardScript.myOwner, cardScript.mana);

            // Diz que player já sumonou (para que não precise descartar depois)
            Players.Instance.playerData[(int)cardScript.myOwner].hasSummoned = true;

            // Modelo

            if (card.GetComponent<CardScript2>().cardModel != null)
            {
                ZoneScript tmpZone = NeoSlot.Instance.zoneList[currentSlot].GetComponent<ZoneScript>();

                tmpZone.zModel.SetModel(card.GetComponent<CardScript2>().cardModel.modelPrefab, card.GetComponent<CardScript2>().cardInfo.guild);//, card.GetComponent<CardScript2>().cardModel.modelAnimator);
            }

        }


    }

    // Método que remove a carta da lista anterior sempre que ela mudar de estado

    public void RemoveCardFromList(GameObject card)
    {
        CardScript2 cardScript = card.GetComponent<CardScript2>();

        if (cardScript.myState == CardState.Deck)
        {
            Deck2.Instance.RemoveFromDeck(cardScript.gameObject);
        }

        else if (cardScript.myState == CardState.Hand)
        {
            HandScript.Instance.RemoveFromHand((int)cardScript.myOwner , cardScript.gameObject);
        }

        else if (cardScript.myState == CardState.Field)
        {
            NeoSlot.Instance.ChangeZoneState(ZoneState2.Empty, cardScript.myIndex);
        }
    }

    // Block

    public void Block(bool c)
    {
        blocked = c;
    }

    // Invocando tempo de resposta

    public void InvokeAnswer()
    {
        AnswerEvent.Invoke();
    }

    /*public void SetAttack()
    {
       // Battle.Instance.SetAttacker(GameManager.Instance.selectedCard);

        attacking = true;

        //CardScript2 cardScript = NeoSlot.Instance.selectedTower.GetComponent<CardScript2>();

        //ZoneScript zoneScript = NeoSlot.Instance.zoneList[cardScript.myIndex].GetComponent<ZoneScript>();

        ZoneScript zoneScript = NeoSlot.Instance.selectedTower.GetComponent<ZoneScript>();

        // Temporário, lixo
        for (int i = 0; i < 2; i++)
        {
            if (zoneScript.towerSkills[i].skill.skill.type == SkillType.Attack)
            {
                Battle.Instance.SetAttacker(zoneScript.gameObject, zoneScript.towerSkills[i].skill);
            }
        }

    }*/

    public void SetTarget()
    {
        Battle.Instance.SetTarget(GameManager.Instance.selectedCard);

        attacking = false;

    }

    public void SetBattle()
    {
        //ActionStack.Instance.NewAttack(Battle.Instance.actor, Battle.Instance.target, Battle.Instance.tmpSkill[0].value);
     //   int attackValue = Battle.Instance.attacker.GetComponent<CardScript2>().tmpStatus.cost;
     //   ActionStack.Instance.NewAttack(Battle.Instance.attacker, Battle.Instance.target, attackValue);
    }

    public void ResolveBattle(GameObject attackingZone, GameObject targetZone, int value, int cost)
    {

        Energy2.Instance.SubtractEnergy((int)attackingZone.GetComponent<ZoneScript>().myOwner, cost);
        UI.Instance.UpdateScore();

        targetZone.GetComponent<ZoneScript>().ChangeEnergy(-value);

        // Reduzindo a energia
        //targetZone.GetComponent<ZoneScript>().towerEnergy -= value;

        // Perfurando (pra caso o valor de ataque seja maior que a energia da torre)
        /*if (targetZone.GetComponent<ZoneScript>().towerEnergy < value)
        {
            int pierceAmount = value - targetZone.GetComponent<ZoneScript>().towerEnergy;

            Energy2.Instance.SubtractEnergy((int)targetZone.GetComponent<ZoneScript>().myOwner, pierceAmount);
            UI.Instance.UpdateScore();

            targetZone.GetComponent<ZoneScript>().towerEnergy = 0;
        }*/

        // Atualizando o texto de energia
        //attackingZone.GetComponent<ZoneScript>().EnergyText();
        //targetZone.GetComponent<ZoneScript>().EnergyText();

        //Energy2.Instance.SubtractEnergy((int)attackingZone.GetComponent<ZoneScript>().myOwner, 1);
        //UI.Instance.UpdateScore();

        /*if (targetZone.GetComponent<ZoneScript>().towerEnergy < 1)
        {
            GameObject card = targetZone.GetComponent<ZoneScript>().cardOnZone;
            Cemetery.Instance.AddToCemetery(card, (int)card.GetComponent<CardScript2>().myOwner);

            targetZone.GetComponent<ZoneScript>().zModel.DestroyModel();
        }*/
    }

    public void ResolveRegeneration(GameObject curingZone, GameObject targetZone, int value, int cost)
    {

        Energy2.Instance.SubtractEnergy((int)curingZone.GetComponent<ZoneScript>().myOwner, cost);
        UI.Instance.UpdateScore();

        targetZone.GetComponent<ZoneScript>().ChangeEnergy(value);

    }

    public void ResolveSteal(GameObject stealingZone, GameObject targetZone, int value, int cost)
    {
        // apenas roubando energia caso valor de roubo seja menor que a energia da torre alvo
        if (targetZone.GetComponent<ZoneScript>().towerEnergy > value)
        {
            //stealingZone.GetComponent<ZoneScript>().towerEnergy =+ value;

            //stealingZone.GetComponent<ZoneScript>().EnergyText();
            //targetZone.GetComponent<ZoneScript>().EnergyText();


            WaitingList.Instance.AddToStealSleepList(stealingZone, SleepType.StealBuff, 2, value);

            stealingZone.GetComponent<ZoneScript>().ChangeEnergy(value);




        }

        else
        {
            stealingZone.GetComponent<ZoneScript>().ChangeEnergy(targetZone.GetComponent<ZoneScript>().towerEnergy);

            WaitingList.Instance.AddToStealSleepList(stealingZone, SleepType.StealBuff, 2, targetZone.GetComponent<ZoneScript>().towerEnergy);
        }

        Energy2.Instance.SubtractEnergy((int)stealingZone.GetComponent<ZoneScript>().myOwner, cost);
        UI.Instance.UpdateScore();
    }

}
