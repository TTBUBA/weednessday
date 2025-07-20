using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrashCompactor : MonoBehaviour
{
    [Header("Trash Compactor")]
    [SerializeField] private SlootManager CurrentData;
    [SerializeField] private SlootManager slootTrash;
    [SerializeField] private float TimeToCompact = 1f;

    private Coroutine coroutineTrash;

    public InventoryManager inventoryManager;

    private void Start()
    {
        coroutineTrash = StartCoroutine(ActiveTrashCompactor());
    }

    public void CompactTrash()
    {
        if (CurrentData.slootData != null && CurrentData.slootData.NameTools == "plastic")
        {
            inventoryManager.AddItem(slootTrash.slootData, slootTrash.CurrentStorage);
            slootTrash.CurrentStorage = 0;
        }
    }

    private IEnumerator ActiveTrashCompactor()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToCompact);

            if (CurrentData.slootData != null && CurrentData.slootData.NameTools == "plastic")
            {
                if (CurrentData.CurrentStorage > 0 && slootTrash.CurrentStorage <= slootTrash.slootData.MaxStorage - 1)
                {
                    CurrentData.CurrentStorage--;
                    slootTrash.CurrentStorage++;
                    slootTrash.UpdateSlot();
                    CurrentData.UpdateSlot();

                    if (CurrentData.CurrentStorage < 1)
                    {
                        CurrentData.StorageFull = false;
                        CurrentData.slootData = null;
                        CurrentData.iconTools.enabled = false;
                    }
                    Debug.Log("Compattazione eseguita");
                }
            }
        }
    }
}
