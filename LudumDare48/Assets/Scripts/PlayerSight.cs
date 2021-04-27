using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerSight : MonoBehaviour
{
    
    [SerializeField]
    private PostProcessVolume m_PostProcessVolume = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSight();
        GameEvents.Instance.onUpgrade += UpdateSight;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -3)
        {
            UpdateSight();
        }
    }

    void UpdateSight()
    {
        var p = Mathf.Clamp01(Mathf.InverseLerp(0, 3, -transform.position.y) + 0.3f);

        if (m_PostProcessVolume != null)
        {
            Vignette vignette;
            if (m_PostProcessVolume.profile.TryGetSettings(out vignette))
            {
                vignette.intensity.value = UpgradeManager.Instance.Sight*p;
            }
        }
    }
}
