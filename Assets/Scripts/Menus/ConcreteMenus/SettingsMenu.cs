using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public Button backButton;


    public override void Init(MenuController currentContext)
    {
        base.Init(currentContext);
        state = MenuStates.Settings;
        if (backButton) backButton.onClick.AddListener(() => JumpBack());
        
    }
}

