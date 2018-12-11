using System;

namespace lol
{
    class GameObject
    {
		public int x, y;
        public char icon { get; private set; }
		public ConsoleColor color { get; private set; }

		public GameObject(char i, ConsoleColor c) : this(0,0, i, c) {}

		public GameObject(int x, int y, char i, ConsoleColor c){
			this.icon = i;
			this.color = c;
			this.x = x;
			this.y = y;
		}

		public void Draw(){
			Console.SetCursorPosition(x, y);

			Console.ForegroundColor = color;
			Console.Write(icon);
		}

		/// <summary>
		/// When player step on this GameObject
		/// </summary>
		/// <returns>true if player can move on this slot</returns>
		public virtual bool Use(){
			return true;
		}



    }
}