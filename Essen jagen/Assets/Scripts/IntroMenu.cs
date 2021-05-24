using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    string[] Texts = new string[]
    {
        "Essen iagen - игра, в которую играют двое." +
        "Поэтому, если ты сейчас один, то скорее зови коллегу, бабушку или друга!" +
        "Приключение Начинается!",
        "Людям всегда был интересен ад." +
        " С раем все было понятно - всевозможные земные удовольствия, да и только." +
        "А вот ад... Как же конкретно грешные душы страдают? Через что проходят?" +
        "Остаётся только догадываться... Или нет?",
        "Еда, в наше время - одно из самых легкодоступных удовольствий." +
        "Уверен и вы, дорогие игроки не раз сабирались ночью в холодильник:)" +
        "Сейчас мы покажем вам, каков он, третий круг ада - место, куда рискует попасть почти каждый"
    };

    private string[] Buttoms = new string[]
    {
        "Окей",
        "Далее",
        "Поехали!"
    };
    
    public Text button;
    public Text Introduction;
    private int index = 1;

    public void ChangeText()
    {
        if (index > 2)
        {
            Intro.hint = 20;
            Intro.sceneName = "MenuScene";
        }
        else
        {
            Introduction.text = Texts[index];
            button.text = Buttoms[index];
            index++;
            
        }
    }
}