using UnityEngine;
using DG.Tweening;
public class AnimationArrow : MonoBehaviour
{
    private void OnEnable()
    {
        Animation();
    }

    private void Animation()
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f).SetLoops(-1, LoopType.Yoyo);
    }
}
