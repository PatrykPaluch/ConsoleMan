using System;

namespace lol
{
    class Food : GameObject
    {
		public int value {get; private set; }
		public Food(int x, int y, int v, char i) : base (x,y,i, ConsoleColor.Green){
			this.value = v;
		}

		public override bool Use(){
			Game.Instance.player.hp += value;
			Game.Instance.RemoveGameObject(this);
			return true;
		}
    }
}