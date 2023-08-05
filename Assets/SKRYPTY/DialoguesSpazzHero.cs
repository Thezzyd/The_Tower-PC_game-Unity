/*
using UnityEngine;
using TMPro;

public class DialoguesSpazzHero : MonoBehaviour
{
    // public bool[] dialogues = new bool[10];
    [HideInInspector] public int dialogueCounter = 0;

    public TextMeshProUGUI HeroText;
    public TextMeshProUGUI SpazzText;

    [HideInInspector] public LevelManager levelManager;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


    public void HeroSpazzDialogue()
    {
        if (dialogueCounter == 0)
        {
            HeroText.text = "You : \n\n- What happend?!";
        }

        else if (dialogueCounter == 1)
        {
            levelManager.SpazzDialogueFrame.SetActive(true);
            //HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            SpazzText.text = "Stranger : \n\n- You touched a dam star... \nThat`s what happend...";
        }

        else if (dialogueCounter == 2)
        {
            //HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Stranger : \n\n- This is the only way you could get here, they mess with us every time we touch them...";

        }
        else if (dialogueCounter == 3)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- Sounds like a dream...";
          //  SpazzText.text = "Stranger : ... ";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 4)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- Who are you?";
          //  SpazzText.text = "Stranger : ... ";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 5)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Stranger : \n\n- Shadow in the dark that once has been a human, the last one in this place. ";

        }
        else if (dialogueCounter == 6)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Stranger : \n\n- Now there`s none... ";

        }
        else if (dialogueCounter == 7)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Stranger : \n\n- Spazz is my name. ";

        }
        else if (dialogueCounter == 8)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You will have to fight to get back... ";

        }
        else if (dialogueCounter == 9)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- What do you mean??";
          //  SpazzText.text = "Spazz : ... ";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 10)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You see, when you touched a star, in that moment you were granted with som special skills, kinda magicall skills I would say... ";

        }
        else if (dialogueCounter == 11)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- Stars contain strange but powerfull energy, very powerfull...  ";

        }
        else if (dialogueCounter == 12)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- This place is sourranded by various creatures, that wil want to hurt you , and you going to defend yourself...";

        }
        else if (dialogueCounter == 13)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You will have to get me something...";

        }
        else if (dialogueCounter == 14)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- I know the way we can escape, it`s right here, under ground, this gate you see? ...";

        }
        else if (dialogueCounter == 15)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n Will you help me?";
           // SpazzText.text = "Spazz : ...";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 16)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- No.";

        }
        else if (dialogueCounter == 17)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You are the person that will help me. And then I will help you to help yourself";

        }
        else if (dialogueCounter == 18)
        {
            //HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- I am pretty useless now, I am more of a ghost with no any power... ";

        }
        else if (dialogueCounter == 19)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- So what do you need?";
          //  SpazzText.text = "Spazz : ...";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 20)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- Stars, a lot of them";

        }
        else if (dialogueCounter == 21)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You know, this kind of stars you touched right before you got here. 200 should be enought...";

        }
        else if (dialogueCounter == 22)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- And why do you need them?";
          //  SpazzText.text = "Spazz : ...";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 23)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- I ask for the stars becaouse we will need that pure energy to survive down there";

        }
        else if (dialogueCounter == 24)
        {
          //  HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You need to trust me if you want to get back";

        }
        else if (dialogueCounter == 25)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- You need to trust me if you want to get back";

        }
        else if (dialogueCounter == 26)
        {
            levelManager.HeroDialogueFrame.SetActive(true);

            HeroText.text = "You : \n\n- Right... Let say i will try get them for you";
          //  SpazzText.text = "Spazz : ...";
            levelManager.SpazzDialogueFrame.SetActive(false);


        }
        else if (dialogueCounter == 27)
        {
            //HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- Good boy!";

        }
        else if (dialogueCounter == 28)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- Ours future lays in yours hands now...";

        }
        else if (dialogueCounter == 29)
        {
           // HeroText.text = "You : ...";
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(true);

            SpazzText.text = "Spazz : \n\n- And remember, when you touch the star you never stay the same!";

        }

      

        if (dialogueCounter >= 30)
        {
            levelManager.HeroDialogueFrame.SetActive(false);
            levelManager.SpazzDialogueFrame.SetActive(false);
            levelManager.ENEMY_ALLOWED = true;
            levelManager.PLAYER_ALLOWED = true;
         //   levelManager.SpazzQuest = true;
            levelManager.RefreshStarCounterText();
            levelManager.SpazzHerpCanvas.SetActive(false);
        }

        dialogueCounter++;

    }

}

*/