using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class PlayingCard : NetworkBehaviour
{
    public MeshRenderer _meshRenderer;
    private Material _newMat;
    [SerializeField]
    private BlackBoardTextEditor _blackBoardText;
    public GameMaster _gm;
    public BuschLiteCan _can;

    [SerializeField]
    [SyncVar]
    private int cardTypeVal;

    public PlayingCardType CardType
    {
        get { return (PlayingCardType)cardTypeVal; }
    }

    public string CardDescription
    {
        get
        {
            var enumType = typeof(PlayingCardType);
            var memberInfos = enumType.GetMember(CardType.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)valueAttributes[0]).Description;
        }
    }

    private void Start()
    {
        cardTypeVal = 0;
    }

    public void Select(int playerTurn)
    {
        _gm.SelectCard(this, playerTurn);
        _blackBoardText.SetBoardText(this.CardDescription);
    }

    public void SetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
        SetCardType();

        RpcSetMaterial(rand);
    }

    [ClientRpc]
    private void RpcSetMaterial(int rand)
    {
        _meshRenderer.material = _gm.GetMat(rand);
    }

    /// <summary>
    /// Temporary function for determining the card value. 
    /// </summary>
    private void SetCardType()
    {
        string matName = _meshRenderer.material.name;

        string[] matNameChunks = matName.Split('_'); //split the name out
        string cardName = matNameChunks[matNameChunks.Length - 2]; //get the card name. Formatted as Heart02, Spade09, Diamond13 etc

        this.cardTypeVal = Int32.Parse(cardName.Substring(cardName.Length - 2, 2)); //parse the numeric value       
    }

    public enum PlayingCardType
    {
        [Description("No Value")]
        NoVal = 0,
        [Description("Waterfall")]
        Waterfall = 1,
        [Description("You")]
        You = 2,
        [Description("Me")]
        Me = 3,
        [Description("Ladies")]
        Ladies = 4,
        [Description("Drive")]
        Drive = 5,
        [Description("Guys")]
        Guys = 6,
        [Description("Heaven")]
        Heaven = 7,
        [Description("Date")]
        Date = 8,
        [Description("Rhyme")]
        Rhyme = 9,
        [Description("Categories")]
        Categories = 10,
        [Description("Viking Master")]
        VikingMaster = 11,
        [Description("Question Master")]
        QuestionMaster = 12, 
        [Description("Rule Master")]
        RuleMaster = 13
    }
}
