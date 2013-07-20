﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Chess.Player.Board;

namespace Chess.Player.Pieces
{
	public abstract class Piece
	{
		protected Piece(Color color, PieceType type)
		{
			m_color = color;
			m_type = type;
		}

		public Color Color
		{
			get { return m_color; }
		}

		public PieceType Type
		{
			get { return m_type; }
		}

		public abstract ReadOnlyCollection<Move> GenerateMoves(int row, int column, Square[,] board);

		public override string ToString()
		{
			switch (m_type)
			{
			case PieceType.King:
				return "K";
			case PieceType.Queen:
				return "Q";
			case PieceType.Rook:
				return "R";
			case PieceType.Bishop:
				return "B";
			case PieceType.Knight:
				return "N";
			case PieceType.Pawn:
				return "";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		protected ReadOnlyCollection<Move> Scan(int xDirection, int yDirection, int row, int column, Square[,] board)
		{
			List<Move> moves = new List<Move>();
			Coordinate from = new Coordinate(column, row);

			int newRow = row;
			int newColumn = column;
			bool finished = false;

			while (!finished)
			{
				newRow += yDirection;
				newColumn += xDirection;

				if (newRow >= 0 && newRow <= 7 && newColumn >= 0 && newColumn <= 7)
				{
					Coordinate to = new Coordinate(newColumn, newRow);
					Move move = new Move(MoveType.Standard, from, to);

					if (!board[newRow, newColumn].HasPiece)
					{
						// empty square
						moves.Add(move);
					}
					else
					{
						// occupied square
						if (board[newRow, newColumn].Piece.Color != m_color)
							moves.Add(move);

						finished = true;
					}
				}
				else
				{
					finished = true;
				}

				// kings and knights can only move once in a given direction
				finished |= m_type == PieceType.King || m_type == PieceType.Knight;
			}

			return moves.AsReadOnly();
		}

		readonly Color m_color;
		readonly PieceType m_type;
	}
}
