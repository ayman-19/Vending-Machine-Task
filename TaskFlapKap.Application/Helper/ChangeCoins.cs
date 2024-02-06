namespace TaskFlapKap.Application.Helper
{
	public class ChangeCoins
	{
		public static double CalcCoins(double amount)
		{
			int[] coins = [100, 50, 20, 10, 5];
			double auntafterChange = 0;
			foreach (var coin in coins)
			{
				int numberOfCoins = (int)(amount / coin);
				if (numberOfCoins > 0)
				{
					auntafterChange += numberOfCoins * coin;
					amount -= numberOfCoins * coin;
				}
			}
			return auntafterChange;
		}
	}
}
