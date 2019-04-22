using System;

namespace Lab8
{
    public class OX
    {
        Random rnd = new Random();
        public int[] PMX(int[] parent1, int[] parent2)
        {
            if (parent1.Length != parent2.Length)
            {
                throw new Exception("Błędni rodzice");
            }

            int crossPoint1 = rnd.Next(1, parent1.Length + 1);
            int crossPoint2 = rnd.Next(1, parent1.Length + 1);

            if (crossPoint1 > crossPoint2)
            {
                int temp = crossPoint1;
                crossPoint1 = crossPoint2;
                crossPoint2 = temp;
            }

            int[] child = new int[parent1.Length];

            for (int i = crossPoint1; i < crossPoint2; i++)
            {
                child[i] = parent2[i];
            }

            for (int i = 0; i < crossPoint1; i++)
            {
                if (!child.Contains(parent1[i]))
                {
                    child[i] = parent1[i];
                }
                else
                {
                    //todo
                }
            }
            for (int i = crossPoint2; i < parent1.Length; i++)
            {
                if (!child.Contains(parent1[i]))
                {
                    child[i] = parent1[i];
                }
                else
                {
                    //todo
                }
            }

            return child;
        }
    }
}