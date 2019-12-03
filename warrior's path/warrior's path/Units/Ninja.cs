using System;

namespace warrior_s_path.Units
{
    class Ninja : Fighter
    {
        public Ninja(bool isEnemy = false) : base(/*HP*/ 60, 75, /*Damage*/ 8, 15,
                         /*Guard*/ 4, 5,   /*Evade*/ 0.3, 0.6, /*Health*/ 1.5/*price*/, isEnemy)
        {
            poison = Convert.ToDouble(random.Next(3, 7)) / 10;

            Apperance = new string[] {
                @"   -/&\- .\   ",
                @"  ( *_*) . \  ",
                @" __[][]__. =\ ",
                @"| [_][_] |_// ",
                "\" '*][*' . /  ",
                @"   ['']  ./   ",
                @"   |. |.      ",
            };
        }

        //----------------------------------Отравление.
        protected double poison;
        /// <summary>
        /// Получаем шанс отравления противника.
        /// </summary>
        public double Poison
        {
            get => poison;
        }

        //----------------------------------Необходимые по заданию методы.
        override public void Attack(Human enemy)
        {
            base.Attack(enemy);
        }
        override public void BattleCry()
        {
            MoonSpeak("Кия!!");
        }
    }
    /* 
        012345678901234
 0          -/&\-  .\  
 1         ( *_*)  . \ 
 2        __[][]__ . =\ 
 3       | [_][_] |. // 
 4       " '*][*'  . /    
 5          ['']   ./  
 6          |. |.             
 */
}
