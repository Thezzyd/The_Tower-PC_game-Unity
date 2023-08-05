using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class CorelationGraph : MonoBehaviour
{

    private static CorelationGraph instance;
    private FirebaseManager firebaseManager;

    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashContainer;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;
    private List<IGraphVisualObject> graphVisualObjectList;
    private GameObject tooltipGameObject;
    private List<RectTransform> yLabelList;

    // Cached values
    public List<AgeCorelationData> corelationDataList;
    public List<string> ageValueList;
    public List<int> scoreValueList;

    private IGraphVisual graphVisual;
    private int maxVisibleValueAmount;
    private Func<int, string> getAxisLabelX;
    private Func<float, string> getAxisLabelY;


    IGraphVisual lineGraphVisual;
    IGraphVisual barChartVisual;

    public RectTransform btnTooltipTransform;

    public Material graphGlowMaterial;

    public Transform graphContainerTransform;
    public Vector2 regressionStartingPoint;
    public Vector2 regressionEndinggPoint;


    private void Awake()
    {

        instance = this;
        // Grab base objects references
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashContainer = graphContainer.Find("dashContainer").GetComponent<RectTransform>();
        dashTemplateX = dashContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = dashContainer.Find("dashTemplateY").GetComponent<RectTransform>();
        tooltipGameObject = graphContainer.Find("tooltip").gameObject;

        gameObjectList = new List<GameObject>();
        graphVisualObjectList = new List<IGraphVisualObject>();

        lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.white, new Color(1, 1, 1, .5f), graphContainerTransform);
   

        HideTooltip(null);

   
        firebaseManager = FindObjectOfType<FirebaseManager>();

    }

    public void LoadGrapgWithData()
    {
        ageValueList = new List<string>();
        scoreValueList = new List<int>();

        IGraphVisual lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.white, new Color(1, 1, 1, .5f), graphContainerTransform);

        int interations = 0;


        for (int i = 0; i < corelationDataList.Count; i++)
        {

            ageValueList.Add(corelationDataList[i].UserAge.ToString());
            scoreValueList.Add(corelationDataList[i].AverageScorePerGame);
            interations++;

        }

        ShowGraph(scoreValueList, lineGraphVisual, -1, (int _i) => ageValueList[_i], (float _f) => Mathf.RoundToInt(_f).ToString());
        Debug.Log("Powinno pokazac kurłaaa noo :/");
    }


    public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition, float tooltipDistance, GameObject barGameObject)
    {
        instance.ShowTooltip(tooltipText, anchoredPosition, tooltipDistance, barGameObject);
    }

    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition, float tooltipDistance, GameObject barGameObject)
    {
        tooltipGameObject.SetActive(true);

        tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        TextMeshProUGUI tooltipUIText = tooltipGameObject.transform.Find("text").GetComponent<TextMeshProUGUI>();
        tooltipUIText.text = "<mark=#000000 padding='40, 40, 20, 20'><font=\"Oswald Bold SDF\">" + tooltipText + "</font></mark >";

        tooltipGameObject.transform.Find("text").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, tooltipDistance, 0);
        tooltipGameObject.transform.SetAsLastSibling();

        barGameObject.GetComponent<Image>().material = graphGlowMaterial;

    }

    public static void HideTooltip_Static(GameObject barGameObject)
    {
        instance.HideTooltip(barGameObject);
    }

    private void HideTooltip(GameObject barGameObject)
    {
        tooltipGameObject.SetActive(false);

        if (barGameObject != null)
            barGameObject.GetComponent<Image>().material = null;
    }

    private void SetGetAxisLabelX(Func<int, string> getAxisLabelX)
    {
        ShowGraph(this.scoreValueList, this.graphVisual, this.maxVisibleValueAmount, getAxisLabelX, this.getAxisLabelY);
    }

    private void SetGetAxisLabelY(Func<float, string> getAxisLabelY)
    {
        ShowGraph(this.scoreValueList, this.graphVisual, this.maxVisibleValueAmount, this.getAxisLabelX, getAxisLabelY);
    }

    private void IncreaseVisibleAmount()
    {
        ShowGraph(this.scoreValueList, this.graphVisual, this.maxVisibleValueAmount + 1, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void DecreaseVisibleAmount()
    {
        ShowGraph(this.scoreValueList, this.graphVisual, this.maxVisibleValueAmount - 1, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void SetGraphVisual(IGraphVisual graphVisual)
    {
        ShowGraph(this.scoreValueList, graphVisual, this.maxVisibleValueAmount, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void ShowGraph(List<int> valueList, IGraphVisual graphVisual, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        this.scoreValueList = valueList;
        this.graphVisual = graphVisual;
        this.getAxisLabelX = getAxisLabelX;
        this.getAxisLabelY = getAxisLabelY;

        if (maxVisibleValueAmount <= 0)
        {
            // Show all if no amount specified
            maxVisibleValueAmount = valueList.Count;
        }
        if (maxVisibleValueAmount > valueList.Count)
        {
            // Validate the amount to show the maximum
            maxVisibleValueAmount = valueList.Count;
        }

        this.maxVisibleValueAmount = maxVisibleValueAmount;

        // Test for label defaults
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        // Clean up previous graph
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
        // yLabelList.Clear();

        foreach (IGraphVisualObject graphVisualObject in graphVisualObjectList)
        {

            graphVisualObject.CleanUp();

        }
        graphVisualObjectList.Clear();

        graphVisual.CleanUp();

        // Grab the width and height from the container
        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = 0;
        float yMinimum = 0;


            yMaximum = valueList[0];
            yMinimum = valueList[0];
      

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            int value = valueList[i];
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        float yDifference = yMaximum - yMinimum;
        if (yDifference <= 0)
        {
            yDifference = yMaximum;
        }
        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        yMinimum = 0f; // Start the graph at zero

        // Set the distance between each point on the graph 
        float xSize = graphWidth / (maxVisibleValueAmount + 1);

        // Cycle through all visible data points
        int xIndex = 0;

        int xValueSeparator = 2;
        int xValueBox = 0;
        int tempIterator = 0;

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            tempIterator++;

            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

            // Add data point visual
            string tooltipText = getAxisLabelY(valueList[i]);
         
            IGraphVisualObject graphVisualObject = graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, tooltipText, true, false, graphContainerTransform);
            graphVisualObjectList.Add(graphVisualObject);

            if (tempIterator == 1)
            {
                xPosition = xSize + xIndex * xSize;
                yPosition = ((regressionStartingPoint.y - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

                IGraphVisualObject graphVisualObjectRegressionLineA = graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, "", false, true, graphContainerTransform);
                graphVisualObjectList.Add(graphVisualObjectRegressionLineA);

                Debug.Log("powinno je naniesc");
                Debug.Log("start x = " + regressionStartingPoint.x + " , y = " + regressionStartingPoint.y);
            } 
            else if (i == valueList.Count - 1)
            {
                xPosition = xSize + xIndex * xSize;
                yPosition = ((regressionEndinggPoint.y - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

                IGraphVisualObject graphVisualObjectRegressionLineB = graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, "", false, true, graphContainerTransform);
                graphVisualObjectList.Add(graphVisualObjectRegressionLineB);
                Debug.Log("koniec x = " + regressionEndinggPoint.x + " , y = " + regressionEndinggPoint.y);

            /*    GameObject lineRegression = new GameObject();
                lineRegression.AddComponent<LineRenderer>();
                lineRegression.GetComponent<LineRenderer>().SetPosition(0, regressionLinePoints[0]);
                lineRegression.GetComponent<LineRenderer>().SetPosition(1, regressionLinePoints[1]);*/

            }

            // Duplicate the x label template


            if ( i == 0 || (xValueBox + xValueSeparator <= int.Parse(ageValueList[i]))) 
            {
                xValueBox = int.Parse(ageValueList[i]);

                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer, false);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.GetComponent<Text>().text = getAxisLabelX(i);
                gameObjectList.Add(labelX.gameObject);
           
                // Duplicate the x dash template
                RectTransform dashX = Instantiate(dashTemplateX);
                dashX.SetParent(dashContainer, false);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, -3f);
                gameObjectList.Add(dashX.gameObject);
            }
            xIndex++;

         /*   if (i == valueList.Count - 1)
            {
                xPosition = xSize + xIndex * xSize;
                yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
                // Duplicate the x dash template
                dashX = Instantiate(dashTemplateX);
                dashX.SetParent(dashContainer, false);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, -3f);
                gameObjectList.Add(dashX.gameObject);
            }*/
        }

        // Set up separators on the y axis
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            // Duplicate the label template
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            gameObjectList.Add(labelY.gameObject);

            // Duplicate the dash template
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(dashContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }

      
    }



    /*
     * Interface definition for showing visual for a data point
     * */
    private interface IGraphVisual
    {

        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText, bool inValueListIndex, bool isRegressionLinePoint, Transform parent);
        void CleanUp();
    }

    /*
     * Represents a single Visual Object in the graph
     * */
    private interface IGraphVisualObject
    {

        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void CleanUp();

    }


    private class LineGraphVisual : IGraphVisual
    {
        private Transform graphContainerTransform;

        private RectTransform graphContainer;
        private Sprite dotSprite;
        static LineGraphVisualObject lastLineGraphVisualObject = null;
        // private List<LineGraphVisualObject> withConnectionLineGraphVisualObject = new List<LineGraphVisualObject>();
        static List<Vector2> regressionLinePoints = new List<Vector2>();
        private Color dotColor;
        private Color dotConnectionColor;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor, Transform graphContainerTransform)
        {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConnectionColor = dotConnectionColor;
            this.graphContainerTransform = graphContainerTransform;
          //  lastLineGraphVisualObject = null;
        }

        public void CleanUp()
        {
            lastLineGraphVisualObject = null;
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText, bool isProgressionBar, bool isRegressionLinePoint, Transform parent)
        {
            GameObject dotGameObject = CreateDot(graphPosition, isProgressionBar);
            this.graphContainerTransform = parent;

            GameObject dotConnectionGameObject = null;
            if (isRegressionLinePoint)
            {
                regressionLinePoints.Add(graphPosition);
                
             //   dotConnectionGameObject = CreateDotConnection(lastLineGraphVisualObject.GetGraphPosition(), dotGameObject.GetComponent<RectTransform>().anchoredPosition, isProgressionBar);
            }

            if (regressionLinePoints.Count == 2)
            {
                dotConnectionGameObject = CreateDotConnection(regressionLinePoints[0], regressionLinePoints[1], isProgressionBar);

              /*  GameObject lineRegression = new GameObject();
                lineRegression.AddComponent<LineRenderer>();
                lineRegression.GetComponent<LineRenderer>().SetPosition(0, regressionLinePoints[0]);
                lineRegression.GetComponent<LineRenderer>().SetPosition(1, regressionLinePoints[1]);*/

            }

            Debug.Log("pojemnosc statycznej listy lini regresjii: "+ regressionLinePoints.Count);

            LineGraphVisualObject lineGraphVisualObject = new LineGraphVisualObject(dotGameObject, dotConnectionGameObject, lastLineGraphVisualObject);
            lineGraphVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            lastLineGraphVisualObject = lineGraphVisualObject;

            return lineGraphVisualObject;
        }



        private GameObject CreateDot(Vector2 anchoredPosition, bool isProgressionBar)
        {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = dotSprite;
            gameObject.GetComponent<Image>().color = dotColor;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1);
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);

            if (isProgressionBar)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            else 
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
            }
            // Add Button_UI Component which captures UI Mouse Events
            Button_UI dotButtonUI = gameObject.AddComponent<Button_UI>();

            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, bool isProgressionBar)
        {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = dotConnectionColor;
            gameObject.GetComponent<Image>().raycastTarget = false;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));

            if (isProgressionBar)
            {
                gameObject.GetComponent<Image>().color = new Color(0.0741f, 1, 0, 0.8f);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(0.0741f, 1, 0, 0.8f);
            }
            var tak = Instantiate(gameObject);
            tak.transform.SetParent(graphContainerTransform, false);
            tak.name = "liniaRegresjii";
            return gameObject;
        }
    }



    public class LineGraphVisualObject : IGraphVisualObject
    {

        public event EventHandler OnChangedGraphVisualObjectInfo;

        private GameObject dotGameObject;
        private GameObject dotConnectionGameObject;
        private LineGraphVisualObject lastVisualObject;

        public LineGraphVisualObject(GameObject dotGameObject, GameObject dotConnectionGameObject, LineGraphVisualObject lastVisualObject)
        {
            this.dotGameObject = dotGameObject;
            this.dotConnectionGameObject = dotConnectionGameObject;
            this.lastVisualObject = lastVisualObject;

            if (lastVisualObject != null)
            {
                lastVisualObject.OnChangedGraphVisualObjectInfo += LastVisualObject_OnChangedGraphVisualObjectInfo;
            }
        }

        private void LastVisualObject_OnChangedGraphVisualObjectInfo(object sender, EventArgs e)
        {
            UpdateDotConnection();
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText)
        {
            RectTransform rectTransform = dotGameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = graphPosition;

            UpdateDotConnection();

            Button_UI dotButtonUI = dotGameObject.GetComponent<Button_UI>();

            // Show Tooltip on Mouse Over
            dotButtonUI.MouseOverOnceFunc = () => {
                ShowTooltip_Static(tooltipText, graphPosition, 15.0f, dotGameObject);
            };

            // Hide Tooltip on Mouse Out
            dotButtonUI.MouseOutOnceFunc = () => {
                HideTooltip_Static(dotGameObject);
            };

            if (OnChangedGraphVisualObjectInfo != null) OnChangedGraphVisualObjectInfo(this, EventArgs.Empty);
        }

        public void CleanUp()
        {
            Destroy(dotGameObject);
            Destroy(dotConnectionGameObject);
        }

        public Vector2 GetGraphPosition()
        {
            RectTransform rectTransform = dotGameObject.GetComponent<RectTransform>();
            return rectTransform.anchoredPosition;
        }

        private void UpdateDotConnection()
        {
            if (dotConnectionGameObject != null)
            {
                RectTransform dotConnectionRectTransform = dotConnectionGameObject.GetComponent<RectTransform>();
                Vector2 dir = (lastVisualObject.GetGraphPosition() - GetGraphPosition()).normalized;
                float distance = Vector2.Distance(GetGraphPosition(), lastVisualObject.GetGraphPosition());
                dotConnectionRectTransform.sizeDelta = new Vector2(distance, 3f);
                dotConnectionRectTransform.anchoredPosition = GetGraphPosition() + dir * distance * .5f;
                dotConnectionRectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            }
        }

    }

}

