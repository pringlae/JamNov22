using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private GameObject _bubble;
    [SerializeField] private Transform _bubblePosition;
    private int _currentSpeech = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _bubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _bubble.SetActive(false);
        }
    }

    public void Activate()
    {
        if (_currentSpeech == -1)
        {
            StartDialogue();
        }
        _currentSpeech++;
        if (_currentSpeech >= _dialogueData.speeches.Length)
        {
            StopDialogue();
            return;
        }
        var speech = _dialogueData.speeches[_currentSpeech];
        var pos = speech.isPlayer ? Player.instance.DialoguePosition : _bubblePosition.position;
        DialogueBubble.instance.Setup(pos, speech.text);
    }

    private void StartDialogue()
    {
        _bubble.SetActive(false);
        Player.instance.CanMove = false;
    }
    private void StopDialogue()
    {
        DialogueBubble.instance.Hide();
        Player.instance.CanNotInteract();
        Player.instance.CanMove = true;
        _currentSpeech = -1;
    }
}
