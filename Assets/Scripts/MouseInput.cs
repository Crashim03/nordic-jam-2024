using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [HideInInspector] public CleaningTool CurrentCleaningTool;
    [SerializeField] Camera _camera;
    private void Update()
    {
        if (CurrentCleaningTool == null || !Input.GetMouseButton(0))
        {
            return;
        }

        Vector3 mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));
        CurrentCleaningTool.Move(mousePosition);
    }

    private void Awake()
    {
        CurrentCleaningTool = GetComponent<CleaningTool>();
    }
}