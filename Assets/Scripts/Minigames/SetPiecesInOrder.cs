using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPiecesInOrder : Minigame
{
    [SerializeField] private List<PuzzlePiece> itemsToOrder = new List<PuzzlePiece>();
    [SerializeField] private Text infoText;
    private static Image _greySprite = null;

    private Dictionary<PuzzlePiece, Image> _places = new Dictionary<PuzzlePiece, Image>();

    protected override void StartMinigame()
    {
        base.StartMinigame();
        Camera.main.orthographicSize = 5;
        infoText.gameObject.SetActive(false);

        if (_greySprite == null)
            _greySprite = Resources.Load<Image>("GreySprite");

        foreach (var item in itemsToOrder)
        {
            var greyCopy = Object.Instantiate(_greySprite, transform);
            greyCopy.name = item.name;
            greyCopy.rectTransform.anchoredPosition = item.rectTransform.anchoredPosition;
            greyCopy.rectTransform.localScale = item.rectTransform.localScale;
            greyCopy.rectTransform.sizeDelta = item.rectTransform.sizeDelta;
            greyCopy.sprite = item.Sprite;
            
            item.rectTransform.anchoredPosition += new Vector2(Random.value * 500 - 250, Random.value * 500 - 250);
            _places[item] = greyCopy;
        }

        infoText.text = "Пазл собран!";

        foreach (var item in itemsToOrder)
            item.transform.SetAsLastSibling();
    }

    private void Update()
    {
        infoText.gameObject.SetActive(CheckEverythingInPlace());
    }

    public override void Close()
    {
        enabled = false;
        _success = CheckEverythingInPlace();
        base.Close();
    }

    private bool CheckEverythingInPlace()
    {
        foreach (var entry in _places)
            if (Vector3.Distance(entry.Key.rectTransform.anchoredPosition, entry.Value.rectTransform.anchoredPosition) > 20)
                return false;
        return true;
    }
}
