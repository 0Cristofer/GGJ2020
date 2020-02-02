using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controller.Gameplay;
using Domains;
using UnityEngine;

public class BearPiecesGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> pieceHeadList, pieceTorsoList, pieceArmLeftList, pieceArmRightlist, pieceLegLeftList, pieceLegRightList;
    [SerializeField] private List<GameObject> groupLeftList, groupCenterList, groupRightList;

    [SerializeField] private List<GameObject> playerAList, playerBList;

    [SerializeField] private List<GameObject> playerTargetAList, playerTargetBList;

    [SerializeField] private List<int> indexPiecesList = new List<int> { 1, 2, 3, 4, 5, 6 };
    [SerializeField] private List<int> sortedPiecesList;

    [SerializeField] private List<GameObject> targetAPosition, targetBPosition;

    [SerializeField]
    private GameController.GameController _gameController;

    void Start()
    {
        playerAList = new List<GameObject>();
        playerBList = new List<GameObject>();

        DefineCommonPieces();
        DefinePlayerPieces();
        
        List<BearItemController> player1Objective = playerAList.Select(bearItem => bearItem.GetComponent<BearItemController>()).ToList();
        List<BearItemController> player2Objective = playerBList.Select(bearItem => bearItem.GetComponent<BearItemController>()).ToList();
       
        CreateBearInGame();
        DefineTargetUi();
        DefinePositionPieces();
        DefinePositionPiecesRest();

        print("//--------------------------------------//");
        

        _gameController.Init(player1Objective, player2Objective);
    }

    private void DefinePlayerPieces()
    {
        CheckIfIsCommon(pieceHeadList, (int)PiecesEnum.Head);
        CheckIfIsCommon(pieceTorsoList, (int)PiecesEnum.Torso);
        CheckIfIsCommon(pieceArmLeftList, (int)PiecesEnum.ArmLeft);
        CheckIfIsCommon(pieceArmRightlist, (int)PiecesEnum.ArmRight);
        CheckIfIsCommon(pieceLegLeftList, (int)PiecesEnum.LegLeft);
        CheckIfIsCommon(pieceLegRightList, (int)PiecesEnum.LegRight);
    }

    private void CheckIfIsCommon(List<GameObject> list, int indexPiece)
    {
        GameObject newPiece = GetRandomItem(list);
        playerAList.Add(newPiece);

        if (sortedPiecesList.Contains(indexPiece + 1))
        {
            playerBList.Add(newPiece);
        }
        else
        {
            newPiece = GetRandomItem(list);
            playerBList.Add(newPiece);
        }
    }

    private GameObject GetRandomItem(List<GameObject> list)
    {
        GameObject newPiece = list[Random.Range(0, list.Count)];
        list.Remove(newPiece);

        return newPiece;
    }

    private int GetBiggerPosition(List<int> list)
    {
        if (list[0] > list[1])
        {
            return list[0];
        }

        return list[1];
    }

    private int GetSmallerPosition(List<int> list)
    {
        if (list[0] < list[1])
        {
            return list[0];
        }

        return list[1];
    }

    private void DefineCommonPieces()
    {
        int selectedIndex;

        selectedIndex = indexPiecesList[Random.Range(0, indexPiecesList.Count)];
        indexPiecesList.Remove(selectedIndex);

        sortedPiecesList.Add(selectedIndex);

        selectedIndex = indexPiecesList[Random.Range(0, indexPiecesList.Count)];
        indexPiecesList.Remove(selectedIndex);

        sortedPiecesList.Add(selectedIndex);
    }

    private void DefinePositionPieces()
    {
        //CENTER===================================================
        GameObject newPiecePosition;
        int newindex;

        newPiecePosition = GetRandomItem(groupCenterList);
        newindex = GetBiggerPosition(sortedPiecesList);

        playerAList[newindex - 1].transform.position = newPiecePosition.transform.position;
        playerAList.Remove(playerAList[newindex - 1]);
        playerBList.Remove(playerBList[newindex - 1]);

        newPiecePosition = GetRandomItem(groupCenterList);
        newindex = GetSmallerPosition(sortedPiecesList);

        playerAList[newindex - 1].transform.position = newPiecePosition.transform.position;
        playerAList.Remove(playerAList[newindex - 1]);
        playerBList.Remove(playerBList[newindex - 1]);
    
        //LEFT===================================================

        MovePieceInPosition(groupLeftList, playerAList);
        MovePieceInPosition(groupLeftList, playerAList);

        MovePieceInPosition(groupLeftList, playerBList);
        MovePieceInPosition(groupLeftList, playerBList);

        //RIGHT===================================================

        MovePieceInPosition(groupRightList, playerAList);
        MovePieceInPosition(groupRightList, playerAList);

        MovePieceInPosition(groupRightList, playerBList);
        MovePieceInPosition(groupRightList, playerBList);
    }

    private void DefinePositionPiecesRest()
    {
        List<GameObject> pieceRestlist = new List<GameObject>();

        pieceRestlist = pieceRestlist.Concat(pieceHeadList).ToList();
        pieceRestlist = pieceRestlist.Concat(pieceTorsoList).ToList();
        pieceRestlist = pieceRestlist.Concat(pieceArmLeftList).ToList();
        pieceRestlist = pieceRestlist.Concat(pieceArmRightlist).ToList();
        pieceRestlist = pieceRestlist.Concat(pieceLegLeftList).ToList();
        pieceRestlist = pieceRestlist.Concat(pieceLegRightList).ToList();

        int groupIndex = 0;

        while(pieceRestlist.Count > 0)
        {
            var positionList = GetGroupPiecePositionByIndex(groupIndex);

            MovePieceInPosition(positionList, pieceRestlist);

            groupIndex++;
            if (groupIndex == 3) groupIndex = 0;
        }
    }

    private List<GameObject> GetGroupPiecePositionByIndex(int index)
    {
        if(index == 0)
        {
            return groupLeftList;
        }
        else if (index == 1)
        {
            return groupCenterList;
        }
        else
        {
            return groupRightList;
        }

    }

    private void MovePieceInPosition(List<GameObject> positionList, List<GameObject> pieceList)
    {
        GameObject newPiecePosition = GetRandomItem(positionList);
        GameObject newPlayerPiece = GetRandomItem(pieceList);
        newPlayerPiece.transform.position = newPiecePosition.transform.position;
    }

    private void CreateBearInGame()
    {
        for (int i = 0; i < playerAList.Count; i++)
        {
            GameObject clonePiece =  Instantiate(playerAList[i]);
            clonePiece.transform.position = targetAPosition[i].transform.position;
        }

        for (int i = 0; i < playerBList.Count; i++)
        {
            GameObject clonePiece = Instantiate(playerBList[i]);
            clonePiece.transform.position = targetBPosition[i].transform.position;
        }
    }

    private void DefineTargetUi() 
    {
        print(playerTargetAList.Count() + " " + playerAList.Count());

        foreach(GameObject piece in playerTargetAList) 
        {
            foreach (GameObject piecePlayer in playerAList)
            {
                if(piece.name.Contains(piecePlayer.name))
                {
                    piece.SetActive(true);
                }
            }
        }

        foreach (GameObject piece in playerTargetBList)
        {
            foreach (GameObject piecePlayer in playerBList)
            {
                if (piece.name.Contains(piecePlayer.name))
                {
                    piece.SetActive(true);
                }
            }
        }
    }
}

public enum PiecesEnum
{
    Head,
    Torso,
    ArmLeft,
    ArmRight,
    LegLeft,
    LegRight
}
