using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


// ENUMS

public enum Facing { Up, Down, Left, Right }





public class EventManager : Singleton<EventManager>
{
    protected override void Awake() => base.Awake();

    // ----------------------------------
    public UnityEvent<Facing> PlayerTurning = new ();


    public void OnPlayerTurning(Facing newFacing)
        => PlayerTurning.Invoke(newFacing);
    public void OnPlayerTurningSubscribe(UnityAction<Facing> action)
        => PlayerTurning.AddListener(action);
    public void OnPlayerTurningUnsubscribe(UnityAction<Facing> action)
        => PlayerTurning.RemoveListener(action);
    

    // ----------------------------------
    public UnityEvent<bool> GamePause = new ();
    // passa TRUE se il gioco è già in pausa, per riprendere

    public void OnGamePause(bool isPaused)
        => GamePause.Invoke(isPaused);
    public void OnGamePauseSubscribe(UnityAction<bool> action)
        => GamePause.AddListener(action);
    public void OnGamePauseUnsubscribe(UnityAction<bool> action)
        => GamePause.RemoveListener(action);
}