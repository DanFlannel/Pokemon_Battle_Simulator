
using System.Collections.Generic;

namespace CryptoCurrency
{
	public class CMC_Global
	{
		public double total_market_cap_usd { get; set; }
		public double total_24h_volume_usd { get; set; }
		public double bitcoin_percentage_of_market_cap { get; set; }
		public int active_currencies { get; set; }
		public int active_assets { get; set; }
		public int active_markets { get; set; }
		public int last_updated { get; set; }

		public double total_market_cap_convert { get; set; }
		public double total_24h_volume_convert { get; set; }
	}
}