/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */
 /*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class Window_Graph : MonoBehaviour {

    private static Window_Graph instance;
    private FirebaseManager firebaseManager;
    private bool loadUserData;
    private bool isLoadingUserDataInProcess;

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

    // Cached values
    public List<int> valueList;
    public List<int> gamesNumberList;
    private int maxBarCountInGraph;
    private int startingOnScreenValueIndex;
    public List<int> onScreenValueList;
    public List<string> onScreenDatesList;
    public List<int> onScreenGamesList;

    private IGraphVisual graphVisual;
    private int maxVisibleValueAmount;
    private Func<int, string> getAxisLabelX;
    private Func<float, string> getAxisLabelY;

    public List<GameData> userGameDataList;
    // List<int> valueList;
    public List<string> datesList;

    private string currentGraphInterval;
    IGraphVisual lineGraphVisual;
    IGraphVisual barChartVisual;
    private void Awake() {

        maxBarCountInGraph = 20;
        startingOnScreenValueIndex = 0;
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
        
         lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.green, new Color(1, 1, 1, .5f));
         barChartVisual = new BarChartVisual(graphContainer, Color.white, .8f);

        // Set up buttons
        transform.Find("barChartBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGraphVisual(barChartVisual);
        };
        transform.Find("lineGraphBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGraphVisual(lineGraphVisual);
        };
        
        transform.Find("decreaseVisibleAmountBtn").GetComponent<Button_UI>().ClickFunc = () => {
            DecreaseVisibleAmount();
        };
        transform.Find("increaseVisibleAmountBtn").GetComponent<Button_UI>().ClickFunc = () => {
            IncreaseVisibleAmount();
        };
        
        transform.Find("dollarBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGetAxisLabelY((float _f) => "$" + Mathf.RoundToInt(_f));
        };
        transform.Find("euroBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGetAxisLabelY((float _f) => "€" + Mathf.RoundToInt(_f / 1.18f));
        };
        
        HideTooltip();

        loadUserData = true;

        // Set up base values
        // List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        //  List<int> valueList = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //  ShowGraph(valueList, barChartVisual, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));

        userGameDataList = new List<GameData>();
        firebaseManager = FindObjectOfType<FirebaseManager>();

        /*
        // Automatically modify graph values and visual
        bool useBarChart = true;
        FunctionPeriodic.Create(() => {
            for (int i = 0; i < valueList.Count; i++) {
                valueList[i] = Mathf.RoundToInt(valueList[i] * UnityEngine.Random.Range(0.8f, 1.2f));
                if (valueList[i] < 0) valueList[i] = 0;
            }
            if (useBarChart) {
                ShowGraph(valueList, barChartVisual, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));
            } else {
                ShowGraph(valueList, lineGraphVisual, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));
            }
            useBarChart = !useBarChart;
        }, .5f);
        //*/
   /* }

    public void Start()
    {
        LoadUserData();
    }

    public void LoadUserData()
    {
        firebaseManager.GetUserGameData();
        loadUserData = false;
        isLoadingUserDataInProcess = true;
    }

    public void MoveGraphContentRight()
    {

        startingOnScreenValueIndex++;
        if (startingOnScreenValueIndex >= 0)
            startingOnScreenValueIndex = 0;

        LoadGrapgWithData();
    }

    public void MoveGraphContentLeft()
    {
        startingOnScreenValueIndex--;
        if (startingOnScreenValueIndex <= -valueList.Count + maxBarCountInGraph)
            startingOnScreenValueIndex = -valueList.Count + maxBarCountInGraph;

        LoadGrapgWithData();

    }

    public void LoadGrapgWithData()
    {
      
        IGraphVisual barChartVisual = new BarChartVisual(graphContainer, Color.white, .8f);

        onScreenValueList = new List<int>();
        onScreenDatesList = new List<string>();
        onScreenGamesList = new List<int>();

        for (int i = valueList.Count + startingOnScreenValueIndex - maxBarCountInGraph; i < (valueList.Count + startingOnScreenValueIndex); i++)
        {
            try
            {
                onScreenValueList.Add(valueList[i]);
                onScreenDatesList.Add(datesList[i]);
                onScreenGamesList.Add(gamesNumberList[i]);
            }
            catch (ArgumentOutOfRangeException e)
            {
                continue;
            }
        }

               // List<int> valueList = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        ShowGraph(onScreenValueList, barChartVisual, -1, (int _i) => onScreenDatesList[_i] , (float _f) => Mathf.RoundToInt(_f).ToString());



      
    }

    public void RearangeUserGameDataInList( string valueParameterName)
    {
        userGameDataList = firebaseManager.userGamesDataList;
       

        int sumOfScoreInOneTimePeriod = 0;
        int sumOfGames = 0;
        DateTime currentlyCheckedDateTime = DateTime.Now;

        switch (valueParameterName)
        {
            case "score_daily":

                currentGraphInterval = "score_daily";
                valueList = new List<int>();
                datesList = new List<string>();
                gamesNumberList = new List<int>();

                sumOfScoreInOneTimePeriod = 0;
                sumOfGames = 0;
                currentlyCheckedDateTime = DateTime.Now;

                for (int i = 0; i < userGameDataList.Count; i++)
                {

                    if (i == 0)
                    {
                        currentlyCheckedDateTime = userGameDataList[i].GameDateTime.Date;
                        sumOfScoreInOneTimePeriod += userGameDataList[i].Score;
                        sumOfGames++;
                    }

                    if (i != 0)
                    {
                        if (userGameDataList[i].GameDateTime.Date.Equals(currentlyCheckedDateTime))
                        {
                            sumOfScoreInOneTimePeriod += userGameDataList[i].Score;
                            sumOfGames++;
                        }
                        else
                        {
                            valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                            datesList.Add(currentlyCheckedDateTime.ToString("dd/MM")+"\n" + currentlyCheckedDateTime.ToString("yyyy"));
                            gamesNumberList.Add(sumOfGames);

                            currentlyCheckedDateTime = userGameDataList[i].GameDateTime.Date;
                            sumOfScoreInOneTimePeriod = userGameDataList[i].Score;
                            sumOfGames = 1;
                        } 
                    }

                    if (i == userGameDataList.Count - 1)
                    {
                        valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                        datesList.Add(currentlyCheckedDateTime.ToString("dd/MM") + "\n" + currentlyCheckedDateTime.ToString("yyyy"));
                        gamesNumberList.Add(sumOfGames);
                    }
                }

                break;

            case "score_monthly":

                currentGraphInterval = "score_monthly";
                valueList = new List<int>();
                datesList = new List<string>();
                gamesNumberList = new List<int>();

                sumOfScoreInOneTimePeriod = 0;
                sumOfGames = 0;
                currentlyCheckedDateTime = DateTime.Now;

                for (int i = 0; i < userGameDataList.Count; i++)
                {

                    if (i == 0)
                    {
                        currentlyCheckedDateTime = userGameDataList[i].GameDateTime.Date;
                        sumOfScoreInOneTimePeriod += userGameDataList[i].Score;
                        sumOfGames++;
                    }

                    if (i != 0)
                    {
                        if (userGameDataList[i].GameDateTime.Date.Month == currentlyCheckedDateTime.Month)
                        {
                            sumOfScoreInOneTimePeriod += userGameDataList[i].Score;
                            sumOfGames++;
                        }
                        else
                        {
                            valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                            datesList.Add(currentlyCheckedDateTime.ToString("MMMM") + "\n" + currentlyCheckedDateTime.ToString("yyyy"));
                            gamesNumberList.Add(sumOfGames);

                            currentlyCheckedDateTime = userGameDataList[i].GameDateTime.Date;
                            sumOfScoreInOneTimePeriod = userGameDataList[i].Score;
                            sumOfGames = 1;
                        }
                    }

                    if (i == userGameDataList.Count - 1)
                    {
                        valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                        datesList.Add(currentlyCheckedDateTime.ToString("MMMM") + "\n" + currentlyCheckedDateTime.ToString("yyyy"));
                        gamesNumberList.Add(sumOfGames);
                    }
                }

                break;
        }
    }

    public void Update()
    {
      /*  if (loadUserData)
        {
            firebaseManager.GetUserGameData();
            loadUserData = false;
            isLoadingUserDataInProcess = true;
        }*/

  /*      if (!loadUserData && isLoadingUserDataInProcess)
        {
            if (firebaseManager.isUserDataListLoaded)
            {
                //write data to chart || refresch chart with new data
                RearangeUserGameDataInList("score_daily");
              
                LoadGrapgWithData();

                isLoadingUserDataInProcess = false;
                firebaseManager.isUserDataListLoaded = false;
            }
        }

    }

    public void SwapIntervalOfGraph()
    {
        if(currentGraphInterval.Equals("score_daily"))
            RearangeUserGameDataInList("score_monthly");
        else if(currentGraphInterval.Equals("score_monthly"))
            RearangeUserGameDataInList("score_daily");

        LoadGrapgWithData();
    }

    public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition) {
        instance.ShowTooltip(tooltipText, anchoredPosition);
    }

    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition) {
        // Show Tooltip GameObject
        tooltipGameObject.SetActive(true);

        tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        TextMeshProUGUI tooltipUIText = tooltipGameObject.transform.Find("text").GetComponent<TextMeshProUGUI>();
        tooltipUIText.text = "<mark=#000000 padding='40, 40, 20, 20'><font=\"Oswald Bold SDF\">" + tooltipText + "</font></mark >";

        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(
            tooltipUIText.preferredWidth + textPaddingSize * 2f, 
            tooltipUIText.preferredHeight + textPaddingSize * 2f
        );

     //   tooltipGameObject.transform.Find("background").GetComponent<RectTransform>().sizeDelta = backgroundSize;

        // UI Visibility Sorting based on Hierarchy, SetAsLastSibling in order to show up on top
        tooltipGameObject.transform.SetAsLastSibling();
    }

    public static void HideTooltip_Static() {
        instance.HideTooltip();
    }

    private void HideTooltip() {
        tooltipGameObject.SetActive(false);
    }

    private void SetGetAxisLabelX(Func<int, string> getAxisLabelX) {
        ShowGraph(this.onScreenValueList, this.graphVisual, this.maxVisibleValueAmount, getAxisLabelX, this.getAxisLabelY);
    }

    private void SetGetAxisLabelY(Func<float, string> getAxisLabelY) {
        ShowGraph(this.onScreenValueList, this.graphVisual, this.maxVisibleValueAmount, this.getAxisLabelX, getAxisLabelY);
    }

    private void IncreaseVisibleAmount() {
        ShowGraph(this.onScreenValueList, this.graphVisual, this.maxVisibleValueAmount + 1, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void DecreaseVisibleAmount() {
        ShowGraph(this.onScreenValueList, this.graphVisual, this.maxVisibleValueAmount - 1, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void SetGraphVisual(IGraphVisual graphVisual) {
        ShowGraph(this.onScreenValueList, graphVisual, this.maxVisibleValueAmount, this.getAxisLabelX, this.getAxisLabelY);
    }

    private void ShowGraph(List<int> valueList, IGraphVisual graphVisual, int maxVisibleValueAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null) {
        this.onScreenValueList = valueList;
        this.graphVisual = graphVisual;
        this.getAxisLabelX = getAxisLabelX;
        this.getAxisLabelY = getAxisLabelY;

        if (maxVisibleValueAmount <= 0) {
            // Show all if no amount specified
            maxVisibleValueAmount = valueList.Count;
        }
        if (maxVisibleValueAmount > valueList.Count) {
            // Validate the amount to show the maximum
            maxVisibleValueAmount = valueList.Count;
        }

        this.maxVisibleValueAmount = maxVisibleValueAmount;

        // Test for label defaults
        if (getAxisLabelX == null) {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null) {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        // Clean up previous graph
        foreach (GameObject gameObject in gameObjectList) {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        foreach (IGraphVisualObject graphVisualObject in graphVisualObjectList) {
            try
            {
                Debug.Log(graphVisualObject);
                graphVisualObject.CleanUp();
            } catch (NullReferenceException e)
            {
                continue;
            }
        }
        graphVisualObjectList.Clear();
        graphVisualObjectList = new List<IGraphVisualObject>();

        // Grab the width and height from the container
        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        // Identify y Min and Max values
        float yMaximum = valueList[0];
        float yMinimum = valueList[0];
        
        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++) {
            int value = valueList[i];
            if (value > yMaximum) {
                yMaximum = value;
            }
            if (value < yMinimum) {
                yMinimum = value;
            }
        }

        float yDifference = yMaximum - yMinimum;
        if (yDifference <= 0) {
            yDifference = 5f;
        }
        yMaximum = yMaximum + (yDifference * 0.2f);
        yMinimum = yMinimum - (yDifference * 0.2f);

        yMinimum = 0f; // Start the graph at zero

        // Set the distance between each point on the graph 
        float xSize = graphWidth / (maxVisibleValueAmount + 1);

        // Cycle through all visible data points
        int xIndex = 0;
        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++) {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;

            // Add data point visual
            string tooltipText = getAxisLabelY(valueList[i]) +" | "+ onScreenGamesList[i] + " games";
            graphVisualObjectList.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, tooltipText));

            // Duplicate the x label template
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

            xIndex++;
        }

        // Set up separators on the y axis
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++) {
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
   /* private interface IGraphVisual {

        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText);

    }

    /*
     * Represents a single Visual Object in the graph
     * */
   /* private interface IGraphVisualObject {

        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void CleanUp();

    }


    /*
     * Displays data points as a Bar Chart
     * */
    /*private class BarChartVisual : IGraphVisual {

        private RectTransform graphContainer;
        private Color barColor;
        private float barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier) {
            this.graphContainer = graphContainer;
            this.barColor = barColor;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText) {
            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth);

            BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, barWidthMultiplier);
            barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            return barChartVisualObject;
        }

        private GameObject CreateBar(Vector2 graphPosition, float barWidth) {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = barColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);
            
            // Add Button_UI Component which captures UI Mouse Events
            Button_UI barButtonUI = gameObject.AddComponent<Button_UI>();

            return gameObject;
        }


        public class BarChartVisualObject : IGraphVisualObject {

            private GameObject barGameObject;
            private float barWidthMultiplier;

            public BarChartVisualObject(GameObject barGameObject, float barWidthMultiplier) {
                this.barGameObject = barGameObject;
                this.barWidthMultiplier = barWidthMultiplier;
            }

            public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText) {
                RectTransform rectTransform = barGameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
                rectTransform.sizeDelta = new Vector2(graphPositionWidth * barWidthMultiplier, graphPosition.y);

                Button_UI barButtonUI = barGameObject.GetComponent<Button_UI>();

                // Show Tooltip on Mouse Over
                barButtonUI.MouseOverOnceFunc = () => {
                    ShowTooltip_Static(tooltipText, graphPosition);
                };

                // Hide Tooltip on Mouse Out
                barButtonUI.MouseOutOnceFunc = () => {
                    HideTooltip_Static();
                };
            }

            public void CleanUp() {
                Debug.Log(barGameObject);
                Destroy(barGameObject);
            }


        }

    }


    /*
     * Displays data points as a Line Graph
     * */
    /*private class LineGraphVisual : IGraphVisual {

        private RectTransform graphContainer;
        private Sprite dotSprite;
        private GameObject lastDotGameObject;
        private Color dotColor;
        private Color dotConnectionColor;
        List<GameObject> gameObjectList;
        GameObject dotGameObject;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor) {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConnectionColor = dotConnectionColor;
            lastDotGameObject = null;
        }


        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText) {
            gameObjectList = new List<GameObject>();
            dotGameObject = CreateDot(graphPosition);
            
            // Add Button_UI Component which captures UI Mouse Events
            Button_UI dotButtonUI = dotGameObject.AddComponent<Button_UI>();
            
            // Show Tooltip on Mouse Over
            dotButtonUI.MouseOverOnceFunc += () => {
                ShowTooltip_Static(tooltipText, graphPosition);
            };
            
            // Hide Tooltip on Mouse Out
            dotButtonUI.MouseOutOnceFunc += () => {
                HideTooltip_Static();
            };

            gameObjectList.Add(dotGameObject);
            if (lastDotGameObject != null) {
                GameObject dotConnectionGameObject = CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastDotGameObject = dotGameObject;

            return null;
        }

    

        private GameObject CreateDot(Vector2 anchoredPosition) {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = dotSprite;
            gameObject.GetComponent<Image>().color = dotColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
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
            return gameObject;
        }
    }

}
*/