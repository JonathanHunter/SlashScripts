using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Enemies
{
	class LevelTwoEnemyStateMachine
	{
		public enum State
		{
			Idle = 0, Hit, Walk, Turn, NumOfStates
		}
		private int[] frameCounts;
		private int[][] animationArray;
		private State currState;
		private double frame, hold;
		
		public LevelTwoEnemyStateMachine()
		{
			frame = 0.0;
			currState = State.Idle;
			frameCounts = new int[] { 1, 1, 2, 1};
			generateAnimationArray();
		}
		
		private void generateAnimationArray()
		{
			animationArray = new int[((int)State.NumOfStates)][];
			int flag = 0;
			for (int i = 0; i < ((int)State.NumOfStates); i++)
			{
				animationArray[i] = new int[frameCounts[i]];
				for (int j = 0; j < animationArray[i].Length; j++)
					animationArray[i][j] = flag++;
			}
		}
		
		public State update(bool beingHit, bool shouldTurn, UnityEngine.Animator anim)
		{
			runMachine(beingHit, shouldTurn);
			checkFrameOverFlow();
			setAnimationFrame(anim);
			incrementFrame();
			return currState;
		}

		private void checkFrameOverFlow()
		{
			if (frame >= frameCounts[((int)currState)])
				frame = 0;
		}
		
		private void incrementFrame()
		{
			frame += UnityEngine.Time.deltaTime * 9;
		}
		
		private void runMachine(bool beingHit, bool shouldTurn)
		{
			State prev = currState;
			switch (currState)
			{
			case State.Idle: currState = Idle(beingHit, shouldTurn); break;
			case State.Hit: currState = Hit(beingHit, shouldTurn); break;
			case State.Walk: currState = Walk(beingHit, shouldTurn); break;
			case State.Turn: currState = Turn(beingHit, shouldTurn); break;    
			}
			if (prev != currState)
				frame = 0;
		}
		
		private void setAnimationFrame(UnityEngine.Animator anim)
		{
			anim.SetInteger("frame", animationArray[((int)currState)][(int)frame]);
		}
		
		private State Idle(bool beingHit, bool shouldTurn)
		{
			if (beingHit)
				return State.Hit;
			if (shouldTurn)
				return State.Turn;
			return State.Walk;
		}
		private State Hit(bool beingHit, bool shouldTurn)
		{
			if (hold++ > 5)
			{
				hold = 0;
				return State.Idle;
			}
			return State.Hit;
		}
		private State Walk(bool beingHit, bool shouldTurn)
		{
			if (beingHit)
				return State.Hit;
			if (shouldTurn)
				return State.Turn;
			return State.Walk;
		}
		private State Turn(bool beingHit, bool shouldTurn)
		{
			if (beingHit)
				return State.Hit;
			return State.Walk;
		}
	}
}

