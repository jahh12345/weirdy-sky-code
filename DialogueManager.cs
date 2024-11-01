using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public Image backgroundImage; 
    public List<Sprite> backgroundSprites; 

    private Queue<string> sentences;
    private int sentenceIndex; 
    private bool isTyping = false; 

    void Start()
    {
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        StartDialogue();
    }

    void StartDialogue()
    {
        sentences.Clear();
        sentenceIndex = 0;

        sentences.Enqueue("ตำนานเทพีแห่งดวงจันทร์ที่ถูกผนึกเอาไว้จากสามผู้วิเศษผู้ยิ่งใหญ่ทั้งสามเป็นเวลานับ 1000 ปี เพราะเทพีพยายามจะทำลายหมู่บ้านผู้วิเศษ");
        sentences.Enqueue("เนื่องจากความอิจฉาที่เทพแห่งแสงอาทิตย์ได้รับการบูชามากกว่านาง ");
        sentences.Enqueue("เมื่อนางเทพีหลุดจากการผนึกจองจำมาได้");
        sentences.Enqueue("นางก็ได้ทำการเสกท้องฟ้าของโลกเวทย์มนต์ให้วิปราศจนทำให้หมู่บ้านผู้วิเศษเดือดร้อนอย่างหนัก");
        sentences.Enqueue(" ");
        sentences.Enqueue("เนื่องจากในตอนนี้ทั่วทุกทีล้วนมีแต่เวทย์มนต์คำสาปของนางเทพี ");
        sentences.Enqueue("เส้นทางเดียวที่จะสามารถเดินทางไปสู้กับเทพีแห่งดวงจันทร์ คือการเก็บรวบรวมคัมภีร์โบราณจากเทพประจำ 12 นักษัตรเพื่อเปิดประตูไปสู่ดินแดนที่นางเทพีดวงจันทร์อาศัยอยู่");
        sentences.Enqueue("ที่หมู่บ้านของพ่อมดและแม่มดที่หน้ากระท่อมของเอลฟ์ คนรับใช้ของตำนานสามผู้วิเศษเพื่อตามหาเบาะแสของอาวุธศักดิ์สิทธิ์");
        sentences.Enqueue("ในเบาะแสจะมีที่ซ่อนของกุญแจ เพื่อเปิดหีบ");
        sentences.Enqueue("ได้เบาะแสมาแล้ว ไปที่ถ้ำเพื่อเข้าไปสู้มินิเกมส์ตามหากุญแจในถ้ำกันเลย!");
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (isTyping) return; 

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));

        
        if (sentenceIndex < backgroundSprites.Count)
        {
            backgroundImage.sprite = backgroundSprites[sentenceIndex];
        }

        sentenceIndex++;
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; 
        dialogueText.text = "";
        nextButton.interactable = false; 

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); 
        }

        isTyping = false; 
        nextButton.interactable = true; 
    }

    void EndDialogue()
    {
 

        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("findkey"); 
    }
}
