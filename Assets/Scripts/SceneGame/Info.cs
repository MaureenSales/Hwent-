using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Info : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CardInfo = null; //objeto donde se va ha ubicar la carta ampliada
    public Vector3 originalScale;
    public void OnPointerEnter(PointerEventData eventData)
    {
        originalScale = this.transform.localScale;
        this.transform.localScale = new Vector3(originalScale.x + 0.1f, originalScale.y + 0.1f, 0f);
        if (GameObject.Find("Info") != null)
        {
            CardInfo = Instantiate(this.gameObject, GameObject.Find("Info").transform);
            CardInfo.transform.position = GameObject.Find("Info").transform.position;
            CardInfo.transform.localScale = new Vector3(0.4f, 0.9f, 0.4f);
        }


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = originalScale;
        if(CardInfo != null) Destroy(CardInfo);
    }
    
}
