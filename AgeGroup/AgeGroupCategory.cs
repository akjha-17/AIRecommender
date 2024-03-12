using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeGroup
{
    public class AgeGroupCategory
    {
        public static int FindAgeGroup(int age)
        {

            if (age >= 1 && age <= 16)
                return 1;
            if (age >= 17 && age <= 30)
                return 2;
            if (age >= 31 && age <= 50)
                return 3;
            if (age >= 51 && age <= 60)
                return 4;
            if (age >= 61 && age <= 100)
                return 5;
            return 0;

        }
    }
}
