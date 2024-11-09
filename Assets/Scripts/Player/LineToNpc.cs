using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LineRenderer = UnityEngine.LineRenderer;
using Transform = UnityEngine.Transform;

public class LineToNpc : MonoBehaviour
{
    public Transform npc;           // Tham chiếu tới NPC
    public LineRenderer lineRenderer;
    public Material lineMaterial;
    private Transform _npcTransform = new RectTransform();
    private bool _activeArrSearch = false;
    private float _speedRotaion = 3f;
    private void OnEnable()
    {
        ActionManager.OnUpdatenextStepTutorial += OnUpdateDirectionLookNpc;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdatenextStepTutorial -= OnUpdateDirectionLookNpc;
    }


    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.yellow;
    }
    private void OnUpdateDirectionLookNpc(int npcId)
    {
        if (npcId != 0 && DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialStep) != 6)
        {
            _activeArrSearch = true;
            npc = VillageHomeScreen.Instance.modelNpcList[npcId-1].transform;
        }
        else
        {
            npc = VillageHomeScreen.Instance.playerModelEquipManager.transform;
        }
    }

    void Update()
    {
        if (DataManager.Instance.GetDataPrefGame(DataManager.EDataPrefName.TutorialVillage) == 0 && _activeArrSearch)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, npc.position);
        }
    }
}
