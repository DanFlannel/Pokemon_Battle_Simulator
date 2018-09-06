using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    //Singleton
    private static PageManager instance;

    public static PageManager Instance { get { return instance; } }

    [Header("Page Information")]
    public BasePage currentPage;

    public List<BasePage> pages;
    public int index;
    public bool canTimeout;

    private float baseTimer;
    public float timer;
    private float timerMultiplier = 1f;

    public int curPageIndex { get { return getCurrentPageIndex(); } }

    private bool nextPageFlag;
    private bool previousPageFlag;
    private bool reloadPageFlag;

    private Dictionary<string, int> pageDict = new Dictionary<string, int>();

    //.. Unity Methods

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
        }
    }

    private void Start()
    {
        loadAllPages();
        switchToPage(0);
    }

    private void Update()
    {
        checkFlag();
        index = curPageIndex;
        countdownTimer();
        currentPage.doUpdate();
        if (Input.anyKey)
        {
            resetTimer();
        }
    }

    private void FixedUpdate()
    {
        currentPage.doFixedUpdate();
    }

    private void LateUpdate()
    {
        currentPage.doLateUpdate();
    }

    //.. Public switch pages

    public void nextPage()
    {
        nextPageFlag = true;
    }

    public void previousPage()
    {
        previousPageFlag = true;
    }

    public void reloadPreviousPage()
    {
        reloadPageFlag = true;
    }

    public void loadPage(string identifier)
    {
        if (!pageDict.ContainsKey(identifier))
        {
            Debug.LogWarning(string.Format("Cannot find page with identifier {0}", identifier));
            return;
        }

        if (pageDict[identifier] == curPageIndex) { return; }
        switchToPage(pageDict[identifier]);
    }

    public int GetPageIndex(string identifier)
    {
        if (!pageDict.ContainsKey(identifier))
        {
            Debug.LogWarning(string.Format("Cannot find page with identifier {0}", identifier));
            return -1;
        }
        return pageDict[identifier];
    }

    //.. Internal switch pages

    private void switchToPage(int index)
    {
        if (currentPage == null)
        {
            currentPage = pages[index];
            currentPage.Open();
            baseTimer = currentPage.baseTimer * timerMultiplier;
            timer = baseTimer;
            return;
        }

        if (index >= pages.Count)
        {
            Debug.Log("Index too high");
            index = pages.Count - 1;
        }

        BasePage prevPage = currentPage;
        currentPage = pages[index];

        currentPage.Open();
        prevPage.Close();

        baseTimer = currentPage.baseTimer * timerMultiplier;
        timer = baseTimer;
    }

    private void internalNextPage()
    {
        int index = getCurrentPageIndex();
        index++;
        switchToPage(index);
    }

    private void internalPreviousPage()
    {
        int index = getCurrentPageIndex();
        index--;
        switchToPage(index);
    }

    private void internalReloadPreviousPage()
    {
        int index = getCurrentPageIndex();
        index--;
        BasePage prevPage = currentPage;
        currentPage = pages[index];

        currentPage.Reload();
        prevPage.Close();

        baseTimer = currentPage.baseTimer * timerMultiplier;
        timer = baseTimer;
    }

    public void goToHomePage()
    {
        switchToPage(0);
    }

    //.. Page Helpers

    private void checkFlag()
    {
        if (nextPageFlag)
        {
            nextPageFlag = false;
            internalNextPage();
        }

        if (previousPageFlag)
        {
            previousPageFlag = false;
            internalPreviousPage();
        }

        if (reloadPageFlag)
        {
            reloadPageFlag = false;
            internalReloadPreviousPage();
        }
    }

    private int getCurrentPageIndex()
    {
        if (currentPage == null)
        {
            //Debug.LogError("No current page assigned");
            return -1;
        }
        for (int i = 0; i < pages.Count; i++)
        {
            if (currentPage == pages[i])
            {
                return i;
            }
        }
        Debug.LogError("No matching for current page and page array");
        return -1;
    }

    private void loadAllPages()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].Initalize();
            pages[i].root.SetActive(true);
            pages[i].enableObjects(false);
            if (!pageDict.ContainsKey(pages[i].name))
            {
                pageDict.Add(pages[i].name, i);
            }
            else
            {
                Debug.LogWarning("Need a unqiue name for each page for some features");
            }
        }
    }

    //.. Timers

    public void resetTimer()
    {
        baseTimer = currentPage.baseTimer * timerMultiplier;
        timer = baseTimer;
    }

    private void countdownTimer()
    {
        if (!canTimeout) { return; }

        if (currentPage == pages[0])
        {
            return;
        }

        timer = (timer - Time.deltaTime > 0) ? timer - Time.deltaTime : 0;

        if (timer <= 0)
        {
            goToHomePage();
        }
    }
}