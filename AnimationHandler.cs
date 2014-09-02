using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class AnimationHandler
    {
        private int[][] animationArray;
        private int[] frameCounts;
        private double frame;
        public float frameRate = 9;

        public AnimationHandler(params int[] frameCounts)
        {
            frame = 0.0;
            this.frameCounts = frameCounts;
            generateAnimationArray();
        }

        private void generateAnimationArray()
        {
            animationArray = new int[frameCounts.Length][];
            int flag = 0;
            for (int i = 0; i < frameCounts.Length; i++)
            {
                animationArray[i] = new int[frameCounts[i]];
                for (int j = 0; j < animationArray[i].Length; j++)
                    animationArray[i][j] = flag++;
            }
        }

        public double stepAnimation(int currState, UnityEngine.Animator anim)
        {
            checkFrameOverFlow(currState);
            setAnimationFrame(anim, currState);
            incrementFrame();
            return frame;
        }

        private void checkFrameOverFlow(int currState)
        {
            if (frame >= frameCounts[currState])
                frame = 0;
        }

        private void incrementFrame()
        {
            frame += UnityEngine.Time.deltaTime * frameRate;
        }

        private void setAnimationFrame(UnityEngine.Animator anim, int currState)
        {
            anim.SetInteger("frame", animationArray[currState][(int)frame]);
        }

        public double resetFrame()
        {
            return frame = 0;
        }

        public bool isDone(int state)
        {
            return frame >= frameCounts[state];
        }
    }
}
