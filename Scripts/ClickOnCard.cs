using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class ClickOnCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject ClearImages;
    async public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("EnterOnClickRigth");
            if (eventData.pointerClick.GetComponent<ThisCard>().thisCard is Clear && eventData.pointerClick.transform.parent.name == "Hand")
            {
                LeanTween.move(eventData.pointerClick.gameObject, new Vector3(549.9293f, 300.0388f, 2.52447f), 2f);
                await Task.Delay(2000);
                ClearImages.SetActive(true);
                GetComponentInParent<Canvas>().GetComponent<GameController>().Clear();
                await Task.Delay(400);
                Destroy(eventData.pointerClick);
                ClearImages.SetActive(false);
                if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();

                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearImages = GetComponentInParent<Canvas>().transform.Find("ClearImages").gameObject;
        ClearImages.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
