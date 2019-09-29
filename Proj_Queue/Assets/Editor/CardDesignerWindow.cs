using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


public class CardDesignerWindow : EditorWindow
{
    //Sections Rect
    private Rect _headerSection;
    private Rect _bodySection;
    
    //Textures for sections
    private Texture2D _headerSectionTexture;
    private Texture2D _bodySectionTexture;
    
    //Color for sections
    readonly Color _headerSectionColor = Color.gray;

    //Card data and actions references
    private static CardData _cardData;
    private static List<ActionData> _actionDataList = new List<ActionData>();
    
    //GUI skin for editor window
    private GUISkin _skin;

    //Setting the parameters of the window 
    [MenuItem("Window/Card Designer")]
    static void OpenMenu()
    {
        CardDesignerWindow window = (CardDesignerWindow) GetWindow(typeof(CardDesignerWindow));
        window.minSize = new Vector2(600,300); 
        window.Show();
    }
    
    //Init all data and textures
    private void OnEnable()
    {
        InitData();
        InitTexture();
        
        _skin = Resources.Load<GUISkin>("GUIstyle/CardDesignerSkin");
    }
    
    
    //Creating the instance of data
    private static void InitData()
    {
        _cardData = (CardData) ScriptableObject.CreateInstance(typeof(CardData));
        _actionDataList.Add((ActionData) ScriptableObject.CreateInstance(typeof(ActionData)));
    }
    
    //Creating the textures
    void InitTexture()
    {
        _headerSectionTexture = new Texture2D(1,1);
        _headerSectionTexture .SetPixel(0,0, _headerSectionColor);
        _headerSectionTexture.Apply();
        
        _bodySectionTexture = Resources.Load<Texture2D>("Icons/gradient");
    }
    
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawBody();
    }
    
    //Setting the layouts and drawing the textures
    void DrawLayouts()
    {
        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = Screen.width;
        _headerSection.height = 40;

        _bodySection.x = 0;
        _bodySection.y = _headerSection.height;
        _bodySection.width = Screen.width;
        _bodySection.height = Screen.height - _headerSection.height;
        

        GUI.DrawTexture(_headerSection, _headerSectionTexture);
        GUI.DrawTexture(_bodySection, _bodySectionTexture);
    }
    
    void DrawHeader()
    {
        GUILayout.BeginArea(_headerSection);
        
        GUILayout.Label("Card Designer", _skin.GetStyle("Header"));
        
        GUILayout.EndArea();
    }

    void DrawBody()
    {
        GUILayout.BeginArea(_bodySection);
        
        GUILayout.Label("New Card");
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card name: ");
        _cardData.cardName = EditorGUILayout.TextField(_cardData.cardName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card description: ");
        EditorGUILayout.EndHorizontal();
        _cardData.cardDescription = EditorGUILayout.TextArea(_cardData.cardDescription);
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Actions");
        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Create new action", GUILayout.Height(40)))
        {
            _actionDataList.Add((ActionData) ScriptableObject.CreateInstance(typeof(ActionData)));
        }

        for (int i = 0; i < _actionDataList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Action Name:");
            _actionDataList[i].actionName = EditorGUILayout.TextField(_actionDataList[i].actionName);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Behaviour");
            _actionDataList[i].behaviours =
                (BehaviourData) EditorGUILayout.ObjectField(_actionDataList[i].behaviours, typeof(BehaviourData), false);
            GUILayout.Label("Pattern");
            _actionDataList[i].patterns =
                (PatternData) EditorGUILayout.ObjectField(_actionDataList[i].patterns, typeof(PatternData), false);

            if (GUILayout.Button("-", GUILayout.Height(20)))
            {
                if (_actionDataList.Count > 1)
                {
                    _actionDataList.Remove(_actionDataList[i]);
                    break;
                }
            }

            if (_actionDataList.Count >= 2)
            {
                EditorGUILayout.BeginVertical();
                if (GUILayout.Button("^", GUILayout.Height(20)))
                {
                    if (i > 0)
                    {
                        ActionData temp = _actionDataList[i];
                        _actionDataList.RemoveAt(i);
                        _actionDataList.Insert( i - 1, temp);
                        break;
                    }
                }
                else if (GUILayout.Button("v", GUILayout.Height(20)))
                {
                    if (i < _actionDataList.Count - 1)
                    {
                        ActionData temp = _actionDataList[i];
                        _actionDataList.RemoveAt(i);
                        _actionDataList.Insert( i + 1, temp);
                        break;
                    }
                }
                EditorGUILayout.EndVertical();
            }
            
            
            
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new behaviour", GUILayout.Height(20)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingType.BEHAVIOUR);
        }
        if (GUILayout.Button("Create new pattern", GUILayout.Height(20)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingType.PATTERN);
        }
        EditorGUILayout.EndHorizontal();

        bool canSave = true;

        foreach (var action in _actionDataList)
        {
            if (action.behaviours == null || action.patterns == null)
            {
                canSave = false;
                break;
            }
        }

        if (!canSave)
        {
            EditorGUILayout.HelpBox("All actions needs a [Behaviour] and [Pattern] before it can be created", MessageType.Warning);
        }
        else if (string.IsNullOrEmpty(_cardData.cardName) || string.IsNullOrEmpty(_cardData.cardDescription))
        {
            EditorGUILayout.HelpBox("Cards needs a [Card name] and [Card description] before it can be created", MessageType.Warning);
        }
        else if (GUILayout.Button("Save card", GUILayout.Height(20)))
        {
            SaveCard();
        }
        
        GUILayout.EndArea();
    }

    void SaveCard()
    {
        //Default path for saving action data 
        string actionDataPath = "Assets/Resources/CardData/Data/Actions/";
        //Default path for saving card data 
        string cardDataPath = "Assets/Resources/CardData/Data/Cards/";

        //Saving every new action data inside of list of actions
        foreach (var action in _actionDataList)
        {
            string newActionPath = actionDataPath;
            newActionPath += action.actionName + ".asset";
            AssetDatabase.CreateAsset(action, newActionPath);
        }

        //Saving new card data
        _cardData.actions = _actionDataList;
        cardDataPath += _cardData.cardName + ".asset";
        AssetDatabase.CreateAsset(_cardData, cardDataPath);
    }
}

