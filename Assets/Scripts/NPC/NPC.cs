using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected DialogueData _dialogueData;
    [SerializeField] private GameObject _bubble;
    [SerializeField] protected Transform _bubblePosition;
    protected int _currentSpeech = -1;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _bubble.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            _bubble.SetActive(false);
        }
    }

    public virtual void Activate()
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

    protected virtual void StartDialogue()
    {
        _bubble.SetActive(false);
        Player.instance.CanMove = false;
    }
    protected virtual void StopDialogue()
    {
        DialogueBubble.instance.Hide();
        Player.instance.CanNotInteract(this);
        Player.instance.CanMove = true;
        _currentSpeech = -1;
    }
}
