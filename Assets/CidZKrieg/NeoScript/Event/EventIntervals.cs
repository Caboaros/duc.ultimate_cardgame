using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventIntervals : MonoBehaviour
{
    public static EventIntervals Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    [Header("Script que mantém intervalos entre ações \n assim evitando que hajam transições bruscas \n sempre que houver um evento")]

    //bool animating;

    public bool transition;

    private void Start()
    {
        if (GameManager.Instance.GState == GameStates.StartMatch)
        {
            GameManager.Instance.CheckState();
        }
    }


    public void CheckTransition(bool v)
    {
        transition = v;
    }

    public void TransitionAutomatic()
    {
        if (transition)
        {
            CheckTransition(false);
        }

        else
        {
            CheckTransition(true);
        }
    }

    // Classe de intervalos

    [System.Serializable]
    public class Interval
    {
        public int timeToWait;
        public UnityEvent eventToHappen;
        public AnimationClip clipToWait;

        public IntervalType type;
    }

    [System.Serializable]
    public class IntervalOBJ
    {
        public Interval[] interval;
    }

    Stack<Interval> intervalStack = new Stack<Interval>();

    public void InvokeInterval(IntervalOBJ intervalObj)
    {
        foreach (Interval v in intervalObj.interval)
        {
            intervalStack.Push(v);
        }

        Interval tmpInterval = intervalStack.Pop();

        if (tmpInterval.type == IntervalType.TimedEvent)
        {
            StartCoroutine(ResolveTimedEvent(tmpInterval.timeToWait, tmpInterval.eventToHappen));
        }

        else if (tmpInterval.type == IntervalType.PositionLerp)
        {
            
        }

        // Usar esse ao mesmo tempo que a animação acontece
        else if (tmpInterval.type == IntervalType.AnimationClip)
        {
            StartCoroutine(ResolveClipEvent(tmpInterval.clipToWait, tmpInterval.eventToHappen));
        }
    }

    public IEnumerator ResolveTimedEvent(int delay, UnityEvent eventToInvoke)
    {
        yield return new WaitForSeconds(delay);
        eventToInvoke.Invoke();

    }

    public IEnumerator ResolveClipEvent(AnimationClip clip, UnityEvent eventToInvoke)
    {
        yield return new WaitForSeconds(clip.length + 0.5f);
        eventToInvoke.Invoke();
    }



}

public enum IntervalType
{
    TimedEvent,
    PositionLerp,
    DrawCard,
    //PhaseJump,
    AnimationClip,
}

