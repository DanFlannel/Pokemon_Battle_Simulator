

namespace FBG.Battle
{
    public static class BattleEnviornment
    {
        public static Weather weather;
        public static int weatherDur;

        public static void init()
        {
            weather = Weather.normal;
            weatherDur = 0;
        }

        public static void changeWeather(Weather w, int dur)
        {
            weather = w;
            weatherDur = dur;
        }

        public static void endOfTurn()
        {
            if (weather != Weather.normal)
            {
                weatherDur--;
                if (weatherDur <= 0)
                {
                    weather = Weather.normal;
                }
            }
        }
    }
}