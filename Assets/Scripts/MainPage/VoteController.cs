using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainPage.Panel;

public class VoteController : MonoBehaviour
{
    int vote = 0;
    [SerializeField] Image voteImage;
    [SerializeField] Sprite zeroVoteImage, upVoteImage, downVoteImage;
    [SerializeField] Color zeroVoteColor, upVoteColor, downVoteColor;
    [SerializeField] Text voteText;
    [SerializeField] DownloadPanelController downloadPanel;
    [SerializeField] QueryVote queryVote;
    [SerializeField] SearchInputField searchInputField;
    public void SetVote(int vote)
    {
        this.vote = vote;
        RefreshUI();
    }
    public void RefreshUI()
    {

        voteText.text = vote.ToString();

        if(vote>=0)
        {
            if(vote == 0)
            {
                voteImage.sprite = zeroVoteImage;
                voteText.color = zeroVoteColor;
            }
            else
            {
                voteImage.sprite = upVoteImage;
                voteText.color = upVoteColor;
            }
        }
        else
        {
            voteImage.sprite = downVoteImage;
            voteText.color = downVoteColor;
        }
    }
    public void RequestModifyVote(int amount)
    {
        Response.ResponseItem responseData = downloadPanel.GetCurrentResponseData();
        int currentVote = PlayerPrefs.GetInt("vote_"+responseData.getGameID(), 0);
        //if(currentVote == 0)
        {
            PlayerPrefs.SetInt("vote_"+responseData.getGameID(), amount);
            AttributeUpdateQuery query = new AttributeUpdateQuery(
                new AttributeUpdateQuery.KeyData(
                    "GameAttribute", downloadPanel.GetCurrentResponseData().getGameID()
                ),
                new AttributeUpdateQuery.AttributeUpdateData(
                    new AttributeUpdateQuery.AttributeUpdateData.VoteData(
                        "ADD", amount
                    )
                )
            );

            Debug.Log("asdf");

            SetVote(vote+amount);

            StartCoroutine(queryVote.SendRequest(query, OnReceiveResponse));
        }
    }

    public void OnReceiveResponse()
    {
        searchInputField.QueryProcess();
    }
}