public class GeneralSettings : EditorWindow
{
    //Enum for new Scriptable Object Setting
    public enum SettingType
    {
        BEHAVIOUR,
        PATTERN
    }

    //Enum for new Behaviour type
    private enum BehaviourType
    {
        DAMAGE,
        POISON
    }

    private static GeneralSettings _window;
    private static SettingType _dataSetting;
    private static BehaviourType _behaviourType;

    //Behaviours data references
    private static DamageBehaviourData _damageBehaviourData;
    private static PoisonBehaviourData _poisonBehaviourData;
    
    //Setting the parameters of the window 
    public static void OpenWindow(SettingType type)
    {
        _dataSetting = type;
        _window = (GeneralSettings) GetWindow(typeof(GeneralSettings));
        _window.minSize = new Vector2(250,200);
        _window.Show();
    }

    private void OnEnable()
    {
        InitData();
    }
    
    void InitData()
    {
        //Creating the instances of behaviour data scriptable objects
        _damageBehaviourData = (DamageBehaviourData) ScriptableObject.CreateInstance(typeof(DamageBehaviourData));
        _poisonBehaviourData = (PoisonBehaviourData) ScriptableObject.CreateInstance(typeof(PoisonBehaviourData));
    }

    private void OnGUI()
    {
        //Showing different parameters depends on the type of settings
        switch (_dataSetting)
        {
            case SettingType.BEHAVIOUR:
                DrawBehaviourSettings();
                break;
            
            case SettingType.PATTERN:
                DrawPatternSettings();
                break;
        }
    }

    void DrawBehaviourSettings()
    {
        //Choosing the type of behaviour
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Behaviour type");
        _behaviourType = (BehaviourType) EditorGUILayout.EnumPopup(_behaviourType);
        EditorGUILayout.EndHorizontal();

        //Showing different parameters depends on the type of behaviour
        switch (_behaviourType)
        {
            case BehaviourType.DAMAGE:
                
                //Damage amount
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Damage");
                _damageBehaviourData.damage = EditorGUILayout.IntField(_damageBehaviourData.damage);
                EditorGUILayout.EndHorizontal();
                
                //Checking if can be saved
                if (_damageBehaviourData.damage == 0)
                {
                    EditorGUILayout.HelpBox("This behaviour needs a [Damage] before it can be created", MessageType.Warning);
                }
                else if (GUILayout.Button("Save",GUILayout.Height(30)))
                {
                    SaveBehaviourData();
                    _window.Close();
                }
                break;
            
            case BehaviourType.POISON:
                
                //Damage amount
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Damage");
                _poisonBehaviourData.damage = EditorGUILayout.IntField(_poisonBehaviourData.damage);
                EditorGUILayout.EndHorizontal(); 
                
                //How many turns it will keeps
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Turns");
                _poisonBehaviourData.turns = EditorGUILayout.IntField(_poisonBehaviourData.turns);
                EditorGUILayout.EndHorizontal(); 
                
                //Checking if can be saved
                if (_poisonBehaviourData.damage == 0)
                {
                    EditorGUILayout.HelpBox("This behaviour needs a [Damage] before it can be created", MessageType.Warning);
                }
                else if (_poisonBehaviourData.turns == 0)
                {
                    EditorGUILayout.HelpBox("This behaviour needs a [Turns] before it can be created", MessageType.Warning);
                }
                else if (GUILayout.Button("Save",GUILayout.Height(30)))
                {
                    SaveBehaviourData();
                    _window.Close();
                }
                break;
        }
    }

    void SaveBehaviourData()
    {
        //Default path for saving behaviour data 
        string behavioursDataPath = "Assets/Resources/CardData/Data/Behaviours/";

        
        //Changing th path and saving 
        switch (_behaviourType)
        {
            case BehaviourType.DAMAGE:
                behavioursDataPath += "Damage/Damage" + _damageBehaviourData.damage + ".asset";
                AssetDatabase.CreateAsset(_damageBehaviourData, behavioursDataPath);
                break;
            
            case BehaviourType.POISON:
                behavioursDataPath += "Poison/Poison" + _poisonBehaviourData.damage+"_"+_poisonBehaviourData.turns + ".asset";
                AssetDatabase.CreateAsset(_poisonBehaviourData, behavioursDataPath);
                break;
        }
    }

    void DrawPatternSettings()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Pattern");
        EditorGUILayout.EndHorizontal();
    }
}

