using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;



/// <summary> Rendi questa classe automaticamente un singleton </summary>
/// <typeparam name="T"> Passagli il nome della classe come valore generico </typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get {
            // se non è assegnata un'istanza di T...
            if (_instance == null)
            {   // ...cercala
                _instance = FindFirstObjectByType<T>();
                // se ancora non esiste...
                if (_instance == null)
                {
                    // ...crea l'oggetto con il nome della classe
                    GameObject obj = new (typeof(T).Name);
                    // aggiungi il componente T all'oggetto
                    _instance = obj.AddComponent<T>();
                }
            }
            // restituisci l'istanza di T
            return _instance;
        }
    }

    private void OnDestroy () { _instance = null; }

    /// <summary> Assicurati di usare "base.Awake();" per assegnare l'istanza statica </summary>
    protected virtual void Awake()
    {
        // se non esiste un'istanza di T assegna questa
        if (_instance == null)
            _instance = this as T;
        // altrimenti distruggi questa istanza
        else Destroy(this);
    }
}
