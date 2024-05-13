using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SnapScrollHandleryears : MonoBehaviour
{
    public static SnapScrollHandleryears instance;
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;
    public VerticalLayoutGroup VLG;
    //public TMP_Text NameLabel;
    public GameObject[] ItemNames;
    bool isSnapped;
    public float snapforce, snapSpeed;
    public int yearsIndex; public string selectedday;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
    }
    // Update is called once per frame
    void Update()
    {
         yearsIndex = Mathf.RoundToInt((0 + contentPanel.localPosition.y / (sampleListItem.rect.height + VLG.spacing)));
      //  print(currentItem);
        if (scrollRect.velocity.magnitude < 200&&!isSnapped)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapforce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                contentPanel.localPosition.x,
                Mathf.MoveTowards(contentPanel.localPosition.y,  0 + (yearsIndex * (sampleListItem.rect.height + VLG.spacing)),snapSpeed),
                contentPanel.localPosition.z);

            if(contentPanel.localPosition.y == 0 + (yearsIndex * (sampleListItem.rect.height + VLG.spacing)))
            {

                 isSnapped=true;
            }
        }
        if (scrollRect.velocity.magnitude > 200)
        {   
            isSnapped = false;
            snapSpeed = 0;
        } 
    }
    public void CurrentDaySelected()
    {
        if (yearsIndex > ItemNames.Length)
        {
            yearsIndex = ItemNames.Length;
        }
        print("current Day - " + ItemNames[yearsIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        selectedday = ItemNames[yearsIndex].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
}
