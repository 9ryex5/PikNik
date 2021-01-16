using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    private SaveFile SF;
    private ManagerUI MUI;

    private Animator myAnimator;

    //NEW PLAYER
    public GameObject panelNewPlayer;
    public InputField inputName;
    public InputField inputNumber;
    public Image imageConfirmationName;
    public Image imageConfirmationNumber;
    public Sprite spriteCheck;
    public Sprite spriteError;
    public Sprite spriteNothing;

    //PLAYER PROFILE
    public Animator panelPlayerProfile;
    private ItemPlayer currentItemPlayer;
    public InputField inputProfileName;
    public InputField inputProfileNumber;
    public Image imageProfileConfirmationName;
    public Image imageProfileConfirmationNumber;
    public Text textGoals;
    public Text textAssists;
    public Text textFouls;
    public Text textYellows;
    public Text textReds;
    public CanvasGroup confirmDelete;

    public Transform parentItemPlayers;
    public GameObject prefabItemPlayer;

    private void Awake()
    {
        SF = SaveFile.SF;
        MUI = ManagerUI.MUI;
        ItemPlayer.P = this;

        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < parentItemPlayers.childCount; i++)
            Destroy(parentItemPlayers.GetChild(i).gameObject);

        for (int i = 0; i < SF.GetAllPlayers().Count; i++)
        {
            ItemPlayer ip = Instantiate(prefabItemPlayer, parentItemPlayers).GetComponent<ItemPlayer>();
            ip.StartThis(SF.GetPlayer(i));
        }
    }

    public void ButtonNewPlayer()
    {
        inputName.text = string.Empty;
        inputNumber.text = string.Empty;
        imageConfirmationName.sprite = spriteNothing;
        imageConfirmationNumber.sprite = spriteNothing;
    }

    public void ButtonDoneNewPlayer()
    {
        bool valid = true;

        if (inputName.text == string.Empty)
        {
            imageConfirmationName.sprite = spriteError;
            valid = false;
        }
        else
            imageConfirmationName.sprite = spriteCheck;

        if (inputNumber.text == string.Empty)
        {
            imageConfirmationNumber.sprite = spriteError;
            valid = false;
        }
        else
            imageConfirmationNumber.sprite = spriteCheck;

        if (!valid)
            return;

        Player p = new Player(int.Parse(inputNumber.text), inputName.text);
        ItemPlayer ip = Instantiate(prefabItemPlayer, parentItemPlayers).GetComponent<ItemPlayer>();
        ip.StartThis(p);
        SF.AddPlayer(p);
        MUI.GoRight(myAnimator);
    }

    public void OpenPlayer(ItemPlayer _itemPlayer)
    {
        currentItemPlayer = _itemPlayer;
        inputProfileName.text = _itemPlayer.GetPlayer().name;
        inputProfileNumber.text = _itemPlayer.GetPlayer().number.ToString();
        imageProfileConfirmationName.sprite = spriteNothing;
        imageProfileConfirmationNumber.sprite = spriteNothing;
        textGoals.text = currentItemPlayer.GetPlayer().goals.ToString();
        textAssists.text = currentItemPlayer.GetPlayer().assists.ToString();
        textFouls.text = currentItemPlayer.GetPlayer().fouls.ToString();
        textYellows.text = currentItemPlayer.GetPlayer().yellows.ToString();
        textReds.text = currentItemPlayer.GetPlayer().reds.ToString();
        confirmDelete.gameObject.SetActive(false);
        MUI.GoRight(panelPlayerProfile);
    }

    public void ButtonSavePlayerProfile()
    {
        bool valid = true;

        if (inputProfileName.text == string.Empty)
        {
            imageProfileConfirmationName.sprite = spriteError;
            valid = false;
        }
        else
            imageProfileConfirmationName.sprite = spriteCheck;

        if (inputProfileNumber.text == string.Empty)
        {
            imageProfileConfirmationNumber.sprite = spriteError;
            valid = false;
        }
        else
            imageProfileConfirmationNumber.sprite = spriteCheck;

        if (!valid)
            return;

        currentItemPlayer.GetPlayer().name = inputProfileName.text;
        currentItemPlayer.GetPlayer().number = int.Parse(inputProfileNumber.text);
        currentItemPlayer.Refresh();
        MUI.GoRight(myAnimator);
    }

    public void ButtonDeletePlayer()
    {
        SF.RemovePlayer(currentItemPlayer.GetPlayer());
        Destroy(currentItemPlayer.gameObject);
    }
}
