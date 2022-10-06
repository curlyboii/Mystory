using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;

    [SerializeField] private bool PlayerSpeakingFirst;

    [Header("Dialogue TMP Text")]
    [SerializeField] private TextMeshProUGUI playerDialogeText;
    [SerializeField] private TextMeshProUGUI nPCDialogeText;

    [Header("Continue Buttons")]
    [SerializeField] private GameObject playerContinueButton;
    [SerializeField] private GameObject nPCContinueButton;

    [Header("Animation Controllers")]
    [SerializeField] private Animator playerSpeechBubbleAnimator;
    [SerializeField] private Animator nPCSpeechBubbleAnimator;

   [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] playerDialogueSentences;
    [TextArea]
    [SerializeField] private string[] nPCDialogueSentences;



    private bool dialogueStarted;

    private int playerIndex;
    private int nPCIndex;

    private float speechBubbleAnimationDelay = 0.6f;


    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {

        if (PlayerSpeakingFirst)
        {
            playerSpeechBubbleAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay);
            StartCoroutine(TypePlayerDialogue());

        }
        else
        {

            nPCSpeechBubbleAnimator.SetTrigger("Open");
            yield return new WaitForSeconds(speechBubbleAnimationDelay);


            StartCoroutine(TypeNPCDialogue());
        
        }
    
    }

    private IEnumerator TypePlayerDialogue()
    {
        foreach (char letter in playerDialogueSentences[playerIndex].ToCharArray())
        {
            playerDialogeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        playerContinueButton.SetActive(true);
    }

    private IEnumerator TypeNPCDialogue()
    {
        foreach (char letter in nPCDialogueSentences[nPCIndex].ToCharArray())
        {
            nPCDialogeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        nPCContinueButton.SetActive(true);
    }

    private IEnumerator ContinuePlayerDialogue()
    {
        nPCDialogeText.text = string.Empty;

        nPCSpeechBubbleAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        playerDialogeText.text = string.Empty;

        playerSpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

 

        if (playerIndex < playerDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
            {
                playerIndex++;
            }
            else
            {
                dialogueStarted = true;
            }
            playerDialogeText.text = string.Empty;
            StartCoroutine(TypePlayerDialogue());
        
        }
    
    }

    private IEnumerator ContinueNPCDialogue()
    {

        playerDialogeText.text = string.Empty;

        playerSpeechBubbleAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);

        nPCDialogeText.text = string.Empty;

        nPCSpeechBubbleAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(speechBubbleAnimationDelay);



      

        if (nPCIndex < nPCDialogueSentences.Length - 1)
        {
            if (dialogueStarted)
            {
                nPCIndex++;
            }
            else
            {
                dialogueStarted = true;
            }
            nPCDialogeText.text = string.Empty;
            StartCoroutine(TypeNPCDialogue());

        }
    }

    public void TriggerContinuePlayerDialogue()
    {
        nPCContinueButton.SetActive(false);

        if (playerIndex >= playerDialogueSentences.Length - 1)
        {

            nPCDialogeText.text = string.Empty;

            nPCSpeechBubbleAnimator.SetTrigger("Close");

        }
        else
        {
            StartCoroutine(ContinuePlayerDialogue());
        }
    
    }

    public void TriggerContinueNPCDialogue()
    {

        playerContinueButton.SetActive(false);

        if (nPCIndex >= nPCDialogueSentences.Length - 1)
        {

            playerDialogeText.text = string.Empty;

            playerSpeechBubbleAnimator.SetTrigger("Close");

        }
        else
            StartCoroutine(ContinueNPCDialogue()); 


    }
}
