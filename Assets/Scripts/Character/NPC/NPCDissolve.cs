using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDissolve : MonoBehaviour
{
    List<Material> materials = new List<Material>();
    private float _duration = 1.5f;
    private float _timeCurrent = 0;

    void Start()
    {
        var renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            materials.AddRange(renders[i].materials);
        }
    }
    public void SetValue(float value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_Dissolve", value);
        }
    }
    public void NpcDissolveIn()
    {
        StartCoroutine(DissolveIn());
    }
    IEnumerator DissolveIn()
    {
        _timeCurrent = 0;
        while (_timeCurrent < _duration)
        {
            _timeCurrent += Time.deltaTime;
            SetValue(Mathf.Lerp(1f, 0f, _timeCurrent / _duration));
            yield return null;
        }
    }
}
