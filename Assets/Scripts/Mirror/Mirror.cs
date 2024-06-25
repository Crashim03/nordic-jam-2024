using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private GameObject _dirtyArea;
    [SerializeField] private Transform _dirtyAreasParent;
    [SerializeField] private int _areasToClean = 10;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private bool _setManually = false;

    public void CleanArea(float cleaned)
    {
        Debug.Log(cleaned);
        _progressBar.AreaCleaned += cleaned;
        _progressBar.UpdateBar();
    }

    private void Start()
    {
        if (_setManually)
        {
            return;
        }

        _progressBar.SetAreaToClean(_areasToClean * 100);
        float width = gameObject.transform.localScale.x;
        float height = gameObject.transform.localScale.y;

        float xInterval = width / _areasToClean;
        float yInterval = height / _areasToClean;

        for (float i = gameObject.transform.position.x - width / 2 + xInterval / 2; i < gameObject.transform.position.x + width / 2; i += xInterval)
        {
            for (float j = gameObject.transform.position.y - height / 2 + yInterval / 2; j < gameObject.transform.position.y + height / 2; j+= yInterval)
            {
                GameObject dirtyArea = Instantiate(_dirtyArea, new Vector3(i, j, 1f), transform.rotation);
                dirtyArea.transform.localScale = new Vector3(xInterval, yInterval, 1f);
                dirtyArea.transform.SetParent(_dirtyAreasParent);
                dirtyArea.GetComponent<DirtyArea>().Mirror = this;
            }
        }
    }
}