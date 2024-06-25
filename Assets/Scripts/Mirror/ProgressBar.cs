using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float AreaToClean;
    public float AreaCleaned = 0f;
    [SerializeField] private TMP_Text _progressText;

    public void SetAreaToClean(float areaToClean)
    {
        AreaToClean = areaToClean;
        UpdateBar();
    }

    public void UpdateBar()
    {
        _progressText.text = "Progress: " + AreaCleaned / AreaToClean + "%";
    }
}