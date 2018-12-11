using System;

namespace lol
{
    class Wall : GameObject
    {
		public Wall(int x, int y) : base(x,y, '#', ConsoleColor.Gray){}

		public override bool Use(){
			return false;
		}
    }
}