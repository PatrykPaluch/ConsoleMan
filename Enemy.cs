using System;

namespace lol
{
    class Enemy : GameObject
    {
		public int hp {get; private set; }
		public Enemy(int x, int y, int hp, char i) : base (x,y,i, ConsoleColor.Red){
			this.hp = hp;
		}

		public override bool Use(){
			hp -= Game.Instance.random.Next(1,4);
			Game.Instance.player.hp -= 2;
			if(hp<=0){
				Game.Instance.RemoveGameObject(this);
				return true;
			}
			return false;
		}
    }
}