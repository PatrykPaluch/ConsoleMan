using System;

namespace lol
{
    class Lever : GameObject
    {
		public Lever(int x, int y) : base (x,y,'%', ConsoleColor.Yellow){}

		public override bool Use(){
			GameObject newGO;
			Random r = Game.Instance.random;
			do{
				int x = r.Next(1, Console.WindowWidth);
				int y = r.Next(1, Console.WindowHeight);
				if( Game.Instance.random.Next(0, 10) < 7){
					newGO = new Food(x,y, r.Next(1,4), '$');
				}else {
					newGO = new Enemy(x,y, r.Next(10, 30), 'X');
				}
			}while( !Game.Instance.AddGameObject(newGO) );

			return false;
		}
    }
}