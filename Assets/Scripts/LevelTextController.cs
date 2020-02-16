using UnityEngine.UI;

public class LevelTextController : Singleton<LevelTextController>
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void UpdateText(string text)
    {
        _text.text = text;
    }
}
