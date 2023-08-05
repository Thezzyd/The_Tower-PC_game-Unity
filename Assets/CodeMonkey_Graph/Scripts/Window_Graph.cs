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
    private List<RectTransform> yLabelList;

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


    private string currentGraphType;
    IGraphVisual lineGraphVisual;
    IGraphVisual barChartVisual;

    private string intervalTypeOnGraph;
    private string valueTypeOnGraph;
    private string calculationsTypeOnGraph;

    public GameObject[] btnGameObj;
    public RectTransform btnTooltipTransform;
    public TextMeshProUGUI graphDescriptionText;
    public TextMeshProUGUI notEnoughtDataText;
    public Material graphGlowMaterial;

    private int maxProgressionbarsOnScreen;
    private int startingIndexOfProgressionBars;


    private void Awake() {

        maxBarCountInGraph = 20;
        maxProgressionbarsOnScreen = 5;
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

        lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.white, new Color(1, 1, 1, .5f));
        barChartVisual = new BarChartVisual(graphContainer, Color.white, .8f);

    
      /*  transform.Find("dollarBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGetAxisLabelY((float _f) => "$" + Mathf.RoundToInt(_f));
        };
        transform.Find("euroBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGetAxisLabelY((float _f) => "€" + Mathf.RoundToInt(_f / 1.18f));
        };*/
        
        HideTooltip(null);

        loadUserData = true;

        userGameDataList = new List<GameData>();
        firebaseManager = FindObjectOfType<FirebaseManager>();

    }

    /*  public void Start()
      {
          LoadUserData();
      }*/

    void OnEnable()
    {
        maxBarCountInGraph = 20;
        maxProgressionbarsOnScreen = 5;
        startingOnScreenValueIndex = 0;
        instance = this;
        // Grab base objects references

        loadUserData = true;

        LoadUserData();
     //   Debug.Log("PrintOnEnable: script was enabled");
    }

    public void LoadUserData()
    {
        firebaseManager = FindObjectOfType<FirebaseManager>();
        firebaseManager.GetUserGameData();
        loadUserData = false;
        isLoadingUserDataInProcess = true;
    }

    public void ChangeGraphType()
    {
        if (currentGraphType.Equals("bar_graph"))
        {
            SetGraphVisual(lineGraphVisual);
            currentGraphType = "line_graph";
        }

        else if (currentGraphType.Equals("line_graph"))
        {
            SetGraphVisual(barChartVisual);
            currentGraphType = "bar_graph";
        }
       

        /*  transform.Find("barChartBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGraphVisual(barChartVisual);
        };
        transform.Find("lineGraphBtn").GetComponent<Button_UI>().ClickFunc = () => {
            SetGraphVisual(lineGraphVisual);
        };*/
    }

    public void MoveGraphContentRight()
    {

        startingOnScreenValueIndex++;
        if (startingOnScreenValueIndex >= maxProgressionbarsOnScreen)
            startingOnScreenValueIndex = maxProgressionbarsOnScreen;

        LoadGrapgWithData(currentGraphType);
    }

    public void MoveGraphContentLeft()
    {
        startingOnScreenValueIndex--;
        if (startingOnScreenValueIndex <= -valueList.Count + maxBarCountInGraph + maxProgressionbarsOnScreen)
            startingOnScreenValueIndex = -valueList.Count + maxBarCountInGraph + maxProgressionbarsOnScreen;

        LoadGrapgWithData(currentGraphType);

    }

    public void ShowBtnTooltipOnPointerEnter(string whichBtn)
    {
        switch (whichBtn)
        {
            case "left": ShowBtnTooltip("Move graph to the left", btnGameObj[0].GetComponent<RectTransform>().anchoredPosition); break;
            case "right": ShowBtnTooltip("Move graph to the right", btnGameObj[1].GetComponent<RectTransform>().anchoredPosition); break;
            case "zoomOut": ShowBtnTooltip("Show more data", btnGameObj[2].GetComponent<RectTransform>().anchoredPosition); break;
            case "zoomIn": ShowBtnTooltip("Show less data", btnGameObj[3].GetComponent<RectTransform>().anchoredPosition); break;
            case "interval": ShowBtnTooltip("Change graph time interval", btnGameObj[4].GetComponent<RectTransform>().anchoredPosition); break;
            case "graph": ShowBtnTooltip("Change graph style", btnGameObj[5].GetComponent<RectTransform>().anchoredPosition); break;
            case "calculations": ShowBtnTooltip("Change calculate function", btnGameObj[6].GetComponent<RectTransform>().anchoredPosition); break;
            case "value": ShowBtnTooltip("Change shown base value", btnGameObj[7].GetComponent<RectTransform>().anchoredPosition); break;
        }
    }

    private void ShowBtnTooltip(string tooltipText, Vector2 anchoredPosition)
    {
        btnTooltipTransform.gameObject.SetActive(true);


        btnTooltipTransform.anchoredPosition = anchoredPosition;
        btnTooltipTransform.GetComponentInChildren<TextMeshProUGUI>().text = "<mark=#000000 padding='40, 40, 20, 20'><font=\"Oswald Bold SDF\">" + tooltipText + "</font></mark >";

        btnTooltipTransform.SetAsLastSibling();
    }

    public void HideBtnTooltip()
    {
        btnTooltipTransform.gameObject.SetActive(false);
    }

    public void LoadGrapgWithData(string graphType)
    {
        currentGraphType = graphType;

        IGraphVisual barChartVisual = new BarChartVisual(graphContainer, Color.white, .8f);
        IGraphVisual lineGraphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.white, new Color(1, 1, 1, .5f));


        onScreenValueList = new List<int>();
        onScreenDatesList = new List<string>();
        onScreenGamesList = new List<int>();
        int interations = 0;
        

        for (int i = valueList.Count + startingOnScreenValueIndex - maxBarCountInGraph - maxProgressionbarsOnScreen; i < (valueList.Count + startingOnScreenValueIndex - maxProgressionbarsOnScreen); i++)
        {
            try
            {
                onScreenValueList.Add(valueList[i]);
                onScreenDatesList.Add(datesList[i]);
                onScreenGamesList.Add(gamesNumberList[i]);
                interations++;
            }
            catch (ArgumentOutOfRangeException e)
            {
              
                continue;
            }
        }

       // startingIndexOfProgressionBars = onScreenValueList

        if (interations < maxBarCountInGraph)
        {
            maxBarCountInGraph = interations;
        }

        if (graphType.Equals("bar_graph"))
        {
            ShowGraph(onScreenValueList, barChartVisual, -1, (int _i) => onScreenDatesList[_i], (float _f) => Mathf.RoundToInt(_f).ToString());
        }
        else
        {
            ShowGraph(onScreenValueList, lineGraphVisual, -1, (int _i) => onScreenDatesList[_i], (float _f) => Mathf.RoundToInt(_f).ToString());
        }

        string calculationTypeText = calculationsTypeOnGraph;
        string valueTypeText = "";
        string intervalTypeText = "";

    

        switch (valueTypeOnGraph)
        {
            case "score": valueTypeText = "achieved score";  break;
            case "tower_hight": valueTypeText = "achieved tower hight"; break;
            case "stars": valueTypeText = "collected stars"; break;
            case "kills": valueTypeText = "killed monsters"; break;
            case "play_time": valueTypeText = "play time"; break;
        }

        switch (intervalTypeOnGraph)
        {
            case "daily": intervalTypeText = "day"; break;
            case "monthly": intervalTypeText = "month"; break;
        }

        graphDescriptionText.text = "Currently shown graph calculates <color=#2bff87>" + calculationTypeText + "</color> value of <color=#ffc72b>" + valueTypeText + "</color> per game throughout a <color=#ff2b75>" + intervalTypeText+"</color>";

    }

    public void RearangeUserGameDataInList( string valueTypeOnGraph, string intervalTypeOnGraph, string calculationsTypeOnGraph)
    {
        userGameDataList = firebaseManager.userGamesDataList;
        //  RearangeUserGameDataInList(valueTypeOnGraph+"_"+ intervalTypeOnGraph+"_"+ calculationsTypeOnGraph);

        valueList = new List<int>();
        datesList = new List<string>();
        gamesNumberList = new List<int>();

        this.valueTypeOnGraph = valueTypeOnGraph;
        this.intervalTypeOnGraph = intervalTypeOnGraph;
        this.calculationsTypeOnGraph = calculationsTypeOnGraph;

        int sumOfScoreInOneTimePeriod = 0;
        int sumOfGames = 0;
        int currentlyCheckedDateTime = 0;

        List<int> selectedValueTypeList = new List<int>();
        List<int> selectedIntervalTypeList = new List<int>();
        List<int> dateYearList = new List<int>();

        string dateToStringConversionParameter = "";

        for (int i = 0; i < userGameDataList.Count; i++)
        {
            switch (valueTypeOnGraph)
            {
                case "score":  selectedValueTypeList.Add(userGameDataList[i].Score);  break;
                case "tower_hight": selectedValueTypeList.Add(userGameDataList[i].Towerhight);  break;
                case "stars": selectedValueTypeList.Add(userGameDataList[i].Stars);  break;
                case "kills": selectedValueTypeList.Add(userGameDataList[i].Kills);  break;
                case "play_time": selectedValueTypeList.Add(userGameDataList[i].PlayTime);  break;
            }

            switch (intervalTypeOnGraph)
            {
                case "daily": selectedIntervalTypeList.Add(userGameDataList[i].GameDateTime.DayOfYear); dateToStringConversionParameter = "dd/MM"; break;
                case "monthly": selectedIntervalTypeList.Add(userGameDataList[i].GameDateTime.Month); dateToStringConversionParameter = "MMMM"; break;
            }

            dateYearList.Add(userGameDataList[i].GameDateTime.Year);

        }

        
        switch (calculationsTypeOnGraph)
        {
            case "average":

                for (int i = 0; i < userGameDataList.Count; i++)
                {

                    if (i == 0)
                    {
                  
                        currentlyCheckedDateTime = selectedIntervalTypeList[i];
                        sumOfScoreInOneTimePeriod += selectedValueTypeList[i];
                        sumOfGames++;
                      
                    }

                    if (i != 0)
                    {

                        if (selectedIntervalTypeList[i].Equals(currentlyCheckedDateTime) && userGameDataList[i].GameDateTime.Year == dateYearList[i -1])
                        {
                            sumOfScoreInOneTimePeriod += selectedValueTypeList[i];
                            sumOfGames++;
                        }
                        else
                        {
                            valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                            datesList.Add(userGameDataList[i - 1].GameDateTime.Date.ToString(dateToStringConversionParameter) +"\n" + userGameDataList[i - 1].GameDateTime.Date.ToString("yyyy"));
                            gamesNumberList.Add(sumOfGames);

                            currentlyCheckedDateTime = selectedIntervalTypeList[i];
                            sumOfScoreInOneTimePeriod = selectedValueTypeList[i];
                            sumOfGames = 1;
                        }
                       
                    }

                    if (i == userGameDataList.Count - 1)
                    {
                        valueList.Add(sumOfScoreInOneTimePeriod / sumOfGames);
                        datesList.Add(userGameDataList[i].GameDateTime.Date.ToString(dateToStringConversionParameter) + "\n" + userGameDataList[i].GameDateTime.Date.ToString("yyyy"));
                        gamesNumberList.Add(sumOfGames);
                    }
                }



                break;

            case "median":

                List<int> valuesInDefinedPeriod = new List<int>();

                for (int i = 0; i < userGameDataList.Count; i++)
                {

                    if (i == 0)
                    {

                        currentlyCheckedDateTime = selectedIntervalTypeList[i];
                        valuesInDefinedPeriod.Add(selectedValueTypeList[i]);
                       // sumOfScoreInOneTimePeriod += selectedValueTypeList[i];
                        // sumOfGames++;

                    }

                    if (i != 0)
                    {

                        if (selectedIntervalTypeList[i].Equals(currentlyCheckedDateTime) && userGameDataList[i].GameDateTime.Year == dateYearList[i - 1])
                        {
                            valuesInDefinedPeriod.Add(selectedValueTypeList[i]);
                            // sumOfScoreInOneTimePeriod += selectedValueTypeList[i];
                            //  sumOfGames++;
                        }
                        else
                        {
                            int median = 0;
                            valuesInDefinedPeriod.Sort();

                            if (valuesInDefinedPeriod.Count % 2 == 0)
                            {
                                median = (valuesInDefinedPeriod[valuesInDefinedPeriod.Count / 2] + valuesInDefinedPeriod[(valuesInDefinedPeriod.Count / 2) - 1]) / 2;
                            }
                            else
                            {
                                median = valuesInDefinedPeriod[valuesInDefinedPeriod.Count / 2];
                            }

                            valueList.Add(median);
                            datesList.Add(userGameDataList[i - 1].GameDateTime.Date.ToString(dateToStringConversionParameter) + "\n" + userGameDataList[i - 1].GameDateTime.Date.ToString("yyyy"));
                            gamesNumberList.Add(valuesInDefinedPeriod.Count);

                            currentlyCheckedDateTime = selectedIntervalTypeList[i];
                            valuesInDefinedPeriod.Clear();
                            valuesInDefinedPeriod.Add(selectedValueTypeList[i]);
                           // sumOfGames = 1;
                        }

                    }

                    if (i == userGameDataList.Count - 1)
                    {
                        int median = 0;
                        valuesInDefinedPeriod.Sort();

                        if (valuesInDefinedPeriod.Count % 2 == 0)
                        {
                            median = (valuesInDefinedPeriod[valuesInDefinedPeriod.Count / 2] + valuesInDefinedPeriod[(valuesInDefinedPeriod.Count / 2) - 1]) / 2;
                        }
                        else
                        {
                            median = valuesInDefinedPeriod[valuesInDefinedPeriod.Count / 2];
                        }

                        valueList.Add(median);
                        datesList.Add(userGameDataList[i].GameDateTime.Date.ToString(dateToStringConversionParameter) + "\n" + userGameDataList[i].GameDateTime.Date.ToString("yyyy"));
                        gamesNumberList.Add(valuesInDefinedPeriod.Count);
                    }
                }



                break;
        }

        if (valueList.Count > 0)
        {
            startingIndexOfProgressionBars = valueList.Count; // pierwszy index z progression bar jężeli bedzie istniał

            //kalkulacje value progresjii... i później dodanie do tablicy


            int numberOfPreviousDaysForForecast = 20;

            if (valueList.Count < numberOfPreviousDaysForForecast)
            {
                numberOfPreviousDaysForForecast = valueList.Count;
            }


            int iterator_x = 1;
            int sumOfxy = 0;
            int sumOfx = 0;
            int sumOfy = 0;
            int sumOfxx = 0;
            int sumOfyy = 0;


            for (int j = valueList.Count - numberOfPreviousDaysForForecast; j < valueList.Count; j++)
            {

                sumOfxy += iterator_x * valueList[j];
                sumOfx += iterator_x;
                sumOfy += valueList[j];
                sumOfxx += iterator_x * iterator_x;
                sumOfyy += valueList[j] * valueList[j];

                iterator_x++;
            }

            float x_ = (float)sumOfx / (float)numberOfPreviousDaysForForecast;
            float y_ = (float)sumOfy / (float)numberOfPreviousDaysForForecast;

           // Debug.Log("x_ = " + x_ + " ; y_ = " + y_ + " ; sumOfxy = " + sumOfxy + " ; sumOfx = " + sumOfx + " ; sumOfy = " + sumOfy + " ; sumOfxx = " + sumOfxx + " ; sumOfyy = " + sumOfyy);
            float b = (sumOfxy - (numberOfPreviousDaysForForecast * x_ * y_)) / (sumOfxx - (numberOfPreviousDaysForForecast * x_ * x_));
            float a = y_ - (b * x_);
         //   Debug.Log("a = " + a + " ; b = " + b);


            if (valueList.Count <= 1)
            {
                b = 0;
                a = valueList[0];
            }

            for (int j = 1; j <= maxProgressionbarsOnScreen; j++)
            {
                int finalValue = (int)(a + (b * (j + numberOfPreviousDaysForForecast)));

                if (finalValue < 0)
                    finalValue = 0;

                valueList.Add(finalValue);

                gamesNumberList.Add(-1);
                switch (intervalTypeOnGraph)
                {
                    case "daily":
                        // tempDateTime = userGameDataList[userGameDataList.Count - 1].GameDateTime.AddDays(i+1);
                        //  datesList.Add(tempDateTime.ToString(dateToStringConversionParameter) + "\n" + tempDateTime.ToString("yyyy"));
                        datesList.Add("<i>future\nday</i>");
                        break;

                    case "monthly":
                        //   tempDateTime = userGameDataList[userGameDataList.Count - 1].GameDateTime.AddMonths(i+1);
                        //   datesList.Add(tempDateTime.ToString(dateToStringConversionParameter) + "\n" + tempDateTime.ToString("yyyy"));
                        datesList.Add("<i>future\nmonth</i>");
                        break;
                }
            }

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

        if ( isLoadingUserDataInProcess)
        {
            if (firebaseManager.isUserDataListLoaded)
            {
                //write data to chart || refresch chart with new data
                currentGraphType = "bar_graph";

                intervalTypeOnGraph = "daily";
                valueTypeOnGraph = "score";
                calculationsTypeOnGraph = "average";

                RearangeUserGameDataInList(valueTypeOnGraph, intervalTypeOnGraph, calculationsTypeOnGraph);
              
                LoadGrapgWithData(currentGraphType);

                isLoadingUserDataInProcess = false;
                firebaseManager.isUserDataListLoaded = false;
              //  Debug.Log("Wczytalo graf...");
            }
        }

    }

    public void ChangeValueType()
    {
        if (valueTypeOnGraph.Equals("score"))
        {
            valueTypeOnGraph = "tower_hight";
        }
        else if (valueTypeOnGraph.Equals("tower_hight"))
        {
            valueTypeOnGraph = "stars";
        }
        else if (valueTypeOnGraph.Equals("stars"))
        {
            valueTypeOnGraph = "kills";
        }
        else if (valueTypeOnGraph.Equals("kills"))
        {
            valueTypeOnGraph = "play_time";
        }
        else if (valueTypeOnGraph.Equals("play_time"))
        {
            valueTypeOnGraph = "score";
        }

        RearangeUserGameDataInList(valueTypeOnGraph, intervalTypeOnGraph, calculationsTypeOnGraph);
        LoadGrapgWithData(currentGraphType);

    }

    public void ChangeCalculationsType()
    {
        if (calculationsTypeOnGraph.Equals("average"))
        {
            calculationsTypeOnGraph = "median";
        }
        else
        {
            calculationsTypeOnGraph = "average";
        }

        RearangeUserGameDataInList(valueTypeOnGraph, intervalTypeOnGraph, calculationsTypeOnGraph);
        LoadGrapgWithData(currentGraphType);
    }


    public void IncreaseMaxOnScreenValuesCount()
    {
          maxBarCountInGraph++;
          if (maxBarCountInGraph >= 30)
              maxBarCountInGraph = 30;

          LoadGrapgWithData(currentGraphType);
       // IncreaseVisibleAmount();
    }

    public void DecreaseMaxOnScreenValuesCount()
    {
         maxBarCountInGraph--;
         if (maxBarCountInGraph <= 2)
             maxBarCountInGraph = 2;

         LoadGrapgWithData(currentGraphType);
       // DecreaseVisibleAmount();
    }

    public void SwapIntervalOfGraph()
    {
        startingOnScreenValueIndex = 0;
        maxBarCountInGraph = 20;

        if (intervalTypeOnGraph.Equals("daily"))
        {
            RearangeUserGameDataInList(valueTypeOnGraph, "monthly", calculationsTypeOnGraph);
        }
        else if (intervalTypeOnGraph.Equals("monthly"))
        {
            RearangeUserGameDataInList(valueTypeOnGraph, "daily", calculationsTypeOnGraph);
        }

        LoadGrapgWithData(currentGraphType);
    }

    public static void ShowTooltip_Static(string tooltipText, Vector2 anchoredPosition, float tooltipDistance, GameObject barGameObject) {
        instance.ShowTooltip(tooltipText, anchoredPosition, tooltipDistance, barGameObject);
    }

    private void ShowTooltip(string tooltipText, Vector2 anchoredPosition, float tooltipDistance, GameObject barGameObject) {
        // Show Tooltip GameObject
        tooltipGameObject.SetActive(true);

        tooltipGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        TextMeshProUGUI tooltipUIText = tooltipGameObject.transform.Find("text").GetComponent<TextMeshProUGUI>();
        tooltipUIText.text = "<mark=#000000 padding='40, 40, 20, 20'><font=\"Oswald Bold SDF\">" + tooltipText + "</font></mark >";

        tooltipGameObject.transform.Find("text").GetComponent<RectTransform>().anchoredPosition = new Vector3(0,tooltipDistance, 0);
        tooltipGameObject.transform.SetAsLastSibling();

        barGameObject.GetComponent<Image>().material = graphGlowMaterial;

    }

    public static void HideTooltip_Static(GameObject barGameObject) {
        instance.HideTooltip(barGameObject);
    }

    private void HideTooltip(GameObject barGameObject) {
        tooltipGameObject.SetActive(false);

        if (barGameObject != null)
            barGameObject.GetComponent<Image>().material = null;
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
       // yLabelList.Clear();

        foreach (IGraphVisualObject graphVisualObject in graphVisualObjectList) {
          //  try
          //  {
           //     Debug.Log(graphVisualObject);
                graphVisualObject.CleanUp();
           // } catch (NullReferenceException e)
           // {
           //     continue;
           // }
        }
        graphVisualObjectList.Clear();

        graphVisual.CleanUp();

        // Grab the width and height from the container
        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = 0;
        float yMinimum = 0;

        try
        {
            // Identify y Min and Max values
            yMaximum = valueList[0];
            yMinimum = valueList[0];
            notEnoughtDataText.gameObject.SetActive(false);
            for (int i = 0; i < btnGameObj.Length; i++)
            {
                btnGameObj[i].GetComponent<Button>().interactable = true;
            }
        } catch (ArgumentOutOfRangeException e)
        {
            notEnoughtDataText.gameObject.SetActive(true);
            notEnoughtDataText.text = "Not enought data to draw graph...";
            for (int i = 0; i < btnGameObj.Length; i++)
            {
                btnGameObj[i].GetComponent<Button>().interactable = false;
            }
            return;
        }

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
            yDifference = yMaximum ;
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

            string valueTypeText = "";
            
            switch (valueTypeOnGraph)
            {
                case "score": valueTypeText = " score points based on ";  break;
                case "tower_hight": valueTypeText = " m of tower high based on "; break;
                case "stars": valueTypeText = " collected stars based on "; break;
                case "kills": valueTypeText = " killed monsters based on "; break;
                case "play_time": valueTypeText = " s of play time based on "; break;
            }

            // Add data point visual
            string tooltipText = "";
            if (onScreenGamesList[i] >= 0)
            {
                tooltipText = getAxisLabelY(valueList[i]) + valueTypeText + onScreenGamesList[i] + " games";
            }
            else
            {
                tooltipText = getAxisLabelY(valueList[i]) + valueTypeText + " data from past games";
            }//graphVisualObjectList.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, tooltipText));
            bool inValueListIndex = (this.valueList.Count + startingOnScreenValueIndex - maxBarCountInGraph - maxProgressionbarsOnScreen + i) >= startingIndexOfProgressionBars;

            IGraphVisualObject graphVisualObject = graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize, tooltipText, inValueListIndex);
            graphVisualObjectList.Add(graphVisualObject);

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

            if (i == valueList.Count - 1)
            {
                 xPosition = xSize + xIndex * xSize;
                 yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
                // Duplicate the x dash template
                dashX = Instantiate(dashTemplateX);
                dashX.SetParent(dashContainer, false);
                dashX.gameObject.SetActive(true);
                dashX.anchoredPosition = new Vector2(xPosition, -3f);
                gameObjectList.Add(dashX.gameObject);
            }
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
    private interface IGraphVisual {

        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText, bool inValueListIndex);
        void CleanUp();
    }

    /*
     * Represents a single Visual Object in the graph
     * */
    private interface IGraphVisualObject {

        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth, string tooltipText);
        void CleanUp();

    }


    /*
     * Displays data points as a Bar Chart
     * */
    private class BarChartVisual : IGraphVisual {

        private RectTransform graphContainer;
        private Color barColor;
        private float barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier) {
            this.graphContainer = graphContainer;
            this.barColor = barColor;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public void CleanUp()
        {
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText, bool isProgressionBar) {
            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth, isProgressionBar);

            BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, barWidthMultiplier);
            barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth, tooltipText);

            

            return barChartVisualObject;
        }

        private GameObject CreateBar(Vector2 graphPosition, float barWidth, bool isProgressionBar) {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = barColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);

            if (isProgressionBar)
            {
                gameObject.GetComponent<Image>().color = new Color(1,1,1, 0.5f);
            }
            
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
                    ShowTooltip_Static(tooltipText, graphPosition, 4.0f, barGameObject);
                };

                // Hide Tooltip on Mouse Out
                barButtonUI.MouseOutOnceFunc = () => {
                    HideTooltip_Static(barGameObject);
                };
            }

            public void CleanUp() {
             //   Debug.Log(barGameObject);
                Destroy(barGameObject);
            }


        }

    }


    /*
     * Displays data points as a Line Graph
     * */
    private class LineGraphVisual : IGraphVisual {

        private RectTransform graphContainer;
        private Sprite dotSprite;
        private LineGraphVisualObject lastLineGraphVisualObject;
        private Color dotColor;
        private Color dotConnectionColor;

        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectionColor) {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConnectionColor = dotConnectionColor;
            lastLineGraphVisualObject = null;
        }

        public void CleanUp()
        {
            lastLineGraphVisualObject = null;
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth, string tooltipText, bool isProgressionBar)
        {
            GameObject dotGameObject = CreateDot(graphPosition, isProgressionBar);


            GameObject dotConnectionGameObject = null;
            if (lastLineGraphVisualObject != null)
            {
                dotConnectionGameObject = CreateDotConnection(lastLineGraphVisualObject.GetGraphPosition(), dotGameObject.GetComponent<RectTransform>().anchoredPosition, isProgressionBar);
            }

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
            gameObject.transform.localScale = new Vector3(1.8f, 1.8f, 1);
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);

            if (isProgressionBar)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            // Add Button_UI Component which captures UI Mouse Events
            Button_UI dotButtonUI = gameObject.AddComponent<Button_UI>();

            return gameObject;
        }

        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, bool isProgressionBar) {
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
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.25f);
            }

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

