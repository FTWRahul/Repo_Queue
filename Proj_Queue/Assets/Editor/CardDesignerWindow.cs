using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CardDesignerWindow : EditorWindow
{
    //Sections Rect
    private Rect _headerSection;
    private Rect _bodySection;
    private Rect _buttonsSection;
    
    //Textures for sections
    private Texture2D _headerSectionTexture;
    private Texture2D _bodySectionTexture;
    private Texture2D _buttonsSectionTexture;
    
    //Color for sections
    readonly Color _headerSectionColor = Color.gray;
    readonly Color _buttonsSectionColor = Color.white;

    //Card data and actions references
    private static CardData _cardData;

    //GUI skin for editor window
    private GUISkin _skin;
    
    private bool _canSave;

    //Setting the parameters of the window 
    [MenuItem("Window/Card Designer")]
    static void OpenMenu()
    {
        CardDesignerWindow window = (CardDesignerWindow) GetWindow(typeof(CardDesignerWindow));
        window.minSize = new Vector2(600,300); 
        window.maxSize = new Vector2(600,600);
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
        
        if (_cardData.actions.Count > 0) return;
        
        _cardData.actions.Add(null);
    }
    
    //Creating the textures
    void InitTexture()
    {
        _headerSectionTexture = new Texture2D(1,1);
        _headerSectionTexture .SetPixel(0,0, _headerSectionColor);
        _headerSectionTexture.Apply();
        
        _buttonsSectionTexture = new Texture2D(1,1);
        _buttonsSectionTexture .SetPixel(0,0, _buttonsSectionColor);
        _buttonsSectionTexture.Apply();
        
        _bodySectionTexture = Resources.Load<Texture2D>("Icons/gradient");
    }
    
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeaderSection();
        DrawBodySection();
        DrawButtonsSection();
    }
    
    //Setting the layouts and drawing the textures
    void DrawLayouts()
    {
        _headerSection.x = 0;
        _headerSection.y = 0;
        _headerSection.width = Screen.width;
        _headerSection.height = 40;

        _buttonsSection.x = 0;
        _buttonsSection.y = Screen.height - 150;
        _buttonsSection.width = Screen.width;
        _buttonsSection.height = 150;
        
        _bodySection.x = 0;
        _bodySection.y = _headerSection.height;
        _bodySection.width = Screen.width;
        _bodySection.height = Screen.height - _buttonsSection.height - _bodySection.y;
        
        GUI.DrawTexture(_headerSection, _headerSectionTexture);
        GUI.DrawTexture(_bodySection, _bodySectionTexture);
    }
    
    void DrawHeaderSection()
    {
        GUILayout.BeginArea(_headerSection);
        
        GUILayout.Label("Card Designer", _skin.GetStyle("Header"));
        
        GUILayout.EndArea();
    }
    
    void DrawBodySection()
    {
        _canSave = true;
        
        GUILayout.BeginArea(_bodySection);
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card name: ");
        _cardData.cardName = EditorGUILayout.TextField(_cardData.cardName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card description: ");
        EditorGUILayout.EndHorizontal();
        _cardData.cardDescription = EditorGUILayout.TextArea(_cardData.cardDescription);
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Actions: ");
        
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < _cardData.actions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (_cardData.actions.Count > 1)
            {
                if (GUILayout.Button("^", GUILayout.Width(20)))
                {
                    if (i > 0)
                    {
                        var temp = _cardData.actions[i];
                        _cardData.actions.RemoveAt(i);
                        _cardData.actions.Insert(i - 1, temp);
                    }
                }
                if(GUILayout.Button("v", GUILayout.Width(20)))
                {
                    if (i < _cardData.actions.Count-1)
                    {
                        var temp = _cardData.actions[i];
                        _cardData.actions.RemoveAt(i);
                        _cardData.actions.Insert(i + 1, temp);
                    }
                }
            }
            _cardData.actions[i] = (ActionData) EditorGUILayout.ObjectField(_cardData.actions[i], typeof(ActionData), false);
            EditorGUILayout.EndHorizontal();

            if (_cardData.actions[i] == null)
            {
                _canSave = false;
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+", GUILayout.Height(20)))
        {
            _cardData.actions.Add(null);
        }
        if (GUILayout.Button("-", GUILayout.Height(20)))
        {
            if (_cardData.actions.Count > 1)
            {
                _cardData.actions.RemoveAt(_cardData.actions.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void DrawButtonsSection()
    {
        GUILayout.BeginArea(_buttonsSection);
        
        EditorGUILayout.Space();
        
        if (string.IsNullOrEmpty(_cardData.cardName) || string.IsNullOrEmpty(_cardData.cardDescription))
        {
            EditorGUILayout.HelpBox("Cards needs a [Card name] and [Card description] before it can be created",
                MessageType.Warning);
        }
        else if (!_canSave)
        {
            //TODO:: error prevention for saving without action
            EditorGUILayout.HelpBox("All actions needs to be assigned before it can be created", MessageType.Warning);
        }
        else if (GUILayout.Button("Save card", GUILayout.Height(40)))
        {
            SaveCard();
        }
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Create new action", GUILayout.Height(40)))
        {
            ActionSettings.OpenWindow();
        }

        GUILayout.EndArea();
    }
    
    void SaveCard()
    {
        //Default path for saving card data 
        string cardDataPath = "Assets/Resources/CardData/Data/Cards/";

        //Saving new card data
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
        
        _window.Close();
    }

    void DrawPatternSettings()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Pattern");
        EditorGUILayout.EndHorizontal();
    }
}

public class ActionSettings : EditorWindow
{
    private static ActionSettings _window;

    private static ActionData _actionData;

    private bool _canSave;
    public static void OpenWindow()
    {
        _window = (ActionSettings) GetWindow(typeof(ActionSettings));
        _window.minSize = new Vector2(250, 200);
        _window.Show();
    }

    private void OnEnable()
    {
        InitData();
    }

    void InitData()
    {
        //Creating the instances of behaviour data scriptable objects
        _actionData = (ActionData) ScriptableObject.CreateInstance(typeof(ActionData));
        
        _actionData.patterns = new List<PatternData>();
        _actionData.patterns.Add(null);
    }

    private void OnGUI()
    {
        _canSave = true;
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Action Name:");
        _actionData.actionName = EditorGUILayout.TextField(_actionData.actionName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Behaviour");
        _actionData.behaviour =
            (BehaviourData) EditorGUILayout.ObjectField(_actionData.behaviour, typeof(BehaviourData), false); 
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Pattern");
        EditorGUILayout.BeginVertical();

        for (int j = 0; j < _actionData.patterns.Count; j++)
        {
            EditorGUILayout.BeginHorizontal();
            if (_actionData.patterns.Count > 1)
            {
                if (GUILayout.Button("^", GUILayout.Width(20)))
                {
                    if (j > 0)
                    {
                        var temp = _actionData.patterns[j];
                        _actionData.patterns.RemoveAt(j);
                        _actionData.patterns.Insert(j - 1, temp);
                    }
                }
                if(GUILayout.Button("v", GUILayout.Width(20)))
                {
                    if (j < _actionData.patterns.Count-1)
                    {
                        var temp = _actionData.patterns[j];
                        _actionData.patterns.RemoveAt(j);
                        _actionData.patterns.Insert(j + 1, temp);
                    }
                }
            }
            
            _actionData.patterns[j] =
                (PatternData) EditorGUILayout.ObjectField(_actionData.patterns[j], typeof(PatternData), false);
            EditorGUILayout.EndHorizontal();
            
            if (_actionData.patterns[j] == null)
            {
                _canSave = false;
            }
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(("-"), GUILayout.Height(20)))
        {
            if (_actionData.patterns.Count > 1)
            {
                _actionData.patterns.RemoveAt(_actionData.patterns.Count - 1);
            }
        }

        if (GUILayout.Button(("+"), GUILayout.Height(20)))
        {
            _actionData.patterns.Add(null);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        
        if (!_canSave)
        {
            EditorGUILayout.HelpBox("All [Pattern] needs assigned before it can be created",
                MessageType.Warning);
        }
        else if (_actionData.behaviour == null)
        {
            EditorGUILayout.HelpBox("Action needs a [Behaviour] before it can be created",
                MessageType.Warning);
        }
        else if (GUILayout.Button(("Save action"), GUILayout.Height(40)))
        {
            SaveAction();
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
    }

    void SaveAction()
    {
        //Default path for saving action data 
        string actionDataPath = "Assets/Resources/CardData/Data/Actions/";
        
        actionDataPath += _actionData.actionName + ".asset";
        AssetDatabase.CreateAsset(_actionData, actionDataPath);
        
        _window.Close();
    }
}