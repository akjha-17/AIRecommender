using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.CoreEngine
{
     public class PearsonRecommender:IRecommender
    {

       public double GetCorellation(int[] baseData, int[] otherData)
        {
            int baseSize=baseData.Length;
            int otherSize=otherData.Length;
            var baseList = baseData.ToList();
            var otherList= otherData.ToList();

            int valueToAppend = 1;
            
                if (baseSize < otherSize)
                {
                        otherList.RemoveRange(baseSize, otherSize - baseSize);            
                }
                if (baseSize > otherSize)
                {
                    /*if (valueToAppend <=0)
                        throw new ArgumentNullException("invalid value to Append");*/
                    otherList.AddRange(Enumerable.Repeat(valueToAppend, baseSize - otherSize));
                }
            //for (int i = 0; i < baseSize; i++)
            Parallel.For(0, baseSize, i =>
            {
                if (baseList[i] < 0 || otherList[i] < 0)
                {
                    //throw new InvalidValueException("Negative Value Not allowed");
                    throw new AggregateException();
                }
                if (baseList[i] == 0)
                {
                    baseList[i] = 1;
                    otherList[i] += 1;
                }
                if (otherList[i] == 0)
                {
                    otherList[i] = 1;
                    baseList[i] += 1;
                }
            }
            );
            
            
            int sigma_x_y = 0;
            int sigma_x = 0;
            int sigma_y = 0;
            int sigma_x_sq = 0;
            int sigma_y_sq = 0;

            for (int i = 0;i < baseSize;i++)
            //Parallel.For(0, baseSize, i => 
            {
                sigma_x_y += (baseList[i] * otherList[i]);
                sigma_x += baseList[i];
                sigma_y += otherList[i];
                sigma_x_sq += (baseList[i] * baseList[i]);
                sigma_y_sq += (otherList[i] * otherList[i]);
            }
            //);
            double denominator = ((baseSize * sigma_x_sq) - (sigma_x * sigma_x)) * ((baseSize * sigma_y_sq) - (sigma_y * sigma_y));
            if (denominator == 0)
            {
                return 0;
                //throw new DivideByZeroException("can't divide by zero");
            }
            if (denominator < 0)
            {
                return 0;
                //throw new ArgumentOutOfRangeException("Square root of negative not allowed ");
            }
            double res = ((baseSize * sigma_x_y) - ((sigma_x) * (sigma_y))) / Math.Sqrt(denominator);
            return res;
        }
    }
}
