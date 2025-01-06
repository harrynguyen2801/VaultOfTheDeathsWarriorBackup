using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class QualityManager2 : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    
    public TextMeshProUGUI antiAliasing;
    public TextMeshProUGUI textureQuality;
    public TextMeshProUGUI shadowQuality;
    public TextMeshProUGUI postProcessing;

    public void ChangeLevelQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, true);
        QualitySettings.renderPipeline = qualityLevels[level];
        
        // antiAliasing.text = QualitySettings.antiAliasing.ToString();
        // textureQuality.text = QualitySettings.antiAliasing.ToString();
        shadowQuality.text = QualitySettings.shadowResolution.ToString();
        postProcessing.text = QualitySettings.antiAliasing.ToString();
    }
    
    public void TextureSet(int index)
    {
        // 0 = Full, 4 = Eight Resolution
        QualitySettings.globalTextureMipmapLimit = index;
    }
    
    public void ReflectionSet(int index)
    {
        if (index == 0)
            QualitySettings.realtimeReflectionProbes = false;
        else if (index == 1)
            QualitySettings.realtimeReflectionProbes = true;
    }

    public void SetAntiAliasing(int value)
    {
        //MSAA (1: Tắt, 2: 2x, 4: 4x, 8: 8x)
        var urpAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        if (urpAsset != null)
        {
            urpAsset.msaaSampleCount = value;
            // if (Camera.main != null)
            // {
            //     Camera.main.allowMSAA = true;
            //     Debug.Log("MSAA enabled on Camera.");
            // }
        }
    }

    public void ChangeShadowResolution(int resolution)
    {
        var urpAsset = (UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        if (urpAsset != null)
        {
            // Thay đổi độ phân giải bóng
            // urpAsset.mainLightShadowmapResolution = UnityEngine.Rendering.Universal.ShadowResolution._4096;
        }
    }
}
