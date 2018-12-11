using System;

namespace lol
{
    class Player : GameObject
    {
		public int maxHp { get; private set; }
		
		private int _hp;
		public int hp{
			get{return _hp; }
			set{
				_hp = value;
				if(_hp<=0)Kill();
				if(_hp>maxHp) _hp = maxHp;
			}
		}
		public Player() : base(5, 5, '@', ConsoleColor.White){
			_hp = maxHp =  20;
		}


		public void Kill(){
			Game.Instance.StopGame();
		}
    }
}