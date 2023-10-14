using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapManager : Singleton<MapManager>
{
    /// <summary>
    /// 共有几层地图
    /// </summary>
    [SerializeField] int maxLevel;
    /// <summary>
    /// 每层有几个可选房间
    /// </summary>
    [SerializeField] int maxCount;
    public int iSeed;
    public System.Random random;
    MapNode[][] mapNodes;
    [SerializeField] int leftPadding;
    [SerializeField] int paddingX;
    [SerializeField] int paddingY;
    [SerializeField] EveryLevel[] levels;

    public Transform lineParent;

    public GameObject currentPlace;
    public int currentLevel=0;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        random = new System.Random(iSeed);
        mapNodes = new MapNode[maxLevel][];
        int count = 0;
        for (int i = 0; i < mapNodes.Length; i++)
        {
            mapNodes[i] = levels[i].nodes;
            for (int j = 0; j < mapNodes[0].Length; j++)
            {
                mapNodes[i][j].level = i;
                mapNodes[i][j].value = count++;
            }
      
           
        }
        
        CreateMap();
        // LogTrue();
        // SerTree();
        ShowRooms();
        SetLevelsPosition();
        ShowLine();
        
        
        this.gameObject.SetActive(false);
    }

    void CreateMap()
    {
        CreateTheFirstLevel();
        for (int i = 0; i < maxLevel - 1; i++)
        {
            ProcessThisLevel(i);
        }
        
        
    }

  
    void ProcessThisLevel(int currentLevel)
    {
        
        MapNode beUsedNode;
        bool canLeft = true;
        bool canRight = true;
        int firstNum = 0;
        int secondNum = 0;
        int nextLevelLeftPoint = 0;
        
        
        
        for (int i = 0; i < maxCount; i++)
        {
         
            if (mapNodes[currentLevel][i].isUsed)
            {
                if (mapNodes[currentLevel + 1][i].isUsed || nextLevelLeftPoint >= i)
                {
                    canLeft = false;
                }

                if (i >= maxCount - 1 || nextLevelLeftPoint >= maxCount - 1)
                {
                    canRight = false;
                }

                if (i == 0)
                {
                    canLeft = false;
                }

                if (canLeft && canRight)
                {
                    firstNum = random.Next(3);
                    secondNum = random.Next(3);
                }
                else if (canRight || canLeft)
                {
                    firstNum = random.Next(2);
                    secondNum = random.Next(2);
                    
                }

                //确保firstNum <= secondNum
                if (firstNum > secondNum)
                {
                    firstNum = firstNum ^ secondNum;
                    secondNum = firstNum ^ secondNum;
                    firstNum = firstNum ^ secondNum;
                }

                beUsedNode = mapNodes[currentLevel + 1][nextLevelLeftPoint + firstNum];
                beUsedNode.isUsed = true;
                mapNodes[currentLevel][i].left = beUsedNode;
                beUsedNode = mapNodes[currentLevel + 1][nextLevelLeftPoint + secondNum];
                beUsedNode.isUsed = true;
                mapNodes[currentLevel][i].right = beUsedNode;
                nextLevelLeftPoint += secondNum;
                //如果左右连接的都是同一个节点，置空右节点
                mapNodes[currentLevel][i].right = mapNodes[currentLevel][i].left == mapNodes[currentLevel][i].right
                    ? null
                    : mapNodes[currentLevel][i].right;
                firstNum = 0;
                secondNum = 0;
                canLeft = true;
                canRight = true;
            }
            // else
            // {
            //     mapNodes[currentLevel][i] = null;
            // }
            
        }
    }


    void CreateTheFirstLevel()
    {
        // int thisLevelCount = random.Next(maxCount);
        int thisLevelCount = 3;//Random.Range(3,6);
        for (int i = 0; i < thisLevelCount; i++)
        {
            mapNodes[0][random.Next(maxCount)].isUsed = true;
        }
    }
 
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }


    public void ShowRooms()
    {
        for (int i = 0; i < mapNodes.Length; i++)
        {
            for (int j = 0; j < mapNodes[0].Length; j++)
            {
                mapNodes[i][j].gameObject.SetActive(mapNodes[i][j].isUsed);
                if (i==mapNodes.Length-1)
                {
                    mapNodes[i][j].roomType = RoomType.BossRoom;
                    mapNodes[i][j].GetComponent<Button>().image.sprite = mapNodes[i][j].RoomSprite[3];
                }
                else
                {
                    mapNodes[i][j].RandomRoomType();
                }

                // mapNodes[i][j].GetComponent<Text>().text = mapNodes[i][j].roomType.ToString();
            }
        }
    }
    public void SetLevelsPosition()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int x = i * 100 + paddingX + leftPadding;
            int y = paddingY;
            levels[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            levels[i].SetRoomsPosition(random);
        }
    }
    public void ShowLine()
    {
        for (int i = 0; i < mapNodes.Length; i++)
        {
            for (int j = 0; j < mapNodes[0].Length; j++)
            {
                if (mapNodes[i][j].isUsed)
                {
                    if (mapNodes[i][j].left != null)
                    {
                        GameObject line = new GameObject();
                        line.transform.SetParent(lineParent);
                        LineRenderer ren = line.AddComponent<LineRenderer>();
                        Vector3[] current = { mapNodes[i][j].transform.position, mapNodes[i][j].left.transform.position };
                        ren.SetPositions(current);
                        ren.startWidth = 0.05f;
                        ren.startColor=Color.gray;
                        ren.endWidth = 0.05f;
                        ren.endColor=Color.gray;
                    }
                    if (mapNodes[i][j].right != null)
                    {
                        GameObject line = new GameObject();
                        line.transform.SetParent(lineParent);
                        LineRenderer ren = line.AddComponent<LineRenderer>();
                        Vector3[] current = { mapNodes[i][j].transform.position, mapNodes[i][j].right.transform.position };
                        ren.SetPositions(current);
                        ren.startWidth = 0.05f;
                        ren.startColor=Color.gray;
                        ren.endWidth = 0.05f;
                        ren.endColor=Color.gray;
                    }
                }
            }
        }
    }

    public void setFirstButton()
    {
        if(currentLevel==0)
        {
            for (int i = 0; i < mapNodes[currentLevel].Length; i++)
            {
                if (mapNodes[currentLevel][i].isUsed==true)
                {
                    mapNodes[currentLevel][i].ifCanBeClick = true;
                }
            }
             currentPlace.SetActive(false);
        }
       
    }

    public void setActiveButtonTrue(MapNode mapNode)
    {
       Debug.Log("set button");
            if (mapNode.left!=null)
            {
                mapNode.left.ifCanBeClick = true;
            }

            if (mapNode.right!=null)
            {
                mapNode.right.ifCanBeClick = true;
            }

            if (currentLevel>0)
            {
                for (int i = 0; i < mapNodes[currentLevel-1].Length; i++)
                {
                    if (mapNodes[currentLevel-1][i].isUsed == true)
                    {
                        mapNodes[currentLevel-1][i].ifCanBeClick = false;
                    }
                }
            }
            
    }

    public void SetCurrentPlace(Vector3 position)
    {
        StartCoroutine(MoveObjectToDestination(currentPlace.transform.position,position));
        
    }
    
    public float moveDuration = 1.0f; // 移动的时间（秒）

    
    private IEnumerator MoveObjectToDestination(Vector3 startTransform,Vector3 endTransform)
    {
        float journeyLength = Vector3.Distance(startTransform, endTransform);
        float startTime = Time.time;

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * (journeyLength / moveDuration);
            float fractionOfJourney = distanceCovered / journeyLength;

            // 移动物体
            currentPlace.transform.position = Vector3.Lerp(startTransform, endTransform, fractionOfJourney);

            // 如果移动完成，退出协程
            if (fractionOfJourney >= 1.0f)
            {
                // 移动完成后的处理
                Debug.Log("移动完成！");
                yield break;
            }

            yield return null;
        }
    }
}