
using UnityEngine;

public class StarPointer : MonoBehaviour
{
    public GameObject pointerArrow;
   public Camera uiCamera;
    [HideInInspector] private Vector3 targetPosition;
    [HideInInspector] private RectTransform pointerRectTransform;
    [HideInInspector] public LevelManager levelManager;
    public static bool isGameWon;
    private TowerSpawnManager towerSpawnManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Start()
    {
        towerSpawnManager = FindObjectOfType<TowerSpawnManager>();
    }

    private void FixedUpdate()
    {
        if (!isGameWon && towerSpawnManager.starObjectsList.Count > 0)
        {
            targetPosition = towerSpawnManager.starObjectsList[towerSpawnManager.starObjectsList.Count - 1].transform.position;
        
        }
        else
            targetPosition = levelManager.cameraAndEnemiesTargetPoint.position;

        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 10f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);


  //  Camera.main.ScreenPointToRay

        float borderSize = 12f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        // Debug.Log(isOffScreen+" "+targetPositionScreenPoint);

        if (isOffScreen)
        {
            pointerArrow.SetActive(true);
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;


            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition) ;
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else { pointerArrow.SetActive(false); }
    }

}
