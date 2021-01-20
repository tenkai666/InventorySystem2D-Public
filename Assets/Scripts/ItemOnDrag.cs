using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;//当前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;

        transform.SetParent(transform.parent.parent);//返回上一级
        transform.position = eventData.position;//鼠标位置

        GetComponent<CanvasGroup>().blocksRaycasts = false;//射线阻挡关闭
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);//输出鼠标当前位置下到第一个碰到到物体名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //判断下面物体名字是：Item Image 那么互换位置
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                //指向位置slot
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                //itemList的物品存储位置改变
                var temp = myBag.itemList[currentItemID];

                //找到鼠标划过物品对应的id
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.
                    gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                //否则直接挂在检测到到Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                //itemList的物品存储位置改变
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.
                    GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];

                //解决自己放在自己位置
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                {
                    myBag.itemList[currentItemID] = null;
                }

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        //其他任何位置都归位物品
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


}
