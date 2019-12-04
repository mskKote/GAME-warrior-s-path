using System;

namespace warrior_s_path_WF.Units
{
    class Samurai : Fighter
    {
        protected double retaliation;
        public double Reliation
        {
            get => retaliation;
        }
        public Samurai(bool isEnemy = false) : base(/*HP*/ 70, 85, /*Damage*/ 7, 12,
                          /*Guard*/ 4, 6,   /*Evade*/ 0.3, 0.5,   /*Health*/ 2.0 /*price*/, isEnemy)
        {
            retaliation = Convert.ToDouble(random.Next(3, 6)) / 10;

            Apperance = new string[] {
                @"   // +++\\  |\",
                "   /\"~_~\"\\  ||",
                "  {0|\"\"\"|0' ||",
                " {0 |\"\"\"| 0_|=",
                "  \" // /\\\\\\  ! ",
                "    || ||     ",
                "    [_ [_     " };

            Tomb = new string[]{
                // Нулевая строчка для имени.
                "              ",
                "      /'___   ",
               @"    \' [RIP]| ",
                "   //   |^^^|| ",
                " _|/___|===||_",
                " .'    // // //  ",
                "//     // // //   "};
        }
        /// <summary>
        /// Смысл этого метода в классе Samurai заключается в том, 
        /// чтобы дословно выполнить инструкцию. Так-то всё, что только нужно
        /// определено в Fighter.
        /// </summary>
        /// <param name="enemy"></param>
        override public void Attack(Human enemy)
        {
            base.Attack(enemy);
        }
        override public void BattleCry()
        {
            MoonSpeak("Чхуа!!");
        }
    }
    /*
  01234567891234
 0   // +++\\  |\  
 1   /"~_~"\  ||   
 2  {0|"""|0' ||
 3 {0 |"""| 0_|=
 4  " // /\\\  !     
 5    || ||     
 6    [_ [_     
          _    _____
 0        \\  (ААААА)
 1    /++] \\  |/ 
 2   // />}' \\__
 3   {[][]}_/\]  
 4  *[][]'    \'  
 5  //  _\\
 6 [.  [.   

  01234567891234
0               
1
2                
3      /' ___   
4    \'  [RIP]  
5   //    |^^^|  
6 _|/____|===|__
     */
}