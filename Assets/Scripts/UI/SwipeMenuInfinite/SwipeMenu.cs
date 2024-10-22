using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

public class SwipeMenu : MonoBehaviour
{
    #region DynamicScroll

    public Scrollbar scrollbar;
    private float _scrollPos = 0;
    private float[] _pos;

    #endregion

    #region InfiniteScroll

    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public RectTransform contentPanelTransform;
    public HorizontalLayoutGroup hlg;
    public RectTransform[] itemList;
    private UnityEngine.Vector2 oldVelocity;
    private bool isUpdated;
    bool checkPos;
    #endregion

    private void Start()
    {
        StartCoroutine(SetupScroll());
    }

    void InitScroll()
    {
        isUpdated = false;
        checkPos = true;
        oldVelocity = UnityEngine.Vector2.zero;
        
        int itemToAdd = Mathf.CeilToInt(1920 / (itemList[0].rect.width + hlg.spacing));
        Debug.Log(itemToAdd);

        for (int i = 0; i < itemToAdd; i++)
        {
            RectTransform rt = Instantiate(itemList[i % itemList.Length], contentPanelTransform);
            rt.SetAsLastSibling();
        }

        for (int i = 0; i < itemToAdd; i++)
        {
            int num = itemList.Length - 1 - i;
            while (num < 0 )
            {
                num += itemList.Length;
            }
            RectTransform rt = Instantiate(itemList[num], contentPanelTransform);
            rt.SetAsFirstSibling();
        }
        contentPanelTransform.localPosition = new Vector3((0-(itemList[0].rect.width + hlg.spacing) * itemToAdd), 0,0);

    }

    IEnumerator SetupScroll()
    {
        SwipeMenuPetItemManager.Instance.SetDataPetItem();
        InitScroll();
        yield return null;
    }
    
    int index;

    private void Update()
    {
        //infinite scroll
        if (isUpdated)
        {
            isUpdated = false;
            scrollRect.velocity = oldVelocity;
        }
        if (contentPanelTransform.localPosition.x > 0 - (2 * (itemList[0].rect.width + hlg.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition -=
                new Vector3(itemList.Length * (itemList[0].rect.width + hlg.spacing), 0, 0);
            isUpdated = true;
        }
        if (contentPanelTransform.localPosition.x < 0 - (itemList.Length * 2 * (itemList[0].rect.width + hlg.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            oldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition +=
                new Vector3(itemList.Length * (itemList[0].rect.width + hlg.spacing), 0, 0);
            isUpdated = true;
        }
        
        //Zoom swipe menu
        _pos = new float[transform.childCount];
        float distance = 1f / (_pos.Length-1f);
        for (int i = 0; i < _pos.Length; i++)
        {
            _pos[i] = distance * i;
        }

        if (checkPos)
        {
            _scrollPos = scrollbar.value;
            checkPos = false;
        }
        if (Input.GetMouseButton(0))
        {
            _scrollPos = scrollbar.value;
        }
        else
        {
            for (int i = 0; i < _pos.Length; i++)
            {
                if (_scrollPos < _pos[i] + distance/2 && _scrollPos > _pos[i] - distance/2)
                {
                    index = i;
                    scrollbar.value = Mathf.Lerp(scrollbar.value, _pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < _pos.Length; i++)
        {
            if (index == i)
            {
                transform.GetChild(i).localScale =
                    Vector3.Lerp(transform.GetChild(i).localScale, new Vector3(1.1f, 1.1f,1.1f), 0.1f);
            }
            else
            {
                transform.GetChild(i).localScale =
                    Vector3.Lerp(transform.GetChild(i).localScale, new Vector3(0.8f, 0.8f,0.8f), 0.1f);
            }
        }
    }
}
