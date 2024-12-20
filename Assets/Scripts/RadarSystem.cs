using UnityEngine;
using HWRcomponent;
using System.Collections;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;


namespace HWRcomponent
{
    public enum Alignment
    {
        None,
        LeftTop,
        RightTop,
        LeftBot,
        RightBot,
        MiddleTop,
        MiddleBot
    }

}
public class RadarSystem : MonoBehaviour
{

    private Vector2 inposition;
    public float Size = 400; //  minimap size
    public float Distance = 100;// maximum distance of game objects
    public float Transparency = 0.5f;// Texture Transparency
    public Texture2D[] Navtexture;// textutes list
    public string[] EnemyTag;// object tags list
    public Texture2D NavCompass;// compass texture
    public Texture2D CompassBackground;// background texture
    public Vector2 PositionOffset = new Vector2(0, 0);// minimap position offset
    public float Scale = 1;// mini map scale ( Scale < 1 = zoom in , Scale > 1 = zoom out)
    public Alignment PositionAlignment = Alignment.None;// position alignment
    public bool MapRotation;
    public GameObject Player;
    public bool Show = true;
    public Color ColorMult = Color.white;
    public Image navback, player, enemy;
    void Start()
    {

    }

    void Update()
    {
        //if (!Player)
        //{
        //    Player = this.gameObject;
        //}

        if (Scale <= 0)
        {
            Scale = 100;
        }
        // define the position
        switch (PositionAlignment)
        {
            case Alignment.None:
                inposition = PositionOffset;
                break;
            case Alignment.LeftTop:
                inposition = Vector2.zero + PositionOffset;
                break;
            case Alignment.RightTop:
                inposition = new Vector2(Screen.width - Size, 0) + PositionOffset;
                break;
            case Alignment.LeftBot:
                inposition = new Vector2(0, Screen.height - Size) + PositionOffset;
                break;
            case Alignment.RightBot:
                inposition = new Vector2(Screen.width - Size, Screen.height - Size) + PositionOffset;
                break;
            case Alignment.MiddleTop:
                inposition = new Vector2((Screen.width / 2) - (Size / 2), Size) + PositionOffset;
                break;
            case Alignment.MiddleBot:
                inposition = new Vector2((Screen.width / 2) - (Size / 2), Screen.height - Size) + PositionOffset;
                break;
        }

    }
    // convert 3D position to 2D position
    Vector2 ConvertToNavPosition(Vector3 pos)
    {
        Vector2 res = Vector2.zero;
        if (Player)
        {
            res.x = inposition.x + (((pos.x - Player.transform.position.x * 20) + (Size * Scale) / 2f) / Scale);
            res.y = inposition.y + ((-(pos.z - Player.transform.position.z * 20) + (Size * Scale) / 2f) / Scale);
        }
        return res;
    }

    void DrawNav(GameObject[] enemylists, Texture2D navtexture)
    {
        if (Player)
        {
            for (int i = 0; i < enemylists.Length; i++)
            {
                if (Vector3.Distance(Player.transform.position, enemylists[i].transform.position) <= (Distance * Scale))
                {
                    Vector2 pos = ConvertToNavPosition(enemylists[i].transform.position);

                    if (Vector2.Distance(pos, (inposition + new Vector2(Size / 2f, Size / 2f))) + (navtexture.width / 2) < (Size / 2f))
                    {
                        float navscale = Scale;
                        if (navscale < 1)
                        {
                            navscale = 1;
                        }
                        GUI.DrawTexture(new Rect(pos.x - (navtexture.width / navscale) / 2, pos.y - (navtexture.height / navscale) / 2, navtexture.width / navscale, navtexture.height / navscale), navtexture);
                        //enemy.rectTransform.position = new Vector3(pos.x - (navtexture.width / navscale) / 2, 1, pos.y - (navtexture.height / navscale) / 2);
                        //enemy.rectTransform.sizeDelta = new Vector2(navtexture.width / navscale, navtexture.height / navscale);
                        Debug.Log(pos.x - (navtexture.width / navscale) / 2 + " " + (pos.y - (navtexture.height / navscale) / 2));
                    }
                }
            }
        }
    }

    float[] list;

    void OnGUI()
    {
        if (!Show)
            return;

        GUI.color = new Color(ColorMult.r, ColorMult.g, ColorMult.b, Transparency);
        if (MapRotation)
        {
            GUIUtility.RotateAroundPivot(-(this.transform.eulerAngles.y), inposition + new Vector2(Size / 2f, Size / 2f));
        }

        for (int i = 0; i < EnemyTag.Length; i++)
        {
            DrawNav(GameObject.FindGameObjectsWithTag(EnemyTag[i]), Navtexture[i]);
        }
        if (CompassBackground)
            GUI.DrawTexture(new Rect(inposition.x, inposition.y, Size, Size), CompassBackground);
        //navback.rectTransform.position = new Vector3(inposition.x, 1, inposition.y);
        //navback.rectTransform.sizeDelta = new Vector2(Size, Size);
        GUIUtility.RotateAroundPivot((this.transform.eulerAngles.y), inposition + new Vector2(Size / 2f, Size / 2f));
        if (NavCompass)
            //{
            //player.rectTransform.position = new Vector3(inposition.x + (Size / 2f) - (NavCompass.width / 2f), 1, 1);
            //player.rectTransform.sizeDelta = new Vector2(NavCompass.width, NavCompass.height);
            //}
            GUI.DrawTexture(new Rect(inposition.x + (Size / 2f) - (NavCompass.width / 2f), inposition.y + (Size / 2f) - (NavCompass.height / 2f), NavCompass.width, NavCompass.height), NavCompass);

    }
}
