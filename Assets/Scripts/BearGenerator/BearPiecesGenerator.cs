using System.Collections.Generic;
using System.Linq;
using Controller.Gameplay;
using GameManager;
using UnityEngine;

namespace BearGenerator
{
	public class BearPiecesGenerator : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> _pieceHeadList = null,
			_pieceTorsoList = null,
			_pieceArmLeftList = null,
			_pieceArmRightlist = null,
			_pieceLegLeftList = null,
			_pieceLegRightList = null;
		[SerializeField]
		private List<GameObject> _groupLeftList = null, _groupCenterList = null, _groupRightList = null;

		[SerializeField]
		private List<GameObject> _playerAList = null, _playerBList = null;

		[SerializeField]
		private List<GameObject> _playerTargetAList = null, _playerTargetBList = null;

		[SerializeField]
		private List<int> _indexPiecesList = new List<int> {1, 2, 3, 4, 5, 6};
		[SerializeField]
		private List<int> _sortedPiecesList = null;

		[SerializeField]
		private List<GameObject> _targetAPosition = null, _targetBPosition = null;

		[SerializeField]
		private GameController _gameController = null;

		void Start()
		{
			_playerAList = new List<GameObject>();
			_playerBList = new List<GameObject>();

			DefineCommonPieces();
			DefinePlayerPieces();

			List<BearItemController> player1Objective =
				_playerAList.Select(bearItem => bearItem.GetComponent<BearItemController>()).ToList();
			List<BearItemController> player2Objective =
				_playerBList.Select(bearItem => bearItem.GetComponent<BearItemController>()).ToList();

			CreateBearInGame();
			DefineTargetUi();
			DefinePositionPieces();
			DefinePositionPiecesRest();

			print("//--------------------------------------//");


			_gameController.Init(player1Objective, player2Objective);
		}

		private void DefinePlayerPieces()
		{
			CheckIfIsCommon(_pieceHeadList, (int) PiecesEnum.Head);
			CheckIfIsCommon(_pieceTorsoList, (int) PiecesEnum.Torso);
			CheckIfIsCommon(_pieceArmLeftList, (int) PiecesEnum.ArmLeft);
			CheckIfIsCommon(_pieceArmRightlist, (int) PiecesEnum.ArmRight);
			CheckIfIsCommon(_pieceLegLeftList, (int) PiecesEnum.LegLeft);
			CheckIfIsCommon(_pieceLegRightList, (int) PiecesEnum.LegRight);
		}

		private void CheckIfIsCommon(List<GameObject> list, int indexPiece)
		{
			GameObject newPiece = GetRandomItem(list);
			_playerAList.Add(newPiece);

			if (_sortedPiecesList.Contains(indexPiece + 1))
			{
				_playerBList.Add(newPiece);
			}
			else
			{
				newPiece = GetRandomItem(list);
				_playerBList.Add(newPiece);
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

			selectedIndex = _indexPiecesList[Random.Range(0, _indexPiecesList.Count)];
			_indexPiecesList.Remove(selectedIndex);

			_sortedPiecesList.Add(selectedIndex);

			selectedIndex = _indexPiecesList[Random.Range(0, _indexPiecesList.Count)];
			_indexPiecesList.Remove(selectedIndex);

			_sortedPiecesList.Add(selectedIndex);
		}

		private void DefinePositionPieces()
		{
			//CENTER===================================================
			GameObject newPiecePosition;
			int newindex;

			newPiecePosition = GetRandomItem(_groupCenterList);
			newindex = GetBiggerPosition(_sortedPiecesList);

			_playerAList[newindex - 1].transform.position = newPiecePosition.transform.position;
			_playerAList.Remove(_playerAList[newindex - 1]);
			_playerBList.Remove(_playerBList[newindex - 1]);

			newPiecePosition = GetRandomItem(_groupCenterList);
			newindex = GetSmallerPosition(_sortedPiecesList);

			_playerAList[newindex - 1].transform.position = newPiecePosition.transform.position;
			_playerAList.Remove(_playerAList[newindex - 1]);
			_playerBList.Remove(_playerBList[newindex - 1]);

			//LEFT===================================================

			MovePieceInPosition(_groupLeftList, _playerAList);
			MovePieceInPosition(_groupLeftList, _playerAList);

			MovePieceInPosition(_groupLeftList, _playerBList);
			MovePieceInPosition(_groupLeftList, _playerBList);

			//RIGHT===================================================

			MovePieceInPosition(_groupRightList, _playerAList);
			MovePieceInPosition(_groupRightList, _playerAList);

			MovePieceInPosition(_groupRightList, _playerBList);
			MovePieceInPosition(_groupRightList, _playerBList);
		}

		private void DefinePositionPiecesRest()
		{
			List<GameObject> pieceRestlist = new List<GameObject>();

			pieceRestlist = pieceRestlist.Concat(_pieceHeadList).ToList();
			pieceRestlist = pieceRestlist.Concat(_pieceTorsoList).ToList();
			pieceRestlist = pieceRestlist.Concat(_pieceArmLeftList).ToList();
			pieceRestlist = pieceRestlist.Concat(_pieceArmRightlist).ToList();
			pieceRestlist = pieceRestlist.Concat(_pieceLegLeftList).ToList();
			pieceRestlist = pieceRestlist.Concat(_pieceLegRightList).ToList();

			int groupIndex = 0;

			while (pieceRestlist.Count > 0)
			{
				var positionList = GetGroupPiecePositionByIndex(groupIndex);

				MovePieceInPosition(positionList, pieceRestlist);

				groupIndex++;
				if (groupIndex == 3) groupIndex = 0;
			}
		}

		private List<GameObject> GetGroupPiecePositionByIndex(int index)
		{
			if (index == 0)
			{
				return _groupLeftList;
			}
			else if (index == 1)
			{
				return _groupCenterList;
			}
			else
			{
				return _groupRightList;
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
			for (int i = 0; i < _playerAList.Count; i++)
			{
				GameObject clonePiece = Instantiate(_playerAList[i]);
				clonePiece.transform.position = _targetAPosition[i].transform.position;
			}

			for (int i = 0; i < _playerBList.Count; i++)
			{
				GameObject clonePiece = Instantiate(_playerBList[i]);
				clonePiece.transform.position = _targetBPosition[i].transform.position;
			}
		}

		private void DefineTargetUi()
		{
			print(_playerTargetAList.Count() + " " + _playerAList.Count());

			foreach (GameObject piece in _playerTargetAList)
			{
				foreach (GameObject piecePlayer in _playerAList)
				{
					if (piece.name.Contains(piecePlayer.name))
					{
						piece.SetActive(true);
					}
				}
			}

			foreach (GameObject piece in _playerTargetBList)
			{
				foreach (GameObject piecePlayer in _playerBList)
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
}