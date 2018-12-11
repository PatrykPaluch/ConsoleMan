using System;
using System.Collections.Generic;
using System.Threading;

namespace lol
{
    class Game
    {
		private static Game instance;
        public static Game Instance {
			get{
				return instance;
			}
		}

		public Random random {get; private set;}

		private Thread current;
		public bool isRunning{ get; private set;}

		public Player player { get; private set; }
		private List<GameObject> gameObjects;
		private List<GameObject> gosToRemove;
		private List<GameObject> gosToAdd;

		public Game(){
			if(instance!=null){
				throw new Exception("Game was created arleady");
			}
			instance = this;
			random = new Random();

			gameObjects = new List<GameObject>();
			gosToRemove = new List<GameObject>();
			gosToAdd = new List<GameObject>();

			current= new Thread(Loop);
			current.Name = "Game Thread";
			current.Start();
			isRunning = true;
		}


		protected void Init(){

			gameObjects.Add( player = new Player() ); 


			for(int i = 1 ; i < Console.WindowHeight ; ++i){
				if(i==4 || i==5 || i==6) continue;

				gameObjects.Add( new Wall(i,i) );	
				gameObjects.Add( new Wall(1,i) );	
			}
			gameObjects.Add( new Lever(3,2) );

		}

		protected void Draw(){
			Console.Clear();
			foreach( GameObject go in gameObjects ){
				go.Draw();
			}

			player.Draw();

			Console.SetCursorPosition(3, Console.WindowHeight-1);
			Console.Write("HP: ");
			Console.BackgroundColor = ConsoleColor.Red;
			for(int i =0 ; i < player.maxHp ; ++i){
				if(i==player.hp){
					Console.BackgroundColor = ConsoleColor.DarkGray;
				}
				Console.Write(" ");
			}
			Console.BackgroundColor = ConsoleColor.Black;
		}

		protected void ProcessInput(){
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition(1, Console.WindowHeight-1);
			ConsoleKeyInfo c= Console.ReadKey();
			switch(c.Key){
				case ConsoleKey.Escape: StopGame(); break;

				case ConsoleKey.W:
				case ConsoleKey.S:
				case ConsoleKey.A:
				case ConsoleKey.D:
					PlayerInput(c.Key);
					break;
			}
		}

		protected void PlayerInput(ConsoleKey key){
			int newX = player.x;
			int newY = player.y;
			switch(key){
				case ConsoleKey.W:
					--newY;
					break;
				case ConsoleKey.S:
					++newY;
					break;
				case ConsoleKey.A:
					--newX;
					break;
				case ConsoleKey.D:
					++newX;
					break;
			}

			if(	   newX<1 || newX >= Console.WindowWidth
				|| newY<1 || newY >= Console.WindowHeight-1
			){
				return;
			}

			GameObject go = GetGameObjectAt(newX, newY);

			//  go==null || go.Use()
			if( go==null ){
				player.x = newX;
				player.y = newY;
			}else if( go.Use() ){
				player.x = newX;
				player.y = newY;
			}
		}

		public GameObject GetGameObjectAt( int x, int y ){
			foreach( GameObject go in gameObjects ){
				if(go.x==x && go.y==y) return go;
			}
			return null;
		}


		protected void Loop(){
			Init();

			while(isRunning){
				Draw();
				ProcessInput();	

				foreach(GameObject go in gosToAdd){
					gameObjects.Add(go);
				}
				gosToAdd.Clear();

				foreach(GameObject go in gosToRemove){
					gameObjects.Remove(go);
				}
				gosToRemove.Clear();
			}
			OnStop();
		}

		protected void OnStop(){
			Console.Clear();
			string message = "Dzieki za gre";
			int x = Console.WindowWidth/2 - (message.Length/2);
			Console.SetCursorPosition( x, Console.WindowHeight/2);
			Console.Write(message);
			Thread.Sleep(1000);
			Console.Clear();
		}

		public void StopGame(){
			isRunning = false;
		}
		public void RemoveGameObject(GameObject go){
			gosToRemove.Add(go);
		}

		public bool AddGameObject(GameObject go){
			if(GetGameObjectAt(go.x, go.y) != null) return false;

			gosToAdd.Add(go);
			return true;
		}
    }
}