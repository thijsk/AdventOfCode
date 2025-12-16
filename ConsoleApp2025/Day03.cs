using Common;

namespace ConsoleApp2025;

public class Day03 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long totalOutput = 0L;

		foreach (var battery in input)
		{
			var joltage = DetermineJoltage(battery);
			Console.WriteLine($"{string.Join(", ", battery)} -> {joltage}");
			totalOutput += joltage;

		}

		return totalOutput;
	}

	private long DetermineJoltage(int[] battery)
	{
		var max = 0;
		for (int i = 0; i < battery.Length - 1; i++)
		{
			for (int j = i + 1; j < battery.Length; j++)
			{
				var voltage = (10 * battery[i]) + battery[j];
				max = Math.Max(max, voltage);
			}
		}

		return max;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long totalOutput = 0L;

		foreach (var battery in input)
		{
			var joltage = DetermineJoltage12(battery);
			Console.WriteLine($"{string.Join(", ", battery)} -> {joltage}");
			totalOutput += joltage;

		}

		return totalOutput;
	}


	// if it works it aint stupid

	private long DetermineJoltage12(int[] battery)
	{
		var max = 0L;
		var maxi = 0L;
		for (int i = 0; i < battery.Length - 11; i++)
		{
			long ival = (100000000000L * battery[i]);
			if (ival <= maxi)
				continue;
			maxi = ival;

			var maxj = 0L;
			for (int j = i + 1; j < battery.Length - 10; j++)
			{
				long jval = (10000000000L * battery[j]);
				if (jval <= maxj)
					continue;
				maxj = jval;

				var maxk = 0L;
				for (int k = j + 1; k < battery.Length - 9; k++)
				{
					long kval = (1000000000L * battery[k]);
					if (kval <= maxk)
						continue;
					maxk = kval;

					var maxl = 0L;
					for (int l = k + 1; l < battery.Length - 8; l++)
					{
						long lval = (100000000L * battery[l]);
						if (lval <= maxl)
							continue;
						maxl = lval;

						var maxm = 0L;
						for (int m = l + 1; m < battery.Length - 7; m++)
						{
							long mval = (10000000L * battery[m]);
							if (mval <= maxm)
								continue;
							maxm = mval;

							var maxn = 0L;
							for (int n = m + 1; n < battery.Length - 6; n++)
							{
								long nval = (1000000L * battery[n]);
								if (nval <= maxn)
									continue;
								maxn = nval;

								var maxo = 0L;
								for (int o = n + 1; o < battery.Length - 5; o++)
								{
									long oval = (100000L * battery[o]);
									if (oval <= maxo)
										continue;
									maxo = oval;

									var maxp = 0L;
									for (int p = o + 1; p < battery.Length - 4; p++)
									{
										long pval = (10000L * battery[p]);
										if (pval <= maxp)
											continue;
										maxp = pval;

										var maxq = 0L;
										for (int q = p + 1; q < battery.Length - 3; q++)
										{
											long qval = (1000L * battery[q]);
											if (qval <= maxq)
												continue;
											maxq = qval;

											var maxr = 0L;
											for (int r = q + 1; r < battery.Length - 2; r++)
											{
												long rval = (100L * battery[r]);
												if (rval <= maxr)
													continue;
												maxr = rval;

												var maxs = 0L;
												for (int s = r + 1; s < battery.Length - 1; s++)
												{
													long sval = (10L * battery[s]);
													if (sval <= maxs)
														continue;
													maxs = sval;

													for (int t = s + 1; t < battery.Length; t++)
													{
														long voltage = ival + jval + kval + lval + mval + nval + oval + pval + qval + rval + sval + battery[t];
														max = Math.Max(max, voltage);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return max;
	}

	private int[] Parse(string line)
	{
		return line.ToCharArray().Select(c => int.Parse($"{c}")).ToArray();
	}

}