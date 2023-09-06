using System.Collections.Generic;

public abstract class Weather
{
    public int Temperature;

    public abstract void ApplyWeatherEffects();
    public List<ItemAttributes> qualities = new List<ItemAttributes>();

    public enum ItemAttributes
    {
        Hot, Cold, Spicy, Salty, Sour, Sweet, Unami, Bitter, Alcoholic, Crunchy, Cheap, Chicken, Ginger, Beef
    }


}



//public class RainyWeather : Weather
//{
//    public override void ApplyEffects()
//    {
//        // Apply the effects specific to rainy weather
//    }
//}

//public class SnowyWeather : Weather
//{
//    public override void ApplyEffects()
//    {
//        // Apply the effects specific to snowy weather
//    }
//}

//public class HeatWaveWeather : Weather
//{
//    public override void ApplyEffects()
//    {
//        // Apply the effects specific to snowy weather
//    }
//}

//public class StormWeather : Weather
//{
//    public override void ApplyEffects()
//    {
//        // Apply the effects specific to snowy weather
//    }
//}